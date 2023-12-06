namespace AbaJohn.Services.CartItems
{
    public interface ICart
    {
        public (bool,int) CheeckFirstItemInCart(string USerId);

        public int CreateNewCart(string UserId);

        public int AddProductToCartItem(int PoductID, int CartId, string? color, string Size);
    }
}
