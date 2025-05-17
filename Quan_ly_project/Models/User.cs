using SQLite;

namespace Quan_ly_project.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Role: Admin/User
        public DateTime CreatedAt { get; set; } // Timestamps when the user was created
    }
}