using AbaJohn.Models;
using AbaJohn.ViewModel;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AbaJohn.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> usermanger; // بيكلم الداتا بيز 
        private readonly SignInManager<ApplicationUser> signinmanger; //  بيعمل الكوكيز 

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            usermanger = _userManager;
            signinmanger = _signInManager;
        }

        [Authorize]
        public IActionResult index()
        {
            return View();
        }

        public IActionResult registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> registration(userViewModel newuser_account)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = newuser_account.user_name;
                user.Name = newuser_account.name;
                user.Email = newuser_account.email;
                user.img = newuser_account.image;
                user.age = newuser_account.age;
                user.Gender = newuser_account.gender;
                user.PhoneNumber = newuser_account.phone_number;

                IdentityResult result = await usermanger.CreateAsync(user, newuser_account.password);

                if (result.Succeeded == true)
                {
                    await signinmanger.SignInAsync(user, false);
                    return RedirectToAction("index");

                }
                else
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        
                    }
            
            }
            return View(newuser_account);

        }
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(userViewModel newuser_account)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = newuser_account.user_name;
                user.Name = newuser_account.name;
                user.Email = newuser_account.email;
                user.img = newuser_account.image;
                user.age = newuser_account.age;
                user.Gender = newuser_account.gender;
                user.PhoneNumber = newuser_account.phone_number;

                IdentityResult result = await usermanger.CreateAsync(user, newuser_account.password);

                if (result.Succeeded == true)
                {
                    
                    await usermanger.AddToRoleAsync(user, "admin");
                    
                    await signinmanger.SignInAsync(user, false);
                    return RedirectToAction("index","Home");

                }
                else
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);

                    }

            }
            return View(newuser_account);

        }

        public IActionResult login(string? returnurl = "~/Home/Index")
        {
            ViewData["returnurl"] = returnurl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> login(loginViewModel loginaccount, string? returnurl ="~/Home/Index")
        {
            if(ModelState.IsValid==true)
            {
                var user = await usermanger.FindByNameAsync(loginaccount.UserName);
                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await signinmanger.PasswordSignInAsync(user, loginaccount.password, loginaccount.rememberme, false);
                    if (result.Succeeded == true)
                    {
                        var  userRole = await usermanger.GetRolesAsync(user);
                        if (userRole.Contains("seller"))
                        {
                            return LocalRedirect("~/seller/index");
                        }      
                        else if (userRole.Contains("admin"))
                        {
                            return LocalRedirect("~/admin/index");
                        }

                        return LocalRedirect(returnurl);
                    }
                    else
                    {
                        ModelState.AddModelError("", " wrong, try again! !");
                    }
                }
                else
                {
                    ModelState.AddModelError("", " invalid !");
                }
            }
            return View(loginaccount);
        }

        public async Task<IActionResult> logout()
        {
            await signinmanger.SignOutAsync();
            return RedirectToAction("login", "Account");
        }


    }
}
