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
    private readonly UserManager _userManager;
    private readonly SignInManager<User> _signInManager;

    public ProfileController(UserManager userManager, SignInManager<User> signInManager)
    {
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
    public async Task<IActionResult> Index(string username, string returnUrl)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            ModelState.AddModelError("User.Email", "User not found");
            return GetIndexView(user, returnUrl);
        }
        if (!ModelState.IsValid)
        {
            return GetIndexView(user, returnUrl);
        }
        
        user.UserName = username;
        await _userManager.UpdateAsync(user);
        await _signInManager.RefreshSignInAsync(user);
        return GetIndexView(user, returnUrl);
    }
    
    
    [HttpGet("changing-password")]
    public async  Task<IActionResult> PasswordChanging(string returnUrl)
    {
        var isPasswordSet = await _userManager.HasPasswordAsync((await _userManager.GetUserAsync(User))!);
        return View(new PasswordChangingViewModel() { ReturnUrl = returnUrl, IsPasswordSet = isPasswordSet});
    }
    
    [HttpPost("changing-password")]
    public async Task<IActionResult> ChangePassword(PasswordChangingInputModel model)
    {
        var vm = new PasswordChangingViewModel() { ReturnUrl = model.ReturnUrl, IsPasswordSet = false };
        if(model.Password != model.ConfirmPassword || string.IsNullOrEmpty(model.Password))
        {
            ModelState.AddModelError(string.Empty, "Passwords do not match");
            return View("PasswordChanging", vm);
        }
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View("PasswordChanging", vm );
        }
        var isPasswordSet = await _userManager.HasPasswordAsync(user);
        IdentityResult? result;
        if (isPasswordSet)
        {
            if (string.IsNullOrEmpty(model.OldPassword))
            {
                ModelState.AddModelError(string.Empty, "Old password is required");
                return View("PasswordChanging", vm);
            }
            result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
        }
        else
            result = await _userManager.AddPasswordAsync(user, model.Password);
        
        if (result.Succeeded)
        {
            vm.IsPasswordSet= true;
            vm.IsSuccessful = true;
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View("PasswordChanging", vm);
    }
    
    private IActionResult GetIndexView(User? user, string returnUrl)
    {
        var vm = new ProfileViewModel();
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(vm);
        }

        var userViewModel = CreateUserViewModel(user);

        {
            vm.User = userViewModel;
            vm.Email = user.Email;
            vm.Username = user.UserName;
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
            Guid.TryParse(user.UserName, out _));
    }
    
}