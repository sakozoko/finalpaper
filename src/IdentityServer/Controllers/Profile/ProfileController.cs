using System.Text.Encodings.Web;
using IdentityServer.Abstraction;
using IdentityServer.Entities;
using IdentityServer.Features;
using IdentityServer.Models;
using IdentityServer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers.Profile;

[Authorize]
[Route("profile")]
public class ProfileController : Controller
{
    private readonly IEmailSender _emailSender;
    private readonly IModelStateErrorMessageStore _errorMessageStore;
    private readonly IPhoneValidator _phoneValidator;
    private readonly SignInManager<User> _signInManager;
    private readonly ISmsSender _smsSender;
    private readonly UserManager _userManager;

    public ProfileController(UserManager userManager,
        SignInManager<User> signInManager,
        IEmailSender emailSender,
        ISmsSender smsSender,
        IModelStateErrorMessageStore errorMessageStore,
        IPhoneValidator phoneValidator)
    {
        _phoneValidator = phoneValidator;
        _smsSender = smsSender;
        _errorMessageStore = errorMessageStore;
        _emailSender = emailSender;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string returnUrl)
    {
        var user = await _userManager.GetUserAsync(User);
        return GetIndexView(user, returnUrl);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(string username, string phoneNumber, string returnUrl)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("UserNotFound"));
            return GetIndexView(user, returnUrl);
        }

        if (!ModelState.IsValid) return GetIndexView(user, returnUrl);

        if (user.PhoneNumber != phoneNumber
            && !user.PhoneNumberConfirmed
            && !string.IsNullOrWhiteSpace(phoneNumber))
        {
            var phoneValidationResult = await _phoneValidator.ValidatePhoneNumberAsync(phoneNumber);
            if (!phoneValidationResult.IsValid)
            {
                ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("InvalidPhoneNumber"));
                return GetIndexView(user, returnUrl);
            }

            user.PhoneNumber = phoneValidationResult.International;
            await _userManager.UpdateAsync(user);
        }


        if (Guid.TryParse(user.UserName, out _) && user.UserName != username)
        {
            await _userManager.SetUserNameAsync(user, username);
            await _signInManager.RefreshSignInAsync(user);
        }

        return GetIndexView(user, returnUrl);
    }


    [HttpGet("changing-password")]
    public async Task<IActionResult> PasswordChanging(string returnUrl)
    {
        var isPasswordSet = await _userManager.HasPasswordAsync((await _userManager.GetUserAsync(User))!);
        return View(new PasswordChangingViewModel { ReturnUrl = returnUrl, IsPasswordSet = isPasswordSet });
    }

    [HttpPost("changing-password")]
    public async Task<IActionResult> ChangePassword(PasswordChangingInputModel model)
    {
        var vm = new PasswordChangingViewModel { ReturnUrl = model.ReturnUrl, IsPasswordSet = true };
        var user = await _userManager.GetUserAsync(User);
        if (user is null || !ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("UserNotFound"));
            return View("PasswordChanging", vm);
        }

        var isPasswordSet = await _userManager.HasPasswordAsync(user);
        vm.IsPasswordSet = isPasswordSet;
        if (!ModelState.IsValid)
            return View("PasswordChanging", vm);

        if (model.Password != model.ConfirmPassword || string.IsNullOrEmpty(model.Password))
        {
            ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("PasswordsNotMatch"));
            return View("PasswordChanging", vm);
        }

        IdentityResult? result;
        if (isPasswordSet)
            result = await _userManager.ChangePasswordAsync(user, model.OldPassword!, model.Password);
        else
            result = await _userManager.AddPasswordAsync(user, model.Password);

        if (result.Succeeded)
        {
            vm.IsPasswordSet = true;
            vm.IsSuccessful = true;
        }

        foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
        return View("PasswordChanging", vm);
    }

    [HttpGet("send-confirmation-email")]
    public async Task<IActionResult> SendConfirmationEmail(string returnUrl)
    {
        var vm = new ConfirmEmailViewModel { ReturnUrl = returnUrl };
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("UserNotFound"));
            return View("ConfirmationEmailSent", vm);
        }

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var callbackUrl = Url.Action("ConfirmEmail", "Profile", new { userId = user.Id, code, returnUrl },
            HttpContext.Request.Scheme);
        var emailSendingResult = await _emailSender.SendEmailAsync(user.Email!, "Підтвердіть вашу пошту",
            $"Підтвердіть вашу пошту за <a href='{HtmlEncoder.Default.Encode(callbackUrl!)}'>цим посиланням</a>.");
        if (!emailSendingResult)
        {
            ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("EmailSendingError"));
            return View("ConfirmationEmailSent", vm);
        }

        vm.IsSuccessful = true;
        return View("ConfirmationEmailSent", vm);
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string? userId, string? code, string? returnUrl)
    {
        if (userId is null || code is null || returnUrl is null)
        {
            ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("BadRequest"));
            return View("ConfirmEmail");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("UserNotFound"));
            return View("ConfirmEmail");
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded) RedirectToAction("Index", new { returnUrl });
        ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("EmailConfirmationError"));
        return View("ConfirmEmail");
    }

    [HttpGet("send-confirmation-phone")]
    public async Task<IActionResult> SendConfirmationPhone(string returnUrl)
    {
        var vm = new ConfirmPhoneViewModel { ReturnUrl = returnUrl };
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("UserNotFound"));
            return View("ConfirmationPhoneSent", vm);
        }

        var phoneValidationResult = await _phoneValidator.ValidatePhoneNumberAsync(user.PhoneNumber);
        if (!phoneValidationResult.IsValid)
        {
            ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("InvalidPhoneNumber"));
            return View("ConfirmationPhoneSent", vm);
        }

        var code = await _userManager.GeneratePhoneConfirmationCodeAsync(user);

        var smsSendingResult =
            await _smsSender.SendSmsAsync(phoneValidationResult.International!, $"Ваш код підтвердження: {code}");
        if (!smsSendingResult)
        {
            ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("SmsSendingError"));
            return View("ConfirmationPhoneSent", vm);
        }

        vm.IsSuccessful = true;
        return View("ConfirmationPhoneSent", vm);
    }

    [HttpPost("confirm-phone")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmPhone(ConfirmPhoneInputModel model)
    {
        var vm = new ConfirmPhoneViewModel { ReturnUrl = model.ReturnUrl };
        if (!ModelState.IsValid)
            return View("ConfirmationPhoneSent", vm);
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("UserNotFound"));
            return View("ConfirmationPhoneSent", vm);
        }

        var result = await _userManager.ValidatePhoneConfirmationCodeAsync(user, model.Code);

        if (result) return RedirectToAction("Index", "Profile", new { returnUrl = model.ReturnUrl });

        ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("PhoneConfirmationError"));
        return View("ConfirmationPhoneSent", vm);
    }

    private IActionResult GetIndexView(User? user, string returnUrl)
    {
        var vm = new ProfileViewModel();
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, _errorMessageStore.GetErrorMessage("UserNotFound"));
            return View(vm);
        }

        var userViewModel = CreateUserViewModel(user);
        {
            vm.User = userViewModel;
            vm.Email = user.Email;
            vm.Username = user.UserName;
            vm.PhoneNumber = user.PhoneNumber;
            vm.ReturnUrl = returnUrl;
        }
        return View(vm);
    }

    private static UserViewModel CreateUserViewModel(User user)
    {
        return new UserViewModel(user.UserName ?? string.Empty,
            user.Email ?? string.Empty,
            user.ProviderName ?? string.Empty,
            user.PasswordHash is not null,
            Guid.TryParse(user.UserName, out _),
            user.PhoneNumber ?? string.Empty,
            user.EmailConfirmed,
            user.PhoneNumberConfirmed);
    }
}