using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HijoPortal.classes
{
    public class QuerySPClass
    {
        public static void InsertUpdateDirectMaterials(int ModuleType, int TransType, int PK, string HeaderDocNum, int TableIdentifier, string ExpenseCode, string ActivityCode, string OprUnit, string ItemCode, string ItemDescription, string ItemDescriptionAddl, string UOM, Double Qty, Double Cost, Double TotalCost)
        {
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            SqlCommand cmd = null;
            cn.Open();
            cmd = new SqlCommand("sp_InsertUpdateDM", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModuleType", ModuleType);
            cmd.Parameters.AddWithValue("@TransType", TransType);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@HeaderDocNum", HeaderDocNum);
            cmd.Parameters.AddWithValue("@TableIdentifier", TableIdentifier);
            cmd.Parameters.AddWithValue("@ExpenseCode", ExpenseCode);
            cmd.Parameters.AddWithValue("@ActivityCode", ActivityCode);
            cmd.Parameters.AddWithValue("@OprUnit", OprUnit);
            cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
            cmd.Parameters.AddWithValue("@ItemDescription", ItemDescription);
            cmd.Parameters.AddWithValue("@ItemDescriptionAddl", ItemDescriptionAddl);
            cmd.Parameters.AddWithValue("@UOM", UOM);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Cost", Cost);
            cmd.Parameters.AddWithValue("@TotalCost", TotalCost);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void InsertUpdateOperatingExpense()
        {

        }
    }
}