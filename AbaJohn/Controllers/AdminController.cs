using AbaJohn.Models;
using AbaJohn.Services.AdminRepository;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
// Mody Medhat
namespace AbaJohn.Controllers
{
   // [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
      
        private readonly   IcategoeryRepository categoeryRepository;
        private readonly   IProductRepository productRepository;


        public AdminController( IcategoeryRepository _categoeryRepository, IProductRepository _productRepository)
        {
              
              categoeryRepository= _categoeryRepository;
              productRepository= _productRepository;
        }
        [Authorize(Roles ="admin , seller")]
        public IActionResult Index()
        {
            return  View();
        }
   /*     [HttpGet]
        public IActionResult Show_all_category()
        {
            List<Category> categories = categoeryRepository.get_all();
            return View(categories);
        }*/
/*        public IActionResult Add_categoery()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_categoery(categoeryViewModel new_category_view)
        {
            if(ModelState.IsValid)
            { 
               
               categoeryRepository.create(new_category_view);
               return RedirectToAction("index", "home");
            }
            else
            {
                ModelState.AddModelError("", "Error!");
            }

            return View(new_category_view);

        }*/
        /*public IActionResult Edit(int id)
        {
            Category old_category = context.categories.FirstOrDefault(s => s.Id == id);
            return View(old_category);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id,Category old_category_view)
        {
            if (ModelState.IsValid)
            {
                
                categoeryRepository.update(id,old_category_view);
                return RedirectToAction("Show_all_category", "AdminServics");
            }
            else
            {
                ModelState.AddModelError("", "Error!");
            }

            return View(old_category_view);
            }
        public IActionResult Delete(int id)
        {
            try
            {
                categoeryRepository.Delete(id);
                return RedirectToAction("Show_all_category");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.InnerException.Message);//

                return RedirectToAction("Index","Home");
            }
        }

*/


        //________________________________________________________________________________________________________________
        //    Product operations.



        public IActionResult Show_all_product()
        {
           
            List<Product> products = productRepository.get_all_product();
            return View(products);
        }

        public IActionResult Add_product()
        {
           ViewBag.cat = categoeryRepository.get_all();
            /*ViewBag.cat = context.categories.Select(x => new { x.Id, x.Name }).ToList();*/
            return View();
        }
        [HttpPost]
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



        public IActionResult Edit_product(int id)
        {
          //  Product old_product = context.products.FirstOrDefault(s => s.ID == id);

            ViewData["old_product"] = productRepository.get_product_byid(id);
     
            return View();
        }

        [HttpPost]
        public IActionResult Edit_product([FromRoute] int id, productViewModel old_product)
        {
            if (ModelState.IsValid)
            {
                productRepository.update(id, old_product);
                return RedirectToAction("Show_all_product", "AdminServics");
            }
            else
            {
                ModelState.AddModelError("", "Error!");
            }

            return View(old_product);
        }

        public IActionResult Delete_product(int id)
        {
            try
            {
                productRepository.Delete(id);
                return RedirectToAction("Show_all_product", "AdminServics");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.InnerException.Message);

                return RedirectToAction("Index", "Home");
            }
        }


        //____________________________________________________________________________________________________
        //    productimage










    }
}
