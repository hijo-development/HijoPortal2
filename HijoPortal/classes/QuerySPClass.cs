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

        public static void InsertMRPList(string DocNumber, DateTime DateCreated, string EntityCode, string BUCode, int Month, int Year, int StatusKey, int CreatorKey, string LastModified)
        {
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            SqlCommand cmd = null;
            cn.Open();
            cmd = new SqlCommand("sp_InsertMRPList", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DocNumber", DocNumber);
            cmd.Parameters.AddWithValue("@DateCreated", DateCreated);
            cmd.Parameters.AddWithValue("@EntityCode", EntityCode);
            cmd.Parameters.AddWithValue("@BUCode", BUCode);
            cmd.Parameters.AddWithValue("@Month", Month);
            cmd.Parameters.AddWithValue("@Year", Year);
            cmd.Parameters.AddWithValue("@StatusKey", StatusKey);
            cmd.Parameters.AddWithValue("@CreatorKey", CreatorKey);
            cmd.Parameters.AddWithValue("@LastModified", LastModified);
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static int InsertUpdateDirectMaterials(int ModuleType, int TransType, int PK, string HeaderDocNum, int TableIdentifier, string ExpenseCode, string ActivityCode, string OprUnit, string ItemCode, string ItemDescription, string ItemDescriptionAddl, string UOM, Double Qty, Double Cost, Double TotalCost)
        {
            int retVal = 0;
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
            retVal = cmd.ExecuteNonQuery();
            cn.Close();
            return retVal;
        }

        public static int InsertUpdateOperatingExpense(int ModuleType, int TransType, int PK, string HeaderDocNum, int TableIdentifier, string ExpenseCode, string OprUnit, string ProCat, string ItemCode, string ItemDescription, string ItemDescriptionAddl, string UOM, Double Qty, Double Cost, Double TotalCost)
        {
            int retVal = 0;
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            SqlCommand cmd = null;
            cn.Open();
            cmd = new SqlCommand("sp_InsertUpdateOP", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModuleType", ModuleType);
            cmd.Parameters.AddWithValue("@TransType", TransType);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@HeaderDocNum", HeaderDocNum);
            cmd.Parameters.AddWithValue("@TableIdentifier", TableIdentifier);
            cmd.Parameters.AddWithValue("@ExpenseCode", ExpenseCode);
            cmd.Parameters.AddWithValue("@OprUnit", OprUnit);
            cmd.Parameters.AddWithValue("@ProcCat", ProCat);
            cmd.Parameters.AddWithValue("@ItemCode", ItemCode);
            cmd.Parameters.AddWithValue("@Description", ItemDescription);
            cmd.Parameters.AddWithValue("@DescriptionAddl", ItemDescriptionAddl);
            cmd.Parameters.AddWithValue("@UOM", UOM);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Cost", Cost);
            cmd.Parameters.AddWithValue("@TotalCost", TotalCost);
            retVal = cmd.ExecuteNonQuery();
            cn.Close();
            return retVal;
        }

        public static int InsertUpdateManPower(int ModuleType, int TransType, int PK, string HeaderDocNum, int TableIdentifier, string ActivityCode, string OprUnit, int ManPowerKey, string Description, string UOM, Double Qty, Double Cost, Double TotalCost)
        {
            int retVal = 0;

            //MRPClass.PrintString("doc num: " + HeaderDocNum.ToString());
            //MRPClass.PrintString("Act code: " + ActivityCode.ToString());

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            SqlCommand cmd = null;
            cn.Open();
            cmd = new SqlCommand("sp_InsertUpdateMP", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModuleType", ModuleType);
            cmd.Parameters.AddWithValue("@TransType", TransType);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@HeaderDocNum", HeaderDocNum);
            cmd.Parameters.AddWithValue("@TableIdentifier", TableIdentifier);
            cmd.Parameters.AddWithValue("@ActivityCode", ActivityCode);
            cmd.Parameters.AddWithValue("@OprUnit", OprUnit);
            cmd.Parameters.AddWithValue("@ManPowerTypeKey", ManPowerKey);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@UOM", UOM);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Cost", Cost);
            cmd.Parameters.AddWithValue("@TotalCost", TotalCost);
            retVal = cmd.ExecuteNonQuery();
            cn.Close();
            return retVal;
        }

        public static int InsertUpdateCapitalExpenditures(int ModuleType, int TransType, int PK, string HeaderDocNum, int TableIdentifier, string OprUnit, string ProdCat, string Description, string UOM, Double Qty, Double Cost, Double TotalCost)
        {
            //sp_InsertUpdateCE
            int retVal = 0;
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            SqlCommand cmd = null;
            cn.Open();
            cmd = new SqlCommand("sp_InsertUpdateCE", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ModuleType", ModuleType);
            cmd.Parameters.AddWithValue("@TransType", TransType);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@HeaderDocNum", HeaderDocNum);
            cmd.Parameters.AddWithValue("@TableIdentifier", TableIdentifier);
            cmd.Parameters.AddWithValue("@OprUnit", OprUnit);
            cmd.Parameters.AddWithValue("@ProdCat", ProdCat);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@UOM", UOM);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Cost", Cost);
            cmd.Parameters.AddWithValue("@TotalCost", TotalCost);
            retVal = cmd.ExecuteNonQuery();
            cn.Close();
            return retVal;
        }

        public static int InsertUpdateRevenueAssumptions(int TransType, int PK, string HeaderDocNum, string OprUnit, string ProductName, string FarmName, Double Prize, Double Volume, Double TotalPrize)
        {
            int retVal = 0;
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            SqlCommand cmd = null;
            cn.Open();
            cmd = new SqlCommand("sp_InsertUpdateREV", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TransType", TransType);
            cmd.Parameters.AddWithValue("@PK", PK);
            cmd.Parameters.AddWithValue("@HeaderDocNum", HeaderDocNum);
            cmd.Parameters.AddWithValue("@OprUnit", OprUnit);
            cmd.Parameters.AddWithValue("@ProductName", ProductName);
            cmd.Parameters.AddWithValue("@FarmName", FarmName);
            cmd.Parameters.AddWithValue("@Prize", Prize);
            cmd.Parameters.AddWithValue("@Volume", Volume);
            cmd.Parameters.AddWithValue("@TotalPrize", TotalPrize);
            retVal = cmd.ExecuteNonQuery();
            cn.Close();
            return retVal;
        }

        public static int MRP_Details_Latest_PK(string docNum, int Identifier)
        {
            int latestPK = 0;
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            string qry = "sp_MRPList_Details_Latest_PK '" + docNum + "', "+ Identifier + "";
            cn.Open();
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    latestPK = Convert.ToInt32(row["PK"]);
                }
            }
            dt.Clear();
            cn.Close();
            return latestPK;
        }
    }
}