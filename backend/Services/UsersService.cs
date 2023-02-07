using backend.Models;
using System.Text.Json;

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
    public class UsersService : IUsersService
    {
        private readonly string _fileName = "data.json";
        private User[]? _users;

        public UsersService()
        {
            LoadUsers();
        }
        private void LoadUsers()
        {
            if (!File.Exists(_fileName)) 
            {
                _users = Array.Empty<User>();
                return;
            }
            if (_users == null) 
            {
                _users = Array.Empty<User>();
            }
            string jsonString = File.ReadAllText(_fileName);
            _users = JsonSerializer.Deserialize<User[]>(jsonString)!;
        }

        private void SaveContacts()
        {
            string jsonString = JsonSerializer.Serialize(_users);
            File.WriteAllText(_fileName, jsonString);
        }


        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (_users == null)
            {
                throw new InvalidOperationException("Users have not been loaded");
            }
            
            if (_users.Any(c => c.Id == user.Id))
            {
                throw new ArgumentException("User with this id already exists");
            }
            Array.Resize(ref _users, _users.Length + 1);
            _users[_users.Length - 1] = user;
            SaveContacts();
        }

      


        public void DeleteUser(User user)
        {
            if (user == null) 
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (_users == null) 
            {
                throw new InvalidOperationException("Users have not been loaded");
            }
            int index = Array.FindIndex(_users, c => c.Id == user.Id);
            if (index == -1) 
            {
                throw new ArgumentException("Contact with this id does not exist");
            }
            _users = _users.Where((val, idx) => idx != index).ToArray();
            SaveContacts();
            
        }
      

        
        public User GetUser(int id)
        {
            return _users?.FirstOrDefault(c => c.Id == id)!;
        }

        public IEnumerable<User> GetUsers()
        {
            if (_users == null) 
            {
                throw new InvalidOperationException("Users have not been loaded");
            }
            return _users.ToList();
        }

        public void UpdateUser(User user)
        {
            if (user == null) 
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (_users == null) 
            {
                throw new InvalidOperationException("Users have not been loaded");
            }
            int index = Array.FindIndex(_users, c => c.Id == user.Id);
            if (index == -1) 
            {
                throw new ArgumentException("User with id does not exist");
            }
            _users[index].FirstName = user.FirstName;
            _users[index].LastName = user.LastName;
            _users[index].Email = user.Email;
            _users[index].BillingAddress = user.BillingAddress;
            _users[index].PhysicalAddress = user.PhysicalAddress;
            _users[index].UpdatedAt = DateTime.Now;
            WriteToFile();
        }
        private void WriteToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_fileName, JsonSerializer.Serialize(_users,options));
        }
    }
}
