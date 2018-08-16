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
    public class TwoFactorController : ControllerBase
    {
        private ITwoFactor _twoFactor { get; set; }
        private ILogger<TwoFactorController> _logger;
        public TwoFactorController(ITwoFactor TwoFactor)
        {
            this._twoFactor = TwoFactor;
        }
        /// <summary>Returns a list of all users enrolled under the provided Application Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="Username">The user who requests to validate an OTP session</param>
        /// <param name="Otp">The OTP session Id to be validated</param>
        /// <returns>A json result</returns>
        [HttpGet]
        //[HttpGet("{id}")]
        [Route("doTwoFactorAuthentication/{AppID}")]
        public string doTwoFactorAuthentication(string Username, string Otp)
        {
            try
            {
                string result = _twoFactor.doTwoFactorAuthentication(Username, Otp);
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError("Two Factor Authentication Failed doTwoFactorAuthentication for Username: {0} logged in at {1} with Error Message {2}", Username, DateTime.Now, exception.Message);
                return exception.Message;
            }
        }
    }
}
