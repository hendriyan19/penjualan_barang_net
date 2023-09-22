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

            string query = @"SELECT oi.Item_Id, oi.Order_Id, oi.ID, o.Customer_Id, o.Order_Date, c.Customer_Email, c.Customer_Phone
                           FROM orders_Items oi
                           INNER JOIN orders o 
                           ON oi.Order_Id = o.ID
						   INNER JOIN customers c
						   ON o.Customer_Id = c.ID";

            var result = dbConnection.Query<orderVM>(query).ToList();

            return result;
        }

        public List<orderVM> GetOrderById(long ID)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = @"SELECT oi.Item_Id, oi.Order_Id, oi.ID, o.Customer_Id, o.Order_Date, c.Customer_Email, c.Customer_Phone
                           FROM orders_Items oi
                           INNER JOIN orders o 
                           ON oi.Order_Id = o.ID
						   INNER JOIN customers c
						   ON o.Customer_Id = c.ID
                             WHERE o.ID = @ID";

            var result = dbConnection.Query<orderVM>(query, new { ID }).ToList();

            return result;
        }

        public bool UpdateOrder(orderVM updatedOrder)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = @"UPDATE orders
                             SET Order_Date = @Order_Date, Customer_Id = @Customer_Id
                             WHERE ID = @ID";

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

        public bool AddOrder(orderVM newOrder)
        {
            try
            {
                using IDbConnection dbConnection = new SqlConnection(_connectionString);
                dbConnection.Open();

                string insertOrderItemQuery = "INSERT INTO orders_Items (Item_Id, Order_Id) VALUES (@Item_Id, @Order_Id)";
                dbConnection.Execute(insertOrderItemQuery, new { newOrder.Item_Id, newOrder.Order_Id });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
