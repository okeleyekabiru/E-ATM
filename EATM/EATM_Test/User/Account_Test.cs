using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ATM.Data;
using E_ATM.Data.Infrastructure;
using E_ATM.Data.Models;
using E_ATM.Data.repo;
using E_ATM.Data.ViewModel;
using EATM;
using EATM.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EATM_Test.User
{
 public class Account_Test
 {
   private readonly Guid _id;
   private readonly Mock<IAccount> _mockAccount;
   private readonly ValidateAtmController validate;
   private readonly  Mock<IAtm> _mockAtm;
   private readonly  Mock<IUser> _mockUser;
   private  readonly Mock<IAccountGenerator> _mockAccountGen;
   private readonly Mock<IJwtSecurity> _mockAccountJwt;
   private readonly AtmVm _atmVm;
   private readonly Token _newToken;

   public Account_Test()
    {
      Accounts accounts = new Accounts
      {
        Id = Guid.NewGuid(),
        AccountNumber = "0037686809",
        AccountType = "savings",
        Balance = 0.0,
        DateCreated = DateTime.Now,
        IsActive = true,
      }; var atm = new Atm();
      atm.AtmNumber = "4751763236699647";
      atm.AtmPin = "3310";
      atm.ExpiryDate = DateTime.Now;
      atm.SecurityNumber = "564";
      _mockAccount = new Mock<IAccount>();
      _mockAtm = new Mock<IAtm>();
      _atmVm = new AtmVm();
      _atmVm.AtmNumber = "4751763236699647";
      _atmVm.AtmPin = "3310";
      _atmVm.ExpiryDate = "12/22";
      _atmVm.SecurityNumber = "564";
      _mockAtm.Setup(atm => atm.GetAtm(_atmVm)).ReturnsAsync(GetAtm());
      _mockUser = new Mock<IUser>();
      _mockUser.Setup(user => user.GetUser("toby")).ReturnsAsync(GetUser());
      _mockAccountGen = new Mock<IAccountGenerator>();
      _mockAccountGen.Setup(r => r.Get()).Returns("0090933344");
      _mockAccountJwt = new Mock<IJwtSecurity>();
      _newToken = new Token();
      _newToken.token = "738290hdjzksLKLCjchxkclzKJCxclz:KZJVnmx,ckl";
      _newToken.ExpiryDate = DateTime.Now;
      _mockAccountJwt.Setup(r => r.JwtVerification(GetUser()))
        .Returns(_newToken);
      _mockAccount.Setup(repo => repo.FindAccountById(Guid.NewGuid())).ReturnsAsync(accounts);

      validate = new ValidateAtmController(_mockUser.Object, _mockAccount.Object, _mockAtm.Object, _mockAccountGen.Object, _mockAccountJwt.Object);

    }

    private E_ATM.Data.Models.User GetUser()
   {
     return new E_ATM.Data.Models.User
     {
       Id = Guid.NewGuid().ToString(),
       FirstName = "toby",
       LastName = "damilola",
       Email = "kbdami@gmail.com",
       PhoneNumber = "063478390"
     };
   }

   private Atm GetAtm()
   {
     var atm = new Atm();
     atm.AtmNumber = "4751763236699647";
     atm.AtmPin = "3310";
     atm.ExpiryDate = DateTime.Now;
     atm.SecurityNumber = "564";
     return atm;
   }

   // [Fact]
   //  public async Task Get_Account()
   //  {
   //    var okObject = await validate.CardValidation(_atmVm);
   //    Assert.IsType<OkObjectResult>(okObject);
   //  }
   [Fact]
    public async Task RegisterAccount()
    {
      var mockobject = new Mock<IAccount>();
      mockobject.Setup(r => r.Register(It.IsAny<Accounts>())).Returns(Task.CompletedTask)
        .Verifiable();
      var contol = new ValidateAtmController(_mockUser.Object, mockobject.Object, _mockAtm.Object, _mockAccountGen.Object, _mockAccountJwt.Object);
     
       var okObject =  await validate.RegisterAccount(GetAccount());
       Assert.IsType<BadRequestResult>(okObject);


    }
    private Accounts GetAccount()
    {
      Accounts accounts = new Accounts
      {
        Id = Guid.NewGuid(),
        AccountNumber = "0037686809",
        AccountType = "savings",
        Balance = 0.0,
        DateCreated = DateTime.Now,
        IsActive = true,
      };
      return accounts;
    }
  }
}
