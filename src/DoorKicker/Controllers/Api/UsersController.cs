using DoorKicker.Entities;
using DoorKicker.Models.Api;
using DoorKicker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DoorKicker.Controllers.Api
{
    public class UsersController: Controller
    {
        
        private UserManager<User> _userManager;

        private IRepository<Property> _propertiesRepository;

        public UsersController(IRepository<Property> propertiesRepository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _propertiesRepository = propertiesRepository;
        }

        [Authorize]
        [HttpGet("api/users")]
        public IActionResult Get(string username)
        {
            var users = _userManager.Users;

            if (username != null)
                users = users.Where(u => u.UserName.Contains(username));

            var results = users.Select(u => new UserViewModel() {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                });
            
            return Ok(results);
        }

        [Authorize]
        [HttpGet("api/properties/{propertyId}/users")]
        public IActionResult Get(int propertyId)
        {
            var temp = _userManager.Users.Include(x => x.UserProperties).ToList();
            var result = _userManager
                .Users.Include(x => x.UserProperties)
                .Where(x => x.UserProperties.Any(p => p.PropertyId == propertyId))
                .Select(u => new UserViewModel() {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                });
            ;
            return Ok(result);
        }

        [Authorize]
        [HttpPost("api/properties/{propertyId}/users")]
        public IActionResult Post(int propertyId, [FromBody]UserViewModel user)
        {
            var currentUserId = _userManager.GetUserId(User);

            if (currentUserId == null)
                return Unauthorized();

            var property = _propertiesRepository.GetIncluding(propertyId, p => p.PropertyUsers);

            if (property == null)
                return BadRequest();

            if (!property.PropertyUsers.Any(pu => pu.UserId.Equals(currentUserId)))
                return Unauthorized(); 

            var userToAdd = _userManager.Users.SingleOrDefault(u => u.Id.Equals(user.Id));
            if (userToAdd == null)
                return BadRequest();

            
            if (property.PropertyUsers.Any(pu => pu.UserId.Equals(userToAdd.Id)))
                return Ok();
                

            property.PropertyUsers.Add(new PropertyUser(){
                UserId = userToAdd.Id,
                PropertyId = property.Id
            });

            _propertiesRepository.Update(property);

            return Ok();
        }
    }
}
