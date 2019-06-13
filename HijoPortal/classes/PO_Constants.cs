using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class PO_Constants
    {
        public static string DocNumberTableName()
        {
            return "[dbo].[tbl_DocumentNumber]";
        }

        public static string POCreation_TableName()
        {
            return "[dbo].[tbl_POCreation]";
        }

        public static string POReference_TableName()
        {
            return "[dbo].[tbl_POCreation_Ref]";
        }
    }
}