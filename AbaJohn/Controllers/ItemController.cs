using AbaJohn.Models;
using AbaJohn.Services.Itemss;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AbaJohn.Controllers
{
    public class ItemController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IcategoeryRepository categoeryRepository;
        private readonly IItem itemRepository;

        public ItemController(IProductRepository _productRepository, IcategoeryRepository _categoeryRepository, IItem _item)
        {

            productRepository = _productRepository;
            categoeryRepository = _categoeryRepository;
            itemRepository = _item;
        }
        [HttpGet]
        public IActionResult ShowItemsForProdcut(int ProductID)
        { 
            var item = itemRepository.GetItemsForPrudect(ProductID);
            var product_name = productRepository.get_product_byid(ProductID).Name;
            ViewBag.ProductName = product_name;

            ItemViewModel ProductWithItems = new ItemViewModel()
            {
                // get product by image 
                Colors = Colors_and_Sizes.getcolors(),
                Sizes = Colors_and_Sizes.getSizes(),

                productID = ProductID

            };

            ViewBag.ProductWithItems = ProductWithItems;

            return View(item);
        }
        [HttpGet]
        public IActionResult AddItemToProduct(int product_id)
        {
            ViewBag.Message = "";
            // check if the product form sellerProduct List or not 
            var username = User.Identity?.Name;
            var result = productRepository.CheeckProductForSeller(product_id, username);
            if (result)
            {

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
            else
            {
                  TempData["massege"] = "لم نفسك عيييب عيب ";
                return RedirectToAction("ShowProductSeller", "product");
            }
               


        }
        //_________________________________________________________
        [HttpPost]
        public IActionResult AddItemToProduct(ItemViewModel Item)
        {
            var username = User.Identity?.Name;
            var result = productRepository.CheeckProductForSeller(Item.productID, username);
            if(result)
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
            else
            {
                TempData["massege"] = " العب غيرها يابن النصابه";
                return RedirectToAction("ShowProductSeller", "product");
            }
         

        }
       
        public IActionResult Edit_item(int ID)
        { 
            var items = itemRepository.Get_all_items().FirstOrDefault(x=>x.ID == ID);

            var username = User.Identity?.Name;
            List<Product> products = productRepository.GetSellerProducts(username);
            ViewBag.products = products;
           
            return View(items);
        }
        [HttpPost]
        public IActionResult Edit_item([FromRoute]int id,Item new_item)
        {
            if (ModelState.IsValid)
            {

                itemRepository.update_item(id,new_item);
                var product_id = new_item.productID;
                return RedirectToAction("ShowProductSeller", "product");

            }
            else
            {
                ModelState.AddModelError("", "Error!");
                return View(new_item);
            }




           
          
        }


        public IActionResult DeleteItem(int ItemId)
        {
            try
            {
                itemRepository.Delete(ItemId);
                return RedirectToAction("ShowProductSeller", "product");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.InnerException.Message);

                return RedirectToAction("Index", "Home");
            }

           
        }
    }
}
