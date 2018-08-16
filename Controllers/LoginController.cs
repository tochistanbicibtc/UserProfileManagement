using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StanbicIBTC.UserMgtProfile.Services.Interface;
using StanbicIBTC.EntityFramework.Extensions;
using StanbicIBTC.UserMgtProfile.Domain.Models;
using System.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StanbicIBTC.UserMgtProfile.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogin _login { get; set; }
        private ILogger<LoginController> _logger;
        public LoginController(ILogin Login)
        {
            this._login = Login;
        }
        /// <summary> Returns a list of all users. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="UserName">The UserName to search for</param>
        /// <param name="Password">The Password of the User to search for</param>
        /// <param name="SecurityGroupArray">The SecurityGroupArray to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("AuthenticateUser")]
        public IActionResult AuthenticateUser(string UserName, string Password, string SecurityGroupArray)
        {
            try
            {
                IActionReturn result = _login.AuthenticateUser(UserName, Password, SecurityGroupArray);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError("Login Failed AuthenticateUser for Input: {0} logged in at {1} with Error {2}", UserName, DateTime.Now, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
