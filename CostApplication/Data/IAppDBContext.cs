using CostApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CostApplication.Data
{
    public interface IAppDBContext
    {
        DbSet<Cost> Costs { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
    }
}