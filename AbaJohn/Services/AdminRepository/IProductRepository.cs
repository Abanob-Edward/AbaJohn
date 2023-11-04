using AbaJohn.Models;
using AbaJohn.ViewModel;

namespace AbaJohn.Services.AdminRepository
{
    public interface IProductRepository
    {
        int create(productViewModel new_product_view);
        int Delete(int id);
        List<Product> get_all_product();

        int update(int id, productViewModel product);
        productViewModel get_product_byid(int id);

        int AddItemToProduct(ItemViewModel item);
       List<productViewModel> GetProductsByGender(string? GenderName);

    }
}