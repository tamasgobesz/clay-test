using DoorKicker.Entities;
using DoorKicker.Models.Api;
using DoorKicker.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DoorKicker.Controllers.Api
{
    public class UsersController: Controller
    {
        private UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Route("api/properties/{propertyId}/users")]
        [HttpGet]
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
    }
}
