using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PediVax.BusinessObjects.Helpers
{
    public static class ParseDateHelper
    {
        public static DateTime ParseDate(string date)
        {
            DateTime parsedDate;
            if (DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            else
            {
                throw new ArgumentException("Date must be in dd/MM/yyyy format.");
            }
        }
    }
}
