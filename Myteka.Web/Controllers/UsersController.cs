using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Myteka.Infrastructure.Data.Interfaces;
using Myteka.Models;
using Myteka.Models.ExternalModels;
using Myteka.Models.ExternalModels.RegisterModels;
using Myteka.Models.InternalModels;
using Myteka.Models.InternalModels.Users;

namespace Myteka.Web.Controllers;

[Route("api/user")]
public class UsersController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;

    public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Register user
    /// </summary>
    /// <param name="user"></param>
    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterModel user)
    {
        User userToCreate = _mapper.Map<User>(user);
        var result = await _userManager.CreateAsync(userToCreate, user.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(userToCreate, false);
            return Ok();
        }

        return BadRequest(new ErrorResponse
        {
            Error = "User not created",
            Message = String.Join(", ", result.Errors.Select(er => er.Description))
        });
    }
    
    /// <summary>
    /// Logins the user to the system
    /// </summary>
    /// <param name="authenticationData"></param>
    /// <returns></returns>
    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login(Login authenticationData)
    {
        // TODO: Add validation
        
        var user = await _userManager.FindByEmailAsync(authenticationData.Email);
        await _signInManager.SignInAsync(user, false);
        
        return Ok();
    }
    
    /// <summary>
    /// Logouts the user from the system
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [Route("logout")]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        
        return Ok();
    }
    
    /// <summary>
    /// Returns the user by id
    /// </summary>
    /// <returns></returns>
    [Route("get/{userId}")]
    [HttpGet]
    public async Task<IActionResult> GetProfile(Guid userId)
    {
        User user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user != null)
        {
            UserExternal result = _mapper.Map<UserExternal>(user);

            return Ok(result);
        }

        return BadRequest(new ErrorResponse
        {
            Error = "User not found",
            Message = "User not found"
        });
    }
    
    /// <summary>
    /// Edits the user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [Route("update")]
    [HttpPost]
    public IActionResult EditProfile(Guid userId, UserExternal user)
    {
        
        
        return Ok();
    }

    /// <summary>
    /// Changes the password
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [Route("change-password")]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(Guid userId, string oldPassword, string newPassword)
    {
        User user = await _userManager.FindByIdAsync(userId.ToString());
        
        var changeResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        if (changeResult.Succeeded)
            return Ok(new { message = "Password changed" });
        
        List<ErrorResponse> result = new List<ErrorResponse>();
        foreach (var error in changeResult.Errors)
        {
            result.Add(new ErrorResponse
            {
                Error = error.Code,
                Message = error.Description
            });
        }

        return BadRequest(result);
    }
    
    /// <summary>
    /// Changes the email
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="newEmail"></param>
    /// <returns></returns>
    [Authorize]
    [Route("change-email")]
    [HttpPost]
    public async Task<IActionResult> ChangeEmail(Guid userId, string newEmail)
    {
        User user = await _userManager.FindByIdAsync(userId.ToString());

        string mailToken = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
        IdentityResult changeResult = await _userManager.ChangeEmailAsync(user, newEmail, mailToken);
        if (changeResult.Succeeded)
            return Ok(new { message = "Email changed" });
        
        List<ErrorResponse> result = new List<ErrorResponse>();
        foreach (var error in changeResult.Errors)
        {
            result.Add(new ErrorResponse
            {
                Error = error.Code,
                Message = error.Description
            });
        }

        return BadRequest(result);
    }

    public IActionResult GetAllUsers()
    {
        return Ok(_userManager.Users.ToList());
    }

    /// <summary>
    /// Deletes the user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [Authorize]
    [Route("delete/{userId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteProfile(Guid userId)
    {
        User user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
            return Ok();
        }
        
        return BadRequest();
    }
}