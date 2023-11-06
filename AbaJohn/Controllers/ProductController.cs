using AbaJohn.Models;
using AbaJohn;

using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using AbaJohn.Services.Itemss;

namespace AbaJohn.Controllers
{
    //Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
       
        private readonly IProductRepository productRepository;
        private readonly IcategoeryRepository categoeryRepository;
        private readonly IItem itemRepository;

        public ProductController( IProductRepository _productRepository, IcategoeryRepository _categoeryRepository , IItem _item)
        {
           
            productRepository = _productRepository;
            categoeryRepository = _categoeryRepository;
            itemRepository = _item;
        }
        [Authorize(Roles = "admin")]
        public IActionResult Show_all_product()
        {

            List<Product> products = productRepository.get_all_product();
            return View(products);
        } 
        [Authorize(Roles = "seller")]
        public IActionResult ShowProductSeller()
        {
           var username = User.Identity?.Name;
            List<Product> products = productRepository.GetSellerProducts(username);
            return View("Show_all_product" ,products);
        }

        [HttpGet]
        public IActionResult ShowProdcutItems(int ProductID)
        {
            var item = itemRepository.GetItemsForPrudect(ProductID);
            return View();
        }
        public IActionResult ShowProductsByGender(string ProductGender, int PageNo = 1)
        {
            if (ProductGender == null || ProductGender == "")
                RedirectToAction("index", "home");
            var productList = productRepository.GetProductsByGender(ProductGender);
            var model = new ProductListVM_Paging
            {
                products = productList,
                CurrentPage = PageNo,
                NoOfRecordPerPage = 2,
                ProductGender = ProductGender
            };
          return View(model); 
        }
     
        [Authorize(Roles = "admin , seller")]
        public IActionResult Add_product()
        {
           
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
        public IActionResult Edit_product([FromRoute] int id, productViewModel product)
        {
            if (ModelState.IsValid)
            {
                productRepository.update(id, product);

                return RedirectToAction("Show_all_product", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Error!");
            }

            return View(product);
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

       
        public IActionResult AddItemToProduct(int product_id)
        {
            // check if the product form sellerProduct List or not 

            ViewBag.Message = "";
            ItemViewModel ProductWithItems = new ItemViewModel()
            {
                // get product by image 
                Product = productRepository.get_product_byid(product_id),
                Colors = Colors_and_Sizes.getcolors(),
                Sizes = Colors_and_Sizes.getSizes(),

                productID = product_id
                
            };

       
          

            return View(ProductWithItems);

        }

        [HttpPost]
        public IActionResult AddItemToProduct(ItemViewModel Item)
        {
            productRepository.AddItemToProduct(Item);
            ViewBag.Message = "Item Successfully Added";
            ItemViewModel ProductWithItems = new ItemViewModel()
            {
                // get product by image 
                Product = productRepository.get_product_byid(Item.productID),
                Colors = Colors_and_Sizes.getcolors(),
                Sizes = Colors_and_Sizes.getSizes(),
                productID = Item.productID

            };

            return View(ProductWithItems);
         
        }
        [HttpDelete]
        public IActionResult DeleteItem(int ItemId)
        {
            return View ();
        }


    }
}