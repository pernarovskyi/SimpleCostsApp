using CostApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostApplication.Repositories
{
    public interface ICostRepository
    {
        List<Cost> GetAll();

        Cost Get(int id);

        Cost Add(Cost cost);

        Cost Update(Cost cost);

        void Delete(int id);
    }
}
