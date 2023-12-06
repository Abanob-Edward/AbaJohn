﻿using AbaJohn.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AbaJohn.ViewModel
{
    public class productViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double price { get; set; }

        public string Code { get; set; }

        public string prodeuctGender { get; set; }
        public string title { get; set; }

        public string Description { get; set; }



        [AllowNull]
        public IFormFile? BaseFileImg { get; set; }
        [AllowNull]
        public IFormFile? Img1File { get; set; }
        [AllowNull]
        public IFormFile? Img2File { get; set; }
        [AllowNull]
        public IFormFile? Img3File { get; set; }
        public string? BaseImg { get; set; }
        public string? Img1 { get; set; }
        public string? Img2 { get; set; }
        public string? Img3 { get; set; }
        public int category_id { get; set; }
        public string? CategoryName { get; set; }
        public string? seller_id { get; set; }
        public string? Castomer_id { get; set; }
        [AllowNull]
        public List<Item> Items { get; set; } //Items
        public List<Colors_and_Sizes> Colors { get; set; }
        public List<Colors_and_Sizes> Sizes { get; set; }


    }
}