using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using E_ATM.Data.Entity;
using E_ATM.Data.Models;
using E_ATM.Data.ViewModel;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Accounts> FindAccountById(Guid id)
    {
      return await _context.Account.Include(e => e.User).FirstAsync(r => r.Id == id);
    }

    public async Task<Accounts> GetAccountByNumber(string accountNumber)
    {
      return await _context.Account.FirstOrDefaultAsync(r => r.AccountNumber.Equals(accountNumber));
    }

    public async Task<Accounts> Withdraw(PaymentVm accounts) 
    {
      
      var account = await _context.Account.FirstOrDefaultAsync(a => a.AccountNumber.Equals(accounts.AccountNumber));
      if (account.Balance == 0) return null;
      if (account.Balance >= accounts.Amount)
      {
        account.Balance -= accounts.Amount;
        _context.Entry(account).State = EntityState.Modified;
        return account;
      }

      return null;
    }
  }
}
