using CineCritique.Models;
using Microsoft.EntityFrameworkCore;

namespace CineCritique.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly IMovieReviewContext _repository;

        public UserRepository(IMovieReviewContext repository)
        {
            _repository = repository;
        }

        public IEnumerable<User> GetUsers()
        {
            return _repository.Users.Include(u => u.Reviews).ToList();
        }

        public User GetUserByEmail(string email)
        {
            return _repository.Users.First(u => u.Email == email);
        }

        public User AddUser(User user)
        {
            _repository.Users.Add(user);
            _repository.SaveChanges();
            return user;
        }

        public User UpdateUser(User user)
        {
            var existingUser = _repository.Users.Find(user.UserId)
            ?? throw new KeyNotFoundException("Usuário não encontrado.");

            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Name = user.Name;

            _repository.Users.Update(existingUser);
            _repository.SaveChanges();

            return existingUser;
        }

        public void DeleteUser(int usuarioId)
        {
            var user = _repository.Users.Find(usuarioId) ?? throw new KeyNotFoundException("Usuário não encontrado.");
            Console.WriteLine($"Tentando deletar o usuário com ID: {usuarioId}");
            _repository.Users.Remove(user);
            _repository.SaveChanges();
        }
    }
}