using DataAccess;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ItemService
    {
        private readonly Context context;

        public ItemService(Context _context)
        {
            context = _context;
        }

        public void CreatItem(string ItemName, int quantity)
        {
            var item = new Item
            {
                ItemName = ItemName,
                Quantity = quantity
            };
            context.Items.Add(item);
            context.SaveChanges();
        }
        public void IncreaseItemStock(int ItemId, int quantity)
        {
            var item = context.Items.Find(ItemId);
            if (item != null)
            {
                item.Quantity += quantity;
                context.SaveChanges();
            }
        }

        public void DecreaseItemStock(int ItemId, int quantity)
        {
            var item = context.Items.Find(ItemId);
            if (item != null)
            {
                item.Quantity -= quantity;
                context.SaveChanges();
            }
        }
        public void DeleteItem(int ItemId)
        {
            var item = context.Items.Find(ItemId);
            if (item != null)
            {
                context.Items.Remove(item);
                context.SaveChanges();
            }
            
        }
    }
}
