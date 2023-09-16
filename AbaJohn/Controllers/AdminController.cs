using AbaJohn.Models;
using AbaJohn.Services.AccountRepository;
using AbaJohn.Services.AdminRepository;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
// Mody Medhat
namespace AbaJohn.Controllers
{
    // [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {

        private readonly IcategoeryRepository categoeryRepository;
        private readonly IProductRepository productRepository;
        private readonly IAccountRepository accountRepository;
        private readonly UserManager<ApplicationUser> usermanger; // بيكلم الداتا بيز 
        private readonly SignInManager<ApplicationUser> signinmanger; //  بيعمل الكوكيز 

        public AdminController(IcategoeryRepository _categoeryRepository, IProductRepository _productRepository
            , UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager,
            IAccountRepository _accountRepository)
        {
            accountRepository = _accountRepository;
            categoeryRepository = _categoeryRepository;
            productRepository = _productRepository;
            usermanger = _userManager;
            signinmanger = _signInManager;
        }

        [Authorize(Roles = "admin , seller")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult home()
        {

            return RedirectToAction("index", "home");
        }
        public IActionResult AdminProfile()
        {

            return View();
        }
        [HttpGet]
        public IActionResult AddBussnessACount()
        {
            ViewBag.roles = accountRepository.get_all_roles();
            return View();
        }
    
        [HttpPost]
        public async Task<IActionResult> AddBussnessACount(registrationuserViewModel newuser_account)
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

    }
}
