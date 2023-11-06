using AbaJohn.Models;
using Microsoft.EntityFrameworkCore;

namespace AbaJohn.Services.Itemss
{
    public class ItemRepository : IItem
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Item> GetItemsForPrudect(int ProID)
        {
          return  _context.item.Where(i=>i.productID == ProID).Include(c=>c.Product).ToList();
        }
    }
}
