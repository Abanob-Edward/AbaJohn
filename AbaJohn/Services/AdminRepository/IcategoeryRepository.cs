using AbaJohn.Models;
using AbaJohn.ViewModel;

namespace AbaJohn.Services.AdminRepository
{
    public interface IcategoeryRepository
    {
        int create(categoeryViewModel new_category_view);
        int Delete(int id);
        List<Category> get_all();
        Category get_category_id(int id);
        int update(int id, Category old_category);
    }
}