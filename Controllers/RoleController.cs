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
    public class RoleController : ControllerBase
    {
        private IRolePages _iRole { get; set; }
        private ILogger<RoleController> _logger;
        public RoleController(IRolePages Role)
        {
            this._iRole = Role;
        }
        /// <summary>Returns a list of all roles enrolled under the provided Application Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="AppID">The application Id to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        //[HttpGet("{id}")]
        [Route("GetAllRoleByAppID/{AppID}")]
        public IActionResult GetAllRoleByAppID(string AppID)
        {
            try
            {
                IActionReturn result = _iRole.GetAllRoleByAppID(AppID);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError("Role Failed GetAllRoleByAppID for AppID: {0} logged in at {1} with Error {2}", AppID, DateTime.Now, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
        /// <summary>Returns a list of all roles enrolled under the provided Application Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="AppID">The application Id to search for</param>
        /// <param name="RoleName">The Role Name to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        //[HttpGet("{id}")]
        [Route("GetRoleByRoleName/{AppID}/{RoleName}")]
        public IActionResult GetRoleByRoleName(string AppID, string RoleName)
        {
            try
            {
                IActionReturn result = _iRole.GetRoleByRoleName(AppID, RoleName);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError("Role Failed GetRoleByRoleName for AppID: {0} logged in at {1} with Error {2}", AppID, DateTime.Now, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
        /// <summary>Returns a list of all roles enrolled under the provided Application Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="Id">The application Id to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        //[HttpGet("{id}")]
        [Route("GetRoleByID/{Id}")]
        public IActionResult GetRoleByID(int Id)
        {
            try
            {
                IActionReturn result = _iRole.GetRoleByID(Id);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError("Role Failed GetRoleByID for Id: {0} logged in at {1} with Error {2}", Id, DateTime.Now, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
        /// <summary>Returns a list of all pages under the provided Application Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="AppID">The application Id to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        //[HttpGet("{id}")]
        [Route("GetAllPagesByAppID/{AppID}")]
        public IActionResult GetAllPagesByAppID(string AppID)
        {
            try
            {
                IActionReturn result = _iRole.GetAllPagesByAppID(AppID);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError("Role Failed GetAllPagesByAppID for AppID: {0} logged in at {1} with Error {2}", AppID, DateTime.Now, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
        /// <summary>Returns a list of all roles enrolled under the provided Application Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="AppID">The application Id to search for</param>
        /// <param name="PageName">The application page</param>
        /// <returns>A json result</returns>
        [HttpGet]
        //[HttpGet("{id}")]
        [Route("GetPageByPageName/{AppID}/{PageName}")]
        public IActionResult GetPageByPageName(string AppID, string PageName)
        {
            try
            {
                IActionReturn result = _iRole.GetPageByPageName(AppID, PageName);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError("Role Failed GetPageByPageName for AppID: {0} logged in at {1} with Error {2}", AppID, DateTime.Now, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
        /// <summary>Returns a list of all roles enrolled under the provided Application Id.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="AppID">The application Id to search for</param>
        /// <returns>A json result</returns>
        [HttpGet]
        //[HttpGet("{id}")]
        [Route("GetPageByID/{Id}")]
        public IActionResult GetPageByID(int Id)
        {
            try
            {
                IActionReturn result = _iRole.GetPageByID(Id);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError("Role Failed GetPageByID for Id: {0} logged in at {1} with Error {2}", Id, DateTime.Now, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
        ///////// <summary>Create a new Create Page Role.</summary>
        ///////// <remarks>No remarks.</remarks>
        ///////// <param name="userInfo">The details of the new user profile to create</param>
        ///////// <returns>A json result</returns>
        //////[HttpPost]
        //////[Route("CreatePageRole")]
        //////public List<PageRole> CreatePageRole([FromBody]PageRole userInfo)
        //////{
        //////    try
        //////    {
        //////        List<PageRole> result = _iRole.CreatePageRole(userInfo);
        //////        return result;
        //////    }
        //////    catch (Exception exception)
        //////    {
        //////        _logger.LogError("Role Failed CreatePageRole for Id: {0} logged in at {1} with Error {2}", userInfo.Id, DateTime.Now, exception.Message);
        //////        return null;
        //////    }
        //////}
        //////// <summary>Create a new Create Page Role.</summary>
        ///////// <remarks>No remarks.</remarks>
        ///////// <param name="pageInfo">The details of the new user profile to create</param>
        ///////// <returns>A json result</returns>
        //////[HttpPost]
        //////[Route("CreatePage")]
        //////public List<PageRole> CreatePage([FromBody]PageRole pageInfo)
        //////{
        //////    try
        //////    {
        //////        List<PageRole> result = _iRole.CreatePage(pageInfo);
        //////        return result;
        //////    }
        //////    catch (Exception exception)
        //////    {
        //////        _logger.LogError("Role Failed CreatePage for Id: {0} logged in at {1} with Error {2}", pageInfo, DateTime.Now, exception.Message);
        //////        return null;
        //////    }
        //////}
        //////// <summary>Create a new Create Page Role.</summary>
        ///////// <remarks>No remarks.</remarks>
        ///////// <param name="userInfo">The details of the new user profile to create</param>
        ///////// <returns>A json result</returns>
        //////[HttpPost]
        //////[Route("CreateRole")]
        //////public List<PageRole> CreateRole([FromBody]PageRole userInfo)
        //////{
        //////    try
        //////    {
        //////        List<PageRole> result = _iRole.CreateRole(userInfo);
        //////        return result;
        //////    }
        //////    catch (Exception exception)
        //////    {
        //////        _logger.LogError("Role Failed CreateRole for Id: {0} logged in at {1} with Error {2}", userInfo.Id, DateTime.Now, exception.Message);
        //////        return null;
        //////    }
        //////}
        ///////// <summary>Update a user profile.</summary>
        ///////// <remarks>No remarks.</remarks>
        ///////// <param name="newUserProfile">The details of the user profile to update</param>
        ///////// <returns>A json result</returns>
        //////[HttpPut]
        //////[Route("UpdatePage")]
        //////public IActionResult UpdatePage([FromBody]CustomUserProfile newUserProfile)
        //////{
        //////    try
        //////    {
        //////        IActionReturn result = _iRole.UpdatePage(newUserProfile);
        //////        return Ok(result);
        //////    }
        //////    catch (Exception exception)
        //////    {
        //////        _logger.LogError("Role Failed CreateRole for Id: {0} logged in at {1} with Error {2}", newUserProfile.Id, DateTime.Now, exception.Message);
        //////        return StatusCode((int)HttpStatusCode.InternalServerError, exception);
        //////    }
        //////}
        /// <summary>Update a user profile.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="newUserProfile">The details of the user profile to update</param>
        /// <returns>A json result</returns>
        //[HttpPut]
        //[Route("UpdateRole")]
        //public IActionResult UpdateRole([FromBody]CustomUserProfile newUserProfile)
        //{
        //    try
        //    {
        //        IActionReturn result = _iRole.UpdateRole(newUserProfile);
        //        return Ok(result);
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.LogError("Role Failed CreateRole for Id: {0} logged in at {1} with Error {2}", newUserProfile.Id, DateTime.Now, exception.Message);
        //        return StatusCode((int)HttpStatusCode.InternalServerError, exception);
        //    }
        //}
        /// <summary>Delete a new user.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="UserID">The user id to be deleted.</param>
        /// <returns>A json result</returns>
        [HttpDelete]
        [Route("DeletePageRoleByRoleID/{UserID}")]
        public IActionResult DeletePageRoleByRoleID(int UserID)
        {
            try
            {
                IActionReturn result = _iRole.DeletePageRoleByRoleID(UserID);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError("Role Failed DeletePageRoleByRoleID for UserID: {0} logged in at {1} with Error {2}", UserID, DateTime.Now, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
        /// <summary>Delete a new user.</summary>
        /// <remarks>No remarks.</remarks>
        /// <param name="PageID">The page id to be deleted.</param>
        /// <returns>A json result</returns>
        [HttpDelete]
        [Route("DeletePageRoleByPageID/{PageID}")]
        public IActionResult DeletePageRoleByPageID(int PageID)
        {
            try
            {
                IActionReturn result = _iRole.DeletePageRoleByPageID(PageID);
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError("Role Failed DeletePageRoleByPageID for Id: {0} logged in at {1} with Error {2}", PageID, DateTime.Now, exception.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
