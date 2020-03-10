using System;
using System.Collections.Generic;

namespace E_ATM.Data.Models
{
  public class User
  {
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string Email { get; set; }
    public IEnumerable<Accounts> Accounts { get; set; }
  }
}
