using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();

        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<User> GetUserList()
        {
            var users = new List<User>();
            try
            {
                using var context = new CageShopManagementContext();
                users = context.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }

        public User GetUserById(int userId)
        {
            User user = null;
            try
            {
                using var context = new CageShopManagementContext();
                user = context.Users.FirstOrDefault(e => e.UserId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public User GetUserByUserName(string userName)
        {
            User user = null;
            try
            {
                using var context = new CageShopManagementContext();
                user = context.Users.FirstOrDefault(e => e.UserName == userName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public User GetUserByUserNameAndPassword(string userName, string password)
        {
            User user = null;
            try
            {
                using var context = new CageShopManagementContext();
                user = context.Users.FirstOrDefault(e => e.UserName == userName && e.Password == password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public void AddNew(User user)
        {
            try
            {
                using var context = new CageShopManagementContext();
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(User user)
        {
            try
            {
                User u = GetUserById(user.UserId);
                if (u != null)
                {
                    using var context = new CageShopManagementContext();
                    context.Users.Update(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("User does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int userId)
        {
            try
            {
                User user = GetUserById(userId);
                if (user != null)
                {
                    using var context = new CageShopManagementContext();
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("User does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
