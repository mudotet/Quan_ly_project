using SQLite;

namespace Quan_ly_project.Models
{
    public class ProjectTask
    {
        [PrimaryKey, AutoIncrement]
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int ProgressPercent { get; set; }
        public int AssigneeId { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}