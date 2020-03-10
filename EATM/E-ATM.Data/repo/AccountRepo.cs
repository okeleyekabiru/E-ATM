using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using E_ATM.Data.Entity;
using E_ATM.Data.Models;

namespace E_ATM.Data.repo
{
 public class AccountRepo:IAccount
  {
    private readonly DataContext _context;

    public AccountRepo(DataContext context)
    {
      _context = context;
    }
    public async Task Register(Accounts accounts)
    {
      await _context.AddAsync(accounts);
    }

    public async Task<bool> SaveChangesAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public Task<Accounts> GetAccess(string cardtype)
    {
      throw new NotImplementedException();
    }
  }
}
