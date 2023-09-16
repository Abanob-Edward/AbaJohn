using AbaJohn.Models;
using AbaJohn.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Transactions;

namespace AbaJohn.Services.AdminRepository
{
    public class ProductRepository : IProductRepository
    {
       private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        //CURD
        public List<Product> get_all_product()
        {
            return context.products.Include(x=>x.category).ToList();
        }
        public Product get_product_byid(int id)
        {
            return context.products.FirstOrDefault(s => s.ID == id);
        }

        public string GenerateUniqueImageName()
        {
            // Get the current date and time
            DateTime now = DateTime.Now;

            // Generate a unique name using a combination of timestamp and random number
            string uniqueName = $"{now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString("N").Substring(0, 8)}";

            // Return the unique name
            return uniqueName;
        }

        public int create(productViewModel new_product_view)
        {
            string baseImgFileName = "";
            string img1FileName = "";
            string img2FileName = "";
            string img3FileName = "";

            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/productImg");

                // Create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (new_product_view.BaseImg != null)
                {
                    // Generate a unique file name for BaseImg
                    baseImgFileName = GenerateUniqueImageName() + Path.GetExtension(new_product_view.BaseImg.FileName);
                    string baseImgFilePath = Path.Combine(path, baseImgFileName);

                    using (var stream = new FileStream(baseImgFilePath, FileMode.Create))
                    {
                        new_product_view.BaseImg.CopyTo(stream);
                    }
                }

                if (new_product_view.Img1 != null)
                {
                    // Generate a unique file name for Img1
                    img1FileName = GenerateUniqueImageName() + Path.GetExtension(new_product_view.Img1.FileName);
                    string img1FilePath = Path.Combine(path, img1FileName);

                    using (var stream = new FileStream(img1FilePath, FileMode.Create))
                    {
                        new_product_view.Img1.CopyTo(stream);
                    }
                }

                if (new_product_view.Img2 != null)
                {
                    // Generate a unique file name for Img2
                    img2FileName = GenerateUniqueImageName() + Path.GetExtension(new_product_view.Img2.FileName);
                    string img2FilePath = Path.Combine(path, img2FileName);

                    using (var stream = new FileStream(img2FilePath, FileMode.Create))
                    {
                        new_product_view.Img2.CopyTo(stream);
                    }
                }

                if (new_product_view.Img3 != null)
                {
                    // Generate a unique file name for Img3
                    img3FileName = GenerateUniqueImageName() + Path.GetExtension(new_product_view.Img3.FileName);
                    string img3FilePath = Path.Combine(path, img3FileName);

                    using (var stream = new FileStream(img3FilePath, FileMode.Create))
                    {
                        new_product_view.Img3.CopyTo(stream);
                    }
                }

                Product new_product = new Product();
                ProductImage productImage = new ProductImage();

                new_product.Name = new_product_view.Name;
                new_product.price = new_product_view.price;
                new_product.Quantity = new_product_view.Quantity;
                new_product.Size = new_product_view.Size;
                new_product.Code = new_product_view.Code;
                new_product.title = new_product_view.title;
                new_product.Description = new_product_view.Description;
                new_product.prodeuctGender = "Wommen";
                new_product.CategoryID = new_product_view.category_id;
                context.products.Add(new_product);

                context.SaveChanges();

                var pro_id = new_product.ID;
                productImage.Product_id = pro_id;
                productImage.BaseImg = baseImgFileName;
                productImage.Img1 = img1FileName;
                productImage.Img2 = img2FileName;
                productImage.Img3 = img3FileName;


                context.productImages.Add(productImage);

                int product = context.SaveChanges();
                return product;

                // Commit the transaction if everything succeeds
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
            return 0;
        }


        public int update(int id, productViewModel old_product)
        {
            
            Product new_product = context.products.FirstOrDefault(s => s.ID == id);
            ProductImage new_product_imag = context.productImages.FirstOrDefault(s => s.Id == id);



            new_product.Name = old_product.Name;
            new_product.price = old_product.price;
            new_product.Quantity = old_product.Quantity;
            new_product.Size = old_product.Size;
            new_product.Code = old_product.Code;
            new_product.title = old_product.title;
            new_product.Description = old_product.Description;
            new_product.CategoryID = old_product.category_id;

            new_product_imag.BaseImg = old_product.BaseImg.FileName;
            new_product_imag.Img1 = old_product.Img1.FileName;
            new_product_imag.Img2 = old_product.Img2.FileName;
            new_product_imag.Img3 = old_product.Img3.FileName;





            int update = context.SaveChanges();
            return update;

        }

        public int Delete(int id)
        {
            Product product = context.products.FirstOrDefault(s => s.ID == id);
            ProductImage product_imag = context.productImages.FirstOrDefault(s => s.Id == id);
            context.Remove(product);
            context.Remove(product_imag);
            int delete = context.SaveChanges();
            return delete;
        }

    }
}