using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User GetUserById(int userId);

        User GetUserByUserName(string userName);

        User GetUserByUserNameAndPassword(string userName, string password);

        void InsertUser(User user);

        void UpdateUser(User user);

        void DeleteUser(int userId);
    }
}
