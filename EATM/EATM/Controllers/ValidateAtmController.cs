using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using E_ATM.Data;
using E_ATM.Data.Infrastructure;
using E_ATM.Data.Models;
using E_ATM.Data.repo;
using E_ATM.Data.ViewModel;
using EATM.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IAccountGenerator _generator;
    private readonly IJwtSecurity _jwtSecurity;

    public ValidateAtmController(IUser userContext, IAccount accountContext, IAtm atm, IAccountGenerator generator,
      IJwtSecurity jwtSecurity)
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
        return Ok(new {User = "Registration successful"});
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
        return Ok(new {User = "Registration successful"});
      }

      return BadRequest();
    }

    [HttpPost("atm")]
    public async Task<ActionResult<AtmDto>> RegisterAtm(Atm atm)
    {

      await _atm.Register(atm);
      if (await _atm.SaveChanges())
      {
        return Ok(new {User = "Registration successful"});
      }

      return BadRequest();
    }

    [HttpPost("card")]
    [AllowAnonymous]
    public async Task<ActionResult> CardValidation(AtmVm atm)
    {


      var atmsValidate = AtmValidator.Validate(atm.AtmNumber);
      if (!atmsValidate)
      {
        return BadRequest(new AtmDto {Status = "failed", Token = "invalid"});
      }

      var AtmDetails = await _atm.GetAtm(atm);

      if (AtmDetails == null) return BadRequest(new {AtmCard = "the atm pin you provide is invalid"});
      var user = await _accountContext.FindAccountById(AtmDetails.Accounts.Id);
      var token = _jwtSecurity.JwtVerification(user.User);

      return Ok(token);

    }
  [Authorize(AuthenticationSchemes = "Bearer")]

    [HttpPost("dashboard")]
   
    
    public async Task<ActionResult> GetUserDetail( AtmVm card)
    {
      var userid = _userContext.GetCurrentUser();
      var currentUser = await _userContext.GetUserById(userid);
      if (currentUser == null) return NotFound();

      var atm = await _atm.GetAtmByNumber(card.AtmNumber);
      if (atm == null) return BadRequest();
      var returnedAtm = new AtmAndUserDto
      {
        AccountNumber = atm.Accounts.AccountNumber,
        FullName = $"{currentUser.FirstName} {currentUser.LastName} "
      };
      return Ok(returnedAtm);
    }
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost("balance")]
    public async Task<ActionResult> GetBalance(BalanceVm balance)
    {
      Accounts account = await _accountContext.GetAccountByNumber(balance.AccountNumber);
      if (account == null) return BadRequest();
      BalanceVm accountBalance = new BalanceVm();
      accountBalance.AccountNumber = account.AccountNumber;
      accountBalance.Balance = account.Balance;
      return Ok(accountBalance);
    }
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost("withdraw")]
    public async Task<ActionResult> WithDraw(PaymentVm payment)
    {
      var accounts = await _accountContext.Withdraw(payment);
      if (accounts == null) return BadRequest(new {Account = "Insufficient Balance"});
      if (await _accountContext.SaveChangesAsync()) return Ok(accounts);

      return NotFound(new {Account = "Invalid Transaction"});

    }
    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost("deposit")]
    public async Task<ActionResult> Deposit(PaymentVm payment)
    {
      var accounts = await _accountContext.Deposit(payment);
      if (accounts == null) return BadRequest(new { Account = "Invalid Transaction" });
      if (await _accountContext.SaveChangesAsync()) return Ok(accounts);

      return NotFound(new { Account = "Invalid Transaction" });

    }
  }

}
