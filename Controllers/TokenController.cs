using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using StanbicIBTC.UserMgtProfile.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StanbicIBTC.UserMgtProfile.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserManager _userManager;

        public TokenController(IConfiguration configuration, IUserManager userManager)
        {
            _config = configuration;
            _userManager = userManager;
        }
        [HttpPost("")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] AuthRequest authUserRequest)
        {
            var user = _userManager.FindByEmail(model.UserName);

            if (user != null)
            {
                var checkPwd = _signInManager.CheckPasswordSignIn(user, model.authUserRequest);
                if (checkPwd)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
            }

            return BadRequest("Could not create token");
        }
    }
    //[HttpPost, Route("token")]
    //    public IActionResult Token([FromBody]TokenModel user)
    //    {
    //        if (user == null)
    //        {
    //            return BadRequest("Invalid client request");
    //        }

    //        if (user.UserName == "johndoe" && user.Password == "def@123")
    //        {
    //            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
    //            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

    //            var tokeOptions = new JwtSecurityToken(
    //                issuer: "http://localhost:5000",
    //                audience: "http://localhost:5000",
    //                claims: new List<Claim>(),
    //                expires: DateTime.Now.AddMinutes(5),
    //                signingCredentials: signinCredentials
    //            );

    //            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    //            return Ok(new { Token = tokenString });
    //        }
    //        else
    //        {
    //            return Unauthorized();
    //        }
    //    }
    }
}
