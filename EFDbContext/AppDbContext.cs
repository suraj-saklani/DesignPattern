using Microsoft.EntityFrameworkCore;
using System;
using workingProject.Model;

namespace workingProject.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
