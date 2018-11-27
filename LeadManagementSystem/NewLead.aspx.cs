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

public partial class NewLead : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    LeadBL leadBL = new LeadBL();
    LeadEntity leadEntity = new LeadEntity();
    CommanClass _objComman = new CommanClass();
    ConsultantBL consultantBL = new ConsultantBL();
    FollowupEntity followupEntity = new FollowupEntity();
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();
    int j = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GetSourceData("U");
                others.Visible = false;
                consultantAction.Visible = false;
                GetConsultants();
                GetProducts();
            }
        }
        catch
        {

        }
    }
    protected void GetProducts()
    {
        try
        {
            dataset = leadBL.GetProduct();
            ddlPackage.DataSource = dataset;
            ddlPackage.DataTextField = "ProductType";
            ddlPackage.DataValueField = "ProductTypeID";
            ddlPackage.DataBind();
            ddlPackage.Items.Insert(0, new ListItem("--Select Product --", "-1"));
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void GetSourceData(string Opeartion)
    {
        try
        {
            DataSet dsSouceData = new DataSet();
            dsSouceData = leadBL.GetSourceData(Opeartion);
            ddlSource.DataSource = dsSouceData;
            ddlSource.DataTextField = "SourceType";
            ddlSource.DataValueField = "SourceTypeID";
            ddlSource.DataBind();
            ddlSource.Items.Insert(0, new ListItem("--Select Source --", "-1"));
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSource.SelectedValue == "10")
        {
            txtOthers.Text = "";
            others.Visible = true;
        }
        else
        {
            txtOthers.Text = "";
            others.Visible = false;
        }
    }
    protected void GetConsultants()
    {
        try
        {
            DataSet dsConsultantsAction = new DataSet();
            dsConsultantsAction = consultantBL.GetConsultants(0);
            ddlConsultantsAction.DataSource = dsConsultantsAction;
            ddlConsultantsAction.DataTextField = "Name";
            ddlConsultantsAction.DataValueField = "ConsultantID";
            ddlConsultantsAction.DataBind();
            ddlConsultantsAction.Items.Insert(0, new ListItem("--Select Consultant --", "-1"));

            ViewState["consultData"] = dsConsultantsAction;
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void ddlSendEmail_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSendEmail.SelectedValue == "3")
        {
            consultantAction.Visible = true;
            GetConsultants();
        }
        else if (ddlSendEmail.SelectedValue == "1")
        {
            consultantAction.Visible = false;
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
                    Subject = "New Lead has come.";
                    MailCc = "";
                    MailTo = "consultants@serendipitytours.co.za";
                    //MailTo = "karen@serendipitytours.co.za";
                    MailText = "Hi " + "Team" + ", <br/><br/><br/>";
                    MailText += "A new lead has come, needs to be actioned. <br/><br/>";
                    MailText += "Assigned by : <strong>" + Session["Name"].ToString() + "</strong>";
                    CommanClass.UpdateMail(SmtpServer, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo, DisplayNameTo, MailCc, "", "", "", DisplayNameCc, MailBcc, Subject, MailText, Attachment);
                }
                catch
                { }
            }
        }
        else
        {
            consultantAction.Visible = false;
        }
    }
    private void Clear()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmail.Text = "";
        txtMobile.Text = "";
        txtSource.Text = "";
        txtDestination.Text = "";
        txtDepart.Text = "";
        txtReturnDate.Text = "";
        ddlAdults.SelectedValue = "1";
        ddlChild.SelectedValue = "0";
        ddlInfant.SelectedValue = "0";
        ddlPackage.SelectedValue = "-1";
        txtBudget.Text = "0";
        txtNotes.Text = "";
        ddlSource.SelectedValue = "-1";
        ddlSendEmail.SelectedValue = "-1";
        consultantAction.Visible = false;
        ddlConsultantsAction.SelectedValue = "-1";
        lblMessage.Text = "";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnAddNewLead_Click(object sender, EventArgs e)
    {
        try
        {
            int a = Convert.ToInt32(ddlAdults.SelectedValue);
            int c = Convert.ToInt32(ddlChild.SelectedValue);
            int i = Convert.ToInt32(ddlInfant.SelectedValue);
            j = a + c + i;
            if (i <= a)
            {
                if (a == 9)
                {
                    if (ddlChild.SelectedIndex != 0 || ddlInfant.SelectedIndex != 0)
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "No of Pax should not exceed 9";
                }
                else
                {
                    if (j > 9)
                    {
                        lblMessage.Text = "No of Pax should not exceed 9";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        leadEntity.SourceID = Convert.ToInt32(ddlSource.SelectedValue);
                        leadEntity.SourceRef = ddlSource.SelectedItem.Text;
                        leadEntity.Others = txtOthers.Text;
                        leadEntity.AssignedTo = 0;
                        leadEntity.AssignedBy = 0;
                        leadEntity.FirstName = txtFirstName.Text;
                        leadEntity.LastName = txtLastName.Text;
                        leadEntity.Mobile = txtMobile.Text;
                        leadEntity.Email = txtEmail.Text;
                        leadEntity.OriginName = txtSource.Text;
                        leadEntity.DestinationName = txtDestination.Text;
                        leadEntity.DepartureDate = txtDepart.Text;
                        leadEntity.ReturnDate = txtReturnDate.Text;
                        leadEntity.Adult = a;
                        leadEntity.Child = c;
                        leadEntity.Infant = i;
                        leadEntity.ProductType = Convert.ToInt32(ddlPackage.SelectedValue);
                        leadEntity.Budget = Convert.ToDecimal(txtBudget.Text);
                        leadEntity.Notes = txtNotes.Text;
                        leadEntity.QuotedPrice = 0;
                        leadEntity.FinalPrice = 0;
                        leadEntity.UpdatedBy = 0;
                        leadEntity.LeadStatus = 2;
                        leadEntity.FollowupDate = "";
                        leadEntity.FollowupDesc = "";
                        leadEntity.LeadDescription = "";
                        leadEntity.CreatedBy = Convert.ToInt32(Session["ConsultantID"].ToString());
                        leadEntity.PackageId = "";
                        leadEntity.ProductID = "";
                        leadEntity.WebsiteConsultantNotes = "";
                        int result = leadBL.CUDLead(leadEntity, 'I');
                        if (result > 0)
                        {
                            leadEntity.AssignedBy = Convert.ToInt32(Session["ConsultantID"].ToString());
                            if (ddlSendEmail.SelectedValue == "3")
                            {
                                string Email = string.Empty;
                                leadEntity.AssignedTo = Convert.ToInt32(ddlConsultantsAction.SelectedValue);
                                leadEntity.LeadStatus = 10;
                                DataSet data = (DataSet)ViewState["consultData"];
                                DataTable selectedTable = data.Tables[0].AsEnumerable()
                                                .Where(r => r.Field<int>("ConsultantID") == Convert.ToInt32(ddlConsultantsAction.SelectedValue))
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
                            else if (ddlSendEmail.SelectedValue == "2")
                            {
                                leadEntity.AssignedTo = Convert.ToInt32(Session["ConsultantID"].ToString());
                                leadEntity.LeadStatus = 10;
                            }
                            else
                            {
                                leadEntity.AssignedTo = 0;
                                leadEntity.LeadStatus = 2;
                            }

                            leadEntity.LeadID = result;
                            int resultAssigned = leadBL.LeadAction(leadEntity);
                            if (resultAssigned == 1)
                            {
                                lblMessage.Text = "Lead Details saved Successfully!";
                                lblMessage.ForeColor = System.Drawing.Color.Green;
                                if (ddlSendEmail.SelectedValue == "2" || ddlSendEmail.SelectedValue == "3")
                                {
                                    Clear();
                                    Response.Redirect("AssignedLeads.aspx");
                                }
                                else
                                {
                                    Clear();
                                    Response.Redirect("UnAssignedLeads.aspx");
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Please try again!";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Please try again!";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            else
            {
                lblMessage.Text = "No of infants should not exceed adults";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}