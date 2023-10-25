using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    public class BirdCageDAO
    {
        private static BirdCageDAO instance = null;
        private static readonly object instanceLock = new object();

        public static BirdCageDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BirdCageDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<BirdCage> GetCageList()
        {
            var cages = new List<BirdCage>();
            try
            {
                using var context = new CageShopManagementContext();
                cages = context.BirdCages.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cages;
        }

        public IEnumerable<BirdCage> GetAvailableReadyMadeCageList()
        {
            var readyMadeCages = new List<BirdCage>();
            try
            {
                using var context = new CageShopManagementContext();
                readyMadeCages = context.BirdCages.Where(e => e.CreatedByCustomer == null && e.IsAvailable).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return readyMadeCages;
        }

        public BirdCage GetCageById(int cageId)
        {
            BirdCage cage = null;
            try
            {
                using var context = new CageShopManagementContext();
                cage = context.BirdCages.FirstOrDefault(e => e.CageId == cageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cage;
        }

        public void AddNew(BirdCage cage)
        {
            try
            {
                using var context = new CageShopManagementContext();
                context.BirdCages.Add(cage);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(BirdCage cage)
        {
            try
            {
                BirdCage c = GetCageById(cage.CageId);
                if (c != null)
                {
                    using var context = new CageShopManagementContext();
                    context.BirdCages.Update(cage);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cage does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int cageId)
        {
            try
            {
                BirdCage cage = GetCageById(cageId);
                if (cage != null)
                {
                    using var context = new CageShopManagementContext();
                    context.BirdCages.Remove(cage);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cage does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
