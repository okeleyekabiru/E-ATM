
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using E_ATM.Data.Entity;
using E_ATM.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace E_ATM.Data.repo
{
 public class UserRepo:IUser
  {
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpAccessor;

    public UserRepo(DataContext context, IHttpContextAccessor httpAccessor)
    {
      _context = context;
      _httpAccessor = httpAccessor;
    }
    public async Task Register(User user)
    {
      await _context.AddAsync(user);
    }
    public async Task<bool> SaveChangesAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public async Task<User> GetUser(string name)
    {
     
      return await _context.Users.FirstOrDefaultAsync(n => n.FirstName.Equals(name));
     
    }

    public async Task<User> GetUserById(string id)
    {
      return await _context.Users.FindAsync(id);
    }

    public  string GetCurrentUser()
    {
      return _httpAccessor.HttpContext.User?.Claims?.FirstOrDefault(X => X.Type == ClaimTypes.NameIdentifier)?.Value;

    }

  }
}
