using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EATM.ViewModel;

namespace E_ATM.Data.repo
{
 public  interface IAtm
  {
    Task Register(Atm atm);
    Task<bool> SaveChanges();
    Task<Atm> GetAtm(AtmVm number);
  }
}
