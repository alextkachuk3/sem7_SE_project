using sem7_SE_project.Models;

namespace sem7_SE_project.Services.UserService
{
    public interface IUserService
    {
        public User? GetUser(string username);
    }
}
