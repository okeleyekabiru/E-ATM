using System;
using E_ATM.Data.Models;

namespace E_ATM.Data
{
    public class Atm
    {
      public Guid Id { get; set; }
      public string AtmNumber { get; set; }
      public string AtmPin { get; set; }
      public string  SecurityNumber { get; set; }
      public DateTime IssuedDate  { get; set; }
      public DateTime ExpiryDate { get; set; }
      public bool IsExpired { get; set; }
      public  Accounts Accounts { get; set; }
    }
}
