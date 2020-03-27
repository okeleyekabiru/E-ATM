using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.Configuration;
using E_ATM.Data.Entity;
using E_ATM.Data.Models;

namespace E_ATM.Data.Infrastructure
{
 public class AccountGenerator:IAccountGenerator
  {
    private readonly DataContext _dataContext;
    public AccountGenerator(DataContext dataContext)
    {
      _dataContext = dataContext;
    }
    public virtual  string Get()
    {
      var accountNumbers = _dataContext.Account.ToList();
      Random random =   new Random();
      var accountPrefix = "00";
      Accounts actualAccount;
      var decimalPlace = "";
      do
      {
        var generator = random.Next(1, 99999999);
         decimalPlace = accountPrefix + generator.ToString("D8");
           actualAccount = accountNumbers.FirstOrDefault(a => a.AccountNumber.Equals(decimalPlace));
      } while (actualAccount != null);

      return decimalPlace;
    }
  }
}
