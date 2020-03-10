using System;
using System.Collections.Generic;
using System.Text;
using E_ATM.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_ATM.Data.Entity
{
  public class DataContext : DbContext
  {
    public DbSet<User> User { get; set; }
    public DbSet<Accounts> Account { get; set; }
    public DbSet<Atm> AtmDigits { get; set; }

    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);


    }
  }
}
