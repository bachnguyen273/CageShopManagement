using BusinessObject.Models;
using DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void DeleteUser(int userId) => UserDAO.Instance.Remove(userId);

        public User GetUserById(int userId) => UserDAO.Instance.GetUserById(userId);

        public User GetUserByUserName(string userName) => UserDAO.Instance.GetUserByUserName(userName);

        public User GetUserByUserNameAndPassword(string userName, string password) => UserDAO.Instance.GetUserByUserNameAndPassword(userName, password);

        public IEnumerable<User> GetUsers() => UserDAO.Instance.GetUserList();

        public void InsertUser(User user) => UserDAO.Instance.AddNew(user);

        public void UpdateUser(User user) => UserDAO.Instance.Update(user);
    }
}
