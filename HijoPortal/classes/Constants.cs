using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class Constants
    {
        private const string dm = "Direct Materials", op = "Operating Expense", man = "Manpower", ca = "Capital Expenditure", rev = "Revenue & Assumptions";
        public static string DM_string() { return dm; }
        public static string OP_string() { return op; }
        public static string MAN_string() { return man; }
        public static string CA_string() { return ca; }
        public static string REV_string() { return rev; }
    }
}