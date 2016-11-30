using DoorKicker.Entities;
using DoorKicker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DoorKicker.Controllers.Web
{
    public class AppController: Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
