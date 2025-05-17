using Quan_ly_project.Data;
using Quan_ly_project.Models;
using System.Linq;

namespace Quan_ly_project.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;

        public AuthService()
        {
            _userRepository = new UserRepository(); // Khởi tạo UserRepository để lấy người dùng từ cơ sở dữ liệu
        }

        // Kiểm tra tên người dùng và mật khẩu
        public bool AuthenticateUser(string username, string password)
        {
            // Lấy người dùng từ danh sách (hoặc cơ sở dữ liệu)
            var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.Username == username && u.Password == password);
            return user != null; // Trả về true nếu tìm thấy người dùng, false nếu không tìm thấy
        }

        // Lấy thông tin người dùng theo tên người dùng (nếu cần)
        public User GetUserByUsername(string username)
        {
            return _userRepository.GetAllUsers().FirstOrDefault(u => u.Username == username);
        }

        // Phương thức đăng xuất (nếu cần)
        public void Logout()
        {
            // Logic để xóa session hoặc token
        }
    }
}
