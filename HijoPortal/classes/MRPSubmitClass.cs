using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Collections;
using System.Web;

namespace HijoPortal.classes
{
    public class MRPSubmitClass
    {
        public static string SubmitStatusRes = "";

        public static void MRP_Submit(string docNum, int MRPKey, DateTime dteCreated, int WorkFlowLine, string EntCode, string BuCode, int usrKey)
        {
            SubmitStatusRes = "";

            switch (WorkFlowLine)
            {
                case 0: //At source
                    {
                        MRP_Submit_Atsource(docNum, MRPKey, dteCreated, WorkFlowLine, EntCode, BuCode);
                        break;
                    }
                case 1: //BU SSU Lead
                    {
                        MRP_Submit_BUSSULead(docNum, MRPKey, dteCreated, WorkFlowLine, EntCode, BuCode);
                        break;
                    }
                case 2: //Inventory Analyst
                    {
                        MRP_Submit_InventoryAnalyst(docNum, MRPKey, dteCreated, WorkFlowLine, EntCode, BuCode);
                        break;
                    }
                //case 3: //Finance Budget
                //    {
                //        MRP_Submit_FinanceBudget(docNum, MRPKey, dteCreated, WorkFlowLine, EntCode, BuCode);
                //        break;
                //    }
                case 3: //Deliberation proceed to approval
                    {
                        MRP_Submit_Deliberation(docNum, MRPKey, dteCreated, WorkFlowLine, EntCode, BuCode);
                        break;
                    }
            }
        }

        private static void MRP_Submit_Atsource(string docNum, int MRPKey, DateTime dteCreated, int WorkFlowLine, string EntCode, string BuCode)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            string qry = "";
            string CreatorEmail = "", CreatorSubject = "", CreatorGreetings = "";
            var sCreatorBody = new StringBuilder();
            string sEmail = "", sSubject = "", sGreetings = "";
            var sBody = new StringBuilder();

            int WorkLineNext = WorkFlowLine + 1;


            SqlCommand cmdIns = null;
            SqlCommand cmdUp = null;
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dtable = new DataTable();

            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;
            DataTable dtable1 = new DataTable();

            SqlCommand cmd2 = null;
            SqlDataAdapter adp2;
            DataTable dtable2 = new DataTable();

            SqlCommand cmd3 = null;
            SqlDataAdapter adp3;
            DataTable dtable3 = new DataTable();

