using DoorKicker.Entities;
using DoorKicker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DoorKicker.Controllers.Api
{
    public class PropertiesController : Controller
    {
        private IRepository<Property> _propertiesRepository;
        private UserManager<User> _userManager;

        public PropertiesController(IRepository<Property> propertiesRepository, UserManager<User> userManager)
        {
            _propertiesRepository = propertiesRepository;
            _userManager = userManager;
        }

        [Route("api/properties")]
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var user = _userManager.GetUserId(User);
                var results = _propertiesRepository.GetAllIncluding(p => p.PropertyUsers).Where(p => p.PropertyUsers.Any(x => x.UserId.Equals(user)));
                return Ok(results);
            }
            catch (Exception ex)
            {
                var error = new
                {
                    message = "Uh oh, something bad happened.",
                };

                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
    }
}
