using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using E_ATM.Data.Entity;
using E_ATM.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_ATM.Data.repo
{
 public class UserRepo:IUser
  {
    private readonly DataContext _context;

    public UserRepo(DataContext context)
    {
      _context = context;
    }
    public async Task Register(User user)
    {
      await _context.AddAsync(user);
    }
    public async Task<bool> SaveChangesAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public async Task<User> GetUser(string name)
    {
      return await _context.User.FirstOrDefaultAsync(n => n.FirstName.Equals(name));
    }

  }
}
