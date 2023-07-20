using foodWasteMgt.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace foodWasteMgt.Data
{
    public class FoodDbContext : DbContext
    {
        public FoodDbContext(DbContextOptions<FoodDbContext> options)
           : base(options)
        {
        }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
