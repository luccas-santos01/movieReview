using CineCritique.Models;

namespace CineCritique.Repository
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers();
        public User GetUserByEmail(string email);
        public User AddUser(User user);
        public User UpdateUser(User user);
        public void DeleteUser(int userId);
    }
}