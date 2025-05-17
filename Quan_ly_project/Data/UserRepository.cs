using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quan_ly_project.Data
{
    public class UserRepository
    {
        private readonly SQLiteConnection _connection;

        public UserRepository()
        {
            _connection = new DatabaseHelper().GetConnection();
        }

        // Add a new user
        public void AddUser(Models.User user)
        {
            // Ensure the user is not null
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            _connection.Insert(user);
        }

        // Retrieve user by ID
        public Models.User GetUserById(int userId)
        {
            // Ensure the ID is valid
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.", nameof(userId));
            }

            return _connection.Table<Models.User>().FirstOrDefault(u => u.UserId == userId);
        }

        // Retrieve user by username
        public Models.User GetUserByUsername(string username)
        {
            // Ensure the username is not null or empty
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));
            }

            return _connection.Table<Models.User>().FirstOrDefault(u => u.Username == username);
        }

        // Get all users
        public List<Models.User> GetAllUsers()
        {
            return _connection.Table<Models.User>().ToList();
        }

        // Update an existing user
        public void UpdateUser(Models.User user)
        {
            // Ensure the user exists before updating
            if (user == null || user.UserId <= 0)
            {
                throw new ArgumentException("User does not exist.", nameof(user));
            }

            _connection.Update(user);
        }

        // Delete a user by ID
        public void DeleteUser(int userId)
        {
            // Ensure the ID is valid
            if (userId <= 0)
            {
                throw new ArgumentException("Invalid user ID.", nameof(userId));
            }

            _connection.Delete<Models.User>(userId);
        }
    }
}
