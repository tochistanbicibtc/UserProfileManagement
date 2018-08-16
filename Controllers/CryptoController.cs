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
    public class CryptoController : ControllerBase
    {
        private ICryptoSource _cryptoSource { get; set; }
        private ILogger<CryptoController> _logger;
        public CryptoController(ICryptoSource CryptoSource)
        {
            this._cryptoSource = CryptoSource;
        }
        /// <summary> Returns an Encrypted result of an Input. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="input">The Input to encrypt</param>
        /// <param name="password">The Password for encryption</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("EncryptText")]
        public string EncryptText(string input, string password)
        {
            try
            {
                string result = _cryptoSource.EncryptText(input, password);
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError("Crypto Failed EncryptText for Input: {0} logged in at {1} with Error {2}", input, DateTime.Now, exception.Message);
                return exception.Message;
            }
        }

        /// <summary> Returns a Decryption result of an Input. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="input">The Input to decrypt</param>
        /// <param name="password">The Password for decryption</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("DecryptText")]
        public string DecryptText(string input, string password)
        {
            try
            {
                string result = _cryptoSource.DecryptText(input, password);
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError("Crypto Failed DecryptText for Input: {0} logged in at {1} with Error {2}", input, DateTime.Now, exception.Message);
                return exception.Message;
            }
        }

        /// <summary> Returns an Encrypted  result of a Text. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="text">The Input to encrypt</param>
        /// <param name="password">The Password for encryption</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("EncryptString")]
        public string EncryptString(string text, string password)
        {
            try
            {
                string result = _cryptoSource.EncryptString(text, password);
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError("Crypto Failed EncryptString for Text: {0} logged in at {1} with Error {2}", text, DateTime.Now, exception.Message);
                return exception.Message;
            }
        }

        /// <summary> Returns Decrypted result of a Text. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="text">The Input to decrypt</param>
        /// <param name="password">The Password for decryption</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("DecryptString")]
        public string DecryptString(string text, string password)
        {
            try
            {
                string result = _cryptoSource.DecryptString(text, password);
                return result;
            }
            catch (Exception exception)
            {
                _logger.LogError("Crypto Failed DecryptString for Text: {0} logged in at {1} with Error {2}", text, DateTime.Now, exception.Message);
                return exception.Message;
            }
        }
    }
}
