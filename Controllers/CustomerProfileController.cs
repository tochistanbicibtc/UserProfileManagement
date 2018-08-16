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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace StanbicIBTC.UserMgtProfile.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProfileController : ControllerBase
    {
        private ICustomerProfile _userProfile { get; set; }
        private ILogger<CustomerProfileController> _logger;
        public CustomerProfileController(ICustomerProfile UserProfile)
        {
            this._userProfile = UserProfile;
        }

        //[HttpPost]
        //[ProducesResponseType(typeof(Contact), 200)]
        //[ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        //[ProducesResponseType(typeof(void), 400)]
        //[ProducesResponseType(typeof(void), 404)]
        //[ProducesResponseType(typeof(void), 409)]
        //public async Task<IActionResult> PostContact([FromBody] Contact contact)

        /// <summary> Returns a list of all users. </summary>
        /// <remarks>No remarks.</remarks>
        /// <returns>A json result</returns>
        [HttpGet]
        //[Authorize(JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetAllUsers")]
        public  IActionResult GetAllUsers()
        {
            try
            {
                IActionReturn result =  _userProfile.GetAllUsers();
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError("Customer Profile Failed GetAllUsers logged in at {0} with error Message {1}", DateTime.Now, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
        /// <summary>Returns a list of all users enrolled under the provided Application Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="AppID">The application Id to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        //[HttpGet("{id}")]
        [Route("GetAllUsersByAppID/{AppID}")]
        public IActionResult GetAllUsersByAppID(string AppID)
        {
            try
            {   IActionReturn result = _userProfile.GetAllUsersByAppID(AppID);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError("Customer Profile Failed GetAllUsers for Username: {0} logged in at {1}", AppID, DateTime.Now, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Returns the details of a specific user enrolled under the provided Application Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="AppID">The application Id to search for</param>
        /// <param name="username">The username to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetUserByUserName/{AppID}/{username}")]
        public IActionResult GetUserByUserName(string AppID, string userName)
        {
            try
            {   IActionReturn result = _userProfile.GetUserByUserName(AppID, userName);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Returns the details of a specific user by Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="id">The Id to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetUserByID/{id}")]
        public IActionResult GetUserByID(int id)
        {
            try
            {   IActionReturn result = _userProfile.GetUserByID(id);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Returns the details of the enlisted roles under the provided Application Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="AppID">The application Id to search roles from</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetAllCustomerRoleByAppID/{AppID}")]
        public IActionResult GetAllCustomerRoleByAppID(string AppID)
        {
            try
            {   IActionReturn result = _userProfile.GetAllCustomRoleByAppID(AppID);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Returns the details of the customers with a specific role under the provided Application Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="AppID">The application Id to search roles from</param>
        /// <param name="roleName">The rolename to search roles from</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetCustomerRoleByRoleName/{AppID}/{roleName}")]
        public IActionResult GetCustomerRoleByRoleName(string AppID, string roleName)
        {
            try
            {   IActionReturn result = _userProfile.GetCustomRoleByRoleName(AppID, roleName);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Returns the details of the enlisted roles under the provided Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="id">The Id to search roles from</param>
        /// <returns>A json result</returns>
        [HttpGet]
        [Route("GetCustomerRoleByID/{id}")]
        public IActionResult GetCustomerRoleByID(int id)
        {
            try
            {   IActionReturn result = _userProfile.GetUserByID(id);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Create a new user.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="userInfo">The details of the new user profile to create</param>
        /// <returns>A json result</returns>
        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateUser([FromBody]CustomUserProfile userInfo)
        {
            try
            {   IActionReturn result = _userProfile.CreateUser(userInfo);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Create a new role.</summary>
         /// <remarks>No remarks.</remarks>
         /// <param name="roleInfo">The details of the new role to create</param>
         /// <returns>A json result</returns>
        [HttpPost]
        [Route("CreateCustomRole")]
        public IActionResult CreateCustomRole([FromBody]CustomRole roleInfo)
        {
            try
            {   IActionReturn result = _userProfile.CreateCustomRole(roleInfo);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Create a new user role.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="userRoleList">The details of the new user role to create</param>
        /// <returns>A json result</returns>
        [HttpPost]
        [Route("CreateCustomUserRole")]
        public IActionResult CreateCustomUserRole([FromBody]List<CustomUserRoleId> userRoleList)
        {
            try
            {   IActionReturn result = _userProfile.CreateCustomUserRole(userRoleList);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Update a user profile.</summary>
         /// <remarks>No remarks.</remarks>
         /// <param name="newUserProfile">The details of the user profile to update</param>
         /// <returns>A json result</returns>
        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser([FromBody]CustomUserProfile newUserProfile)
        {
            try
            {   IActionReturn result = _userProfile.UpdateUser(newUserProfile);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Update a user password.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="newUserProfile">The details of the user password to update</param>
        /// <returns>A json result</returns>
        [HttpPut]
        [Route("UpdateUserPassword")]
        public IActionResult UpdateUserPassword([FromBody]CustomUserProfile newUserProfile)
        {
            try
            {   IActionReturn result = _userProfile.UpdateUserPassword(newUserProfile);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Update a user role.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="newRole">The details of the user role to update</param>
        /// <returns>A json result</returns>
        [HttpPut]
        [Route("UpdateCustomRole")]
        public IActionResult UpdateCustomRole([FromBody]CustomRole newRole)
        {
            try
            {   IActionReturn result = _userProfile.UpdateCustomRole(newRole);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Delete role.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="RoleID">The role id to be deleted.</param>
        /// <returns>A json result</returns>
        [HttpDelete]
        [Route("DeleteCustomUserRoleByRoleID/{RoleID}")]
        public IActionResult DeleteCustomUserRoleByRoleID(int RoleID)
        {
            try
            {   IActionReturn result = _userProfile.DeleteCustomUserRoleByRoleID(RoleID);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
        /// <summary>Delete a new user.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="UserID">The user id to be deleted.</param>
        /// <returns>A json result</returns>
        [HttpDelete]
        [Route("DeleteCustomUserRoleByUserID/{UserID}")]
        public IActionResult DeleteCustomUserRoleByUserID(int UserID)
        {
            try
            {   IActionReturn result = _userProfile.DeleteCustomUserRoleByUserID(UserID);
                return Ok(result);
            }
            catch (Exception exception)
            { return StatusCode((int)HttpStatusCode.InternalServerError, exception); }
        }
    }
}
