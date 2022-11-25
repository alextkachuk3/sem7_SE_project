using sem7_SE_project.Data;
using sem7_SE_project.Models;

namespace sem7_SE_project.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public User? GetUser(string username)
        {
            return _dbContext.Users!.FirstOrDefault(u => u.Login!.Equals(username));
        }
    }
}
