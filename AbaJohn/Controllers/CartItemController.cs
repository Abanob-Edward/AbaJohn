using AbaJohn.Models;
using AbaJohn.Services.CartItems;
using AbaJohn.Services.user;
using Microsoft.AspNetCore.Mvc;

namespace AbaJohn.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICart CartRepository;
        private readonly Iuser UserReository;

        public CartItemController(ICart _CartRepository, Iuser _UserReository)
        {
            this.CartRepository = _CartRepository;
            UserReository = _UserReository;
        }
        public IActionResult CartItems(int productID)
        {
            return View();
        }

        public IActionResult AddtoCart(int ProID, string? Color, string? Size)
        {


            // cheek if user have account of not 
            var UserName = User.Identity.Name;
            if (string.IsNullOrEmpty(UserName))
            {
                return RedirectToAction("login", "Account");
            }
            string UserId = UserReository.GetCurrentUserID(UserName);
           
            
            // Cheek if it the first item  and add new  cart if not have 
            var  result = CartRepository.CheeckFirstItemInCart(UserId);
            if (!result.Item1)
            {
                //create new cart for user in cartitem  https://localhost:44367/product/ProductDetails/8
                 var CartitemID = CartRepository.CreateNewCart(UserId);

                // add product in cartItem product 

                CartRepository.AddProductToCartItem(ProID, CartitemID ,"Black","Xl");
            }
            else
            {
                // add product in cartitem product 

                CartRepository.AddProductToCartItem(ProID, result.Item2, "Black", "Xl");
            }
            // reternt to cart item page and show all products in cart 
            return RedirectToAction("CartItems", "CartItem", ProID);
        }
    }
}
