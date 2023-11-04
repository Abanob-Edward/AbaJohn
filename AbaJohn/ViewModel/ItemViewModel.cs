using AbaJohn.Models;

namespace AbaJohn.ViewModel
{
    public class ItemViewModel
    {
     /*   public int ID { get; set; }*/
        public string? Color { get; set; }
        public string? size { get; set; }
        public int Quantity { get; set; }
        public int productID { get; set; }
        public productViewModel Product { get; set; }

        public List<Colors_and_Sizes> Colors{ get; set; }
        public List<Colors_and_Sizes> Sizes{ get; set; }
     

    }
}
