using SQLite;

namespace Quan_ly_project.Models
{
    public class ProjectMember
    {
        [PrimaryKey, AutoIncrement]

        public int MemberId { get; set; } // Unique identifier for the project member
        public string UserName { get; set; } // Name of the user in the project
        public string Role { get; set; } // Role of the user in the project (e.g., Developer, Manager)

        public int ProjectId { get; set; } // Foreign Key from Project
        public int UserId { get; set; } // Foreign Key from User
        public DateTime JoinedAt { get; set; } // Timestamps when the user joined the project
    }
}