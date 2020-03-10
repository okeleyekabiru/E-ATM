using System;
using System.Collections.Generic;
using System.Text;

namespace E_ATM.Data.Models
{
 public class Accounts
  {
    public Guid Id { get; set; }
    public string AccountNumber  { get; set; }
    public User User { get; set; }
    public string AccountType { get; set; }
    public double Balance { get; set; }
    public DateTime DateCreated { get; set; }
    public bool IsActive { get; set; }
   

  }
}
