using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Constants.Message;
using Core.FrontEnd;
using Entities.Concrete;
using Entities.FrontEnd;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PagedList;
using PagedList.Core;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;



        public AdminController(INotyfService notyfService, IUserService userService, IGroupService groupService)
        {
            _notyfService = notyfService;
            _userService = userService;
            _groupService = groupService;
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Dashboard()
        {
            _notyfService.Success("Test");
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Users(int page = 1, int pageSize = 25)
        {
            
            var result = _userService.GetAllUsers(page, pageSize);

            return View("Users", result.Data);

        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> User_Edit(int Id)
        {
            var result = _userService.GetUser(Id);

            if (result.Success)
            {
                return View("User_Edit", result.Data);
            }

            _notyfService.Error(Messages.RecordNotFound);
            return RedirectToAction("Users", "Admin");

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> User_Delete(int Id)
        {
            var user =  _userService.GetUser(Id);
            if (user.Success)
            {
                var result = _userService.User_Delete(user.Data);
                _notyfService.Success(Messages.DeleteRecord);
                if(result.Success)
                {
                    return RedirectToAction("Users", "Admin");
                }
            }

            _notyfService.Error(Messages.RecordNotFound);
            return RedirectToAction("Users", "Admin");

        }

        [Authorize(Roles = "Admin"),HttpPost]
        public async Task<IActionResult> GetGroup(int Id)
        {
            var result = _groupService.GetIdList(Id);

            return new JsonResult(result);

        }



    }
}

