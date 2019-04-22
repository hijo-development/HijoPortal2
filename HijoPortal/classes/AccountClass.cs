using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.IO;

namespace HijoPortal.classes
{
    public class AccountClass
    {
        public static DataTable UserLevelTable()
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

            string qry = "SELECT [PK] AS ID, [UserLevel] AS NAME FROM [hijo_portal].[dbo].[tbl_UserLevel]";

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

        public static DataTable UserStatusTable()
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

            DataRow dtRow = dtTable.NewRow();
            dtRow["ID"] = "1";
            dtRow["NAME"] = "Active";
            dtTable.Rows.Add(dtRow);

            DataRow dtRow1 = dtTable.NewRow();
            dtRow1["ID"] = "0";
            dtRow1["NAME"] = "Inactive";
            dtTable.Rows.Add(dtRow1);

            return dtTable;
        }

        public static DataTable UserListTable()
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

            string qry = "SELECT PK, Lastname, Firstname FROM [hijo_portal].[dbo].[tbl_Users] ORDER BY Lastname, Firstname ";

            cmd = new SqlCommand(qry);
            cmd.Connection = cn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dtRow = dtTable.NewRow();
                    dtRow["ID"] = row["PK"].ToString();
                    dtRow["NAME"] = EncryptionClass.Decrypt(row["Lastname"].ToString()) + ",  " + EncryptionClass.Decrypt(row["Firstname"].ToString());
                    dtTable.Rows.Add(dtRow);
                }
            }
            dt.Clear();
            cn.Close();

            return dtTable;
        }

        public static DataTable UserTypeTable()
        {
            DataTable dtTable = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dtable = new DataTable();

            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("UserType", typeof(string));
            }
            dtTable.Clear();

            using (SqlConnection con = new SqlConnection(GlobalClass.SQLConnString()))
            {
                con.Open();
                string qry = "SELECT tbl_UsersType.* " +
                             " FROM tbl_UsersType";
                cmd = new SqlCommand(qry);
                cmd.Connection = con;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dtable);
                if (dtable.Rows.Count > 0)
                {
                    DataRow rowAdd1 = dtTable.NewRow();
                    rowAdd1["PK"] = "0";
                    rowAdd1["UserType"] = "<-- Select User Type -->";
                    dtTable.Rows.Add(rowAdd1);

                    foreach (DataRow row in dtable.Rows)
                    {
                        DataRow rowAdd = dtTable.NewRow();
                        rowAdd["PK"] = row["PK"].ToString();
                        rowAdd["UserType"] = row["UserType"].ToString();
                        dtTable.Rows.Add(rowAdd);
                    }
                }
                dtable.Clear();
                con.Close();
            }

            return dtTable;
        }

        public static string UserCompleteName(int PK)
        {
            DataTable dtUser = UserList();
            string expression = "PK = " + PK + "";
            string sortOrder = "PK ASC";
            DataRow[] foundRows;
            foundRows = dtUser.Select(expression, sortOrder);
            if (foundRows.Length > 0)
            {
                return foundRows[0]["Lastname"].ToString() + ",  " + foundRows[0]["Firstname"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static DataTable UserList()
        {
            DataTable dtTable = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dtable = new DataTable();

            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("PK", typeof(string));
                dtTable.Columns.Add("UserName", typeof(string));
                dtTable.Columns.Add("Password", typeof(string));
                dtTable.Columns.Add("UserType", typeof(string));
                dtTable.Columns.Add("UserTypeDesc", typeof(string));
                dtTable.Columns.Add("UserLevelKey", typeof(string));
                dtTable.Columns.Add("UserLevelDesc", typeof(string));
                dtTable.Columns.Add("LastName", typeof(string));
                dtTable.Columns.Add("FirstName", typeof(string));
                dtTable.Columns.Add("Gender", typeof(string));
                //dtTable.Columns.Add("MiddleName", typeof(string));
                dtTable.Columns.Add("CompleteName", typeof(string));
                dtTable.Columns.Add("Email", typeof(string));
                dtTable.Columns.Add("EntityCode", typeof(string));
                dtTable.Columns.Add("EntityCodeDesc", typeof(string));
                dtTable.Columns.Add("BUCode", typeof(string));
                dtTable.Columns.Add("BUCodeDesc", typeof(string));
                dtTable.Columns.Add("DomainAccount", typeof(string));
                dtTable.Columns.Add("StatusKey", typeof(string));
                dtTable.Columns.Add("StatusDesc", typeof(string));
                dtTable.Columns.Add("EmployeeKey", typeof(string));
            }
            dtTable.Clear();

            using (SqlConnection con = new SqlConnection(GlobalClass.SQLConnString()))
            {
                con.Open();
                string qry = "SELECT dbo.tbl_Users.PK, dbo.tbl_Users.UserType, dbo.tbl_UsersType.UserType AS UserTypeDesc, " +
                             " dbo.tbl_Users.UserLevelKey, dbo.tbl_UserLevel.UserLevel, dbo.tbl_Users.Username, " +
                             " dbo.tbl_Users.Password, dbo.tbl_Users.DomainAccount, dbo.tbl_Users.Lastname, " +
                             " dbo.tbl_Users.Firstname, dbo.tbl_Users.Email, dbo.tbl_Users.Gender, " +
                             " dbo.tbl_Users.EmployeeKey, dbo.tbl_Users.EntityCode, dbo.tbl_Users.BUCode, " +
                             " dbo.tbl_Users.Image, ISNULL(dbo.vw_AXEntityTable.NAME, '') AS EntityCodeDesc, " +
                             " ISNULL(dbo.vw_AXOperatingUnitTable.NAME, '') AS BUCodeDesc, dbo.tbl_Users.DomainAccount, " +
                             " dbo.tbl_Users.Active, dbo.tbl_Users.EmployeeKey " +
                             " FROM dbo.tbl_Users LEFT OUTER JOIN " +
                             " dbo.vw_AXOperatingUnitTable ON dbo.tbl_Users.BUCode = dbo.vw_AXOperatingUnitTable.OMOPERATINGUNITNUMBER LEFT OUTER JOIN " +
                             " dbo.vw_AXEntityTable ON dbo.tbl_Users.EntityCode = dbo.vw_AXEntityTable.ID LEFT OUTER JOIN " +
                             " dbo.tbl_UsersType ON dbo.tbl_Users.UserType = dbo.tbl_UsersType.PK LEFT OUTER JOIN " +
                             " dbo.tbl_UserLevel ON dbo.tbl_Users.UserLevelKey = dbo.tbl_UserLevel.PK " +
                             " ORDER BY dbo.tbl_Users.Lastname, dbo.tbl_Users.Firstname ";
                cmd = new SqlCommand(qry);
                cmd.Connection = con;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dtable);
                if (dtable.Rows.Count > 0)
                {
                    foreach (DataRow row in dtable.Rows)
                    {
                        DataRow rowAdd = dtTable.NewRow();
                        rowAdd["PK"] = row["PK"].ToString();
                        rowAdd["UserName"] = EncryptionClass.Decrypt(row["Username"].ToString());
                        rowAdd["Password"] = EncryptionClass.Decrypt(row["Password"].ToString());
                        rowAdd["UserType"] = row["UserType"].ToString();
                        rowAdd["UserTypeDesc"] = row["UserTypeDesc"].ToString();
                        rowAdd["UserLevelKey"] = row["UserLevelKey"].ToString();
                        rowAdd["UserLevelDesc"] = row["UserLevel"].ToString();
                        rowAdd["CompleteName"] = EncryptionClass.Decrypt(row["Lastname"].ToString()) + ",  " + EncryptionClass.Decrypt(row["Firstname"].ToString());  //+ "  " + EncryptionClass.Decrypt(row["Middlename"].ToString());
                        rowAdd["LastName"] = EncryptionClass.Decrypt(row["Lastname"].ToString());
                        rowAdd["FirstName"] = EncryptionClass.Decrypt(row["Firstname"].ToString());
                        if (Convert.ToInt32(row["Gender"]) == 1)
                        {
                            rowAdd["Gender"] = "Male";
                        } else
                        {
                            rowAdd["Gender"] = "Female";
                        }
                        //rowAdd["MiddleName"] = EncryptionClass.Decrypt(row["Middlename"].ToString());
                        rowAdd["Email"] = EncryptionClass.Decrypt(row["Email"].ToString());
                        rowAdd["EntityCode"] = row["EntityCode"].ToString();
                        rowAdd["EntityCodeDesc"] = row["EntityCodeDesc"].ToString();
                        rowAdd["BUCode"] = row["BUCode"].ToString();
                        rowAdd["BUCodeDesc"] = row["BUCodeDesc"].ToString();
                        if (row["DomainAccount"].ToString().Trim() == "")
                        {
                            rowAdd["DomainAccount"] = row["DomainAccount"].ToString();
                        }
                        else
                        {
                            rowAdd["DomainAccount"] = EncryptionClass.Decrypt(row["DomainAccount"].ToString());
                        }
                        rowAdd["StatusKey"] = row["Active"].ToString();
                        if (Convert.ToInt32(row["Active"]) == 1)
                        {
                            rowAdd["StatusDesc"] = "Active";
                        }
                        else
                        {
                            rowAdd["StatusDesc"] = "Inactive";
                        }

                        if (!DBNull.Value.Equals(row["EmployeeKey"]))
                        {
                            rowAdd["EmployeeKey"] = row["EmployeeKey"].ToString();
                        } else
                        {
                            rowAdd["EmployeeKey"] = "0";
                        }                        
                        dtTable.Rows.Add(rowAdd);
                    }
                }
                dtable.Clear();
                con.Close();
            }

            return dtTable;

        }

        public static void SaveAccount(int TransType, int iPK, string sLastName, string sFirstName, string sMiddleName,
                                       string sEmail, int iUserType, string sUserName, string sPassword, int EmployeeKey,
                                       string sDomainName = "", string sEntityCode = "", string sBUCode = "")
        {
            using (SqlConnection con = new SqlConnection(GlobalClass.SQLConnString()))
            {
                con.Open();
                SqlCommand cmd = null;
                string qry = "", _sLastName, _sFirstName, _sMiddleName, _sEmail, _sUserName,
                    _sPassword, _sDomainName, _sEntityCode, _sBUCode;

                _sLastName = EncryptionClass.Encrypt(sLastName);
                _sFirstName = EncryptionClass.Encrypt(sFirstName);
                _sMiddleName = EncryptionClass.Encrypt(sMiddleName);
                _sEmail = EncryptionClass.Encrypt(sEmail);
                _sUserName = EncryptionClass.Encrypt(sUserName);
                _sPassword = EncryptionClass.Encrypt(sPassword);
                _sDomainName = EncryptionClass.Encrypt(sDomainName);
                _sEntityCode = sEntityCode;
                _sBUCode = sBUCode;

                if (TransType == 1)
                {
                    // Insert
                    qry = "INSERT INTO tbl_Users " +
                          " (Lastname, Firstname, Middlename, UserType, Username, Password, Email, DomainAccount, EmployeeKey) " +
                          " VALUES ('" + _sLastName + "', '" + _sFirstName + "', '" + _sMiddleName + "', " + iUserType + ", " +
                          " '" + _sUserName + "', '" + _sPassword + "', '" + _sEmail + "', '" + _sDomainName + "', " +
                          " " + EmployeeKey + ")";

                }
                if (TransType == 2)
                {
                    // Edit
                    qry = "UPDATE tbl_Users " +
                          " SET EntityCode = '" + _sEntityCode + "', " +
                          " BUCode = '" + _sBUCode + "', " +
                          " DomainAccount = '" + _sDomainName + "' " +
                          " WHERE (PK = " + iPK + ")";

                }
                try
                {
                    cmd = new SqlCommand(qry);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GlobalClass.QueryError = "Account saved!";
                }
                catch (SqlException e)
                {
                    con.Close();
                    GlobalClass.QueryError = e.ToString();
                }
            }
        }

        public static DataTable GetEmployeeInfo(string IDNum)
        {
            DataTable dtTable = new DataTable();
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dtable = new DataTable();

            if (dtTable.Columns.Count == 0)
            {
                dtTable.Columns.Add("EmployeeKey", typeof(string));
                dtTable.Columns.Add("LastName", typeof(string));
                dtTable.Columns.Add("FirstName", typeof(string));
                dtTable.Columns.Add("MiddleName", typeof(string));
                dtTable.Columns.Add("Email", typeof(string));
                dtTable.Columns.Add("DomainAccount", typeof(string));
                dtTable.Columns.Add("Gender", typeof(int));
            }
            dtTable.Clear();

            using (SqlConnection con = new SqlConnection(GlobalClass.SQLConnStringHRIS()))
            {
                con.Open();
                string qry = "SELECT dbo.tbl_EmployeeIDNumber.PK, dbo.tbl_EmployeeProfile.LastName, " +
                             " dbo.tbl_EmployeeProfile.FirstName, dbo.tbl_EmployeeProfile.MiddleName, " +
                             " dbo.tbl_EmployeeProfile.CompanyEmail, dbo.tbl_EmployeeIDNumber.DomainUN, dbo.tbl_EmployeeProfile.Gender " +
                             " FROM dbo.tbl_EmployeeIDNumber LEFT OUTER JOIN " +
                             " dbo.tbl_EmployeeProfile ON dbo.tbl_EmployeeIDNumber.ProfileKey = dbo.tbl_EmployeeProfile.PK " +
                             " WHERE(dbo.tbl_EmployeeIDNumber.IDNumber = '" + IDNum.ToString() + "')";
                cmd = new SqlCommand(qry);
                cmd.Connection = con;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dtable);
                if (dtable.Rows.Count > 0)
                {
                    foreach (DataRow row in dtable.Rows)
                    {
                        DataRow rowAdd = dtTable.NewRow();
                        rowAdd["EmployeeKey"] = row["PK"].ToString();
                        rowAdd["LastName"] = row["LastName"].ToString();
                        rowAdd["FirstName"] = row["FirstName"].ToString();
                        rowAdd["MiddleName"] = row["MiddleName"].ToString();
                        rowAdd["Email"] = row["CompanyEmail"].ToString();
                        rowAdd["DomainAccount"] = row["DomainUN"].ToString();
                        rowAdd["Gender"] = row["Gender"].ToString();
                        dtTable.Rows.Add(rowAdd);
                    }
                }
                dtable.Clear();
                con.Close();
            }

            return dtTable;
        }

        public static int EmployeePictureInHRIS(string IDNum)
        {
            int HavePicture = 0;
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dtable = new DataTable();

            //string sWebRoot = HttpContext.Current.Server.MapPath("~");
            //string imgPathTmp = sWebRoot + @"images\users\";

            using (SqlConnection con = new SqlConnection(GlobalClass.SQLConnStringHRIS()))
            {
                con.Open();
                string qry = "SELECT dbo.tbl_EmployeeIDNumber.PK, dbo.tbl_EmployeeProfile.LastName, dbo.tbl_EmployeeProfile.FirstName, dbo.tbl_EmployeeProfile.MiddleName, dbo.tbl_EmployeeProfile.CompanyEmail, dbo.tbl_EmployeeIDNumber.DomainUN, dbo.tbl_EmployeeProfile.Picture FROM dbo.tbl_EmployeeIDNumber LEFT OUTER JOIN dbo.tbl_EmployeeProfile ON dbo.tbl_EmployeeIDNumber.ProfileKey = dbo.tbl_EmployeeProfile.PK WHERE(dbo.tbl_EmployeeIDNumber.IDNumber = '" + IDNum + "')";
                cmd = new SqlCommand(qry);
                cmd.Connection = con;
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dtable);
                if (dtable.Rows.Count > 0)
                {
                    foreach (DataRow row in dtable.Rows)
                    {
                        if (row["Picture"] != System.DBNull.Value)
                        {
                            string imgPath = GlobalClass.UserImagePath + IDNum + ".jpg";
                            if (File.Exists(imgPath) == true) { File.Delete(imgPath); }
                            FileStream fs1 = new FileStream(imgPath, FileMode.CreateNew, FileAccess.Write);
                            byte[] bimage1 = (byte[])row["Picture"];
                            fs1.Write(bimage1, 0, bimage1.Length - 1);
                            fs1.Flush();
                            fs1.Dispose();
                            HavePicture = 1;
                        }
                    }
                }
                dtable.Clear();
                con.Close();
            }
            return HavePicture;
        }
    }
}