using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Quan_ly_project.Data
{
    public class ProjectRepository
    {
        private readonly SQLiteConnection _connection;

        public ProjectRepository()
        {
            _connection = new DatabaseHelper().GetConnection();
        }

        // Thêm một dự án mới vào cơ sở dữ liệu
        public void AddProject(Models.Project project)
        {
            // Kiểm tra nếu dự án đã tồn tại (dựa trên tên dự án hoặc ID người tạo)
            var existingProject = _connection.Table<Models.Project>()
                                              .FirstOrDefault(p => p.Name == project.Name && p.CreatedBy == project.CreatedBy);
            if (existingProject == null)
            {
                _connection.Insert(project);  // Thêm mới nếu chưa tồn tại
            }
            else
            {
                // Có thể thêm thông báo khi dự án đã tồn tại
                Console.WriteLine("Dự án này đã tồn tại.");
            }
        }

        // Lấy dự án theo ProjectId
        public Models.Project GetProjectById(int projectId)
        {
            return _connection.Table<Models.Project>().FirstOrDefault(p => p.ProjectId == projectId);
        }

        // Lấy tất cả các dự án
        public List<Models.Project> GetAllProjects()
        {
            return _connection.Table<Models.Project>().ToList();
        }

        // Cập nhật thông tin dự án
        public void UpdateProject(Models.Project project)
        {
            _connection.Update(project);
        }

        // Xóa dự án theo ProjectId
        public void DeleteProject(int projectId)
        {
            // Trước khi xóa, có thể cần kiểm tra các bảng phụ như ProjectMember và ProjectTask có dữ liệu phụ thuộc không.
            // Bạn có thể xóa các bản ghi liên quan từ các bảng phụ trước khi xóa dự án chính (nếu có).

            _connection.Delete<Models.Project>(projectId);
        }
    }
}