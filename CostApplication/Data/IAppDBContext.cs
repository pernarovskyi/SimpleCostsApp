using CostApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CostApplication.Data
{
    public interface IAppDBContext
    {
        DbSet<Cost> Costs { get; set; }

        int SaveChanges();
    }
}