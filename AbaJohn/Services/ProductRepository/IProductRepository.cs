using AbaJohn.Models;
using AbaJohn.ViewModel;

namespace AbaJohn
{
    public interface IProductRepository
    {
        int create(productViewModel new_product_view);
        int Delete(int id);
        List<Product> get_all_product();
        List<Product> GetSellerProducts(string UserName);

        int update(int id, productViewModel product);
        productViewModel get_product_byid(int id);

        int AddItemToProduct(ItemViewModel item);
        List<productViewModel> GetProductsByGender(string? GenderName);
        bool CheeckProductForSeller(int ProductID , string SellerName);

    }
}