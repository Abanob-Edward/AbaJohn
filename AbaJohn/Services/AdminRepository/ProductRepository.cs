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
            return context.products.ToList();
        }
        public Product get_product_byid(int id)
        {
            return context.products.FirstOrDefault(s => s.ID == id);
        }

        public int create(productViewModel new_product_view)
        {

            try
            {

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
                productImage.BaseImg = new_product_view.BaseImg.FileName;
                productImage.Img1 = new_product_view.Img1;
                productImage.Img2 = new_product_view.Img2;
                productImage.Img3 = new_product_view.Img3;


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
            new_product_imag.Img1 = old_product.Img1;
            new_product_imag.Img2 = old_product.Img2;
            new_product_imag.Img3 = old_product.Img3;





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