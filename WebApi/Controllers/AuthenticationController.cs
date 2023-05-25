using ApplicationCore.Const;
using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.Mappers;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Configuration;
using SurveyApp.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

[ApiController, Route("/api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<UserEntity> _manager;
    private readonly JwtSettings _jwtSettings;
    private readonly IMapper _mapper;

    public AuthenticationController(UserManager<UserEntity> manager
        , IConfiguration configuration
        , JwtSettings jwtSettings)
    {
        _manager = manager;
        _jwtSettings = jwtSettings;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate(LoginUserDto user)
    {
        if (!ModelState.IsValid)
        {
            return Unauthorized();
        }
        var logged = await _manager.FindByNameAsync(user.LoginName);
        if (await _manager.CheckPasswordAsync(logged, user.Password))
        {
            return Ok(new { Token = CreateToken(logged) });
        }
        return Unauthorized();
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterUserDto userDto)
    {
        var user = UserMapper.FromDtoToUserEntity(userDto);

        var result = await _manager.CreateAsync(user,userDto.Password);

        if(!result.Succeeded)
        {
            return BadRequest(result);
        }
       await _manager.AddToRoleAsync(user, Roles.User);

        return Ok();
    }

    private string CreateToken(UserEntity user)
    {
        return new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            .AddClaim(JwtRegisteredClaimNames.Name, user.UserName)
            .AddClaim(JwtRegisteredClaimNames.Gender, "male")
            .AddClaim(JwtRegisteredClaimNames.NameId,user.Id)
            .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
            .AddClaim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds())
            .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid())
            .Audience(_jwtSettings.Audience)
            .Issuer(_jwtSettings.Issuer)
            .Encode();
    }
}