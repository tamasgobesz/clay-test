using DoorKicker.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoorKicker.Repositories
{
    public class DoorKickerSeedData
    {
        private DoorDbContext _context;
        private UserManager<User> _userManager;

        public DoorKickerSeedData(DoorDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            if (await _userManager.FindByEmailAsync("tom@doorkickers.com") == null)
            {
                var user = new User()
                {
                    UserName = "Tom",
                    Email = "tom@doorkickers.com"
                };

                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }

            if (await _userManager.FindByEmailAsync("dick@doorkickers.com") == null)
            {
                var user = new User()
                {
                    UserName = "Dick",
                    Email = "dick@doorkickers.com"
                };

                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }

            if (await _userManager.FindByEmailAsync("harry@doorkickers.com") == null)
            {
                var user = new User()
                {
                    UserName = "Harry",
                    Email = "harry@doorkickers.com"
                };

                await _userManager.CreateAsync(user, "P@ssw0rd!");
            }

            var demoUser = await _userManager.FindByEmailAsync("tom@doorkickers.com");

            if (!_context.PropertyUser.Any(x => x.UserId == demoUser.Id))
            {
                var property = new Property()
                {
                    Name = "Office",
                    Doors = new List<Door>()
                };

                _context.Properties.Add(property);
                await _context.SaveChangesAsync();

                var firstDoor = new Door()
                {
                    Name = "Tunnel Door",
                    Token = Guid.NewGuid().ToString(),
                    IsOpen = false,
                    PropertyId = property.Id
                };

                var secondDoor = new Door()
                {
                    Name = "Office Door",
                    Token = Guid.NewGuid().ToString(),
                    IsOpen = false,
                    PropertyId = property.Id
                };

                _context.Doors.Add(firstDoor);
                _context.Doors.Add(secondDoor);

                var propertyUser = new PropertyUser()
                {
                    PropertyId = property.Id,
                    UserId = demoUser.Id
                };

                _context.PropertyUser.Add(propertyUser);

                await _context.SaveChangesAsync();

            }

            //if (!_context.UserDoors.Any(x => x.UserId == demoUser.Id))
            //{
            //    demoUser.UserDoors.Add(new UserDoor()
            //    {
            //        UserId = demoUser.Id,
            //        DoorId = 1
            //    });

            //    demoUser.UserDoors.Add(new UserDoor()
            //    {
            //        UserId = demoUser.Id,
            //        DoorId = 2
            //    });

            //    await _context.SaveChangesAsync();
            //}
        }
    }
}
