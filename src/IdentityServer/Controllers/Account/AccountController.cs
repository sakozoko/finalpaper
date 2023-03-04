using System.Security.Claims;
using IdentityModel;
using IdentityServer.Entities;
using IdentityServer.Features;
using IdentityServer.Models;
using IdentityServer.ViewModels;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Mail;

namespace IdentityServer.Controllers.Account;

[Route("Account")]
public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager _userManager;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IAuthenticationSchemeProvider _schemeProvider;
    private readonly IClientStore _clientStore;

    public AccountController(SignInManager<User> signInManager,
        UserManager userManager,
        IIdentityServerInteractionService interaction,
        IAuthenticationSchemeProvider schemeProvider,
        IClientStore clientStore)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _interaction = interaction;
        _schemeProvider = schemeProvider;
        _clientStore = clientStore;
    }
    [AllowAnonymous]
    [HttpGet("Login")]
    public async Task<IActionResult> Login(string returnUrl)
    {
        var vm = await BuildLoginViewModelAsync(returnUrl);

        return View(vm);
    }
    
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginInputModel model)
    {
        var vm = await BuildLoginViewModelAsync(model.ReturnUrl!);
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        if (model.Username is null ||
            !(await _signInManager.PasswordSignInAsync(model.Username, model.Password!, model.RememberLogin, true))
                .Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View(vm);
        }
        
        var user = await _userManager.FindByNameAsync(model.Username);
        
        if (user == null) return Redirect(model.ReturnUrl!);
        var roles = await _userManager.GetRolesAsync(user);
        AuthenticationProperties? props = null;
        if (model.RememberLogin)
        {
            props = new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30))
            };
        }
        var isuser = new IdentityServerUser(user.Id.ToString())
        {
            DisplayName = user.UserName,
            IdentityProvider = "idsrv",
            AdditionalClaims = roles.Select(x => new Claim(JwtClaimTypes.Role, x)).ToList()
        };
        await HttpContext.SignInAsync(isuser, props);
        
        var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
        if (context != null)
        {
            return Redirect(model.ReturnUrl!);
        }

        ModelState.AddModelError(string.Empty,"ReturnUrl incorrect");
        return View(vm);

    }
    
    [AllowAnonymous]
    [HttpGet("Registration")]
    public IActionResult Registration(string returnUrl)
    {
        var vm = new RegistrationViewModel(returnUrl);

        return View(vm);
    }

    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [HttpPost("Registration")]
    public async Task<IActionResult> Registration(RegistrationInputModel model)
    {
        var vm = new RegistrationViewModel(model.ReturnUrl!);
        if(!ModelState.IsValid || model.Password != model.ConfirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View(vm);
        }
        var user = new User(){UserName = model.Username, Email = model.Email, PhoneNumber = model.PhoneNumber};
        var result = await _userManager.CreateAsync(user, model.Password!);
        
        if (result.Succeeded)
            return Redirect(model.ReturnUrl!);


        foreach (var error in result.Errors)
            ModelState.AddModelError(error.Code, error.Description);
        return View("Login", new LoginViewModel(model.ReturnUrl!));
    }
    [Authorize]
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout(string? returnUrl)
    {
        if (returnUrl is null)
            returnUrl = Url.Action("Login", "Account")!;
        await _signInManager.SignOutAsync();
        await HttpContext.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
        await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
        await HttpContext.SignOutAsync();
        return Redirect(returnUrl);
        return View("LoggetOut", new LoggedOutViewModel(returnUrl));
    }
    
    [HttpGet("SendResetPassword")]
    public IActionResult SendResetPassword(string returnUrl)
    {
        var vm = new SendResetPasswordViewModel(){ReturnUrl = returnUrl};

        return View(vm);
    }
    [HttpPost("SendResetPassword")]
    public async Task<IActionResult> SendResetPassword(SendResetPasswordInputModel model)
    {
        var vm = new SendResetPasswordViewModel()
        {
            ReturnUrl = model.ReturnUrl,
        };
        var user = await _userManager.FindByEmailAsync(model.Credential!);
        if (user == null)
            user = await _userManager.FindByNameAsync(model.Credential!);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or email");
            return View("SendResetPassword", vm);
        }
            
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var callbackUrl = Url.Action("ResetPassword", "Account", new {user.Id,token, model.ReturnUrl}, Request.Scheme);
        if (callbackUrl is null)
        {
            ModelState.AddModelError(string.Empty,"Bad request");
            return View(vm);
        }
        await _userManager.SendRecoveryMessageToEmailAsync(user, callbackUrl);
        vm.StatusMessage = "Reset password link sent to your email";
        return View("SendResetPassword", vm);
    }
    
    [HttpGet("ResetPassword")]
    public async Task<IActionResult> ResetPassword(string id, string token,string returnUrl)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid user id");
            return View("SendResetPassword", new SendResetPasswordViewModel()
            {
                ReturnUrl = returnUrl,
            });
        }
        var result = await _userManager.VerifyUserTokenAsync(user, "Default", UserManager.ResetPasswordTokenPurpose, token); 
        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Invalid token");
            return View("SendResetPassword", new SendResetPasswordViewModel()
            {
                ReturnUrl = returnUrl,
            });
        }
        var vm = new ResetPasswordViewModel(){ReturnUrl = returnUrl, Token = token, Id=id};

        return View(vm);
    }

    [HttpPost("ResetPassword")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordInputModel model)
    {
        var vm = new ResetPasswordViewModel(){ReturnUrl = model.ReturnUrl, Token = model.Token, Id = model.Id};
        if (!ModelState.IsValid || model.Password != model.ConfirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Invalid password or confirm password");
            return View(vm);
        }
        
        var user = await _userManager.FindByIdAsync(model.Id!);
        
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid user id");
            return View(vm);
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token!, model.Password!);
        if (result.Succeeded)
            return RedirectToAction("Login", "Account", new{returnUrl=model.ReturnUrl});
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(vm);
    }

    private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
        if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
        {
            var local = context.IdP == IdentityServerConstants.LocalIdentityProvider;

            // this is meant to short circuit the UI and only trigger the one external IdP
            var vm = new LoginViewModel(returnUrl)
            {
                Username = context.LoginHint,
            };

            if (!local)
            {
                vm.ExternalProviders = new[] { new ExternalProvider("", context.IdP) };
            }

            return vm;
        }

        var schemes = await _schemeProvider.GetAllSchemesAsync();

        var providers = schemes
            .Where(x => x.DisplayName != null)
            .Select(x => new ExternalProvider(x.DisplayName ?? x.Name, x.Name)).ToList();
        
        if (context?.Client.ClientId != null)
        {
            var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
            if (client != null)
            {

                if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                {
                    providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                }
            }
        }

        return new LoginViewModel(returnUrl)
        {
            Username = context?.LoginHint,
            ExternalProviders = providers.ToArray()
        };
    }
}