using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using E_ATM.Data.Entity;
using E_ATM.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace E_ATM.Data.Infrastructure
{
  public class Seed
  {
    
    private readonly AccountGenerator _accountGenerator;
    public Seed() { }
    public Seed( AccountGenerator accountGenerator)
    {
      
      _accountGenerator = accountGenerator;
    }

    public async Task
      SeedGenerator(DataContext _dataContext)
    {
      if (!await _dataContext.Users.AnyAsync())
      {
        var user = new List<User>
        {
          new User
          {
            
            Email = "Bob@gmail.com",
            FirstName = "bob",
            LastName = "corey",
            PhoneNumber = "09065879009"
          },
          new User
          {
          
            Email = "tim@gmail.com",
            FirstName = "tim",
            LastName = "corey",
            PhoneNumber = "09065879809"
          },
          new User
          {
           
            Email = "dan@gmail.com",
            FirstName = "dan",
            LastName = "corey",
            PhoneNumber = "09065989009"
          },
          new User
          {
            Email = "jim@gmail.com",
            FirstName = "jim",
            LastName = "danny",
            PhoneNumber = "09065879005"
          },
          new User
          {
           
            Email = "james@gmail.com",
            FirstName = "james",
            LastName = "danny",
            PhoneNumber = "08126136616"
          }
        };
       await _dataContext.AddRangeAsync(user);
    

      }

      if (!await  _dataContext.Account.AnyAsync())
      {
        var accounts = new List<Accounts>
        {
          new Accounts
          {
            AccountNumber = _accountGenerator.Get(),
            AccountType = "Savings",
            Balance = 0,
            DateCreated = DateTime.Now,
            IsActive = true
          },

          new Accounts
          {
            AccountNumber = _accountGenerator.Get(),
            AccountType = "Savings",
            Balance = 0,
            DateCreated = DateTime.Now,
            IsActive = true

          },
      
            new Accounts
            {
              AccountNumber = _accountGenerator.Get(),
              AccountType = "Savings",
              Balance = 0,
              DateCreated = DateTime.Now,
             IsActive = true

            }, 
              new Accounts
              {
                AccountNumber = _accountGenerator.Get(),
                AccountType = "Savings",
                Balance = 0,
                DateCreated = DateTime.Now,
                IsActive = true

              
            },
             
                new Accounts
                {
                  AccountNumber = _accountGenerator.Get(),
                  AccountType = "Savings",
                  Balance = 0,
                  DateCreated = DateTime.Now,
                 IsActive = true

                
              },
               

        };
        await _dataContext.AddRangeAsync(accounts);
      };
      var Atm = new List <Atm>
      {
        new Atm
        {
          AtmNumber = "4751763236699647",
          AtmPin = "3310",
          IsExpired = false,
          IssuedDate = DateTime.Now,
          ExpiryDate = DateTime.Now.AddYears(3),
          SecurityNumber = "564"

        }, new Atm
        {
          AtmNumber = "5399838383838381",
          AtmPin = "3310",
          IsExpired = false,
          IssuedDate = DateTime.Now,
          ExpiryDate = DateTime.Now.AddYears(3),
          SecurityNumber = "470"

        },
       new Atm
        {
          AtmNumber = "5531886652142950",
          AtmPin = "3310",
          IsExpired = false,
          IssuedDate = DateTime.Now,
          ExpiryDate = DateTime.Now.AddYears(3),
          SecurityNumber = "564"

        },
        new Atm
       {
         AtmNumber = "4187427415564246",
         AtmPin = "3310",
         IsExpired = false,
         IssuedDate = DateTime.Now,
         ExpiryDate = DateTime.Now.AddYears(3),
         SecurityNumber = "828",


       },
      new Atm
        {
          AtmNumber = "5438898014560229",
          AtmPin = "3310",
          IsExpired = false,
          IssuedDate = DateTime.Now,
          ExpiryDate = DateTime.Now.AddYears(3),
          SecurityNumber = "564"

        },
    
      };
      await _dataContext.AddRangeAsync(Atm);
      await _dataContext.SaveChangesAsync();
    }

     
    }


    }
  

