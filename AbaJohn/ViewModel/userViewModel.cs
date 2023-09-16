using AbaJohn.Models;
using System.ComponentModel.DataAnnotations;

namespace AbaJohn.ViewModel
{
    public class userViewModel
    {


        public string Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string user_name { get; set; }
        [Required]


        [DataType(DataType.Text)]
        public string name { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("password")]
        public string confirm_password { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public IFormFile ImageFile { get; set; }
        public string image { get; set; }


        [Required]
        [Range(15, 100)]
        public int age { get; set; }



        [Required]
        public string gender { get; set; }



        [Required]
        [DataType(DataType.PhoneNumber)]
        [MinLength(11)]
        public string phone_number { get; set; }

        public Address? address { get; set; }

        public string? Role1 { get; set; }

    }
}