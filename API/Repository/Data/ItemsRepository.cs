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

            string query = "SELECT ID, Item_Name, Item_Price, System_Deleted FROM items " +
                "WHERE System_Deleted=0";
            var result = dbConnection.Query<itemVM>(query).AsList();

            return result;
        }

        public List<itemVM> GetItemById(long ID)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = "SELECT ID, Item_Name, Item_Price, System_Deleted FROM items " +
                "WHERE ID = @ID and System_Deleted=0";
            var result = dbConnection.Query<itemVM>(query, new { ID }).AsList();

            return result;
        }

        public void AddItem(itemVM newItem)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = "INSERT INTO items (Item_Name, Item_Price, System_Deleted) VALUES (@Item_Name, @Item_Price, 0)";
            dbConnection.Execute(query, newItem);
        }

        public void UpdateItem(itemVM updatedItem)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            string query = "UPDATE items SET Item_Name = 'ASUS TUF Gaming F13', Item_Price = 7240000 WHERE ID = 1 and System_Deleted=0";
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
