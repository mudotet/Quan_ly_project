using SQLite;
using System;
using System.IO;

namespace Quan_ly_project.Data
{
    public class DatabaseHelper
    {
        private static string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuanLyPhanMem.db3");

        // Lấy kết nối tới cơ sở dữ liệu
        public SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(databasePath);
            connection.Execute("PRAGMA foreign_keys = ON;");
            connection.CreateTable<Models.User>();
            connection.CreateTable<Models.Project>();
            connection.CreateTable<Models.ProjectTask>();
            connection.CreateTable<Models.ProjectMember>();
            return connection;
        }

        // Phương thức xóa cơ sở dữ liệu
        public static void DeleteDatabase()
        {
            if (File.Exists(databasePath))
            {
                try
                {
                    File.Delete(databasePath); // Xóa file cơ sở dữ liệu
                    System.Diagnostics.Debug.WriteLine("Cơ sở dữ liệu đã bị xóa.");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Lỗi khi xóa cơ sở dữ liệu: {ex.Message}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Không tìm thấy cơ sở dữ liệu để xóa.");
            }
        }
    }
}