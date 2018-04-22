using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chatter.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chatter.Site.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IUser _user;

        public ErrorController(IUser user)
        {
            _user = user;
        }

        [Route("erro-aplicacao")]
        [Route("erro-aplicacao/{id}")]
        public IActionResult Index(string id)
        {
            switch (id)
            {
                case "404":
                    return View("NotFound");
                case "401":
                case "403":
                    if (!_user.IsAuthenticated())
                        return RedirectToAction("Login", "Account");
                    return View("AccessDenied");
            }
            return View("Error");
        }
    }
}