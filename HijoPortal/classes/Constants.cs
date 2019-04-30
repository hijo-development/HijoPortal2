using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class Constants
    {
        private const string
            dm = "Direct Materials",
            op = "Operating Expense",
            man = "Manpower",
            ca = "Capital Expenditure",
            rev = "Revenue & Assumptions",
            train_entity = "0101",
            prev_reqqty = "Requested Qty",
            prev_recqty = "Recommended Qty",
            prev_head_count = "Head Count";
        public static string DM_string() { return dm; }
        public static string OP_string() { return op; }
        public static string MAN_string() { return man; }
        public static string CA_string() { return ca; }
        public static string REV_string() { return rev; }
        public static string TRAIN_CODE() { return train_entity; }
        public static string Prev_Rec_Qty() { return prev_recqty; }
        public static string Prev_Req_Qty() { return prev_reqqty; }
        public static string Prev_Head_Count() { return prev_head_count; }

        public static string DefaultPage()
        {
            return "default.aspx";
        }

        public static class Foo
        {
            public const string Bar = "hello world.";
        }
    }
}