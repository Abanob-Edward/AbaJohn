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
            return View(item);
        }
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
       
        [HttpDelete]
        public IActionResult DeleteItem(int ItemId)
        {
            return View();
        }
    }
}
