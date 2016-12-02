using DoorKicker.Entities;
using DoorKicker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DoorKicker.Controllers.Api
{
    public class DoorsController: Controller
    {
        private IRepository<Door> _doorRepository;
        private IRepository<Event> _eventRepository;
        private IRepository<Property> _propertyRepository;
        private ILogger<DoorsController> _logger;
        private UserManager<User> _userManager;

        public DoorsController(IRepository<Door> doorRepository, IRepository<Event> eventRepository, IRepository<Property> propertyRepository, UserManager<User> userManager, ILogger<DoorsController> logger)
        {
            _doorRepository = doorRepository;
            _eventRepository = eventRepository;
            _propertyRepository = propertyRepository;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet("api/properties/{propertyId}/doors")]
        public IActionResult Get(int propertyId)
        {
            try
            {
                var results = _doorRepository.GetAll().Where(d => d.PropertyId == propertyId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet("api/properties/{propertyId}/doors/{doorId}/events")]
        public IActionResult GetEvents(int propertyId, int doorId)
        {
            try
            {
                var results = _eventRepository.GetAll().Where(e => e.DoorId == doorId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [Authorize]
        [HttpPost("api/properties/{propertyId}/doors")]
        public IActionResult Create(int propertyId, [FromBody]Door door)
        {
            try
            {
                var property = _propertyRepository.Get(propertyId);

                if (property == null)
                    return BadRequest();

                if (door == null)
                    return BadRequest();

                if (_doorRepository.GetAll().Any(d => d.Token.Equals(door.Token)))
                    return BadRequest();

                door.PropertyId = property.Id;

                _doorRepository.Insert(door);

                return Ok();
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }            
        }

        [Authorize]
        [HttpPost("api/properties/{propertyId}/doors/{doorId}/open")]
        public IActionResult Open(int propertyId, int doorId)
        {
            try
            {
                var property = _propertyRepository.GetIncluding(propertyId, p => p.PropertyUsers);

                if (property == null)
                    return BadRequest();

                var door = _doorRepository.GetIncluding(doorId, d => d.Events);

                if (door == null)
                    return BadRequest();

                var currentUserId = _userManager.GetUserId(User);

                if ((door.PropertyId == propertyId) && (property.PropertyUsers.Any(u => u.UserId.Equals(currentUserId))))
                {
                    door.IsOpen = true;
                    door.Events.Add(new Event()
                    {
                        Created = DateTime.UtcNow,
                        DoorId = door.Id,
                        Message = string.Format("{0} just kicked in the door.", User.Identity.Name)
                    });
                    _doorRepository.Update(door);
                    return Ok();
                }

                var unauthorizedEvent = new Event()
                {
                    Created = DateTime.UtcNow,
                    DoorId = door.Id,
                    Message = "Oh no! Someone you don't know just tried to kick in your door."
                };

                _eventRepository.Insert(unauthorizedEvent);

                return Unauthorized();
            }
            catch(Exception ex)
            {
                return HandleException(ex);
            }
        }

        [Authorize]
        [HttpPost("api/properties/{propertyId}/doors/{doorId}/close")]
        public IActionResult Close(int doorId)
        {
            var door = _doorRepository.Get(doorId);

            door.IsOpen = false;
            _doorRepository.Update(door);

            return Ok();
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
