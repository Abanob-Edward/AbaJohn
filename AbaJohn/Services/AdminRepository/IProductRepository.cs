using AbaJohn.Models;
using AbaJohn.ViewModel;

namespace AbaJohn.Services.AdminRepository
{
    public interface IProductRepository
    {
        int create(productViewModel new_product_view);
        int Delete(int id);
        List<Product> get_all_product();
        Product get_product_byid(int id);
        int update(int id, productViewModel old_product);
    }
}