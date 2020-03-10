using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using E_ATM.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace E_ATM.Data.repo
{
public  class AtmRepo:IAtm
  {
    private readonly DataContext _context;

    public AtmRepo(DataContext context)
    {
      _context = context;
    }
    public async Task Register(Atm atm)
    {
      await _context.AddAsync(atm);
    }

    public async Task<bool> SaveChanges()
    {
    return  await _context.SaveChangesAsync() > 0;
    }

    public async Task<Atm> GetAtm(string number)
    {
      return await _context.AtmDigits.FirstOrDefaultAsync(r => r.AtmNumber == number);
    }
  }
}
