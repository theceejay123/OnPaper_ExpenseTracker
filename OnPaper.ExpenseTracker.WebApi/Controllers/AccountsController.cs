using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnPaper.ExpenseTracker.Core.Interfaces;
using OnPaper.ExpenseTracker.Core.Models.Identity;
using OnPaper.ExpenseTracker.WebApi.DTOs;
using OnPaper.ExpenseTracker.WebApi.Errors;

namespace OnPaper.ExpenseTracker.WebApi.Controllers;

public class AccountsController : BaseWebApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;

    public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AppUserDTO>> Login(LoginDTO loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            return Unauthorized(new ErrorResponse(401));
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized(new ErrorResponse(401));
        }

        return new AppUserDTO
        {
            AppUserId = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName,
            Bio = user.Bio,
            Token = _tokenService.CreateToken(user)
        };
    }
}