using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_ATM.Data;
using E_ATM.Data.Infrastructure;
using E_ATM.Data.Models;
using E_ATM.Data.repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EATM
{
    [Route("api/validate")]
    [ApiController]
    public class ValidateAtmController : ControllerBase
    {
      private readonly IUser _userContext;
      private readonly IAccount _accountContext;
      private readonly IAtm _atm;
      private readonly AccountGenerator _generator;

      public ValidateAtmController(IUser userContext, IAccount accountContext, IAtm atm,AccountGenerator generator)
      {
        _userContext = userContext;
        _accountContext = accountContext;
        _atm = atm;
        _generator = generator;
      }
    [HttpGet("get")]
      public ActionResult Get()
      {
        return Ok();
      }
    [HttpPost("user")]
    public async Task<ActionResult> RegisterUser(User user)
      {
        await _userContext.Register(user);
        if (await _userContext.SaveChangesAsync())
        {
          return Ok(new {User = "Registration successful" });
        }

        return BadRequest();
      }
      [HttpPost("account")]
      public async Task<ActionResult> RegisterAccount(Accounts accounts)
      {
        accounts.AccountNumber = _generator.Get();
        accounts.DateCreated = DateTime.Now;
        accounts.IsActive = true;
        accounts.User = await _userContext.GetUser("tim");
        await _accountContext.Register(accounts);
        if (await _accountContext.SaveChangesAsync())
        {
          return Ok(new { User = "Registration successful" });
        }

        return BadRequest();
      }
      [HttpPost("atm")]
      public async Task<ActionResult> RegisterAtm(Atm atm)
      {
       
        await _atm.Register(atm);
        if (await _atm.SaveChanges())
        {
          return Ok(new { User = "Registration successful" });
        }

        return BadRequest();
      }
    [HttpPost("card")]
      public ActionResult CardValidation(Atm atm)
      {

        var atmsValidate = AtmValidator.Validate(atm.AtmNumber);
        if (string.IsNullOrEmpty(atmsValidate) )
        {
          return BadRequest(new {AtmCard = "the atm pin you provide is invalid"});
        }
 
      return  Ok(new {CardValidation=atmsValidate});
      }

    }
}
