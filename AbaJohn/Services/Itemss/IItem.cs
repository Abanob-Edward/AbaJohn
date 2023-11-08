using AbaJohn.Models;
using AbaJohn.ViewModel;

namespace AbaJohn.Services.Itemss
{
    public interface IItem
    {
         List<Item> GetItemsForPrudect(int ProID);
        List<Item> Get_all_items();
        Item Get_item_byid(int id);
        int update_item(int id, Item new_item);
        int Delete(int id);
    }
}
