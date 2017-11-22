﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pictest.Model;
using Pictest.Service.Interface;
using Pictest.Service.Request;
using Pictest.Service.Response;

namespace Pictest.Controllers
{
    [Route("/api")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthorizationController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> GenerateToken([FromBody] LoginRequest login)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid login request.");

            var user = await _userService.FindByEmailAsync(login.Email);

            if (user == null)
                return NotFound("User not found.");

            var result = await _userService.CheckPasswordSignInAsync(user, login.Password);

            if (!result)
                return Forbid("Invalid password.");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Token:Issuer"],
                _configuration["Token:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return Ok(new LoginResponse
            {
                User = user,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request.");

            return Ok(await _userService.CreateUserAsync(createUserRequest));
        }
    }
}