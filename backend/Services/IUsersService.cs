using backend.Models;

namespace backend.Services
{
    public interface IUsersService
    {
        IEnumerable<User> GetUsers();
        User? GetUser(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
