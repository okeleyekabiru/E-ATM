using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_ATM.Data.Infrastructure;
using E_ATM.Data.Models;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace E_ATM.Data.BusinessLogic
{
   public class JwtUserVerification:IJwtSecurity
    {
      public string JwtVerification(User user)
      {
        var claims = new List<Claim>
        {
          new Claim(JwtRegisteredClaimNames.NameId, user.Id)

        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my super long key"));
        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(7),
        SigningCredentials = creds
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);


    }
    }
}
