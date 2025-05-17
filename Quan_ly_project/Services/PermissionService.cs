namespace Quan_ly_project.Services
{
    public class PermissionService
    {
        public bool HasAdminAccess(string role)
        {
            return role == "Admin";
        }

        public bool HasUserAccess(string role)
        {
            return role == "User";
        }
    }
}