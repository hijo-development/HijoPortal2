using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class Constants
    {
        private const string 
            default_page = "default.aspx", 
            home_page = "home.aspx";
        private const string
            mop = "MRP",
            dm = "Direct Materials",
            op = "Operating Expense",
            man = "Manpower",
            ca = "Capital Expenditure",
            rev = "Revenue Assumptions",
            total_sum = "Total Summary",
            train_entity = "0101", hits_entity = "0303",
            prev_reqqty = "Requested Qty",
            prev_recqty = "Recommended Qty",
            prev_head_count = "Head Count";
        public static string MOP_string() { return mop; }
        public static string DM_string() { return dm; }
        public static string OP_string() { return op; }
        public static string MAN_string() { return man; }
        public static string CA_string() { return ca; }
        public static string REV_string() { return rev; }
        public static string SUMMARY_string() { return total_sum; }
        public static string TRAIN_CODE() { return train_entity; }
        public static string HITS_CODE() { return hits_entity; }
        public static string Prev_Rec_Qty() { return prev_recqty; }
        public static string Prev_Req_Qty() { return prev_reqqty; }
        public static string Prev_Head_Count() { return prev_head_count; }

        public static string DefaultPage()
        {
            return default_page;
        }

        public static string HomePage()
        {
            return home_page;
        }

        public static class Foo
        {
            public const string Bar = "hello world.";
        }
    }
}