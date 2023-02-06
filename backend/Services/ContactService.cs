using backend.Models;

namespace backend.Services
{
    public interface IContactsService 
    {
        IEnumerable<User> GetUsers();
        User? GetUser(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
    public class ContactService : IContactsService
    {
        private readonly string _fileName = "data.json";
        private User?[] _users;

        public ContactService()
        {
                
        }
        private void LoadContacts()
        {
            if (!File.Exists(_fileName)) 
            {
                _users = Array.Empty<User>();
                return;
            }
        }


        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public User? GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
