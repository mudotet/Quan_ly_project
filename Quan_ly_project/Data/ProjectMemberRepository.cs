using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Quan_ly_project.Data
{
    public class ProjectMemberRepository
    {
        private readonly SQLiteConnection _connection;

        public ProjectMemberRepository()
        {
            _connection = new DatabaseHelper().GetConnection();
        }

        // Thêm thành viên mới vào dự án
        public void AddProjectMember(Models.ProjectMember projectMember)
        {
            _connection.Insert(projectMember);
        }

        // Lấy danh sách thành viên của dự án
        public List<Models.ProjectMember> GetMembersByProjectId(int projectId)
        {
            return _connection.Table<Models.ProjectMember>()
                              .Where(pm => pm.ProjectId == projectId)
                              .ToList();
        }

        // Cập nhật thông tin thành viên
        public void UpdateProjectMember(Models.ProjectMember projectMember)
        {
            _connection.Update(projectMember); // Cập nhật thành viên trong cơ sở dữ liệu
        }

        // Xóa thành viên khỏi dự án
        public void RemoveMember(int projectId, int userId)
        {
            var member = _connection.Table<Models.ProjectMember>()
                                     .FirstOrDefault(pm => pm.ProjectId == projectId && pm.UserId == userId);
            if (member != null)
            {
                _connection.Delete(member);
            }
        }
    }
}