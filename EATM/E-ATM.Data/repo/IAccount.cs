using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using E_ATM.Data.Models;
using E_ATM.Data.ViewModel;

namespace E_ATM.Data.repo
{
 public interface IAccount
 {
   Task Register(Accounts accounts);
   Task<bool> SaveChangesAsync();
   Task<Accounts> GetAccess(string cardtype );
   Task<Accounts> FindAccountById(Guid id);
   Task<Accounts> GetAccountByNumber(string accountNumber);

   Task<Accounts> Withdraw(PaymentVm account);

 }
}
