﻿using CostApplication.Models;
using Microsoft.EntityFrameworkCore;


namespace CostApplication.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Cost> Costs { get; set; } // table in database
    }
}