using CostApplication.Data;
using CostApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostApplication.Repositories
{
    public class CostRepository : ICostRepository
    {
        private readonly IAppDBContext appDbContext;

        public CostRepository(IAppDBContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public Cost Add(Cost cost) 
        {
            cost.CreatedOn = DateTime.UtcNow;
            cost.SensetiveData = "SensitiveData";

            appDbContext.Costs.Add(cost);
            appDbContext.SaveChanges();
            return cost;
        }

        public void Delete(int id)
        {
            var entry = appDbContext.Costs.FirstOrDefault(c => c.Id == id);
            if (entry != null)
            {
                appDbContext.Costs.Remove(entry);
                appDbContext.SaveChanges();
            }
        }

        public Cost Get(int id) => appDbContext.Costs.FirstOrDefault(c => c.Id == id);


        public List<Cost> GetAll() => appDbContext.Costs.ToList();


        public Cost Update(Cost cost)
        {
            var obj = appDbContext.Costs.FirstOrDefault(c => c.Id == cost.Id);
            if (obj != null)
            {
                obj.Amount = cost.Amount;
                obj.TypeOfCosts = cost.TypeOfCosts;
                obj.SensetiveData = cost.SensetiveData;
                obj.Date = cost.Date;
                obj.Description = cost.Description;

                obj.ModifiedOn = DateTime.UtcNow;

                appDbContext.Costs.Update(obj);
                appDbContext.SaveChanges();
            }
            return obj;
        }
    }
}
