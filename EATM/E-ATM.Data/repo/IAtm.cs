using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace E_ATM.Data.repo
{
 public  interface IAtm
  {
    Task Register(Atm atm);
    Task<bool> SaveChanges();
    Task<Atm> GetAtm(string name);
  }
}
