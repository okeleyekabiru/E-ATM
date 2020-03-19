using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_ATM.Data;
using E_ATM.Data.Infrastructure;
using E_ATM.Data.Models;
using E_ATM.Data.repo;
using EATM.ViewModel;
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
      private readonly IJwtSecurity _jwtSecurity;

      public ValidateAtmController(IUser userContext, IAccount accountContext, IAtm atm,AccountGenerator generator,IJwtSecurity jwtSecurity)
      {
        _userContext = userContext;
        _accountContext = accountContext;
        _atm = atm;
        _generator = generator;
        _jwtSecurity = jwtSecurity;
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
      public async Task<ActionResult> CardValidation( AtmVm atm)
      {

        var atmsValidate = AtmValidator.Validate(atm.AtmNumber);
        if (!atmsValidate )
        {
          return  BadRequest(new {AtmCard = "the atm pin you provide is invalid"});
        }

        var AtmDetails = await _atm.GetAtm(atm);

        if (AtmDetails == null) return BadRequest(new { AtmCard = "the atm pin you provide is invalid" });
        var user = await _accountContext.FindAccountById(AtmDetails.Accounts.Id);
        var token =  _jwtSecurity.JwtVerification(user.User) ;

        return Ok(new {Token=token});

      }

    }
}
