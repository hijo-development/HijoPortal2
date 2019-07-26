using DevExpress.Web;
using HijoPortal.classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HijoPortal
{
    public partial class hlsSOA_Add : System.Web.UI.Page
    {
        private void CheckCreatorKey()
        {
            if (Session["CreatorKey"] == null)
            {
                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback(MRPClass.DefaultPage());
                else
                    Response.Redirect("default.aspx");
                return;
            }
        }

        //private void LoadSOADetails(string sSOANum)
        //{

        //}

        private void LoadAddSOA(string sTrans, string sYear, string sWeekNum, string sCustCode, string sSOANum, string sUserKey)
        {
            DataTable dt = HLSSOA.Customer_Info(sCustCode);
            if (dt.Rows.Count> 0)
            {
                foreach (DataRow row in dt.Rows )
                {
                    txtCustCode.Text = row["CustCode"].ToString();
                    txtCustomer.Text = row["CustName"].ToString();
                    txtCustomerAdd.Text = row["CustAdd"].ToString();
                    txtAttention.Text = row["CustAtt"].ToString();
                    txtAttentionNum.Text = row["CustAttNum"].ToString();
                }
            }
            dt.Clear();

            DataTable dt1 = HLSSOA.SOA_Footer(sSOANum, sTrans);
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow row in dt1.Rows)
                {
                    txtDate.Text = row["SOADate"].ToString();
                    txtYear.Text = row["Year"].ToString();
                    txtWeekNum.Text = row["WeekNum"].ToString();
                    txtSOANumber.Text = row["SOANum"].ToString();
                    txtRemarks.Text = row["Remarks"].ToString();
                    txtPreparedBy.Text = row["PreparedBy"].ToString();
                    txtPreparedByPost.Text = row["PreparedByPost"].ToString();
                    txtCheckedBy.Text = row["CheckedBy"].ToString();
                    txtCheckedByPost.Text = row["CheckedByPost"].ToString();
                    txtApprovedBy.Text = row["ApprovedBy"].ToString();
                    txtApprovedByPost.Text = row["ApprovedByPost"].ToString();
                }
            }
            dt1.Clear();

            HLSWaybill.DataSource = HLSSOA.HLSSOA_Details(sSOANum, sUserKey, sCustCode, sYear, sWeekNum);
            HLSWaybill.KeyFieldName = "Waybill";
            HLSWaybill.DataBind();

            switch (sTrans)
            {
                case "Add":
                    {
                        
                        Page.Title = "HLS - SOA (Add)";
                        HLSSOATitle.InnerText = "HLS - Statement Of Account (Add)";
                        txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                        txtYear.Text = sYear;
                        txtWeekNum.Text = sWeekNum;
                        txtRemarks.Text = "This is to bill the trucking services rendered for (Commodity: Fresh Bananas) Wk" + sWeekNum + ", details hereunder as follows:";
                        txtDate.ReadOnly = true; 
                        txtYear.ReadOnly = true;
                        txtWeekNum.ReadOnly = true;
                        txtCustomer.ReadOnly = true;
                        txtCustomerAdd.ReadOnly = true;
                        txtSOANumber.ReadOnly = false;
                        txtAttention.ReadOnly = false;
                        txtAttentionNum.ReadOnly = false;
                        txtRemarks.ReadOnly = false;
                        txtPreparedBy.ReadOnly = false;
                        txtPreparedByPost.ReadOnly = false;
                        txtCheckedBy.ReadOnly = false;
                        txtCheckedByPost.ReadOnly = false;
                        txtApprovedBy.ReadOnly = false;
                        txtApprovedByPost.ReadOnly = false;
                        BtnSaveSOA.Text = "S A V E";
                        //BtnSaveSOA.Enabled = true;

                        HLSWaybillList.DataSource = HLSSOA.SOAWaybill_for_Save(sUserKey);
                        HLSWaybillList.KeyFieldName = "Waybill";
                        HLSWaybillList.DataBind();
                        

                        break;
                    }
                case "View":
                    {
                        Page.Title = "HLS - SOA (View)";
                        HLSSOATitle.InnerText = "HLS - Statement Of Account (View)";
                        txtDate.ReadOnly = true;
                        txtYear.ReadOnly = true;
                        txtWeekNum.ReadOnly = true;
                        txtCustomer.ReadOnly = true;
                        txtCustomerAdd.ReadOnly = true;
                        txtSOANumber.ReadOnly = true;
                        txtAttention.ReadOnly = true;
                        txtAttentionNum.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtPreparedBy.ReadOnly = true;
                        txtPreparedByPost.ReadOnly = true;
                        txtCheckedBy.ReadOnly = true;
                        txtCheckedByPost.ReadOnly = true;
                        txtApprovedBy.ReadOnly = true;
                        txtApprovedByPost.ReadOnly = true;
                        BtnSaveSOA.Text = "P R I N T";
                        //BtnSaveSOA.Enabled = false;

                        //HLSWaybillList.DataSource = HLSSOA.SOAWaybill_for_Save(sUserKey);
                        //HLSWaybillList.KeyFieldName = "Waybill";
                        //HLSWaybillList.DataBind();

                        break;
                    }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            CheckCreatorKey();
            if (!Page.IsPostBack)
            {
                LoadAddSOA(Session["HLS_Trans"].ToString(), Session["HLS_Add_Year"].ToString(), Session["HLS_Add_WeekNum"].ToString(), Session["HLS_Add_CustCode"].ToString(), Session["HLS_Add_SOANum"].ToString(), Session["CreatorKey"].ToString());
            }
        }

        protected void BtnBackSOA_Click(object sender, EventArgs e)
        {
            Response.Redirect("hlsSOA.aspx");
        }

        protected void BtnSaveSOA_Click(object sender, EventArgs e)
        {
            if (BtnSaveSOA.Text == "S A V E")
            {
                HLSSOA.Save_HLSSOA(txtSOANumber.Text, Convert.ToDateTime(txtDate.Text), Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtWeekNum.Text), txtCustCode.Text, txtRemarks.Text, txtPreparedBy.Text, txtPreparedByPost.Text, txtCheckedBy.Text, txtCheckedByPost.Text, txtApprovedBy.Text, txtApprovedByPost.Text, HLSWaybillList, txtAttention.Text, txtAttentionNum.Text);

                Response.Redirect("hlsSOA.aspx");
            }
            

        }
    }
}