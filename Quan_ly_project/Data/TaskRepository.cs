using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Quan_ly_project.Data
{
    public class TaskRepository
    {
        private readonly SQLiteConnection _connection;

        public TaskRepository()
        {
            _connection = new DatabaseHelper().GetConnection();
        }

        // Thêm nhiệm vụ mới
        public void AddTask(Models.ProjectTask task)
        {
            _connection.Insert(task);  // Insert vào database
        }

        // Lấy nhiệm vụ theo TaskId
        public Models.ProjectTask GetTaskById(int taskId)
        {
            return _connection.Table<Models.ProjectTask>().FirstOrDefault(t => t.TaskId == taskId);  // Lấy nhiệm vụ theo TaskId
        }

        // Lấy danh sách nhiệm vụ của một dự án
        public List<Models.ProjectTask> GetTasksByProjectId(int projectId)
        {
            return _connection.Table<Models.ProjectTask>().Where(t => t.ProjectId == projectId).ToList();  // Lấy tất cả nhiệm vụ của dự án
        }

        // Cập nhật nhiệm vụ
        public void UpdateTask(Models.ProjectTask task)
        {
            _connection.Update(task);  // Cập nhật thông tin nhiệm vụ trong cơ sở dữ liệu
        }

        // Xóa nhiệm vụ
        public void DeleteTask(int taskId)
        {
            _connection.Delete<Models.ProjectTask>(taskId);  // Xóa nhiệm vụ theo TaskId
        }
        public void DeleteAllTasks()
        {
            _connection.DeleteAll<Models.ProjectTask>();
        }

    }
}