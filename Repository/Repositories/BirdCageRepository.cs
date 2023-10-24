using BusinessObject.Models;
using DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BirdCageRepository : IBirdCageRepository
    {
        public void DeleteCage(int cageId) => BirdCageDAO.Instance.Remove(cageId);

        public BirdCage GetCageById(int cageId) => BirdCageDAO.Instance.GetCageById(cageId);

        public IEnumerable<BirdCage> GetCages() => BirdCageDAO.Instance.GetCageList();

        public void InsertCage(BirdCage cage) => BirdCageDAO.Instance.AddNew(cage);

        public void UpdateCage(BirdCage cage) => BirdCageDAO.Instance.Update(cage);
    }
}
