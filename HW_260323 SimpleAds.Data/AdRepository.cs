using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_260323_SimpleAds.Data
{
    public class AdRepository
    {
        private string _connectionString;

        public AdRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Ad> GetAllAds()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM Ads
                                    ORDER BY DateCreated DESC";
            connection.Open();
            var ads = new List<Ad>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                ads.Add(new Ad
                {
                    Id = (int)reader["Id"],
                    Name = reader.GetOrNull<string>("Name"),
                    PhoneNumber = (string)reader["PhoneNumber"],
                    Description = (string)reader["Description"],
                    DateCreated = (DateTime)reader["DateCreated"]
                });
            }
            return ads;
        }

        public int AddAd(Ad ad)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Ads (Name, PhoneNumber, Description, DateCreated)
                                    VALUES (@name, @phoneNumber, @description, @dateCreated)
                                    SELECT SCOPE_IDENTITY()";

            command.Parameters.AddWithValue("@name", (object)ad.Name ?? DBNull.Value);
            command.Parameters.AddWithValue("@phoneNumber", ad.PhoneNumber);
            command.Parameters.AddWithValue("@description", ad.Description);
            command.Parameters.AddWithValue("@dateCreated", ad.DateCreated);
            connection.Open();
            return (int)(decimal)command.ExecuteScalar();
        }

        public void DeleteAd(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM Ads WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
