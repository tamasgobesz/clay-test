using DoorKicker.Entities;
using DoorKicker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoorKicker.Controllers.Api
{
    public class PropertiesController : Controller
    {
        private IRepository<Property> _propertiesRepository;
        private UserManager<User> _userManager;

        private ILogger<PropertiesController> _logger;

        public PropertiesController(IRepository<Property> propertiesRepository, UserManager<User> userManager, ILogger<PropertiesController> logger)
        {
            _propertiesRepository = propertiesRepository;
            _userManager = userManager;
            _logger = logger;
            
        }

        [Authorize]
        [HttpGet("api/properties")]
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
                return HandleException(ex);
            }
        }

        [Authorize]
        [HttpPost("api/properties")]
        public IActionResult Create([FromBody]Property property)
        {
            try
            {
                var currentUserId = _userManager.GetUserId(User);

                if (currentUserId == null)
                    return Unauthorized();

                

                if (property == null)
                    return BadRequest();

                if (_propertiesRepository.GetAllIncluding(p => p.PropertyUsers)
                    .Any(p => p.Name.Equals(property.Name) && p.PropertyUsers.Any(pu => pu.UserId.Equals(currentUserId))))
                    return Ok();

                var newProperty = _propertiesRepository.Insert(property);

                newProperty.PropertyUsers = new List<PropertyUser>();

                newProperty.PropertyUsers.Add(new PropertyUser(){
                    UserId = currentUserId,
                    PropertyId = newProperty.Id
                });

                _propertiesRepository.Update(newProperty);

                return Ok();
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }            
        }

        private IActionResult HandleException(Exception ex)
        {
            _logger.LogError("{0}", ex);

            var error = new
            {
                message = "Uh oh, something bad happened.",
            };

            return StatusCode(StatusCodes.Status500InternalServerError, error);
        }
    }
}
