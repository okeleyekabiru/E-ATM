using System;
using System.Collections.Generic;
using System.Text;

namespace E_ATM.Data.BusinessLogic
{
  public class   DateConverter
  {
    public static string CoverterToMonthAndYear(DateTime date)
    {
      var year = date.Year.ToString();
      var month = date.Month.ToString();
      return $" 0{month}/{year.Substring(year.Length - 2)}";
    }
  }
}
