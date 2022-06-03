using CostApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CostApplication.Data
{
    public class AppDBContext : DbContext, IAppDBContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Cost> Costs { get; set; }
    }
}
