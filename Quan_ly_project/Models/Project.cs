using SQLite;

namespace Quan_ly_project.Models
{
    public class Project
    {
        [PrimaryKey, AutoIncrement]
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatedBy { get; set; } // UserId of the creator
        public DateTime CreatedAt { get; set; } // Timestamps when the project was created
    }
}