            conn.Open();
            qry = "SELECT dbo.tbl_Users.Email, dbo.tbl_Users.Gender, dbo.tbl_Users.Lastname " +
                  " FROM dbo.tbl_MRP_List LEFT OUTER JOIN " +
                  " dbo.tbl_Users ON dbo.tbl_MRP_List.CreatorKey = dbo.tbl_Users.PK " +
                  " WHERE(dbo.tbl_MRP_List.PK = "+ MRPKey + ")";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow row in dtable.Rows)
                {
                    CreatorEmail = EncryptionClass.Decrypt(row["Email"].ToString());
                    if (Convert.ToInt32(row["Gender"]) == 1)
                    {
                        CreatorGreetings = "Dear Mr. " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    }
                    else
                    {
                        CreatorGreetings = "Dear Ms. " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    }
                    CreatorSubject = "MOP DocNum " + docNum.ToString() + " status";

                    sCreatorBody.Append("<!DOCTYPE html>");
                    sCreatorBody.Append("<html>");
                    sCreatorBody.Append("<head>");
                    sCreatorBody.Append("</head>");
                    sCreatorBody.Append("<body>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>" + CreatorGreetings + ",</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>MOP Document # " + docNum.ToString() + " has been submitted to your Lead for review/approval.</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 10px;font-style:italic;'>***This is a system-generated message. please do not reply to this email.***</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 10px;'>DISCLAIMER: This email is confidential and intended solely for the use of the individual to whom it is addressed. If you are not the intended recipient, be advised that you have received this email in error and that any use, dissemination, forwarding, printing or copying of this email is strictly prohibited. If you have received this email in error please notify the sender or email info@hijoresources.net, telephone number (082) 282-3662.</p>");
                    sCreatorBody.Append("</body>");
                    sCreatorBody.Append("</html>");
                }
            }
            dtable.Clear();

            qry = "SELECT TOP (1) PK " +
                  " FROM dbo.tbl_System_MOP_DataFlow " +
                  " WHERE(EffectDate <= '"+ dteCreated + "') " +
                  " ORDER BY EffectDate DESC";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow row in dtable.Rows)
                {
                    qry = "SELECT dbo.tbl_System_Approval_Position.PositionName, " +
                          " dbo.tbl_System_Approval_Position.SQLQuery, " +
                          " dbo.tbl_System_MOP_DataFlow_Details.PositionNameKey " +
                          " FROM dbo.tbl_System_MOP_DataFlow_Details LEFT OUTER JOIN " +
                          " dbo.tbl_System_Approval_Position ON dbo.tbl_System_MOP_DataFlow_Details.PositionNameKey = dbo.tbl_System_Approval_Position.PK " +
                          " WHERE(dbo.tbl_System_MOP_DataFlow_Details.MasterKey = "+ row["PK"] +") " +
                          " AND(dbo.tbl_System_MOP_DataFlow_Details.Line = "+ WorkLineNext + ")";
                    cmd1 = new SqlCommand(qry);
                    cmd1.Connection = conn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dtable1);
                    if (dtable1.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dtable1.Rows)
                        {
                            if (row1["SQLQuery"].ToString().Trim() !="")
                            {
                                qry = row1["SQLQuery"].ToString() + " '" + EntCode + "', '" + BuCode + "', '" + dteCreated + "'";
                                cmd2 = new SqlCommand(qry);
                                cmd2.Connection = conn;
                                adp2 = new SqlDataAdapter(cmd2);
                                adp2.Fill(dtable2);
                                if (dtable2.Rows.Count > 0)
                                {
                                    foreach (DataRow row2 in dtable2.Rows)
                                    {
                                        sEmail = EncryptionClass.Decrypt(row2["Email"].ToString());
                                        if (Convert.ToInt32(row2["Gender"]) == 1)
                                        {
                                            sGreetings = "Dear Mr. " + EncryptionClass.Decrypt(row2["Lastname"].ToString());
                                        }
                                        else
                                        {
                                            sGreetings = "Dear Ms. " + EncryptionClass.Decrypt(row2["Lastname"].ToString());
                                        }
                                        sSubject = "MOP DocNum " + docNum.ToString() + " is waiting for your review and approval";

                                        sBody.Append("<!DOCTYPE html>");
                                        sBody.Append("<html>");
                                        sBody.Append("<head>");
                                        sBody.Append("</head>");
                                        sBody.Append("<body>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>" + sGreetings + ",</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>MOP Document # " + docNum.ToString() + " is waiting for your review/approval.</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'><a href=" + GlobalClass.Email_Redirect() + ">Goto System</a></p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 10px;font-style:italic;'>***This is a system-generated message. please do not reply to this email.***</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 10px;'>DISCLAIMER: This email is confidential and intended solely for the use of the individual to whom it is addressed. If you are not the intended recipient, be advised that you have received this email in error and that any use, dissemination, forwarding, printing or copying of this email is strictly prohibited. If you have received this email in error please notify the sender or email info@hijoresources.net, telephone number (082) 282-3662.</p>");
                                        sBody.Append("</body>");
                                        sBody.Append("</html>");

                                        bool msgSend = GlobalClass.IsMailSent(sEmail, sSubject, sBody.ToString());

                                        //Update Workflow
                                        qry = "UPDATE tbl_MRP_List_Workflow " +
                                               " SET Visible = 0, " +
                                               " Status = 1 " +
                                               " WHERE (MasterKey = " + MRPKey + ") " +
                                               " AND (Line = " + WorkFlowLine + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Update MRP Status
                                        qry = "UPDATE tbl_MRP_List SET StatusKey = 2 WHERE (PK = " + MRPKey + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Update Workflow Next
                                        qry = "UPDATE tbl_MRP_List_Workflow " +
                                               " SET Visible = 1, " +
                                               " UserKey = "+ row2["UserKey"] +", " +
                                               " PositionNameKey = "+ row1["PositionNameKey"] +" " +
                                               " WHERE (MasterKey = " + MRPKey + ") " +
                                               " AND (Line = " + WorkLineNext + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Update Assigned to me
                                        qry = "UPDATE tbl_Users_Assigned " +
                                               " SET Attended = 1 " +
                                               " WHERE (UserKey = " + row2["UserKey"] + ") " +
                                               " AND (MRPKey = " + MRPKey + ") " +
                                               " AND (WorkFlowLine = " + WorkFlowLine + ") " +
                                               " AND (WorkFlowType = 1)";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Insert to Assigned to me
                                        qry = "SELECT tbl_Users_Assigned.* " +
                                              " FROM tbl_Users_Assigned " +
                                              " WHERE (UserKey = " + Convert.ToInt32(row2["UserKey"]) + ") " +
                                              " AND (MRPKey = " + MRPKey + ") " +
                                              " AND (WorkFlowLine = " + WorkLineNext + ") " +
                                              " AND (WorkFlowType = 1)";
                                        cmd3 = new SqlCommand(qry);
                                        cmd3.Connection = conn;
                                        adp3 = new SqlDataAdapter(cmd3);
                                        adp3.Fill(dtable3);
                                        if (dtable3.Rows.Count == 0)
                                        {
                                            qry = "INSERT INTO tbl_Users_Assigned " +
                                                  " (UserKey, PositionNameKey, MRPKey, WorkFlowLine, WorkFlowType) " +
                                                  " VALUES(" + Convert.ToInt32(row2["UserKey"]) + ", " +
                                                  " " + Convert.ToInt32(row1["PositionNameKey"]) + ", " +
                                                  " " + MRPKey + ", " + WorkLineNext + ", 1)";
                                            cmdIns = new SqlCommand(qry, conn);
                                            cmdIns.ExecuteNonQuery();
                                        }
                                        dtable3.Clear();
                                    }
                                }
                                dtable2.Clear();
                            }

                            bool msgSendToCreator = GlobalClass.IsMailSent(CreatorEmail, CreatorSubject, sCreatorBody.ToString());
                        }
                    }
                    dtable1.Clear();
                }
            }
            dtable.Clear();

            conn.Close();
        }

        private static void MRP_Submit_BUSSULead(string docNum, int MRPKey, DateTime dteCreated, int WorkFlowLine, string EntCode, string BuCode)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            string qry = "";
            string CreatorEmail = "", CreatorSubject = "", CreatorGreetings = "";
            var sCreatorBody = new StringBuilder();
            string sEmail = "", sSubject = "", sGreetings = "";
            var sBody = new StringBuilder();

            int WorkLineNext = WorkFlowLine + 1;


            SqlCommand cmdIns = null;
            SqlCommand cmdUp = null;
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dtable = new DataTable();

            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;
            DataTable dtable1 = new DataTable();

            SqlCommand cmd2 = null;
            SqlDataAdapter adp2;
            DataTable dtable2 = new DataTable();

            SqlCommand cmd3 = null;
            SqlDataAdapter adp3;
            DataTable dtable3 = new DataTable();

            conn.Open();
            qry = "SELECT dbo.tbl_Users.Email, dbo.tbl_Users.Gender, dbo.tbl_Users.Lastname " +
                  " FROM dbo.tbl_MRP_List LEFT OUTER JOIN " +
                  " dbo.tbl_Users ON dbo.tbl_MRP_List.CreatorKey = dbo.tbl_Users.PK " +
                  " WHERE(dbo.tbl_MRP_List.PK = " + MRPKey + ")";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow row in dtable.Rows)
                {
                    CreatorEmail = EncryptionClass.Decrypt(row["Email"].ToString());
                    if (Convert.ToInt32(row["Gender"]) == 1)
                    {
                        CreatorGreetings = "Dear Mr. " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    }
                    else
                    {
                        CreatorGreetings = "Dear Ms. " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    }
                    CreatorSubject = "MOP DocNum " + docNum.ToString() + " status";

                    sCreatorBody.Append("<!DOCTYPE html>");
                    sCreatorBody.Append("<html>");
                    sCreatorBody.Append("<head>");
                    sCreatorBody.Append("</head>");
                    sCreatorBody.Append("<body>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>" + CreatorGreetings + ",</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>MOP Document # " + docNum.ToString() + " has been submitted to Inventory Analyst review/approval.</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 10px;font-style:italic;'>***This is a system-generated message. please do not reply to this email.***</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 10px;'>DISCLAIMER: This email is confidential and intended solely for the use of the individual to whom it is addressed. If you are not the intended recipient, be advised that you have received this email in error and that any use, dissemination, forwarding, printing or copying of this email is strictly prohibited. If you have received this email in error please notify the sender or email info@hijoresources.net, telephone number (082) 282-3662.</p>");
                    sCreatorBody.Append("</body>");
                    sCreatorBody.Append("</html>");
                }
            }
            dtable.Clear();

            qry = "SELECT TOP (1) PK " +
                  " FROM dbo.tbl_System_MOP_DataFlow " +
                  " WHERE(EffectDate <= '" + dteCreated + "') " +
                  " ORDER BY EffectDate DESC";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow row in dtable.Rows)
                {
                    qry = "SELECT dbo.tbl_System_Approval_Position.PositionName, " +
                          " dbo.tbl_System_Approval_Position.SQLQuery, " +
                          " dbo.tbl_System_MOP_DataFlow_Details.PositionNameKey " +
                          " FROM dbo.tbl_System_MOP_DataFlow_Details LEFT OUTER JOIN " +
                          " dbo.tbl_System_Approval_Position ON dbo.tbl_System_MOP_DataFlow_Details.PositionNameKey = dbo.tbl_System_Approval_Position.PK " +
                          " WHERE(dbo.tbl_System_MOP_DataFlow_Details.MasterKey = " + row["PK"] + ") " +
                          " AND(dbo.tbl_System_MOP_DataFlow_Details.Line = " + WorkLineNext + ")";
                    cmd1 = new SqlCommand(qry);
                    cmd1.Connection = conn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dtable1);
                    if (dtable1.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dtable1.Rows)
                        {
                            if (row1["SQLQuery"].ToString().Trim() != "")
                            {
                                qry = row1["SQLQuery"].ToString() + " '" + EntCode + "', '" + BuCode + "', '" + dteCreated + "'";
                                cmd2 = new SqlCommand(qry);
                                cmd2.Connection = conn;
                                adp2 = new SqlDataAdapter(cmd2);
                                adp2.Fill(dtable2);
                                if (dtable2.Rows.Count > 0)
                                {
                                    foreach (DataRow row2 in dtable2.Rows)
                                    {
                                        sEmail = EncryptionClass.Decrypt(row2["Email"].ToString());
                                        if (Convert.ToInt32(row2["Gender"]) == 1)
                                        {
                                            sGreetings = "Dear Mr. " + EncryptionClass.Decrypt(row2["Lastname"].ToString());
                                        }
                                        else
                                        {
                                            sGreetings = "Dear Ms. " + EncryptionClass.Decrypt(row2["Lastname"].ToString());
                                        }
                                        sSubject = "MOP DocNum " + docNum.ToString() + " is waiting for your review and approval";

                                        sBody.Append("<!DOCTYPE html>");
                                        sBody.Append("<html>");
                                        sBody.Append("<head>");
                                        sBody.Append("</head>");
                                        sBody.Append("<body>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>" + sGreetings + ",</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>MOP Document # " + docNum.ToString() + " is waiting for your review/approval.</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'><a href=" + GlobalClass.Email_Redirect() + ">Goto System</a></p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 10px;font-style:italic;'>***This is a system-generated message. please do not reply to this email.***</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 10px;'>DISCLAIMER: This email is confidential and intended solely for the use of the individual to whom it is addressed. If you are not the intended recipient, be advised that you have received this email in error and that any use, dissemination, forwarding, printing or copying of this email is strictly prohibited. If you have received this email in error please notify the sender or email info@hijoresources.net, telephone number (082) 282-3662.</p>");
                                        sBody.Append("</body>");
                                        sBody.Append("</html>");

                                        bool msgSend = GlobalClass.IsMailSent(sEmail, sSubject, sBody.ToString());

                                        //Update Workflow
                                        qry = "UPDATE tbl_MRP_List_Workflow " +
                                               " SET Visible = 0, " +
                                               " Status = 1 " +
                                               " WHERE (MasterKey = " + MRPKey + ") " +
                                               " AND (Line = " + WorkFlowLine + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Update MRP Status
                                        qry = "UPDATE tbl_MRP_List SET StatusKey = 2 WHERE (PK = " + MRPKey + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Update Workflow Next
                                        qry = "UPDATE tbl_MRP_List_Workflow " +
                                               " SET Visible = 1, " +
                                               " UserKey = " + row2["UserKey"] + ", " +
                                               " PositionNameKey = " + row1["PositionNameKey"] + " " +
                                               " WHERE (MasterKey = " + MRPKey + ") " +
                                               " AND (Line = " + WorkLineNext + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Update Assigned to me
                                        qry = "UPDATE tbl_Users_Assigned " +
                                               " SET Attended = 1 " +
                                               " WHERE (UserKey = " + row2["UserKey"] + ") " +
                                               " AND (MRPKey = " + MRPKey + ") " +
                                               " AND (WorkFlowLine = " + WorkFlowLine + ") " +
                                               " AND (WorkFlowType = 1)";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Insert to Assigned to me
                                        qry = "SELECT tbl_Users_Assigned.* " +
                                              " FROM tbl_Users_Assigned " +
                                              " WHERE (UserKey = " + Convert.ToInt32(row2["UserKey"]) + ") " +
                                              " AND (MRPKey = " + MRPKey + ") " +
                                              " AND (WorkFlowLine = " + WorkLineNext + ") " +
                                              " AND (WorkFlowType = 1)";
                                        cmd3 = new SqlCommand(qry);
                                        cmd3.Connection = conn;
                                        adp3 = new SqlDataAdapter(cmd3);
                                        adp3.Fill(dtable3);
                                        if (dtable3.Rows.Count == 0)
                                        {
                                            qry = "INSERT INTO tbl_Users_Assigned " +
                                                  " (UserKey, PositionNameKey, MRPKey, WorkFlowLine, WorkFlowType) " +
                                                  " VALUES(" + Convert.ToInt32(row2["UserKey"]) + ", " +
                                                  " " + Convert.ToInt32(row1["PositionNameKey"]) + ", " +
                                                  " " + MRPKey + ", " + WorkLineNext + ", 1)";
                                            cmdIns = new SqlCommand(qry, conn);
                                            cmdIns.ExecuteNonQuery();
                                        }
                                        dtable3.Clear();
                                    }
                                }
                                dtable2.Clear();
                            }

                            bool msgSendToCreator = GlobalClass.IsMailSent(CreatorEmail, CreatorSubject, sCreatorBody.ToString());
                        }
                    }
                    dtable1.Clear();
                }
            }
            dtable.Clear();

            conn.Close();
        }

        private static void MRP_Submit_InventoryAnalyst(string docNum, int MRPKey, DateTime dteCreated, int WorkFlowLine, string EntCode, string BuCode)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            string qry = "";
            string CreatorEmail = "", CreatorSubject = "", CreatorGreetings = "";
            var sCreatorBody = new StringBuilder();
            string sEmail = "", sSubject = "", sGreetings = "";
            var sBody = new StringBuilder();

            int WorkLineNext = WorkFlowLine + 1;


            SqlCommand cmdIns = null;
            SqlCommand cmdUp = null;
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dtable = new DataTable();

            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;
            DataTable dtable1 = new DataTable();

            SqlCommand cmd2 = null;
            SqlDataAdapter adp2;
            DataTable dtable2 = new DataTable();

            SqlCommand cmd3 = null;
            SqlDataAdapter adp3;
            DataTable dtable3 = new DataTable();

            conn.Open();
            qry = "SELECT dbo.tbl_Users.Email, dbo.tbl_Users.Gender, dbo.tbl_Users.Lastname " +
                  " FROM dbo.tbl_MRP_List LEFT OUTER JOIN " +
                  " dbo.tbl_Users ON dbo.tbl_MRP_List.CreatorKey = dbo.tbl_Users.PK " +
                  " WHERE(dbo.tbl_MRP_List.PK = " + MRPKey + ")";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow row in dtable.Rows)
                {
                    CreatorEmail = EncryptionClass.Decrypt(row["Email"].ToString());
                    if (Convert.ToInt32(row["Gender"]) == 1)
                    {
                        CreatorGreetings = "Dear Mr. " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    }
                    else
                    {
                        CreatorGreetings = "Dear Ms. " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    }
                    CreatorSubject = "MOP DocNum " + docNum.ToString() + " status";

                    sCreatorBody.Append("<!DOCTYPE html>");
                    sCreatorBody.Append("<html>");
                    sCreatorBody.Append("<head>");
                    sCreatorBody.Append("</head>");
                    sCreatorBody.Append("<body>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>" + CreatorGreetings + ",</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>MOP Document # " + docNum.ToString() + " has been submitted to Finance(Budget) review/approval.</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 10px;font-style:italic;'>***This is a system-generated message. please do not reply to this email.***</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 10px;'>DISCLAIMER: This email is confidential and intended solely for the use of the individual to whom it is addressed. If you are not the intended recipient, be advised that you have received this email in error and that any use, dissemination, forwarding, printing or copying of this email is strictly prohibited. If you have received this email in error please notify the sender or email info@hijoresources.net, telephone number (082) 282-3662.</p>");
                    sCreatorBody.Append("</body>");
                    sCreatorBody.Append("</html>");
                }
            }
            dtable.Clear();

            qry = "SELECT TOP (1) PK " +
                  " FROM dbo.tbl_System_MOP_DataFlow " +
                  " WHERE(EffectDate <= '" + dteCreated + "') " +
                  " ORDER BY EffectDate DESC";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow row in dtable.Rows)
                {
                    qry = "SELECT dbo.tbl_System_Approval_Position.PositionName, " +
                          " dbo.tbl_System_Approval_Position.SQLQuery, " +
                          " dbo.tbl_System_MOP_DataFlow_Details.PositionNameKey " +
                          " FROM dbo.tbl_System_MOP_DataFlow_Details LEFT OUTER JOIN " +
                          " dbo.tbl_System_Approval_Position ON dbo.tbl_System_MOP_DataFlow_Details.PositionNameKey = dbo.tbl_System_Approval_Position.PK " +
                          " WHERE(dbo.tbl_System_MOP_DataFlow_Details.MasterKey = " + row["PK"] + ") " +
                          " AND(dbo.tbl_System_MOP_DataFlow_Details.Line = " + WorkLineNext + ")";
                    cmd1 = new SqlCommand(qry);
                    cmd1.Connection = conn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dtable1);
                    if (dtable1.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dtable1.Rows)
                        {
                            if (row1["SQLQuery"].ToString().Trim() != "")
                            {
                                qry = row1["SQLQuery"].ToString() + " '" + EntCode + "', '" + BuCode + "', '" + dteCreated + "'";
                                cmd2 = new SqlCommand(qry);
                                cmd2.Connection = conn;
                                adp2 = new SqlDataAdapter(cmd2);
                                adp2.Fill(dtable2);
                                if (dtable2.Rows.Count > 0)
                                {
                                    foreach (DataRow row2 in dtable2.Rows)
                                    {
                                        sEmail = EncryptionClass.Decrypt(row2["Email"].ToString());
                                        if (Convert.ToInt32(row2["Gender"]) == 1)
                                        {
                                            sGreetings = "Dear Mr. " + EncryptionClass.Decrypt(row2["Lastname"].ToString());
                                        }
                                        else
                                        {
                                            sGreetings = "Dear Ms. " + EncryptionClass.Decrypt(row2["Lastname"].ToString());
                                        }
                                        sSubject = "MOP DocNum " + docNum.ToString() + " is waiting for your review and approval";

                                        sBody.Append("<!DOCTYPE html>");
                                        sBody.Append("<html>");
                                        sBody.Append("<head>");
                                        sBody.Append("</head>");
                                        sBody.Append("<body>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>" + sGreetings + ",</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>MOP Document # " + docNum.ToString() + " is waiting for your review/approval.</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'><a href=" + GlobalClass.Email_Redirect() + ">Goto System</a></p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 10px;font-style:italic;'>***This is a system-generated message. please do not reply to this email.***</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 10px;'>DISCLAIMER: This email is confidential and intended solely for the use of the individual to whom it is addressed. If you are not the intended recipient, be advised that you have received this email in error and that any use, dissemination, forwarding, printing or copying of this email is strictly prohibited. If you have received this email in error please notify the sender or email info@hijoresources.net, telephone number (082) 282-3662.</p>");
                                        sBody.Append("</body>");
                                        sBody.Append("</html>");

                                        bool msgSend = GlobalClass.IsMailSent(sEmail, sSubject, sBody.ToString());

                                        //Update Workflow
                                        qry = "UPDATE tbl_MRP_List_Workflow " +
                                               " SET Visible = 0, " +
                                               " Status = 1 " +
                                               " WHERE (MasterKey = " + MRPKey + ") " +
                                               " AND (Line = " + WorkFlowLine + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Update MRP Status
                                        qry = "UPDATE tbl_MRP_List SET StatusKey = 2 WHERE (PK = " + MRPKey + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Update Workflow Next
                                        qry = "UPDATE tbl_MRP_List_Workflow " +
                                               " SET Visible = 1, " +
                                               " UserKey = " + row2["UserKey"] + ", " +
                                               " PositionNameKey = " + row1["PositionNameKey"] + " " +
                                               " WHERE (MasterKey = " + MRPKey + ") " +
                                               " AND (Line = " + WorkLineNext + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Update Assigned to me
                                        qry = "UPDATE tbl_Users_Assigned " +
                                               " SET Attended = 1 " +
                                               " WHERE (UserKey = " + row2["UserKey"] + ") " +
                                               " AND (MRPKey = " + MRPKey + ") " +
                                               " AND (WorkFlowLine = " + WorkFlowLine + ") " +
                                               " AND (WorkFlowType = 1)";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Insert to Assigned to me
                                        qry = "SELECT tbl_Users_Assigned.* " +
                                              " FROM tbl_Users_Assigned " +
                                              " WHERE (UserKey = " + Convert.ToInt32(row2["UserKey"]) + ") " +
                                              " AND (MRPKey = " + MRPKey + ") " +
                                              " AND (WorkFlowLine = " + WorkLineNext + ") " +
                                              " AND (WorkFlowType = 1)";
                                        cmd3 = new SqlCommand(qry);
                                        cmd3.Connection = conn;
                                        adp3 = new SqlDataAdapter(cmd3);
                                        adp3.Fill(dtable3);
                                        if (dtable3.Rows.Count == 0)
                                        {
                                            qry = "INSERT INTO tbl_Users_Assigned " +
                                                  " (UserKey, PositionNameKey, MRPKey, WorkFlowLine, WorkFlowType) " +
                                                  " VALUES(" + Convert.ToInt32(row2["UserKey"]) + ", " +
                                                  " " + Convert.ToInt32(row1["PositionNameKey"]) + ", " +
                                                  " " + MRPKey + ", " + WorkLineNext + ", 1)";
                                            cmdIns = new SqlCommand(qry, conn);
                                            cmdIns.ExecuteNonQuery();
                                        }
                                        dtable3.Clear();
                                    }
                                }
                                dtable2.Clear();
                            }

                            bool msgSendToCreator = GlobalClass.IsMailSent(CreatorEmail, CreatorSubject, sCreatorBody.ToString());
                        }
                    }
                    dtable1.Clear();
                }
            }
            dtable.Clear();

            conn.Close();
        }

        private static void MRP_Submit_FinanceBudget(string docNum, int MRPKey, DateTime dteCreated, int WorkFlowLine, string EntCode, string BuCode)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            string qry = "";
            string CreatorEmail = "", CreatorSubject = "", CreatorGreetings = "";
            var sCreatorBody = new StringBuilder();
            string sEmail = "", sSubject = "", sGreetings = "";
            var sBody = new StringBuilder();

            int WorkLineNext = WorkFlowLine + 1;


            SqlCommand cmdIns = null;
            SqlCommand cmdUp = null;
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dtable = new DataTable();

            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;
            DataTable dtable1 = new DataTable();

            SqlCommand cmd2 = null;
            SqlDataAdapter adp2;
            DataTable dtable2 = new DataTable();

            SqlCommand cmd3 = null;
            SqlDataAdapter adp3;
            DataTable dtable3 = new DataTable();

            conn.Open();
            qry = "SELECT dbo.tbl_Users.Email, dbo.tbl_Users.Gender, dbo.tbl_Users.Lastname " +
                  " FROM dbo.tbl_MRP_List LEFT OUTER JOIN " +
                  " dbo.tbl_Users ON dbo.tbl_MRP_List.CreatorKey = dbo.tbl_Users.PK " +
                  " WHERE(dbo.tbl_MRP_List.PK = " + MRPKey + ")";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow row in dtable.Rows)
                {
                    CreatorEmail = EncryptionClass.Decrypt(row["Email"].ToString());
                    if (Convert.ToInt32(row["Gender"]) == 1)
                    {
                        CreatorGreetings = "Dear Mr. " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    }
                    else
                    {
                        CreatorGreetings = "Dear Ms. " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    }
                    CreatorSubject = "MOP DocNum " + docNum.ToString() + " status";

                    sCreatorBody.Append("<!DOCTYPE html>");
                    sCreatorBody.Append("<html>");
                    sCreatorBody.Append("<head>");
                    sCreatorBody.Append("</head>");
                    sCreatorBody.Append("<body>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>" + CreatorGreetings + ",</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>MOP Document # " + docNum.ToString() + " has been submitted for Deliberation.</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 10px;font-style:italic;'>***This is a system-generated message. please do not reply to this email.***</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 10px;'>DISCLAIMER: This email is confidential and intended solely for the use of the individual to whom it is addressed. If you are not the intended recipient, be advised that you have received this email in error and that any use, dissemination, forwarding, printing or copying of this email is strictly prohibited. If you have received this email in error please notify the sender or email info@hijoresources.net, telephone number (082) 282-3662.</p>");
                    sCreatorBody.Append("</body>");
                    sCreatorBody.Append("</html>");
                }
            }
            dtable.Clear();

            qry = "SELECT TOP (1) PK " +
                  " FROM dbo.tbl_System_MOP_DataFlow " +
                  " WHERE(EffectDate <= '" + dteCreated + "') " +
                  " ORDER BY EffectDate DESC";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow row in dtable.Rows)
                {
                    qry = "SELECT dbo.tbl_System_Approval_Position.PositionName, " +
                          " dbo.tbl_System_Approval_Position.SQLQuery, " +
                          " dbo.tbl_System_MOP_DataFlow_Details.PositionNameKey " +
                          " FROM dbo.tbl_System_MOP_DataFlow_Details LEFT OUTER JOIN " +
                          " dbo.tbl_System_Approval_Position ON dbo.tbl_System_MOP_DataFlow_Details.PositionNameKey = dbo.tbl_System_Approval_Position.PK " +
                          " WHERE(dbo.tbl_System_MOP_DataFlow_Details.MasterKey = " + row["PK"] + ") " +
                          " AND(dbo.tbl_System_MOP_DataFlow_Details.Line = " + WorkLineNext + ")";
                    cmd1 = new SqlCommand(qry);
                    cmd1.Connection = conn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dtable1);
                    if (dtable1.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dtable1.Rows)
                        {
                            if (row1["SQLQuery"].ToString().Trim() != "")
                            {
                                qry = row1["SQLQuery"].ToString() + " '" + EntCode + "', '" + BuCode + "', '" + dteCreated + "'";
                                cmd2 = new SqlCommand(qry);
                                cmd2.Connection = conn;
                                adp2 = new SqlDataAdapter(cmd2);
                                adp2.Fill(dtable2);
                                if (dtable2.Rows.Count > 0)
                                {
                                    foreach (DataRow row2 in dtable2.Rows)
                                    {
                                        sEmail = EncryptionClass.Decrypt(row2["Email"].ToString());
                                        if (Convert.ToInt32(row2["Gender"]) == 1)
                                        {
                                            sGreetings = "Dear Mr. " + EncryptionClass.Decrypt(row2["Lastname"].ToString());
                                        }
                                        else
                                        {
                                            sGreetings = "Dear Ms. " + EncryptionClass.Decrypt(row2["Lastname"].ToString());
                                        }
                                        sSubject = "MOP DocNum " + docNum.ToString() + " is waiting for your review and approval";

                                        sBody.Append("<!DOCTYPE html>");
                                        sBody.Append("<html>");
                                        sBody.Append("<head>");
                                        sBody.Append("</head>");
                                        sBody.Append("<body>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>" + sGreetings + ",</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>MOP Document # " + docNum.ToString() + " is waiting for deliberation.</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'><a href=" + GlobalClass.Email_Redirect() + ">Goto System</a></p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 10px;font-style:italic;'>***This is a system-generated message. please do not reply to this email.***</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 10px;'>DISCLAIMER: This email is confidential and intended solely for the use of the individual to whom it is addressed. If you are not the intended recipient, be advised that you have received this email in error and that any use, dissemination, forwarding, printing or copying of this email is strictly prohibited. If you have received this email in error please notify the sender or email info@hijoresources.net, telephone number (082) 282-3662.</p>");
                                        sBody.Append("</body>");
                                        sBody.Append("</html>");

                                        bool msgSend = GlobalClass.IsMailSent(sEmail, sSubject, sBody.ToString());

                                        //Update Workflow
                                        qry = "UPDATE tbl_MRP_List_Workflow " +
                                               " SET Visible = 0, " +
                                               " Status = 1 " +
                                               " WHERE (MasterKey = " + MRPKey + ") " +
                                               " AND (Line = " + WorkFlowLine + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Update MRP Status
                                        qry = "UPDATE tbl_MRP_List SET StatusKey = 2 WHERE (PK = " + MRPKey + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Update Workflow Next
                                        qry = "UPDATE tbl_MRP_List_Workflow " +
                                               " SET Visible = 1, " +
                                               " UserKey = " + row2["UserKey"] + ", " +
                                               " PositionNameKey = " + row1["PositionNameKey"] + " " +
                                               " WHERE (MasterKey = " + MRPKey + ") " +
                                               " AND (Line = " + WorkLineNext + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Update Assigned to me
                                        qry = "UPDATE tbl_Users_Assigned " +
                                               " SET Attended = 1 " +
                                               " WHERE (UserKey = " + row2["UserKey"] + ") " +
                                               " AND (MRPKey = " + MRPKey + ") " +
                                               " AND (WorkFlowLine = " + WorkFlowLine + ") " +
                                               " AND (WorkFlowType = 1)";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Insert to Assigned to me
                                        qry = "SELECT tbl_Users_Assigned.* " +
                                              " FROM tbl_Users_Assigned " +
                                              " WHERE (UserKey = " + Convert.ToInt32(row2["UserKey"]) + ") " +
                                              " AND (MRPKey = " + MRPKey + ") " +
                                              " AND (WorkFlowLine = " + WorkLineNext + ") " +
                                              " AND (WorkFlowType = 1)";
                                        cmd3 = new SqlCommand(qry);
                                        cmd3.Connection = conn;
                                        adp3 = new SqlDataAdapter(cmd3);
                                        adp3.Fill(dtable3);
                                        if (dtable3.Rows.Count == 0)
                                        {
                                            qry = "INSERT INTO tbl_Users_Assigned " +
                                                  " (UserKey, PositionNameKey, MRPKey, WorkFlowLine, WorkFlowType) " +
                                                  " VALUES(" + Convert.ToInt32(row2["UserKey"]) + ", " +
                                                  " " + Convert.ToInt32(row1["PositionNameKey"]) + ", " +
                                                  " " + MRPKey + ", " + WorkLineNext + ", 1)";
                                            cmdIns = new SqlCommand(qry, conn);
                                            cmdIns.ExecuteNonQuery();
                                        }
                                        dtable3.Clear();
                                    }
                                }
                                dtable2.Clear();
                            }

                            bool msgSendToCreator = GlobalClass.IsMailSent(CreatorEmail, CreatorSubject, sCreatorBody.ToString());
                        }
                    }
                    dtable1.Clear();
                }
            }
            dtable.Clear();

            conn.Close();
        }

        private static void MRP_Submit_Deliberation(string docNum, int MRPKey, DateTime dteCreated, int WorkFlowLine, string EntCode, string BuCode)
        {
            SqlConnection conn = new SqlConnection(GlobalClass.SQLConnString());
            string qry = "";
            string CreatorEmail = "", CreatorSubject = "", CreatorGreetings = "";
            var sCreatorBody = new StringBuilder();
            string sEmail = "", sSubject = "", sGreetings = "";
            var sBody = new StringBuilder();

            int WorkLineNext = WorkFlowLine + 1;
            int ApprovalLine = 1;

            SqlCommand cmdIns = null;
            SqlCommand cmdUp = null;
            SqlCommand cmd = null;
            SqlDataAdapter adp;
            DataTable dtable = new DataTable();

            SqlCommand cmd1 = null;
            SqlDataAdapter adp1;
            DataTable dtable1 = new DataTable();

            SqlCommand cmd2 = null;
            SqlDataAdapter adp2;
            DataTable dtable2 = new DataTable();

            SqlCommand cmd3 = null;
            SqlDataAdapter adp3;
            DataTable dtable3 = new DataTable();

            conn.Open();
            qry = "SELECT dbo.tbl_Users.Email, dbo.tbl_Users.Gender, dbo.tbl_Users.Lastname " +
                  " FROM dbo.tbl_MRP_List LEFT OUTER JOIN " +
                  " dbo.tbl_Users ON dbo.tbl_MRP_List.CreatorKey = dbo.tbl_Users.PK " +
                  " WHERE(dbo.tbl_MRP_List.PK = " + MRPKey + ")";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow row in dtable.Rows)
                {
                    CreatorEmail = EncryptionClass.Decrypt(row["Email"].ToString());
                    if (Convert.ToInt32(row["Gender"]) == 1)
                    {
                        CreatorGreetings = "Dear Mr. " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    }
                    else
                    {
                        CreatorGreetings = "Dear Ms. " + EncryptionClass.Decrypt(row["Lastname"].ToString());
                    }
                    CreatorSubject = "MOP DocNum " + docNum.ToString() + " status";

                    sCreatorBody.Append("<!DOCTYPE html>");
                    sCreatorBody.Append("<html>");
                    sCreatorBody.Append("<head>");
                    sCreatorBody.Append("</head>");
                    sCreatorBody.Append("<body>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>" + CreatorGreetings + ",</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>MOP Document # " + docNum.ToString() + " has been submitted for SCM Lead Approval.</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 10px;font-style:italic;'>***This is a system-generated message. please do not reply to this email.***</p>");
                    sCreatorBody.Append("<p style='font-family:Tahoma; font-size: 10px;'>DISCLAIMER: This email is confidential and intended solely for the use of the individual to whom it is addressed. If you are not the intended recipient, be advised that you have received this email in error and that any use, dissemination, forwarding, printing or copying of this email is strictly prohibited. If you have received this email in error please notify the sender or email info@hijoresources.net, telephone number (082) 282-3662.</p>");
                    sCreatorBody.Append("</body>");
                    sCreatorBody.Append("</html>");
                }
            }
            dtable.Clear();

            //Update Workflow After Deliberation
            qry = "UPDATE tbl_MRP_List_Workflow " +
                   " SET Visible = 0, " +
                   " Status = 1 " +
                   " WHERE (MasterKey = " + MRPKey + ") " +
                   " AND (Line = " + WorkFlowLine + ")";
            cmdUp = new SqlCommand(qry, conn);
            cmdUp.ExecuteNonQuery();

            //Update MRP Status
            qry = "UPDATE tbl_MRP_List SET StatusKey = 3 WHERE (PK = " + MRPKey + ")";
            cmdUp = new SqlCommand(qry, conn);
            cmdUp.ExecuteNonQuery();

            //Update Workflow Approval
            qry = "UPDATE tbl_MRP_List_Workflow " +
                   " SET Visible = 1, " +
                   " Status = 0 " +
                   " WHERE (MasterKey = " + MRPKey + ") " +
                   " AND (Line = " + WorkLineNext + ")";
            cmdUp = new SqlCommand(qry, conn);
            cmdUp.ExecuteNonQuery();

            // Approval
            qry = "SELECT TOP (1) PK " +
                  " FROM dbo.tbl_System_Approval " +
                  " WHERE(EffectDate <= '" + dteCreated + "') " +
                  " ORDER BY EffectDate DESC";
            cmd = new SqlCommand(qry);
            cmd.Connection = conn;
            adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow row in dtable.Rows)
                {
                    //qry = "SELECT dbo.tbl_System_Approval_Position.PositionName, " +
                    //      " dbo.tbl_System_Approval_Position.SQLQuery, " +
                    //      " dbo.tbl_System_Approval_Details.PositionNameKey " +
                    //      " FROM dbo.tbl_System_Approval_Details LEFT OUTER JOIN " +
                    //      " dbo.tbl_System_Approval_Position ON dbo.tbl_System_Approval_Details.PositionNameKey = dbo.tbl_System_Approval_Position.PK " +
                    //      " WHERE(dbo.tbl_System_Approval_Details.MasterKey = "+ row["PK"] +") " +
                    //      " AND(dbo.tbl_System_Approval_Details.Line = "+ ApprovalLine + ")";
                    qry = "SELECT dbo.tbl_System_Approval_Position.PositionName, " +
                          " dbo.tbl_System_Approval_Position.SQLQuery, " +
                          " dbo.tbl_System_Approval_Details.PositionNameKey, " +
                          " dbo.tbl_System_Approval_Details.Line " +
                          " FROM dbo.tbl_System_Approval_Details LEFT OUTER JOIN " +
                          " dbo.tbl_System_Approval_Position ON dbo.tbl_System_Approval_Details.PositionNameKey = dbo.tbl_System_Approval_Position.PK " +
                          " WHERE(dbo.tbl_System_Approval_Details.MasterKey = " + row["PK"] + ") " +
                          " ORDER BY dbo.tbl_System_Approval_Details.Line";
                    cmd1 = new SqlCommand(qry);
                    cmd1.Connection = conn;
                    adp1 = new SqlDataAdapter(cmd1);
                    adp1.Fill(dtable1);
                    if (dtable1.Rows.Count > 0)
                    {
                        foreach (DataRow row1 in dtable1.Rows)
                        {
                            
                            if (row1["SQLQuery"].ToString().Trim() != "")
                            {
                                ApprovalLine = Convert.ToInt32(row1["Line"]);

                                qry = row1["SQLQuery"].ToString() + " '" + EntCode + "', '" + BuCode + "', '" + dteCreated + "'";
                                cmd2 = new SqlCommand(qry);
                                cmd2.Connection = conn;
                                adp2 = new SqlDataAdapter(cmd2);
                                adp2.Fill(dtable2);
                                if (dtable2.Rows.Count > 0)
                                {
                                    foreach (DataRow row2 in dtable2.Rows)
                                    {
                                        sEmail = EncryptionClass.Decrypt(row2["Email"].ToString());
                                        if (Convert.ToInt32(row2["Gender"]) == 1)
                                        {
                                            sGreetings = "Dear Mr. " + EncryptionClass.Decrypt(row2["Lastname"].ToString());
                                        }
                                        else
                                        {
                                            sGreetings = "Dear Ms. " + EncryptionClass.Decrypt(row2["Lastname"].ToString());
                                        }
                                        sSubject = "MOP DocNum " + docNum.ToString() + " is waiting for your review and approval";

                                        sBody.Append("<!DOCTYPE html>");
                                        sBody.Append("<html>");
                                        sBody.Append("<head>");
                                        sBody.Append("</head>");
                                        sBody.Append("<body>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>" + sGreetings + ",</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'>MOP Document # " + docNum.ToString() + " is waiting for your approval.</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 12px;'><a href=" + GlobalClass.Email_Redirect() + ">Goto System</a></p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 10px;font-style:italic;'>***This is a system-generated message. please do not reply to this email.***</p>");
                                        sBody.Append("<p style='font-family:Tahoma; font-size: 10px;'>DISCLAIMER: This email is confidential and intended solely for the use of the individual to whom it is addressed. If you are not the intended recipient, be advised that you have received this email in error and that any use, dissemination, forwarding, printing or copying of this email is strictly prohibited. If you have received this email in error please notify the sender or email info@hijoresources.net, telephone number (082) 282-3662.</p>");
                                        sBody.Append("</body>");
                                        sBody.Append("</html>");

                                        bool msgSend = GlobalClass.IsMailSent(sEmail, sSubject, sBody.ToString());

                                        //Update Approval
                                        qry = "UPDATE tbl_MRP_List_Approval " +
                                               " SET Visible = 1, " +
                                               " UserKey = " + row2["UserKey"] + " " +
                                               " WHERE (MasterKey = " + MRPKey + ") " +
                                               " AND (Line = " + ApprovalLine + ")";
                                        cmdUp = new SqlCommand(qry, conn);
                                        cmdUp.ExecuteNonQuery();

                                        //Insert to Assigned to me
                                        qry = "SELECT tbl_Users_Assigned.* " +
                                              " FROM tbl_Users_Assigned " +
                                              " WHERE (UserKey = " + Convert.ToInt32(row2["UserKey"]) + ") " +
                                              " AND (MRPKey = " + MRPKey + ") " +
                                              " AND (WorkFlowLine = " + ApprovalLine + ") " +
                                              " AND (WorkFlowType = 2)";
                                        cmd3 = new SqlCommand(qry);
                                        cmd3.Connection = conn;
                                        adp3 = new SqlDataAdapter(cmd3);
                                        adp3.Fill(dtable3);
                                        if (dtable3.Rows.Count == 0)
                                        {
                                            qry = "INSERT INTO tbl_Users_Assigned " +
                                                  " (UserKey, PositionNameKey, MRPKey, WorkFlowLine, WorkFlowType) " +
                                                  " VALUES(" + Convert.ToInt32(row2["UserKey"]) + ", " +
                                                  " " + Convert.ToInt32(row1["PositionNameKey"]) + ", " +
                                                  " " + MRPKey + ", " + ApprovalLine + ", 2)";
                                            cmdIns = new SqlCommand(qry, conn);
                                            cmdIns.ExecuteNonQuery();
                                        }
                                        dtable3.Clear();
                                    }
                                }
                                dtable2.Clear();
                            }

                            bool msgSendToCreator = GlobalClass.IsMailSent(CreatorEmail, CreatorSubject, sCreatorBody.ToString());
                        }
                    }
                    dtable1.Clear();
                }
            }
            dtable.Clear();

            conn.Close();
        }

    }
}