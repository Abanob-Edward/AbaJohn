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
        public List<Item> Get_all_items()
        {
            return _context.item.ToList();
        }

        public Item Get_item_byid(int id)
        {
            return _context.item.Include(c=>c.Product).FirstOrDefault(x => x.ID == id);
        }

        public int update_item(int id ,Item new_item)
        {
            var old_item = _context.item.FirstOrDefault(x => x.ID == id);

            old_item.Color = new_item.Color;
            old_item.size = new_item.size;
            old_item.Quantity = new_item.Quantity;
            old_item.productID = new_item.productID;

            int update_item = _context.SaveChanges();
            return update_item;

        }
        public int Delete(int id)
        {
            Item item = _context.item.FirstOrDefault(s => s.ID == id);
            _context.item.Remove(item);
            int delete = _context.SaveChanges();
            return delete;
        }


    }
}
