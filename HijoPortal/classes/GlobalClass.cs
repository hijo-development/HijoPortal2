using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;

namespace HijoPortal.classes
{
    public class GlobalClass
    {
        public static string QueryError = "";
        public static string EmailError = "";
        public static string sEmailSignature = "";

        public static string WorkFlowSetupMsg = "";

        public static string UserImagePath = HttpContext.Current.Server.MapPath("~") + @"images\users\";

        public static object ContextType { get; private set; }

        public static string UpperCaseFirstLetter(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string Email_Redirect()
        {
            string sWebRoot = HttpContext.Current.Server.MapPath("~");
            string EmailRedirectPath = sWebRoot + @"config\email_redirect.txt";
            string emailRedirect = "";

            try
            {
                if (File.Exists(EmailRedirectPath))
                {
                    using (StreamReader sr = new StreamReader(EmailRedirectPath))
                    {
                        while (sr.Peek() >= 0)
                        {
                            emailRedirect = sr.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                emailRedirect = "";
            }

            return emailRedirect;
        }

        public static string SQLConnString()
        {
            string sConnString = "";
            string sWebRoot = HttpContext.Current.Server.MapPath("~");
            string TextPath = sWebRoot + @"config\cn.txt";

            try
            {
                if (File.Exists(TextPath))
                {
                    using (StreamReader sr = new StreamReader(TextPath))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string ss = sr.ReadLine();
                            string[] txtsplit = ss.Split('{');
                            string Status = txtsplit[0];
                            string ConnStr = txtsplit[1];
                            if (Convert.ToInt32(Status) == 1)
                            {
                                string[] txtConfig = ConnStr.Split('|');
                                string server = txtConfig[0];
                                string database = txtConfig[1];
                                string userid = txtConfig[2];
                                string password = txtConfig[3];
                                sConnString = "Data Source=" + server + "; Initial Catalog=" + database + "; User ID=" + userid + ";Password=" + password + "";
                                goto sConnString;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                sConnString = e.ToString();
            }

            sConnString:
            return sConnString;
        }

        public static string SQLConnStringHRIS()
        {
            string sConnString = "";
            string sWebRoot = HttpContext.Current.Server.MapPath("~");
            string TextPath = sWebRoot + @"config\cn.txt";

            try
            {
                if (File.Exists(TextPath))
                {
                    using (StreamReader sr = new StreamReader(TextPath))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string ss = sr.ReadLine();
                            string[] txtsplit = ss.Split('{');
                            string Status = txtsplit[0];
                            string ConnStr = txtsplit[1];
                            if (Convert.ToInt32(Status) == 2)
                            {
                                string[] txtConfig = ConnStr.Split('|');
                                string server = txtConfig[0];
                                string database = txtConfig[1];
                                string userid = txtConfig[2];
                                string password = txtConfig[3];
                                sConnString = "Data Source=" + server + "; Initial Catalog=" + database + "; User ID=" + userid + ";Password=" + password + "";
                                goto sConnString;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                sConnString = e.ToString();
            }

            sConnString:
            return sConnString;
        }

        public static string FormatSQL(string sText)
        {
            string _sText = sText;
            _sText.Replace("'", "''");
            return _sText;
        }

        public static bool IsEmailValid(string Email)
        {
            try
            {
                var addr = new MailAddress(Email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsMailSent(string sEmailTo, string sEmailSubject, string sEmailBody, string AttachedFile = "")
        {

            bool isSent = false;
            EmailError = "";

            string sEmailFrom = "", sEmailSMTPPW = "", sEmailSMTP = "";
            int sEmailSMTPPort = 0;

            //--- Email Config
            string sWebRoot = HttpContext.Current.Server.MapPath("~");
            string EmailPath = sWebRoot + @"config\email.txt";
            if (File.Exists(EmailPath))
            {
                using (StreamReader sr = new StreamReader(EmailPath))
                {
                    while (sr.Peek() >= 0)
                    {
                        string ss = sr.ReadLine();
                        string[] txtsplit = ss.Split('{');
                        string Status = txtsplit[0];
                        string ConnStr = txtsplit[1];
                        if (Convert.ToInt32(Status) == 1)
                        {
                            string[] txtConfig = ConnStr.Split('|');
                            sEmailFrom = txtConfig[0];
                            sEmailSMTPPW = txtConfig[1];
                            sEmailSMTP = txtConfig[2];
                            sEmailSMTPPort = Convert.ToInt32(txtConfig[3]);
                        }
                    }
                }
            }
            //--- end of Email Config


            MailMessage mailMsg = new MailMessage();
            mailMsg.To.Add(sEmailTo);
            mailMsg.From = new MailAddress(sEmailFrom);
            mailMsg.Subject = sEmailSubject;

            sEmailSignature = sWebRoot + @"images\EmailSig.jpg";

            if (File.Exists(sEmailSignature) == true)
            {
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(sEmailBody + "<img src=cid:EmailSig>", null, "text/html");
                LinkedResource footer = new LinkedResource(sEmailSignature);
                footer.ContentId = "EmailSig";
                htmlView.LinkedResources.Add(footer);
                mailMsg.AlternateViews.Add(htmlView);
            }
            else
            {
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(sEmailBody, null, "text/html");
                mailMsg.AlternateViews.Add(htmlView);
            }

            SmtpClient client = new SmtpClient(sEmailSMTP, sEmailSMTPPort);
            System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential(sEmailFrom, sEmailSMTPPW);
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            if (AttachedFile.ToString().Trim() != "")
            {
                if (File.Exists(AttachedFile) == true)
                {
                    Attachment attachment;
                    attachment = new Attachment(AttachedFile);
                    mailMsg.Attachments.Add(attachment);
                    try
                    {
                        client.Send(mailMsg);
                        isSent = true;
                        attachment.Dispose();
                    }
                    catch (Exception ex)
                    {
                        isSent = false;
                        EmailError = ex.Message.ToString();
                        attachment.Dispose();
                    }
                }
                else
                {
                    try
                    {
                        client.Send(mailMsg);
                        isSent = true;
                    }
                    catch (Exception ex)
                    {
                        isSent = false;
                        EmailError = ex.Message.ToString();
                    }
                }

            }
            else
            {
                try
                {
                    client.Send(mailMsg);
                    isSent = true;
                }
                catch (Exception ex)
                {
                    isSent = false;
                    EmailError = ex.Message.ToString();
                }
            }

            return isSent;
        }

        public static bool IsHaveDomainAccount(string DomainAccount)
        {
            bool HaveDomain = false;

            string groupName = "Domain Users";
            string domainName = "hijo.local";

            //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domainName);
            //GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctx, IdentityType.SamAccountName, groupName);

            return HaveDomain;
        }

        public static DataTable EntityTable()
        {

            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("ID", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            string qry = "SELECT * FROM [dbo].[vw_AXEntityTable]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ID"] = row["ID"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static string EntityCodeDescription(string code)
        {
            string query = "SELECT NAME FROM [dbo].[vw_AXEntityTable] where ID = '" + code + "'";

            string actcodeVal = "";
            SqlConnection conn = new SqlConnection(SQLConnString());
            conn.Open();
            SqlCommand queryCMD = new SqlCommand(query, conn);
            SqlDataReader reader = queryCMD.ExecuteReader();
            while (reader.Read())
            {
                actcodeVal = reader[0].ToString();
            }
            reader.Close();
            conn.Close();
            return actcodeVal;
        }

        public static DataTable BUSSUTable()
        {

            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("ID", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            string qry = "SELECT * FROM [dbo].[vw_AXOperatingUnitTable]";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ID"] = row["OMOPERATINGUNITNUMBER"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable EntBUSSUTable(string entCode)
        {

            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("ID", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            string qry = "SELECT * FROM [dbo].[vw_AXOperatingUnitTable] WHERE (entity = '" + entCode + "')";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ID"] = row["OMOPERATINGUNITNUMBER"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable ProCategoryTable()
        {

            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("ID", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            
            //string qry = "SELECT [NAME],[DESCRIPTION] FROM [dbo].[vw_AXProdCategory] ORDER BY NAME ASC";
            string qry = "SELECT [NAME],[DESCRIPTION] FROM [dbo].[vw_AXProdCategory_Group] ORDER BY NAME ASC";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ID"] = row["NAME"].ToString();
                    dtRow["NAME"] = row["DESCRIPTION"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable PositionNameTable()
        {

            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("ID", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            string qry = "SELECT [PK] AS NAME, [PositionName] AS DESCRIPTION " +
                         " FROM [dbo].[tbl_System_Approval_Position] ORDER BY PositionName ASC";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ID"] = row["NAME"].ToString();
                    dtRow["NAME"] = row["DESCRIPTION"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static string BUCodeDescription(string code)
        {
            string query = "SELECT NAME FROM [dbo].[vw_AXOperatingUnitTable] where OMOPERATINGUNITNUMBER = '" + code + "'";

            string actcodeVal = "";
            SqlConnection conn = new SqlConnection(SQLConnString());
            conn.Open();
            SqlCommand queryCMD = new SqlCommand(query, conn);
            SqlDataReader reader = queryCMD.ExecuteReader();
            while (reader.Read())
            {
                actcodeVal = reader[0].ToString();
            }
            reader.Close();
            conn.Close();
            return actcodeVal;
        }

        public static DataTable BUDeptHeadTable()
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("Ctrl", typeof(string));
                dtTable.Columns.Add("EffectDate", typeof(string));
                dtTable.Columns.Add("EntityCode", typeof(string));
                dtTable.Columns.Add("EntityCodeDesc", typeof(string));
                dtTable.Columns.Add("BUDeptCode", typeof(string));
                dtTable.Columns.Add("BUDeptCodeDesc", typeof(string));
                dtTable.Columns.Add("UserKey", typeof(string));
                dtTable.Columns.Add("UserCompleteName", typeof(string));
                dtTable.Columns.Add("StatusKey", typeof(string));
                dtTable.Columns.Add("StatusDesc", typeof(string));
            }

            string qry = "SELECT dbo.tbl_System_BUDeptHead.PK, dbo.tbl_System_BUDeptHead.Ctrl, " +
                         " dbo.tbl_System_BUDeptHead.EffectDate, dbo.tbl_System_BUDeptHead.EntityCode, " +
                         " dbo.vw_AXEntityTable.NAME AS EntityCodeDesc, dbo.tbl_System_BUDeptHead.BUDeptCode, " +
                         " dbo.vw_AXOperatingUnitTable.NAME AS BUDeptCodeDesc, dbo.tbl_System_BUDeptHead.UserKey, " +
                         " dbo.tbl_Users.Lastname, dbo.tbl_Users.Firstname, dbo.tbl_System_BUDeptHead.StatusKey " +
                         " FROM dbo.tbl_System_BUDeptHead LEFT OUTER JOIN " +
                         " dbo.tbl_Users ON dbo.tbl_System_BUDeptHead.UserKey = dbo.tbl_Users.PK LEFT OUTER JOIN " +
                         " dbo.vw_AXOperatingUnitTable ON dbo.tbl_System_BUDeptHead.BUDeptCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN " +
                         " dbo.vw_AXEntityTable ON dbo.tbl_System_BUDeptHead.EntityCode = dbo.vw_AXEntityTable.ID";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["Ctrl"] = row["Ctrl"].ToString();
                    dtRow["EffectDate"] = Convert.ToDateTime(row["EffectDate"]).ToString("MM/dd/yyyy");
                    dtRow["EntityCode"] = row["EntityCode"].ToString();
                    dtRow["EntityCodeDesc"] = row["EntityCodeDesc"].ToString();
                    dtRow["BUDeptCode"] = row["BUDeptCode"].ToString();
                    dtRow["BUDeptCodeDesc"] = row["BUDeptCodeDesc"].ToString();
                    dtRow["UserKey"] = row["UserKey"].ToString();
                    dtRow["UserCompleteName"] = EncryptionClass.Decrypt(row["Lastname"].ToString()) + ",  " + EncryptionClass.Decrypt(row["Firstname"].ToString());
                    dtRow["StatusKey"] = row["StatusKey"].ToString();
                    if (Convert.ToInt32(row["StatusKey"]) == 1)
                    {
                        dtRow["StatusDesc"] = "Active";
                    }
                    else
                    {
                        dtRow["StatusDesc"] = "Inactive";
                    }
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static string GetControl_DocNum(string sModuleName, DateTime dEffectDate)
        {
            string sDocNum = "", qry = "";

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();
            switch (sModuleName)
            {
                case "BU_Dept_Head":
                    {
                        qry = "SELECT TOP (1) Ctrl " +
                              " FROM tbl_System_BUDeptHead " +
                              " WHERE (Year(EffectDate) = " + dEffectDate.Year + ") " +
                              " ORDER BY Ctrl DESC";
                        break;
                    }
                case "SCM_Head":
                    {
                        qry = "SELECT TOP (1) Ctrl " +
                              " FROM dbo.tbl_System_SCMHead " +
                              " WHERE (Year(EffectDate) = " + dEffectDate.Year + ") " +
                              " ORDER BY Ctrl DESC";
                        break;
                    }
                case "SCM_InventAnal":
                    {
                        qry = "SELECT TOP (1) Ctrl " +
                              " FROM dbo.tbl_System_SCMInventoryAnalyst " +
                              " WHERE (Year(EffectDate) = " + dEffectDate.Year + ") " +
                              " ORDER BY Ctrl DESC";
                        break;
                    }
                case "SCM_ProcOff":
                    {
                        qry = "SELECT TOP (1) Ctrl " +
                              " FROM dbo.tbl_System_SCMProcurementOfficer " +
                              " WHERE (Year(EffectDate) = " + dEffectDate.Year + ") " +
                              " ORDER BY Ctrl DESC";
                        break;
                    }
                case "Finance_Head":
                    {
                        qry = "SELECT TOP (1) Ctrl " +
                              " FROM dbo.tbl_System_FinanceHead " +
                              " WHERE (Year(EffectDate) = " + dEffectDate.Year + ") " +
                              " ORDER BY Ctrl DESC";
                        break;
                    }
                case "Finance_Budget":
                    {
                        qry = "SELECT TOP (1) Ctrl " +
                              " FROM dbo.tbl_System_FinanceBudget " +
                              " WHERE (Year(EffectDate) = " + dEffectDate.Year + ") " +
                              " ORDER BY Ctrl DESC";
                        break;
                    }
                case "Finance_Inventory_Officer":
                    {
                        qry = "SELECT TOP (1) Ctrl " +
                              " FROM dbo.tbl_System_FinanceInventoryOfficer " +
                              " WHERE (Year(EffectDate) = " + dEffectDate.Year + ") " +
                              " ORDER BY Ctrl DESC";
                        break;
                    }
                case "Executive":
                    {
                        qry = "SELECT TOP (1) Ctrl " +
                              " FROM dbo.tbl_System_Executive " +
                              " WHERE (Year(EffectDate) = " + dEffectDate.Year + ") " +
                              " ORDER BY Ctrl DESC";
                        break;
                    }
                case "DataFlow":
                    {
                        qry = "SELECT TOP (1) Ctrl " +
                              " FROM dbo.tbl_System_MOP_DataFlow " +
                              " WHERE (Year(EffectDate) = " + dEffectDate.Year + ") " +
                              " ORDER BY Ctrl DESC";
                        break;
                    }
                case "Approval":
                    {
                        qry = "SELECT TOP (1) Ctrl " +
                              " FROM dbo.tbl_System_Approval " +
                              " WHERE (Year(EffectDate) = " + dEffectDate.Year + ") " +
                              " ORDER BY Ctrl DESC";
                        break;
                    }
            }

            if (qry == "") { return sDocNum; }
            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    sDocNum = Convert.ToString(Convert.ToDouble(row["Ctrl"]) + 1);
                }
            }
            else
            {
                sDocNum = dEffectDate.ToString("yyyy") + "0000";
            }
            dt.Clear();
            cn.Close();
            return sDocNum;
        }

        public static bool IsAdmin(int usrKey)
        {
            bool isAdmin = false;
            string qry = "";
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dtable = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            cn.Open();
            qry = "SELECT PK, UserLevelKey, Active " +
                   " FROM dbo.tbl_Users " +
                   " WHERE(PK = "+ usrKey + ") " +
                   " AND(UserLevelKey = 1) " +
                   " AND(Active = 1)";
            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                isAdmin = true;
            }
            dtable.Clear();
            cn.Close();
            return isAdmin;
        }

        public static bool IsAllowed(int usrKey, string sModuleName, DateTime dtEffect, string sEntCode = "", string sBUCode = "", string ProcCat ="")
        {
            bool isAllowed = false;

            string qry = "";
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dtable = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();
            switch (sModuleName)
            {
                case "MOPSCMLead":
                    {
                        //MRPClass.PrintString("MOPSCMLead");
                        qry = "SELECT TOP (1) UserKey, StatusKey, EffectDate " +
                              " FROM dbo.tbl_System_SCMHead " +
                              " WHERE(UserKey = "+ usrKey + ") " +
                              " AND(StatusKey = 1) " +
                              " AND(EffectDate <= '"+ dtEffect + "') " +
                              " ORDER BY EffectDate";
                        cmd = new SqlCommand(qry);
                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtable);
                        //MRPClass.PrintString("MOPSCMLead - " + dtable.Rows.Count.ToString());
                        if (dtable.Rows.Count > 0)
                        {
                            isAllowed = true;
                        }
                        dtable.Clear();
                        break;
                    }
                case "MOPFinanceLead":
                    {
                        qry = "SELECT TOP (1) UserKey, StatusKey, EffectDate " +
                              " FROM dbo.tbl_System_FinanceHead " +
                              " WHERE(UserKey = " + usrKey + ") " +
                              " AND(StatusKey = 1) " +
                              " AND(EffectDate <= '" + dtEffect + "') " +
                              " ORDER BY EffectDate";
                        cmd = new SqlCommand(qry);
                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtable);
                        if (dtable.Rows.Count > 0)
                        {
                            isAllowed = true;
                        }
                        dtable.Clear();
                        break;
                    }
                case "MOPExecutive":
                    {
                        qry = "SELECT UserKey, StatusKey " +
                              " FROM dbo.tbl_System_Executive " +
                              " WHERE(UserKey = "+ usrKey + ") " +
                              " AND(StatusKey = 1)";
                        cmd = new SqlCommand(qry);
                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtable);
                        if (dtable.Rows.Count > 0)
                        {
                            isAllowed = true;
                        }
                        dtable.Clear();
                        break;
                    }
                case "MOPBULead":
                    {
                        qry = "SELECT TOP (1) UserKey " +
                            " FROM dbo.tbl_System_BUDeptHead " +
                            " WHERE(StatusKey = 1) " +
                            " AND(EntityCode = '"+ sEntCode + "') " +
                            " AND(BUDeptCode = '"+ sBUCode + "') " +
                            " AND(EffectDate <= '"+ dtEffect + "') " +
                            " AND(UserKey = " + usrKey + ") " +
                            " ORDER BY EffectDate DESC";
                        cmd = new SqlCommand(qry);
                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtable);
                        if (dtable.Rows.Count > 0)
                        {
                            isAllowed = true;
                        }
                        dtable.Clear();
                        break;
                    }
                case "MOPInventoryAnalyst":
                    {
                        qry = "SELECT dbo.tbl_System_SCMInventoryAnalyst.* " +
                              " FROM dbo.tbl_System_SCMInventoryAnalyst " +
                              " WHERE(UserKey = " + usrKey + ") " +
                              " AND (StatusKey = 1)";
                        cmd = new SqlCommand(qry);
                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtable);
                        if (dtable.Rows.Count > 0)
                        {
                            isAllowed = true;
                        }
                        dtable.Clear();
                        break;
                    }
                case "MOPBudget":
                    {
                        qry = "SELECT dbo.tbl_System_FinanceBudget.* " +
                              " FROM dbo.tbl_System_FinanceBudget " +
                              " WHERE(UserKey = " + usrKey + ") " +
                              " AND (StatusKey = 1)";
                        cmd = new SqlCommand(qry);
                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtable);
                        if (dtable.Rows.Count > 0)
                        {
                            isAllowed = true;
                        }
                        dtable.Clear();
                        break;
                    }
                case "MOPBudget_PerEntBU":
                    {
                        qry = "SELECT TOP (1) dbo.tbl_System_FinanceBudget.UserKey " +
                              " FROM dbo.tbl_System_FinanceBudget_Details LEFT OUTER JOIN " +
                              " dbo.tbl_System_FinanceBudget ON dbo.tbl_System_FinanceBudget_Details.MasterKey = dbo.tbl_System_FinanceBudget.PK " +
                              " WHERE(dbo.tbl_System_FinanceBudget.EffectDate <= '" + dtEffect + "') " +
                              " AND(dbo.tbl_System_FinanceBudget_Details.EntityCode = '" + sEntCode + "') " +
                              " AND(dbo.tbl_System_FinanceBudget_Details.BUSSUCode = '" + sBUCode + "') " +
                              " AND(UserKey = " + usrKey + ") " +
                              " AND(dbo.tbl_System_FinanceBudget.StatusKey = 1) " +
                              " ORDER BY dbo.tbl_System_FinanceBudget.EffectDate DESC";
                        cmd = new SqlCommand(qry);
                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtable);
                        if (dtable.Rows.Count > 0)
                        {
                            isAllowed = true;
                        }
                        dtable.Clear();
                        break;
                    }
                case "MOPInventoryOfficer":
                    {
                        qry = "SELECT dbo.tbl_System_FinanceInventoryOfficer.* " +
                              " FROM dbo.tbl_System_FinanceInventoryOfficer " +
                              " WHERE(UserKey = " + usrKey + ") " +
                              " AND (StatusKey = 1)";
                        cmd = new SqlCommand(qry);
                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtable);
                        if (dtable.Rows.Count > 0)
                        {
                            isAllowed = true;
                        }
                        dtable.Clear();
                        break;
                    }
                case "MOPProcurementOfficer":
                    {
                        qry = "SELECT dbo.tbl_System_SCMProcurementOfficer.* " +
                              " FROM dbo.tbl_System_SCMProcurementOfficer " +
                              " WHERE(UserKey = " + usrKey + ") " +
                              " AND (StatusKey = 1)";
                        cmd = new SqlCommand(qry);
                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtable);
                        if (dtable.Rows.Count > 0)
                        {
                            isAllowed = true;
                        }
                        dtable.Clear();
                        break;
                    }
                case "MOPProcurementOfficer_ProcCat":
                    {
                        qry = "SELECT dbo.tbl_System_SCMProcurementOfficer.UserKey, " +
                              " dbo.tbl_System_SCMProcurementOfficer_Details.ProcCat, " +
                              " dbo.tbl_System_SCMProcurementOfficer.StatusKey " +
                              " FROM dbo.tbl_System_SCMProcurementOfficer_Details LEFT OUTER JOIN " +
                              " dbo.tbl_System_SCMProcurementOfficer ON dbo.tbl_System_SCMProcurementOfficer_Details.MasterKey = dbo.tbl_System_SCMProcurementOfficer.PK " +
                              " WHERE(dbo.tbl_System_SCMProcurementOfficer.UserKey = " + usrKey + ") " +
                              " AND(dbo.tbl_System_SCMProcurementOfficer_Details.ProcCat = '"+ ProcCat + "') " +
                              " AND(dbo.tbl_System_SCMProcurementOfficer.StatusKey = 1)";
                        cmd = new SqlCommand(qry);
                        cmd.Connection = cn;
                        adp = new SqlDataAdapter(cmd);
                        adp.Fill(dtable);
                        if (dtable.Rows.Count > 0)
                        {
                            isAllowed = true;
                        }
                        dtable.Clear();
                        break;
                    }
                case "":
                    {
                        isAllowed = true;
                        break;
                    }
            }
            cn.Close();
            return isAllowed;
        }

        public static bool CheckWorkFlowSetup(DateTime dtEffect, string sEntCode = "", string sBUCode = "")
        {
            bool workFlowSetup = false;
            string qry = "";
            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dtable = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            DataTable dtable1 = new DataTable();
            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;

            DataTable dtable2 = new DataTable();
            SqlCommand cmd2 = null;
            SqlDataAdapter adp2;

            DataTable dtable3 = new DataTable();
            SqlCommand cmd3 = null;
            SqlDataAdapter adp3;

            DataTable dtable4 = new DataTable();
            SqlCommand cmd4 = null;
            SqlDataAdapter adp4;

            cn.Open();
            WorkFlowSetupMsg = "";

            qry = "SELECT TOP (1) PK, EffectDate " +
                  " FROM dbo.tbl_System_MOP_DataFlow " +
                  " WHERE(EffectDate <= '"+ dtEffect + "') " +
                  " ORDER BY EffectDate DESC";
            cmd2 = new SqlCommand(qry);
            cmd2.Connection = cn;
            adp2 = new SqlDataAdapter(cmd2);
            adp2.Fill(dtable2);
            if (dtable2.Rows.Count > 0)
            {
                foreach(DataRow row2 in dtable2.Rows) {
                    qry = "SELECT dbo.tbl_System_MOP_DataFlow_Details.Line, " +
                          " dbo.tbl_System_MOP_DataFlow_Details.PositionNameKey, " +
                          " dbo.tbl_System_Approval_Position.PositionName, " +
                          " dbo.tbl_System_Approval_Position.SQLQuery " +
                          " FROM dbo.tbl_System_MOP_DataFlow_Details LEFT OUTER JOIN " +
                          " dbo.tbl_System_Approval_Position ON dbo.tbl_System_MOP_DataFlow_Details.PositionNameKey = dbo.tbl_System_Approval_Position.PK " +
                          " WHERE(dbo.tbl_System_MOP_DataFlow_Details.MasterKey = "+ Convert.ToInt32(row2["PK"]) +") " +
                          " ORDER BY dbo.tbl_System_MOP_DataFlow_Details.Line";
                    cmd1 = new SqlCommand(qry);
                    cmd1.Connection = cn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dtable1);
                    if (dtable1.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dtable1.Rows)
                        {
                            if (row1["SQLQuery"].ToString().Trim() != "")
                            {
                                qry = row1["SQLQuery"].ToString() + " '" + sEntCode + "', '" + sBUCode + "', '" + dtEffect + "'";
                                cmd = new SqlCommand(qry);
                                cmd.Connection = cn;
                                adp = new SqlDataAdapter(cmd);
                                adp.Fill(dtable);
                                if (dtable.Rows.Count > 0)
                                {
                                    workFlowSetup = true;
                                    //dtable.Clear();
                                    goto ReturnValue;
                                } else
                                {
                                    WorkFlowSetupMsg = "No current " + row1["PositionName"].ToString() + " setup!";
                                    workFlowSetup = false;
                                    //dtable.Clear();
                                    goto ReturnValue;
                                }
                            } else
                            {
                                if (Convert.ToInt32(row1["PositionNameKey"]) == 9)
                                {
                                    qry = "SELECT TOP (1) PK, EffectDate " +
                                          " FROM dbo.tbl_System_Approval " +
                                          " WHERE(EffectDate <= '" + dtEffect + "') " +
                                          " ORDER BY EffectDate";
                                    cmd3 = new SqlCommand(qry);
                                    cmd3.Connection = cn;
                                    adp3 = new SqlDataAdapter(cmd3);
                                    adp3.Fill(dtable3);
                                    if (dtable3.Rows.Count > 0)
                                    {
                                        foreach (DataRow row3 in dtable3.Rows)
                                        {
                                            qry = "SELECT dbo.tbl_System_Approval_Details.PositionNameKey, " +
                                                  " dbo.tbl_System_Approval_Position.PositionName, " +
                                                  " dbo.tbl_System_Approval_Position.SQLQuery " +
                                                  " FROM dbo.tbl_System_Approval_Details LEFT OUTER JOIN " +
                                                  " dbo.tbl_System_Approval_Position ON dbo.tbl_System_Approval_Details.PositionNameKey = dbo.tbl_System_Approval_Position.PK " +
                                                  " WHERE(dbo.tbl_System_Approval_Details.MasterKey = "+ Convert.ToInt32(row3["PK"]) +") " +
                                                  " ORDER BY dbo.tbl_System_Approval_Details.Line";
                                            cmd4 = new SqlCommand(qry);
                                            cmd4.Connection = cn;
                                            adp4 = new SqlDataAdapter(cmd4);
                                            adp4.Fill(dtable4);
                                            if (dtable4.Rows.Count > 0)
                                            {
                                                foreach (DataRow row4 in dtable4.Rows)
                                                {
                                                    qry = row4["SQLQuery"].ToString() + " '" + sEntCode + "', '" + sBUCode + "', '" + dtEffect + "'";
                                                    cmd = new SqlCommand(qry);
                                                    cmd.Connection = cn;
                                                    adp = new SqlDataAdapter(cmd);
                                                    adp.Fill(dtable);
                                                    if (dtable.Rows.Count > 0)
                                                    {
                                                        workFlowSetup = true;
                                                        //dtable.Clear();
                                                        goto ReturnValue;
                                                    }
                                                    else
                                                    {
                                                        WorkFlowSetupMsg = "No current " + row4["PositionName"].ToString() + " setup!";
                                                        workFlowSetup = false;
                                                        //dtable.Clear();
                                                        goto ReturnValue;
                                                    }
                                                }
                                            }
                                            //dtable4.Clear();
                                        }
                                    } else
                                    {
                                        WorkFlowSetupMsg = "No current " + row1["PositionName"].ToString() + " setup!";
                                        workFlowSetup = false;
                                        //dtable3.Clear();
                                        goto ReturnValue;
                                    }
                                }
                            }
                        }
                    }
                    //dtable1.Clear();
                }
            } else
            {
                WorkFlowSetupMsg = "No current workflow setup!";
                workFlowSetup = false;
                goto ReturnValue;
            }
            //dtable2.Clear();
            cn.Close();

            ReturnValue:
            dtable.Clear();
            dtable1.Clear();
            dtable2.Clear();
            dtable3.Clear();
            dtable4.Clear();
            return workFlowSetup;
        }

        public static string Delete_Data(string sTableName, int PK)
        {
            string sDeleted = "";
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            conn.Open();
            string qry = "DELETE FROM " + sTableName + " WHERE (PK = " + PK + ")";
            try
            {
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                sDeleted = "Successfully Deleted!";
            } catch (SqlException ex)
            {
                conn.Close();
                sDeleted = ex.ToString();
            }
            return sDeleted;
        }

        public static DataTable FixedAssetIDTable(string entCode, string procCat)
        {

            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("ID", typeof(string));
                dtTable.Columns.Add("NAME", typeof(string));
            }

            string qry = "SELECT [ASSETID] AS ID, [NAME] FROM vw_AXFixedAsset WHERE ([DATAAREAID] = '" + entCode + "') AND ([ASSETGROUP] = '" + procCat + "')";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ID"] = row["ID"].ToString();
                    dtRow["NAME"] = row["NAME"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }


        public static DataTable SideNavigation(int userKey)
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;

            cn.Open();

            if (dtTable.Columns.Count == 0)
            {
                //Columns for AspxGridview
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("Sort", typeof(Int32));
                dtTable.Columns.Add("formName", typeof(string));
                dtTable.Columns.Add("formDescription", typeof(string));
                dtTable.Columns.Add("forAdminOnly", typeof(string));
                dtTable.Columns.Add("ModuleName", typeof(string));
            }
            string qry = "SELECT PK, Sort, formName, formDescription, ModuleName, forAdminOnly FROM dbo.tbl_System_SideNav WHERE(formName <> '') ORDER BY Sort";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["PK"] = row["PK"].ToString();
                    dtRow["Sort"] = Convert.ToInt32(row["Sort"]);
                    dtRow["formName"] = row["formName"].ToString();
                    dtRow["formDescription"] = row["formDescription"].ToString();
                    dtRow["forAdminOnly"] = row["forAdminOnly"].ToString();
                    dtRow["ModuleName"] = row["ModuleName"].ToString();
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static void CreateEmailNotification(string EmailAdd, string Greetings, string DocNum, string DocLevel, int EmailType)
        {
            DataTable dtTable = new DataTable();

            SqlConnection cn = new SqlConnection(GlobalClass.SQLConnString());
            SqlCommand cmdIns = null;
            string qry = "";

            cn.Open();
            qry = "INSERT INTO tbl_System_EmailSent (EmailAdd, Greetings, DocNum, DocumentLevel, EmailType) VALUES ('" + GlobalClass.FormatSQL(EmailAdd) + "', '" + GlobalClass.FormatSQL(Greetings) + "', '" + GlobalClass.FormatSQL(DocNum.ToString()) + "', '"+ GlobalClass.FormatSQL(DocLevel) +"', "+ EmailType + ")";
            cmdIns = new SqlCommand(qry, cn);
            cmdIns.ExecuteNonQuery();

            cn.Close();
        }

        public static string DefaultPassword()
        {
            string sWebRoot = HttpContext.Current.Server.MapPath("~");
            string ResetPasswordPath = sWebRoot + @"config\Key.txt";
            string ResetPassword = "";

            try
            {
                if (File.Exists(ResetPasswordPath))
                {
                    using (StreamReader sr = new StreamReader(ResetPasswordPath))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string ss = sr.ReadLine();
                            string[] txtsplit = ss.Split('{');
                            if (txtsplit[0].ToString() == "respass")
                            {
                                ResetPassword = txtsplit[1].ToString();
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ResetPassword = "";
            }

            return ResetPassword;
        }
    }
}