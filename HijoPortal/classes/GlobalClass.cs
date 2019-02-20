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

        public static object ContextType { get; private set; }

        public static string UpperCaseFirstLetter(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
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

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXEntityTable]";

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
            string query = "SELECT NAME FROM [hijo_portal].[dbo].[vw_AXEntityTable] where ID = '" + code + "'";

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

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXOperatingUnitTable]";

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

            string qry = "SELECT * FROM [hijo_portal].[dbo].[vw_AXOperatingUnitTable] WHERE (entity = '"+ entCode + "')";

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

        public static string BUCodeDescription(string code)
        {
            string query = "SELECT NAME FROM [hijo_portal].[dbo].[vw_AXOperatingUnitTable] where OMOPERATINGUNITNUMBER = '" + code + "'";

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
            }

            string qry = "SELECT dbo.tbl_System_BUDeptHead.PK, dbo.tbl_System_BUDeptHead.Ctrl, " +
                         " dbo.tbl_System_BUDeptHead.EffectDate, dbo.tbl_System_BUDeptHead.EntityCode, " +
                         " dbo.vw_AXEntityTable.NAME AS EntityCodeDesc, dbo.tbl_System_BUDeptHead.BUDeptCode, " +
                         " dbo.vw_AXOperatingUnitTable.NAME AS BUDeptCodeDesc, dbo.tbl_System_BUDeptHead.UserKey, " +
                         " dbo.tbl_Users.Lastname, dbo.tbl_Users.Firstname " +
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
                              " WHERE (Year(EffectDate) = "+ dEffectDate.Year + ")";
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
            } else
            {
                sDocNum = dEffectDate.ToString("yyyy") + "0000";
            }
            dt.Clear();
            cn.Close();
            return sDocNum;
        }
    }
}