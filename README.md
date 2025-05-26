# 📱 ỨNG DỤNG QUẢN LÝ DỰ ÁN – MAUI + SQLite

Đây là một ứng dụng **quản lý dự án được xây dựng bằng **.NET MAUI** và sử dụng **SQLite** làm cơ sở dữ liệu cục bộ. Ứng dụng hỗ trợ người dùng cá nhân quản lý công việc và dự án một cách đơn giản, dễ sử dụng, không cần kết nối internet.

---

## 🎯 Tính năng chính

### 🔐 Xác thực tài khoản
- **Đăng ký / Đăng nhập** với email và mật khẩu
- Thông tin người dùng được lưu cục bộ trong SQLite

### 📁 Quản lý Dự án (Project)
- Tạo, cập nhật, xoá và xem chi tiết các dự án
- Mỗi dự án bao gồm: tên, mô tả, ngày bắt đầu, ngày kết thúc

### ✅ Quản lý Công việc (Task)
- Gắn công việc vào từng dự án
- Cập nhật trạng thái, tiến độ %, hạn hoàn thành
- Giao việc cho người dùng trong hệ thống

### 👥 Quản lý Thành viên (Project Members)
- Thêm thành viên tham gia dự án
- Lưu ngày tham gia

---

## 🛠 Công nghệ sử dụng

| Thành phần     | Công nghệ     |
|----------------|---------------|
| UI             | .NET MAUI     |
| Database       | SQLite        |
| Data Access    | Entity Framework Core (SQLite Provider) |
| Auth           | Local logic + Hash mật khẩu |
| Platform       | Windows (cross-platform) |

---

## ⚙️ Cài đặt và chạy ứng dụng

### Yêu cầu
- .NET 8 SDK (hoặc mới hơn)
- Visual Studio 2022 trở lên với workload **.NET MAUI**
- Android Emulator / iOS Simulator / Windows

### Hướng dẫn

```bash
# Clone repository
git clone https://github.com/mudotet/Quan_ly_project.git
cd Quan_ly_project

# Mở trong Visual Studio và nhấn "Run" để chạy ứng dụng
