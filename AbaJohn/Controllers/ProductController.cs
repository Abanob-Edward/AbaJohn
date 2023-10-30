using AbaJohn.Models;
using AbaJohn.Services.AdminRepository;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AbaJohn.Controllers
{
    //Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
       
        private readonly IProductRepository productRepository;
        private readonly IcategoeryRepository categoeryRepository;

        public ProductController( IProductRepository _productRepository, IcategoeryRepository _categoeryRepository)
        {
           
            productRepository = _productRepository;
            categoeryRepository = _categoeryRepository;
        }
        [Authorize(Roles = "admin , seller")]
        public IActionResult Show_all_product()
        {

            List<Product> products = productRepository.get_all_product();
            return View(products);
        }
        public IActionResult ShowProductsByGender(string ProductGender)
        {
            if (ProductGender == null || ProductGender == "")
                RedirectToAction("index", "home");

            var productList = productRepository.GetProductsByGender(ProductGender);
            return View(productList); 
        }
     
        [Authorize(Roles = "admin , seller")]
        public IActionResult Add_product()
        {
            /*ViewBag.Lay = TempData["layout"]; */
           // ViewBag.cat = context.categories.Select(x => new { x.Id, x.Name }).ToList();
            ViewBag.cat = categoeryRepository.get_all();
       

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin , seller")]
        public IActionResult Add_product(productViewModel new_product)
        {
            ViewBag.cat = categoeryRepository.get_all();
            if (ModelState.IsValid)
            {

                productRepository.create(new_product);

                return RedirectToAction("Show_all_product", "Product");

            }
            else
            {
                ModelState.AddModelError("", "Error!");
                return View(new_product);
            }


        }

        [Authorize(Roles = "admin , seller")]
        public IActionResult Edit_product(int id)
        {
            //ViewData["old_product"] = productRepository.get_product_byid(id);
            ViewBag.cat = categoeryRepository.get_all();
            var product = productRepository.get_product_byid(id);
            return View(product);
        }

        [HttpPost]
        [Authorize(Roles = "admin , seller")]
        public IActionResult Edit_product([FromRoute] int id, productViewModel old_product)
        {
            if (ModelState.IsValid)
            {
                productRepository.update(id, old_product);

                return RedirectToAction("Show_all_product", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Error!");
            }

            return View(old_product);
        }
        [Authorize(Roles = "admin , seller")]
        public IActionResult Delete_product(int id)
        {
            try
            {
                productRepository.Delete(id);
                return RedirectToAction("Show_all_product", "product");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.InnerException.Message);

                return RedirectToAction("Index", "Home");
            }
        }



    }
}
