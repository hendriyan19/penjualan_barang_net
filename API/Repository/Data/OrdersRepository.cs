using System;
using System.Collections.Generic;
using System.Linq;
using API.Context;
using API.Models;
using API.Repository;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class OrdersRepository : GeneralRepository<MyContext, orders_item, long>
    {
        private readonly MyContext context;

        public OrdersRepository(MyContext context):base(context)
        {
            this.context = context;
        }

        public List<orderVM> GetAllOrder()
        {
            var result = (from u in context.orders_Items
                          join x in context.orders
                          on u.Order_Id equals x.ID
                          join y in context.items
                          on u.Item_Id equals y.ID
                          select new orderVM
                          {
                              Item_Id = u.Item_Id,
                              Order_Id = u.Order_Id,
                              ID = u.ID,
                              Customer_Id = x.Customer_Id,
                              Order_Date = x.Order_Date, // Menggunakan x.Order_Date karena mengambil tanggal pesanan dari tabel orders
                          }).ToList();
            return result;
        }

        public orderVM GetOrderById(long ID)
        {
            var result = (from u in context.orders_Items
                          join x in context.orders
                          on u.Order_Id equals x.ID
                          join y in context.items
                          on u.Item_Id equals y.ID
                          where u.ID == ID
                          select new orderVM
                          {
                              Item_Id = u.Item_Id,
                              Order_Id = u.Order_Id,
                              ID = u.ID,
                              Customer_Id = x.Customer_Id,
                              Order_Date = x.Order_Date, // Menggunakan x.Order_Date karena mengambil tanggal pesanan dari tabel orders
                          }).FirstOrDefault();
            return result;
        }

        public bool UpdateOrder(orderVM updatedOrder)
        {
            var existingOrder = context.orders.FirstOrDefault(u => u.ID == updatedOrder.ID);
           
            if (existingOrder != null )
            {
                // Mengupdate properti yang diinginkan
                existingOrder.Order_Date = updatedOrder.Order_Date;
                existingOrder.Customer_Id = updatedOrder.Customer_Id;
                context.SaveChanges();
                return true;
            }

            return false;
        }


        public bool DeleteOrder(long ID)
        {
            var existingOrder = context.orders_Items.FirstOrDefault(u => u.ID == ID);

            if (existingOrder != null)
            {
                context.orders_Items.Remove(existingOrder);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool AddOrder(orderVM newOrder)
        {
            try
            {
                var orderItem = new orders_item
                {
                    Item_Id = newOrder.Item_Id,
                    Order_Id = newOrder.Order_Id
                };

                context.orders_Items.Add(orderItem);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                // Penanganan kesalahan jika terjadi
                return false;
            }
        }



    }
}