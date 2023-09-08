using AbaJohn.Models;
using AbaJohn.Services.AdminRepository;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AbaJohn.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
         private readonly IcategoeryRepository categoeryRepository;
         private readonly IProductRepository productRepository;
     
        public CategoryController(IProductRepository _productRepository, IcategoeryRepository _categoeryRepository)
        {
            _productRepository = productRepository;
            _categoeryRepository = categoeryRepository;
        }
        [HttpGet]
        public IActionResult Show_all_category()
        {
            List<Category> categories = categoeryRepository.get_all();
            return View(categories);
        }
        public IActionResult Add_categoery()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add_categoery(categoeryViewModel new_category_view)
        {
            if (ModelState.IsValid)
            {

                categoeryRepository.create(new_category_view);
                return RedirectToAction("index", "home");
            }
            else
            {
                ModelState.AddModelError("", "Error!");
            }

            return View(new_category_view);

        }

        public IActionResult Edit(int id)
        {
            Category old_category = categoeryRepository.get_category_id(id);
            return View(old_category);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Category old_category_view)
        {
            if (ModelState.IsValid)
            {

                categoeryRepository.update(id, old_category_view);
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

                return RedirectToAction("Index", "Home");
            }
        }


    }
}
