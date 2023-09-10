using AbaJohn.Models;
using AbaJohn.Services.AdminRepository;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbaJohn.Controllers
{
    //[Authorize(Roles = "seller")]
    public class SellerController : Controller
    {


        private readonly IProductRepository productRepository;
        private readonly IcategoeryRepository categoeryRepository;

        public SellerController(IProductRepository _productRepository, IcategoeryRepository _categoeryRepository)
        {

            productRepository = _productRepository;
            categoeryRepository = _categoeryRepository;
        }
        [Authorize(Roles = "admin , seller")]
        public IActionResult Index()
        {
          
            return View();
        }
        public IActionResult home()
        {
          
            return RedirectToAction("index","home");
        }
        public IActionResult SellerProfile()
        {
            return View();
        }
    }
}
