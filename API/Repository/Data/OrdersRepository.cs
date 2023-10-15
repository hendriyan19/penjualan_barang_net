using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using API.Context;
using API.Models;
using API.ViewModel;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Repositories
{
    public class OrdersRepository
    {
        private readonly string _connectionString;

        public OrdersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<orderVM> GetAllOrder()
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = @"SELECT oi.Item_Id, oi.Order_Id, oi.ID, o.Customer_Id, i.Item_Name, o.Order_Date, c.Customer_Email, c.Customer_Phone
                           FROM orders_Items oi
                           INNER JOIN orders o 
                           ON oi.Order_Id = o.ID
						   INNER JOIN customers c
						   ON o.Customer_Id = c.ID
						   INNER JOIN items i
						   on oi.Item_Id = i.ID
						   where oi.System_Deleted=0";

            var result = dbConnection.Query<orderVM>(query).ToList();

            return result;
        }

        public List<orderVM> GetAllCustomer()
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = @"Select * FROM customers";

            var result = dbConnection.Query<orderVM>(query).ToList();

            return result;
        }

        public List<orderVM> GetAllDate()
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = @"Select * FROM orders";

            var result = dbConnection.Query<orderVM>(query).ToList();

            return result;
        }

        public List<orderVM> GetAllItem()
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = @"Select * FROM items where System_Deleted=0";

            var result = dbConnection.Query<orderVM>(query).ToList();

            return result;
        }

        public List<orderVM> GetOrderById(long ID)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = @"SELECT oi.Item_Id, oi.Order_Id, oi.ID, o.Customer_Id, i.Item_Name, o.Order_Date, c.Customer_Email, c.Customer_Phone
                           FROM orders_Items oi
                           INNER JOIN orders o 
                           ON oi.Order_Id = o.ID
						   INNER JOIN customers c
						   ON o.Customer_Id = c.ID
						   INNER JOIN items i
						   on oi.Item_Id = i.id
                             WHERE oi.ID = @ID
                           and oi.System_Deleted=0";

            var result = dbConnection.Query<orderVM>(query, new { ID }).ToList();

            return result;
        }

        public bool UpdateOrder(orderVM updatedOrder)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = @"UPDATE orders_Items
                             SET Item_Id = @Item_Id WHERE ID = @ID ";

            int rowsAffected = dbConnection.Execute(query, updatedOrder);

            return rowsAffected > 0;
        }

        public bool DeleteOrder(long ID)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = "DELETE FROM orders_Items WHERE ID = @ID";

            int rowsAffected = dbConnection.Execute(query, new { ID });

            return rowsAffected > 0;
        }

        public bool AddOrderItem(DateTime Order_Date, long Customer_Id, long Item_id)
        {
            try
            {
                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                dbConnection.Open();

                // Operasi INSERT pertama
                string insertOrderItemQuery = "INSERT INTO orders (Order_Date, Customer_Id) VALUES (@Order_Date, @Customer_Id); SELECT SCOPE_IDENTITY()";
                long Order_Id = dbConnection.ExecuteScalar<long>(insertOrderItemQuery, new { Order_Date, Customer_Id });
                bool System_Deleted = false;

                // Operasi INSERT kedua dengan Order_Id yang telah didapatkan
                string insertOrderItemQuery2 = "INSERT INTO orders_Items (Item_Id, Order_Id, System_Deleted) VALUES (@Item_Id, @Order_Id, @System_Deleted)";
                dbConnection.Execute(insertOrderItemQuery2, new { Item_id, Order_Id, System_Deleted });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<orderVM> GetAllDeleted()
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = @"SELECT oi.Item_Id, oi.Order_Id, oi.ID, o.Customer_Id, i.Item_Name, o.Order_Date, c.Customer_Email, c.Customer_Phone
                           FROM orders_Items oi
                           INNER JOIN orders o 
                           ON oi.Order_Id = o.ID
						   INNER JOIN customers c
						   ON o.Customer_Id = c.ID
						   INNER JOIN items i
						   on oi.Item_Id = i.ID
						   where oi.System_Deleted=1";
            var result = dbConnection.Query<orderVM>(query).AsList();

            return result;
        }

        public void DeleteItem(orderVM deletedOrder)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = "UPDATE orders_Items SET System_Deleted = 1 WHERE ID = @ID";
            dbConnection.Execute(query, deletedOrder);
        }
        public void RestoreItem(orderVM deletedOrder)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = "UPDATE orders_Items SET System_Deleted = 0 WHERE ID = @ID";
            dbConnection.Execute(query, deletedOrder);
        }

        // public bool AddOrderItems(long Order_Id, long Item_id)
        // {
        //     try
        //     {
        //         using IDbConnection dbConnection = new SqlConnection(_connectionString);
        //         dbConnection.Open();

        //         string insertOrderItemQuery = "INSERT INTO orders_Items (Item_Id, Order_Id) VALUES (@Item_Id, @Order_Id)";
        //         dbConnection.Execute(insertOrderItemQuery, new { Item_id, Order_Id });

        //         return true;
        //     }
        //     catch (Exception)
        //     {
        //         return false;
        //     }
        // }

        public List<orderVM> SearchOrder(string searchOrder)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = @"SELECT oi.Item_Id, oi.Order_Id, oi.ID, o.Customer_Id, i.Item_Name, o.Order_Date, c.Customer_Email, c.Customer_Phone
                           FROM orders_Items oi
                           INNER JOIN orders o 
                           ON oi.Order_Id = o.ID
						   INNER JOIN customers c
						   ON o.Customer_Id = c.ID
						   INNER JOIN items i
						   on oi.Item_Id = i.ID
						   WHERE (i.Item_Name LIKE @SearchOrder 
                           OR c.Customer_Email LIKE @SearchOrder 
                           OR c.Customer_Phone LIKE @SearchOrder) and oi.System_Deleted=0";

            var result = dbConnection.Query<orderVM>(query, new { SearchOrder = "%" + searchOrder + "%" }).AsList();

            return result;
        }
    }
}
