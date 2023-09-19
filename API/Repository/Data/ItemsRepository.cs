using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using API.Models;
using API.ViewModel;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Repositories
{
    public class ItemsRepository
    {
        private readonly string _connectionString;
        public ItemsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public List<itemVM> GetAllItem()
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = "SELECT ID, Item_Name, Item_Price FROM items";
            var result = dbConnection.Query<itemVM>(query).AsList();

            return result;
        }

        public List<itemVM> GetItemById(long ID)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = "SELECT ID, Item_Name, Item_Price FROM items WHERE ID = @ID";
            var result = dbConnection.Query<itemVM>(query, new { ID }).AsList();

            return result;
        }

        public void AddItem(itemVM newItem)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = "INSERT INTO items (Item_Name, Item_Price) VALUES (@Item_Name, @Item_Price)";
            dbConnection.Execute(query, newItem);
        }

        public void UpdateItem(itemVM updatedItem)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = "UPDATE items SET Item_Name = @Item_Name, Item_Price = @Item_Price WHERE ID = @ID";
            dbConnection.Execute(query, updatedItem);
        }

        public void DeleteItem(long ID)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = "DELETE FROM items WHERE ID = @ID";
            dbConnection.Execute(query, new { ID });
        }
    }
}
