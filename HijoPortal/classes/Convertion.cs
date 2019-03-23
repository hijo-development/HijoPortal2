using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class Convertion
    {
        public static int MONTH_TO_INDEX(string name)
        {
            int index = Convert.ToDateTime("01-" + name + "-2011").Month;
            return index;
        }

        public static string INDEX_TO_MONTH(int iMonth)
        {
            DateTime now = DateTime.Now;
            string sMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(iMonth);
            return sMonth;
        }
    }
}