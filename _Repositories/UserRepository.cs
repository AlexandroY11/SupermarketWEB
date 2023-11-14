using Microsoft.Data.SqlClient;
using SupermarketWEB._Repositories;
using SupermarketWEB.Models;
using System.Data;

namespace SupermarketWEB._Repositories
{
    internal class UserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("SupermarketDB");
        }


        public IEnumerable<User> GetByValue(string value)
        {
            var userList = new List<User>();
            string userEmail = value;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM Users
                                    WHERE Email LIKE @email+ '%'
                                    ORDER By Id DESC";
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = userEmail;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User();
                        user.Email = reader["Email"].ToString();
                        user.Password = reader["Password"].ToString();
                        userList.Add(user);
                    }
                }
            }
            return userList;
        }
    }
}