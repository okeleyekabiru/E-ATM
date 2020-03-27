using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using E_ATM.Data.Models;

namespace E_ATM.Data.repo
{
 public interface IUser
 {
   Task Register(User user);
   Task<bool> SaveChangesAsync();
   Task<User> GetUser(string name);
   Task<User> GetUserById(string id);
   string GetCurrentUser();
 }
}
