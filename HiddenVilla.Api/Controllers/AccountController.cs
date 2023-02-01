using CommonFiles;
using DataAccess.Data;
using HiddenVilla.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HiddenVilla.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly APISettings _apiSettings;

    public AccountController(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<APISettings> options)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _apiSettings = options.Value;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] UserRequestDTO _userRequestDTO)
    {
        if (_userRequestDTO == null || !ModelState.IsValid)
        {
            return BadRequest();
        }
        var user = new ApplicationUser
        {
            UserName = _userRequestDTO.Email,
            Email = _userRequestDTO.Email,
            Name = _userRequestDTO.Name,
            PhoneNumber = _userRequestDTO.PhoneNo,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, _userRequestDTO.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new RegistrationResponseDTO()
            {
                Errors = errors,
                IsRegistrationSuccessful = false
            });
        }
        var roleResult = await _userManager.AddToRoleAsync(user, SD.Role_Customer);
        if (!roleResult.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new RegistrationResponseDTO()
            {
                Errors = errors,
                IsRegistrationSuccessful = false
            });
        }
        return StatusCode(201);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] AuthenticationDTO _authenticationDTO)
    {
        if (_authenticationDTO == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _signInManager.PasswordSignInAsync(_authenticationDTO.UserName, _authenticationDTO.Password, false, false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(_authenticationDTO.UserName);
            if (user == null)
            {
                return Unauthorized(new AuthenticationResponseDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Invalid Authentication"
                });
            }
            //everything is valid and we need to login the user

            var signingCredentials = GetSigningCredentials();
            var Claims = await GetClaims(user);

            var tokenOptions = new JwtSecurityToken(
               issuer: _apiSettings.ValidIssuer,
               audience: _apiSettings.ValidAudience,
               claims: Claims,
               expires: DateTime.Now.AddMinutes(5),
               signingCredentials: signingCredentials
               );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthenticationResponseDTO
            {
                IsAuthSuccessful = true,
                Token = token,
                userDTO = new UserDTO
                {
                    Name = user.Name,
                    Id = user.Id,
                    Email = user.Email,
                    PhoneNo = user.PhoneNumber
                }
            });
        }
        else
        {
            return Unauthorized(new AuthenticationResponseDTO
            {
                IsAuthSuccessful = false,
                ErrorMessage = "Invalid Authentication"
            });
        }
    }

    private SigningCredentials GetSigningCredentials()
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(ApplicationUser user)
    {
        var Claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Id",user.Id),
        };

        var roles = await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(user.UserName));

        foreach (var rol in roles)
        {
            Claims.Add(new Claim(ClaimTypes.Role, rol));
        }

        return Claims;
    }

}