using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IBirdCageRepository
    {
        IEnumerable<BirdCage> GetCages();

        BirdCage GetCageById(int cageId);

        void InsertCage(BirdCage cage);

        void UpdateCage(BirdCage cage);

        void DeleteCage(int cageId);
    }
}
