using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Retail_Management_Console_Application.Database;

namespace Retail_Management_Console_Application.Dto;

public class AppDbContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(MyConnection.connection.ToString());
        }


    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
}
