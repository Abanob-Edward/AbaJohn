using AbaJohn.Models;

namespace AbaJohn.Services.CartItems
{
    public class CartRepository : ICart
    {
        private readonly ApplicationDbContext context;

        public CartRepository(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public int AddProductToCartItem(int PoductID, int CartId, string? color, string Size)
        {
            CartItemProduct product = new CartItemProduct();
            product.ProductID = PoductID;
            product.CartItemId = CartId;
            product.ItemColor = color;
            product.ItemSize = Size;
            context.Add(product);

           return  context.SaveChanges();
        }

        public (bool, int) CheeckFirstItemInCart(string UserId)
        {
            var result = context.cartItems.Any(x => x.UserId == UserId);
            var  CartId = context.cartItems.FirstOrDefault(x => x.UserId == UserId).Id;
            return(result, CartId);
        }

        public int CreateNewCart(string UserId)
        {
          CartItem  cart = new CartItem() { UserId= UserId ,ProdectCount=0};
            context.cartItems.Add(cart);
              context.SaveChanges();
            return cart.Id;

        }
    }
}
