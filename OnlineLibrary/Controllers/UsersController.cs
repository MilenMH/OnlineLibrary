using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Data.DTOs;
using OnlineLibrary.Data.Models;
using OnlineLibrary.Data.Repos;
using OnlineLibrary.Helpers;
using OnlineLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineLibrary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        public UserManager<ApplicationUser> UserManager { get; set; }

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.UserManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(int? page)
        {
            var users = this.UserManager.GetUsersInRoleAsync("User").Result;
            var usersRolesDTOs = users.Select(au => new UserRoleDTO() {Email = au.Email, Role = "User", ToGiveAdminRights = false }).ToList();


            var pagenatedList = PaginatedList<UserRoleDTO>.Create(usersRolesDTOs, page ?? 1, GlobalConstants.pageSize);
            return View(pagenatedList);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Save(IEnumerable<UserRoleDTO> userRoleDTOs)
        {
            foreach (var user in userRoleDTOs)
            {
                if (user.ToGiveAdminRights)
                {
                    var appUser = this.UserManager.FindByEmailAsync(user.Email).Result;
                    var removeRes = this.UserManager.RemoveFromRoleAsync(appUser, "User").Result;
                    var result = this.UserManager.AddToRoleAsync(appUser, "Admin").Result;
                }
            }

            return RedirectToAction("Index");
        }
    }
}