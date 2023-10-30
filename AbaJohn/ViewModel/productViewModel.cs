using AbaJohn.Models;
using System.ComponentModel.DataAnnotations;

namespace AbaJohn.ViewModel
{
    public class productViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }

        public string title { get; set; }

        public string Description { get; set; }

      
  /*      [Required(ErrorMessage = "Please select file")]*/
        public IFormFile? BaseFileImg { get; set; }

        public IFormFile? Img1File { get; set; }
        public IFormFile? Img2File { get; set; }
        public IFormFile? Img3File { get; set; }
        public string? BaseImg { get; set; }
        public string? Img1 { get; set; }
        public string? Img2 { get; set; }
        public string? Img3 { get; set; }

        public int category_id { get; set; } 

    }
}