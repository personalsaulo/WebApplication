﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication.Domain;

namespace WebApplication.Repository
{
    public class FriendsRepository
    {
        public IEnumerable<Friend> GetAllFriends()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\users\saulo\source\repos\WebApplication\WebApplication\App_Data\Database1.mdf;Integrated Security=True";

            using (var connection = new SqlConnection(connectionString))
            {
                var commandText = "Select * from Friends";
                var selectCommand = new SqlCommand(commandText, connection);

                var friends = new List<Friend>();

                try
                {
                    connection.Open();
                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var friend = new Friend();
                            friend.FriendId = (int)reader["FriendId"];
                            friend.FirstName = reader["FirstName"].ToString();
                            friend.LastName = reader["LastName"].ToString();
                            friend.BirthDate = (DateTime)reader["BirthDate"];

                            friends.Add(friend);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
                return friends;
            }
            
        }
    }
}