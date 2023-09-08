using System.ComponentModel.DataAnnotations.Schema;

namespace AbaJohn.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string Name { get; set; }
        public string prodeuctGender  { get; set; }
        public double price { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ICollection<Order> orders { get; set; }
        public ICollection<CartItem> CartItem { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category category { get; set; }

        public List<ProductImage> images { get; set; }
    }


    enum prodeuctGender
    {
        Men , wommen , kides
    }
}
