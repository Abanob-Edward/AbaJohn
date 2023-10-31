using AbaJohn.Models;
using AbaJohn.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Transactions;

namespace AbaJohn.Services.AdminRepository
{
    public class ProductRepository : IProductRepository
    {
       private readonly ApplicationDbContext context;
        private readonly IMapper _mapper;

      

        public ProductRepository(ApplicationDbContext _context, IMapper mapper)
        {
            context = _context;
            _mapper = mapper;
        }

    
        //CURD
        public List<Product> get_all_product()
        {
            return context.products.Include(x=>x.category).ToList();
        }

        public List<productViewModel> GetProductsByGender(string GenderName)
        {
           var productlst = context.products
                .Where(p => p.prodeuctGender.ToLower() == GenderName.ToLower())
                .Include(I =>I.images).Include(c=>c.category)
                .ToList();

            var data = _mapper.Map<List<productViewModel>>(productlst);
            return data;
        }

        public productViewModel get_product_byid(int id)
        {
            productViewModel product_vw = new productViewModel();
            Product product = context.products.FirstOrDefault(x => x.ID == id);

            ProductImage product_images = context.productImages.FirstOrDefault(c => c.Product_id == id);
            if(product != null && product_images != null)
            {
                product_vw.Name = product.Name;
                product_vw.price = product.price;
                product_vw.Quantity = product.Quantity;
                product_vw.Size = product.Size;
                product_vw.Code = product.Code;
                product_vw.title = product.title;
                product_vw.Description = product.Description;
                product.prodeuctGender = "Wommen";
                product_vw.category_id = product.CategoryID;

                product_vw.BaseImg = product_images.BaseImg;
                product_vw.Img1 = product_images.Img1;
                product_vw.Img2 = product_images.Img2;
                product_vw.Img3 = product_images.Img3;
                return product_vw;
            }



            return null;

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

        public int update(int id, productViewModel old_product)
        {
            Product new_product = context.products.FirstOrDefault(s => s.ID == id);
            ProductImage new_product_imag = context.productImages.FirstOrDefault(s => s.Id == id);

            string baseImgFileName = "";
            string img1FileName = "";
            string img2FileName = "";
            string img3FileName = "";

            new_product.Name = old_product.Name;
            new_product.price = old_product.price;
            new_product.Quantity = old_product.Quantity;
            new_product.Size = old_product.Size;
            new_product.Code = old_product.Code;
            new_product.title = old_product.title;
            new_product.Description = old_product.Description;
            new_product.CategoryID = old_product.category_id;

            context.SaveChanges();



            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/productImg");

                // Create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (old_product.BaseFileImg != null)
                {
                    // Generate a unique file name for BaseImg
                    baseImgFileName = GenerateUniqueImageName() + Path.GetExtension(old_product.BaseFileImg.FileName);
                    string baseImgFilePath = Path.Combine(path, baseImgFileName);

                    using (var stream = new FileStream(baseImgFilePath, FileMode.Create))
                    {
                        old_product.BaseFileImg.CopyTo(stream);
                    }
                    string deletepath = path + "/" + old_product.BaseImg;
                    FileInfo file = new FileInfo(deletepath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    new_product_imag.BaseImg = baseImgFileName;
                }

                if (old_product.Img1File != null)
                {
                    // Generate a unique file name for Img1
                    img1FileName = GenerateUniqueImageName() + Path.GetExtension(old_product.Img1File.FileName);
                    string img1FilePath = Path.Combine(path, img1FileName);

                    using (var stream = new FileStream(img1FilePath, FileMode.Create))
                    {
                        old_product.Img1File.CopyTo(stream);
                    }
                    string deletepath = path + "/" + old_product.Img1;
                    FileInfo file = new FileInfo(deletepath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    new_product_imag.Img1 = img1FileName;
                }

                if (old_product.Img2File != null)
                {
                    // Generate a unique file name for Img2
                    img2FileName = GenerateUniqueImageName() + Path.GetExtension(old_product.Img2File.FileName);
                    string img2FilePath = Path.Combine(path, img2FileName);

                    using (var stream = new FileStream(img2FilePath, FileMode.Create))
                    {
                        old_product.Img2File.CopyTo(stream);
                    }

                    string deletepath = path + "/" + old_product.Img2;

                    FileInfo file = new FileInfo(deletepath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    new_product_imag.Img2 = img2FileName;
                }

                if (old_product.Img3File != null)
                {
                    // Generate a unique file name for Img3
                    img3FileName = GenerateUniqueImageName() + Path.GetExtension(old_product.Img3File.FileName);
                    string img3FilePath = Path.Combine(path, img3FileName);

                    using (var stream = new FileStream(img3FilePath, FileMode.Create))
                    {
                        old_product.Img3File.CopyTo(stream);
                    }
                    string deletepath = path + "/" + old_product.Img3;
                    FileInfo file = new FileInfo(deletepath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    new_product_imag.Img3 = img3FileName;
                }


                int updateCount = context.SaveChanges();
                return updateCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                // Handle the exception or log the error here
            }
            return 0;
        }
        public int create(productViewModel new_product_view)
        {
            string baseImgFileName  = "";
            string img1FileName     = "";
            string img2FileName     = "";
            string img3FileName     = "";
            Product new_product = new Product();
            ProductImage productImage = new ProductImage();
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files/productImg");

                // Create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (new_product_view.BaseFileImg != null)
                {
                    // Generate a unique file name for BaseImg
                    baseImgFileName = GenerateUniqueImageName() + Path.GetExtension(new_product_view.BaseFileImg.FileName);
                    string baseImgFilePath = Path.Combine(path, baseImgFileName);

                    using (var stream = new FileStream(baseImgFilePath, FileMode.Create))
                    {
                        new_product_view.BaseFileImg.CopyTo(stream);
                    }
                    productImage.BaseImg = baseImgFileName;
                }

                if (new_product_view.Img1File != null)
                {
                    // Generate a unique file name for Img1
                    img1FileName = GenerateUniqueImageName() + Path.GetExtension(new_product_view.Img1File.FileName);
                    string img1FilePath = Path.Combine(path, img1FileName);

                    using (var stream = new FileStream(img1FilePath, FileMode.Create))
                    {
                        new_product_view.Img1File.CopyTo(stream);
                    }
                    productImage.Img1 = img1FileName;
                }

                if (new_product_view.Img2File != null)
                {
                    // Generate a unique file name for Img2
                    img2FileName = GenerateUniqueImageName() + Path.GetExtension(new_product_view.Img2File.FileName);
                    string img2FilePath = Path.Combine(path, img2FileName);

                    using (var stream = new FileStream(img2FilePath, FileMode.Create))
                    {
                        new_product_view.Img2File.CopyTo(stream);
                    }
                    productImage.Img2 = img2FileName;
                }

                if (new_product_view.Img3File != null)
                {
                    // Generate a unique file name for Img3
                    img3FileName = GenerateUniqueImageName() + Path.GetExtension(new_product_view.Img3File.FileName);
                    string img3FilePath = Path.Combine(path, img3FileName);

                    using (var stream = new FileStream(img3FilePath, FileMode.Create))
                    {
                        new_product_view.Img3File.CopyTo(stream);
                    }
                    productImage.Img3 = img3FileName;
                }

                new_product.Name           = new_product_view.Name;
                new_product.price          = new_product_view.price;
                new_product.Quantity       = new_product_view.Quantity;
                new_product.Size           = new_product_view.Size;
                new_product.Code           = new_product_view.Code;
                new_product.title          = new_product_view.title;
                new_product.Description    = new_product_view.Description;
                new_product.prodeuctGender = "Wommen";
                new_product.CategoryID      = new_product_view.category_id;
                context.products.Add(new_product);

                context.SaveChanges();

                var pro_id = new_product.ID;
                productImage.Product_id = pro_id;





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