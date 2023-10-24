using BusinessObject.Models;
using DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public Role GetRoleById(int roleId) => RoleDAO.Instance.GetRoleById(roleId);
    }
}
