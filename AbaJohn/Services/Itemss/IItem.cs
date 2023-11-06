using AbaJohn.Models;
using AbaJohn.ViewModel;

namespace AbaJohn.Services.Itemss
{
    public interface IItem
    {
        public List<Item> GetItemsForPrudect(int ProID);
    }
}
