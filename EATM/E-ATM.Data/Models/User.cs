using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace E_ATM.Data.Models
{
  public class User:IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public IEnumerable<Accounts> Accounts { get; set; }
  }
}
