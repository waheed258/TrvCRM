using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

public partial class LeadAction : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    LeadBL leadBL = new LeadBL();
    LeadEntity leadEntity = new LeadEntity();
    CommanClass _objComman = new CommanClass();
    ConsultantBL consultantBL = new ConsultantBL();
    FollowupEntity followupEntity = new FollowupEntity();
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();
    BasicDropdownBL _objBasicDropdownBL = new BasicDropdownBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            consultant.Visible = false;
            GetAssigLeadOptions();
        }
    }
    protected void ddlAssignLead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAssignLead.SelectedValue == "1")
        {
            consultant.Visible = true;
            GetConsultants();
        }
        else
        {
            consultant.Visible = false;
        }
    }
    public void GetAssigLeadOptions()
    {
        try
        {
            DataSet dsGetActions = new DataSet();
            dsGetActions = _objBasicDropdownBL.GetAssigLeadOptions();
            ddlAssignLead.DataSource = dsGetActions;
            ddlAssignLead.DataTextField = "LeadStatus";
            ddlAssignLead.DataValueField = "LeadStatusID";
            ddlAssignLead.DataBind();
            ddlAssignLead.Items.Insert(0, new ListItem("--Select Option--", "-1"));
        }
        catch
        {

        }
    }
    protected void GetConsultants()
    {
        try
        {
            DataSet dsConsultants = new DataSet();
            dsConsultants = consultantBL.GetConsultants(0);
            ddlConsultants.DataSource = dsConsultants;
            ddlConsultants.DataTextField = "Name";
            ddlConsultants.DataValueField = "ConsultantID";
            ddlConsultants.DataBind();
            ddlConsultants.Items.Insert(0, new ListItem("--Select Consultant --", "-1"));

            ViewState["consultData"] = dsConsultants;
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnSubmitAssign_Click(object sender, EventArgs e)
    {
        leadEntity.AssignedBy = Convert.ToInt32(Session["ConsultantID"].ToString());
        if (ddlAssignLead.SelectedValue == "1")
        {
            string Email = string.Empty;
            leadEntity.AssignedTo = Convert.ToInt32(ddlConsultants.SelectedValue);
            DataSet data = (DataSet)ViewState["consultData"];

            DataTable selectedTable = data.Tables[0].AsEnumerable()
                            .Where(r => r.Field<int>("ConsultantID") == Convert.ToInt32(ddlConsultants.SelectedValue))
                            .CopyToDataTable();
            Email = selectedTable.Rows[0]["Email"].ToString();
            string Name = selectedTable.Rows[0]["Name"].ToString();

            DataSet ds = leadBL.GetMailInfo();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string SmtpServer = ds.Tables[0].Rows[0]["con_smtp_host"].ToString();
                int SmtpPort = Convert.ToInt32(ds.Tables[0].Rows[0]["con_smtp_port"].ToString());
                string MailFrom = ds.Tables[0].Rows[0]["con_mail_from"].ToString();
                string DisplayNameFrom = ds.Tables[0].Rows[0]["con_from_name"].ToString();
                string FromPassword = ds.Tables[0].Rows[0]["con_from_pwd"].ToString();
                string MailTo = string.Empty;
                string DisplayNameTo = string.Empty;
                string MailCc = string.Empty;
                string DisplayNameCc = string.Empty;
                string MailBcc = string.Empty;
                string Subject = string.Empty;
                string MailText = string.Empty;
                string Attachment = string.Empty;

                try
                {
                    Subject = "New Lead Assigned to you.";
                    MailCc = "";
                    MailTo = Email;
                    MailText = "Hi " + Name + ", <br/><br/><br/>";
                    MailText += "A new lead assigned to you, needs to be actioned. <br/><br/>";
                    MailText += "Assigned by : <strong>" + Session["Name"].ToString() + "</strong>";
                    CommanClass.UpdateMail(SmtpServer, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo, DisplayNameTo, MailCc, "", "", "", DisplayNameCc, MailBcc, Subject, MailText, Attachment);
                }
                catch
                { }
            }

        }
        else if (ddlAssignLead.SelectedValue == "2")
        {
            leadEntity.AssignedTo = Convert.ToInt32(Session["ConsultantID"].ToString());
        }
        else
        {
            leadEntity.AssignedTo = 0;
        }
        leadEntity.LeadStatus = 10;
        leadEntity.LeadID = Convert.ToInt32(Session["lsID"].ToString());
        int result = leadBL.LeadAction(leadEntity);
        if (result == 1)
        {
            if (Request.QueryString["type"] == "1")
                Response.Redirect("UnAssignedLeads.aspx");
            else
                Response.Redirect("AssignedLeads.aspx");
        }
        else
        {
            lblMessage.Text = "Please try again!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnBackAssign_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["type"] == "1")
            Response.Redirect("UnAssignedLeads.aspx");
        else
            Response.Redirect("AssignedLeads.aspx");
    }
}