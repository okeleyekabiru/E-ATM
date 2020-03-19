using System;
using System.Collections.Generic;
using System.Text;
using E_ATM.Data.Models;

namespace E_ATM.Data.Infrastructure
{
 public interface IJwtSecurity
 {
   string JwtVerification(User user);
 }
}
