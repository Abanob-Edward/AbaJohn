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
            /*ViewBag.layout = "_sellerLayout";*/
         /*   TempData["layout"] = "_sellerLayout";*/
            return View();
        }

        public IActionResult ShowAllproduct() {
            List<Product> products = productRepository.get_all_product();
            return View(products);
          
        }
        [Authorize(Roles = "admin , seller")]
        public IActionResult Add_product()
        {

            // ViewBag.cat = context.categories.Select(x => new { x.Id, x.Name }).ToList();
            ViewBag.cat = categoeryRepository.get_all();


            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin , seller")]
        public IActionResult Add_product(productViewModel new_product)
        {
            if (ModelState.IsValid)
            {

                productRepository.create(new_product);

                return RedirectToAction("Show_all_product", "AdminServics");

            }
            else
            {
                ModelState.AddModelError("", "Error!");
            }

            return View(new_product);

        }
    }
}
