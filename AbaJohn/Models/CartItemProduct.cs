using System.ComponentModel.DataAnnotations.Schema;

namespace AbaJohn.Models
{
    public class CartItemProduct
    {

        public int Id { get; set; }
        public int CartItemId { get; set; }
        [ForeignKey("CartItemId")]
        public CartItem cartItems { get; set; }

        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public Product products { get; set; }

        public string ItemColor { get; set; }
        public string ItemSize { get; set; }
    }
}
