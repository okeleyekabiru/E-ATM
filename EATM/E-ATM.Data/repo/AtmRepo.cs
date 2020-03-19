using System.Threading.Tasks;
using E_ATM.Data.BusinessLogic;
using E_ATM.Data.Entity;
using EATM.ViewModel;
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

    public async Task<Atm> GetAtm(AtmVm number)
    {
      var atm = await _context.AtmDigits.Include(e=> e.Accounts).ThenInclude(e=> e.User).FirstOrDefaultAsync(r => r.AtmNumber == number.AtmNumber);
      var atmvm = int.Parse(number.ExpiryDate.Substring(3,2));
      var atmexpiry = int.Parse(DateConverter.CoverterToMonthAndYear(atm.ExpiryDate).Substring(4, 2));
      if (atmvm < atmexpiry &&
          atm.AtmPin.Equals(number.AtmPin) && number.SecurityNumber.Equals(atm.SecurityNumber))
      {
        return atm;
      }

      return null;
      
    }
  }
}
