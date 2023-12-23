using System.Security.Claims;
using Entities;
using Entities.Enums;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services.Helpers;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly ProgramDbContext _dbContext;

        public UserService(ProgramDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public void CreateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException();
            ValidationHelper.ModelValidation(user);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        // Read
        public User GetUserById(int userId)
        {
            return _dbContext.Users.FirstOrDefault(p => p.UserId == userId);
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        // Update
        public void UpdateUser(User updatedUser)
        {
            var existingUser = _dbContext.Users.Find(updatedUser.UserId);

            if (existingUser == null) return;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;
            existingUser.UserName = updatedUser.UserName;
            existingUser.UserType = updatedUser.UserType;

            _dbContext.SaveChanges();
        }

        // Delete
        public void DeleteUser(int userId)
        {
            var userToDelete = _dbContext.Users.Find(userId);

            if (userToDelete == null) return;
            _dbContext.Users.Remove(userToDelete);
            _dbContext.SaveChanges();
        }
    }
}
