using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using E_ATM.Data.Models;

namespace E_ATM.Data.repo
{
 public interface IAccount
 {
   Task Register(Accounts accounts);
   Task<bool> SaveChangesAsync();
   Task<Accounts> GetAccess(string cardtype );
 }
}
