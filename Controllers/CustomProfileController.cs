using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StanbicIBTC.UserMgtProfile.Services.Interface;
using StanbicIBTC.EntityFramework.Extensions;
using StanbicIBTC.UserMgtProfile.Domain.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StanbicIBTC.UserMgtProfile.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CustomProfileController : ControllerBase
    {
        private ICustomerProfile userProfile { get; set; }
        public CustomProfileController(ICustomerProfile UserProfile)
        {
            this.userProfile = UserProfile;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CustomUserProfile userInfo)
        {
            IActionReturn result = userProfile.CreateUser(userInfo);

            return Ok(result);
        }

        [HttpGet()]
        public IActionResult Get()
        {
            IActionReturn result = userProfile.GetAllUsers();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IActionReturn result = userProfile.GetUserByID(id);

            return Ok(result);
        }

       
    }
}
