using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using StanbicIBTC.UserMgtProfile.Services.Interface;
using StanbicIBTC.EntityFramework.Extensions;
using StanbicIBTC.UserMgtProfile.Domain.Models;
using System.Net;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StanbicIBTC.UserMgtProfile.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActiveDirectoryController : ControllerBase
    {
        private IActiveDirectoryUtility _activeDirectory { get; set; }
        private ILogger<ActiveDirectoryController> _logger;
        public ActiveDirectoryController(IActiveDirectoryUtility activeDirectory, ILogger<ActiveDirectoryController> logger)
        {
            this._activeDirectory = activeDirectory;
            _logger = logger;
        }
        /// <summary> Returns a list of all users. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="staffId">The StaffId to search for</param>
        /// <param name="password">The Password to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("Authenticate/{staffId}/{password}")]
        public bool GetAuthenticate(string staffId, string password)
        {
            try
            {
                bool response = _activeDirectory.Authenticate(staffId, password);
                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError("Active Directory Failed Authentication for StaffId: {0} logged in at {1} with Error Message {2}", staffId, DateTime.Now, exception.Message);
                return false;
            }
        }
        /// <summary> Returns the firstname of an ActiveDirectory user. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="userName">The Username to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetADFirstName/{userName}")]
        public string GetADFirstName(string userName)
        {
            try
            {
                string response = _activeDirectory.GetADFirstName(userName);
                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError("Active Directory Failed GetADFirstName for Username: {0} logged in at {1} with Error {2}", userName, DateTime.Now, exception.Message);
                return exception.Message;
            }
        }
        /// <summary> Returns the lastname of an ActiveDirectory user. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="userName">The Username to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetADLastName/{userName}")]
        public string GetADLastName(string userName)
        {
            try
            {
                string response = _activeDirectory.GetADLastName(userName);
                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError("Active Directory Failed GetADLastName for Username: {0} logged in at {1} with Error {2}", userName, DateTime.Now, exception.Message);
                return exception.Message;
            }
        }
        /// <summary> Returns a list of all groups membership of a user. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="userName">The Username to search for</param>
        /// <param name="groupName">The Group Name to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("isGroupMember/{userName}/{groupName}")]
        public bool isGroupMember(string userName, string groupName)
        {
            try
            {
                bool response = _activeDirectory.isGroupMember(userName, groupName);
                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError("Active Directory Failed isGroupMember for Username: {0} logged in at {1} with Error {2}", userName, DateTime.Now, exception.Message);
                return false;
            }
        }
        /// <summary>Returns a list of all users in an Active Directory group. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="groupName">The Group Name to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetGroupMembersEmail/{groupName}")]
        public List<string> GetGroupMembersEmail(string groupName)
        {
            try
            {
                List<string> response = _activeDirectory.GetGroupMembersEmail(groupName);
                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError("Active Directory Failed GetGroupMembersEmail for GroupName: {0} logged in at {1} with Error {2}", groupName, DateTime.Now, exception.Message);
                return null; 
            }
        }
        /// <summary> Returns a list of all users in a group and their respective emails. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="groupName">The Group Name to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetGroupMembersAndEmail/{groupName}")]
        public Dictionary<string, string> GetGroupMembersAndEmail(string groupName)
        {
            try
            {
                Dictionary<string, string> response = _activeDirectory.GetGroupMembersAndEmail(groupName);
                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError("Active Directory Failed GetGroupMembersAndEmail for GroupName: {0} logged in at {1} with Error {2}", groupName, DateTime.Now, exception.Message);
                return null;
            }
        }
        /// <summary> Returns the email address of a specific AD user. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="userName">The Username to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetADUserEmailAddress/{userName}")]
        public string GetADUserEmailAddress(string userName)
        {
            try
            {
                string response = _activeDirectory.GetADUserEmailAddress(userName);
                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError("Active Directory Failed GetADUserEmailAddress for userName: {0} logged in at {1} with Error {2}", userName, DateTime.Now, exception.Message);
                return exception.Message;
            }
        }
        /// <summary> Returns account locked status for active directory user. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="userName">The Username to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("AccountLocked/{userName}")]
        public bool AccountLocked(string userName)
        {
            try
            {
                bool response = _activeDirectory.AccountLocked(userName);
                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError("Active Directory Failed AccountLocked for UserName: {0} logged in at {1} with Error {2}", userName, DateTime.Now, exception.Message);
                return false;
            }
        }
        /// <summary> Returns a departments of a specific user. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="userName">The Username to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetDepartment/{userName}")]
        public string GetDepartment(string userName)
        {
            try
            {
                string response = _activeDirectory.GetDepartment(userName);
                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError("Active Directory Failed GetDepartment for userName: {0} logged in at {1} with Error {2}", userName, DateTime.Now, exception.Message);
                return exception.Message;
            }
        }

        /// <summary> Returns details of an active ActiveDirectory User. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="userName">The Username to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetADUserInfo/{userName}")]
        public string GetADUserInfo(string userName)
        {
            try
            {
                string response = _activeDirectory.GetADUserInfo(userName);
                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError("Active Directory Failed GetADUserInfo for userName: {0} logged in at {1} with Error {2}", userName, DateTime.Now, exception.Message);
                return exception.Message;
            }
        }

        /// <summary> Returns a fullnames of username. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="userName">The Username to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetFullName/{userName}")]
        public string GetFullName(string userName)
        {
            try
            {
                string response = _activeDirectory.GetFullName(userName);
                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError("Active Directory Failed GetFullName for userName: {0} logged in at {1} with Error {2}", userName, DateTime.Now, exception.Message);
                return exception.Message;
            }
        }

        /// <summary> Returns the group of a user. </summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="userName">The Username to search for</param>
        /// <param name="userGroups">The Group Name to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetGroups/{userName}/{userGroups}")]
        public string GetGroups(string userName, string userGroups)
        {
            try
            {
                string response = _activeDirectory.GetGroups(userName, userGroups);
                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError("Active Directory Failed GetGroups for userName: {0} and User Group: {1} logged in at {2} with Error {3}", userName, userGroups, DateTime.Now, exception.Message);
                return exception.Message;
            }
        }
    }
}
