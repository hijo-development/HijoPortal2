using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class MRP_Constants
    {
        public static string DirectMaterials_TableName()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List_DirectMaterials]";
        }

        public static string OperatingExpense_TableName()
        {
            return "[hijo_portal].[dbo].[tbl_MRP_List_OPEX]";
        }
    }
}