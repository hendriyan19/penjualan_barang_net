using System.Collections.Generic;
using System.Linq;
using API.Context;
using API.Models;
using API.Repository;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ItemsRepository : GeneralRepository<MyContext, items, long>
    {
        private readonly MyContext context;

        public ItemsRepository(MyContext context):base (context)
        {
            this.context = context;
        }

        public List<itemVM> GetAllItem()
        {
            var result = (from u in context.items
                          select new itemVM
                          {
                              ID = u.ID,
                              Item_Name=u.Item_Name,
                              Item_Price=u.Item_Price
                          }).ToList();
            return result;
        }
        public List<itemVM> GetItemById(long ID) {
        
            var result = (from u in context.items
                          where u.ID == ID
                          select new itemVM
                          {
                              ID = u.ID,
                              Item_Name = u.Item_Name,
                              Item_Price = u.Item_Price
                          }).ToList();
            return result;
        }

        public void AddItem(itemVM newItem)
        {
            var newItemEntity = new items
            {
                ID = newItem.ID,
                Item_Name = newItem.Item_Name,
                Item_Price = newItem.Item_Price
            };

            context.items.Add(newItemEntity);
            context.SaveChanges();
        }

        public void UpdateItem(itemVM updatedItem)
        {
            var existingItem = context.items.FirstOrDefault(u => u.ID == updatedItem.ID);

            if (existingItem != null)
            {
                existingItem.Item_Name = updatedItem.Item_Name;
                existingItem.Item_Price = updatedItem.Item_Price;

                context.SaveChanges();
            }
        }

        public void DeleteItem(long ID)
        {
            var existingItem = context.items.FirstOrDefault(u => u.ID == ID);

            if (existingItem != null)
            {
                context.items.Remove(existingItem);
                context.SaveChanges();
            }
        }
    }
}