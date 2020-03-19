using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_ATM.Data.Infrastructure
{
  public class AtmValidator
  {
    public static bool Validate(string atmNumber)
    {
      var sum = 0;
      bool returnValue = false;
      int tracker = 1;
      for (int i = atmNumber.Length; i-- > 0;)
      {
        var value = atmNumber[i].ToString();


        if (tracker % 2 != 0) sum += int.Parse(value);
        if (tracker % 2 == 0)
        {
          int backDigit = int.Parse(value) * 2;
          if (backDigit > 9)
          {
            int result = backDigit - 9;
            sum += result;
          }
          else
          {
            sum += backDigit;
          }
        }

        tracker++;
      }


      if (sum % 10 == 0 && (atmNumber.Length == 16) && atmNumber.StartsWith("53") ||
          atmNumber.StartsWith("55"))
        returnValue = true;

      else if ((sum % 10 == 0) && (atmNumber.Length == 16) || atmNumber.Length == 13 && atmNumber.StartsWith("4"))
        returnValue = true;


      return returnValue;
    }
  }
}
