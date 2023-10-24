using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class RoleDAO
    {
        private static RoleDAO instance = null;
        private static readonly object instanceLock = new object();

        public static RoleDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoleDAO();
                    }
                    return instance;
                }
            }
        }

        public Role GetRoleById(int roleId)
        {
            Role role = null;
            try
            {
                using var context = new CageShopManagementContext();
                role = context.Roles.FirstOrDefault(e => e.RoleId == roleId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return role;
        }
    }
}
