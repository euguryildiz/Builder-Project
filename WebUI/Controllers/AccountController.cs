using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Entities.Concrete;
using Entities.FrontEnd;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebUI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly IUserService _userService;


        public AccountController(INotyfService notyfService, IUserService userService)
        {
            _notyfService = notyfService;
            _userService = userService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Login()
        {
            var user = await GetUser();
            if (user != null)
            {
                var role = User.Claims?.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
                if (role == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else if (role == "User")
                {

                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {

            var result = _userService.AdminLogin(user.Username, user.Password);

            if (result.Success)
            {
                await GenerateLoginToken(result.Data);
                return RedirectToAction("Dashboard","Admin");
            }
            _notyfService.Error(result.Message);

            return RedirectToAction("Login");
        }

        private async Task GenerateLoginToken(User user)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Uri, user.Image != null ? user.Image : ""));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Username));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.Surname));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            var UserModel = new Entities.FrontEnd.UserModel()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Username = user.Username,
                Image = user.Image
            };

            identity.AddClaim(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(UserModel)));
            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal), authProperties);
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login","Account");
        }

        [Authorize]
        private async Task<UserModel> GetUser()
        {
            var user = default(UserModel);
            var UserData = User.Claims?.Where(c => c.Type == ClaimTypes.UserData).Select(c => c.Value).SingleOrDefault();
            if (UserData != null)
            {
                user = JsonConvert.DeserializeObject<UserModel>(UserData);
            }

            return user;

        }


    }
}

