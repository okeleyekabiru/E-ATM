using System;
using System.Collections.Generic;
using System.Text;
using E_ATM.Data.Models;
using E_ATM.Data.ViewModel;

namespace E_ATM.Data.Infrastructure
{
 public interface IJwtSecurity
 {
   Token JwtVerification(User user);
 }
}
