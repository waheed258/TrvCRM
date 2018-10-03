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
public partial class Lead : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    LeadBL leadBL = new LeadBL();
    LeadEntity leadEntity = new LeadEntity();
    CommanClass _objComman = new CommanClass();
    ConsultantBL consultantBL = new ConsultantBL();
    FollowupEntity followupEntity = new FollowupEntity();
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();

    int j = 0;


    string ClientName = string.Empty;
    string product = string.Empty;
    string source = string.Empty;
    string toCity = string.Empty;
    string Email = string.Empty;
    string encryptedparamleadid = string.Empty;
    string encryptedparamlblProductID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["t"] == null)
                {
                    GetProducts();
                    GetSourceData("I");
                    others.Visible = false;
                    dvEOthers.Visible = false;
                    GetLeadsList();
                    GetAssinedLeadsList();
                    GetReminders();
                    newlead.Visible = false;
                    btnUpdate.Visible = false;
                    _objComman.GetAssigLeadOptions(ddlAssignLead);
                    consultant.Visible = false;
                    actions.Visible = false;
                    followupdate.Visible = false;
                    desc.Visible = false;
                    dvEdit.Visible = false;
                    consultantAction.Visible = false;
                }
                else
                {
                    GetLeadsList();
                    GetAssinedLeadsList();
                    GetReminders();
                    actions.Visible = false;
                    GetSourceDataEdit("U");
                    MailMessage.Text = "";
                    _objComman.GetStatus(ddlStatus);
                    newlead.Visible = false;
                    LeadList.Visible = false;
                    imgbtnAddLead.Visible = false;
                    dvEdit.Visible = true;
                    string strStatusId = Session["strStatusId"].ToString();
                    DataSet ds = leadBL.GetLeadInfo(Convert.ToInt32(Request.QueryString["idq"].ToString()));
                    DataTable dtLead = ds.Tables[0];
                    DataTable dtLeadHistory = ds.Tables[1];
                    LeadHistory(dtLeadHistory);
                    GetTemplateNames(Request.QueryString["idq"].ToString());
                    if (dtLead.Rows.Count > 0)
                    {
                        txtEFirstName.Text = dtLead.Rows[0]["lsFirstName"].ToString();
                        txtELastName.Text = dtLead.Rows[0]["lsLastName"].ToString();
                        txtEMobile.Text = dtLead.Rows[0]["lsPhone"].ToString();
                        txtEEmail.Text = dtLead.Rows[0]["lsEmailId"].ToString();
                        txtEDepart.Text = String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["lsDepartureDate"]);
                        txtEReturn.Text = String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["lsReturnDate"]);

                        lblLName.Text = string.Format("{0} {1}", dtLead.Rows[0]["lsFirstName"].ToString(), dtLead.Rows[0]["lsLastName"].ToString());
                        lblLEmail.Text = dtLead.Rows[0]["lsEmailId"].ToString();
                        lblLDates.Text = string.Format("{0} / {1}", String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["lsDepartureDate"]), String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["lsReturnDate"]));
                        lblLBudget.Text = dtLead.Rows[0]["lsBudget"].ToString();
                        lblLPhone.Text = dtLead.Rows[0]["lsPhone"].ToString();
                        if (dtLead.Rows[0]["lsProductId"].ToString() == "" || dtLead.Rows[0]["lsProductId"].ToString() == null)
                        {
                            lnkUrl.Text = "Lead not from Serendipity website";
                        }
                        else
                        {
                            lnkUrl.Text = "http://serendipitytravel.co.za/tour-detail.aspx?pid=" + dtLead.Rows[0]["lsProductId"].ToString();
                            lnkUrl.NavigateUrl = "http://serendipitytravel.co.za/tour-detail.aspx?pid=" + dtLead.Rows[0]["lsProductId"].ToString();
                        }
                        lblLNotes.Text = dtLead.Rows[0]["lsNotes"].ToString();

                        txtClientFileId.Text = dtLead.Rows[0]["lsClientFileId"].ToString();
                        txtEConsultNotes.Text = dtLead.Rows[0]["lsConsultantNotes"].ToString();
                        txtEReminder.Text = String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["lsReminder"]);
                        txtERemindNotes.Text = dtLead.Rows[0]["lsReminderNotes"].ToString();

                        txtToEmail.Text = dtLead.Rows[0]["lsEmailId"].ToString();
                        txtEmailSubject.Text = "Serendipity Tours >> More Info Required";

                        ddlESource.SelectedValue = dtLead.Rows[0]["lsSource"].ToString();
                        if (ddlESource.SelectedValue == "10")
                        {
                            dvEOthers.Visible = true;
                        }

                        txtEOthers.Text = dtLead.Rows[0]["lsOthersInfo"].ToString();

                        ddlStatus.SelectedValue = strStatusId;

                        if (strStatusId == "6")
                        {
                            // Client File Id TextBox Show
                            dvClientFileId.Visible = true;
                            txtClientFileId.Text = dtLead.Rows[0]["lsClientFileId"].ToString();
                        }
                        else
                        {
                            txtClientFileId.Text = "";
                            dvClientFileId.Visible = false;
                        }

                        if (ddlStatus.SelectedValue == "4")
                        {
                            if (Convert.ToInt32(dtLead.Rows[0]["Followups"]) == 3)
                            {
                                message.Text = "Maximum follow ups reached!";
                                message.ForeColor = System.Drawing.Color.Red;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            }
                            else
                            {
                                lblFollowup.Text = String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["FollowupDate"]);
                                followupdate.Visible = true;
                            }
                            followupdate.Visible = true;
                            lblFollowup.ForeColor = System.Drawing.Color.Red;
                            lblFollowup.Text = "The last follow up date was : " + Convert.ToDateTime(dtLead.Rows[0]["FollowupDate"].ToString()).Date.ToString("dd-MM-yyyy"); ;
                        }
                        else
                        {
                            lblFollowup.Text = "";
                            followupdate.Visible = false;
                        }

                    }

                    if (ddlStatus.SelectedValue == "2")
                    {
                        desc.Visible = false;
                    }
                    else
                    {
                        desc.Visible = true;
                    }
                }

            }
        }
        catch { }
    }
    protected void GetLeadsList()
    {
        try
        {
            //gvLeadList.PageSize = Convert.ToInt32(DropPage.SelectedValue);
            dataset = leadBL.GetLeadsList(0);
            if (dataset.Tables[0].Rows.Count > 0)
            {
                gvLeadList.DataSource = dataset;
                gvLeadList.DataBind();
                gvLeadList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }


        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void GetReminders()
    {
        try
        {
            //gvLeadList.PageSize = Convert.ToInt32(DropPage.SelectedValue);
            dataset = leadBL.GetReminders();
            if (dataset.Tables[0].Rows.Count > 0)
            {
                gvReminders.DataSource = dataset;
                gvReminders.DataBind();
                gvReminders.HeaderRow.TableSection = TableRowSection.TableHeader;
            }


        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void GetAssinedLeadsList()
    {
        try
        {
            dataset = leadBL.GetAssignedLeadsList(0);
            if (dataset.Tables[0].Rows.Count > 0)
            {
                gvAssignedList.DataSource = dataset;
                gvAssignedList.DataBind();
                gvAssignedList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void GetSourceData(string Opeartion)
    {
        try
        {
            dataset = leadBL.GetSourceData(Opeartion);
            ddlSource.DataSource = dataset;
            ddlSource.DataTextField = "SourceType";
            ddlSource.DataValueField = "SourceTypeID";
            ddlSource.DataBind();
            ddlSource.Items.Insert(0, new ListItem("--Select Source --", "-1"));
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void GetConsultants()
    {
        try
        {
            dataset = consultantBL.GetConsultants(0);
            ddlConsultants.DataSource = dataset;
            ddlConsultants.DataTextField = "Name";
            ddlConsultants.DataValueField = "ConsultantID";
            ddlConsultants.DataBind();
            ddlConsultants.Items.Insert(0, new ListItem("--Select Consultant --", "-1"));

            ddlConsultantsAction.DataSource = dataset;
            ddlConsultantsAction.DataTextField = "Name";
            ddlConsultantsAction.DataValueField = "ConsultantID";
            ddlConsultantsAction.DataBind();
            ddlConsultantsAction.Items.Insert(0, new ListItem("--Select Consultant --", "-1"));

            ViewState["consultData"] = dataset;
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void gvLeadList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Page")
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int RowIndex = row.RowIndex;
                ViewState["lsID"] = ((Label)row.FindControl("lblID")).Text.ToString();
                GetSourceData("U");
                if (e.CommandName == "EditLead")
                {
                    _objComman.GetStatus(ddlStatus);
                    newlead.Visible = true;
                    LeadList.Visible = false;
                    imgbtnAddLead.Visible = false;
                    btnUpdate.Visible = true;
                    ImageButton1.Visible = false;
                    GetProducts();
                    ddlSource.SelectedValue = ((Label)row.FindControl("lbllsSource")).Text.ToString();
                    if (ddlSource.SelectedValue == "10")
                    {
                        others.Visible = true;
                    }
                    txtOthers.Text = ((Label)row.FindControl("lbllsOthersInfo")).Text.ToString();
                    txtFirstName.Text = ((Label)row.FindControl("lblFirstName")).Text.ToString();
                    txtLastName.Text = ((Label)row.FindControl("lblLastName")).Text.ToString();
                    txtMobile.Text = ((Label)row.FindControl("lblMobile")).Text.ToString();
                    txtEmail.Text = ((Label)row.FindControl("lblEmailID")).Text.ToString();
                    txtSource.Text = ((Label)row.FindControl("lblOrigin")).Text.ToString();
                    txtDestination.Text = ((Label)row.FindControl("lblDestination")).Text.ToString();
                    ddlPackage.SelectedValue = ((Label)row.FindControl("lblProdID")).Text.ToString();
                    ddlAdults.SelectedValue = ((Label)row.FindControl("lblAdult")).Text.ToString();
                    ddlChild.SelectedValue = ((Label)row.FindControl("lblChildren")).Text.ToString();
                    ddlInfant.SelectedValue = ((Label)row.FindControl("lblInfants")).Text.ToString();
                    txtDepart.Text = ((Label)row.FindControl("lblDepartDate")).Text.ToString();
                    txtReturnDate.Text = ((Label)row.FindControl("lblReturnDate")).Text.ToString();
                    txtBudget.Text = ((Label)row.FindControl("lblBudget")).Text.ToString();
                    txtNotes.Text = ((Label)row.FindControl("lblNotes")).Text.ToString();
                    ddlStatus.SelectedValue = ((Label)row.FindControl("lsLeadActionsID")).Text.ToString();
                    txtDescription.Text = ((Label)row.FindControl("lblDescription")).Text.ToString();
                }
                else if (e.CommandName == "DeleteLead")
                {
                    lbldeletemessage.Text = "Are you sure, you want to delete Consultant Details?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDeleteModal();", true);
                }
                else if (e.CommandName == "Action")
                {
                    actions.Visible = true;
                    LeadList.Visible = false;
                    newlead.Visible = false;
                    imgbtnAddLead.Visible = false;
                }
            }
        }
        catch
        {
            message.ForeColor = System.Drawing.Color.Red;
            message.Text = "Something went wrong, please contact administrator";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
        ddlAssignLead.SelectedValue = "-1";
        ddlConsultants.SelectedValue = "-1";
        txtDescription.Text = "";
        txtFollowUp.Text = "";
    }
    //protected void gvLeadList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvLeadList.PageIndex = e.NewPageIndex;
    //    GetLeadsList();
    //}
    protected void gvLeadList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Session["LoginID"].ToString() == "admin")
            {
                ImageButton deleteButton = e.Row.FindControl("btnDelete") as ImageButton;
                deleteButton.Visible = true;
            }
            e.Row.BackColor = ((Label)e.Row.FindControl("lblDuplicateLeadList")).Text.ToString() == "Y" ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
            e.Row.ForeColor = ((Label)e.Row.FindControl("lblDuplicateLeadList")).Text.ToString() == "Y" ? System.Drawing.Color.White : System.Drawing.Color.Black;
        }
    }
    protected void imgbtnSubmitAssign_Click(object sender, ImageClickEventArgs e)
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
        leadEntity.LeadID = Convert.ToInt32(ViewState["lsID"].ToString());
        int result = leadBL.LeadAction(leadEntity);
        if (result == 1)
        {

            GetAssinedLeadsList();
            GetLeadsList();
            Clear();
            LeadList.Visible = true;
            newlead.Visible = false;
            actions.Visible = false;
            consultant.Visible = false;
            imgbtnAddLead.Visible = true;



            messageassign.Text = "Lead status changed Successfully!";
            messageassign.ForeColor = System.Drawing.Color.Green;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalassign();", true);


        }
        else
        {
            message.Text = "Please try again!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void imgbtnBackAssign_Click(object sender, ImageClickEventArgs e)
    {
        newlead.Visible = false;
        LeadList.Visible = true;
        imgbtnAddLead.Visible = true;
        actions.Visible = false;
        if (gvAssignedList.Rows.Count > 0)
        {
            gvAssignedList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvLeadList.Rows.Count > 0)
        {
            gvLeadList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvReminders.Rows.Count > 0)
        {
            gvReminders.HeaderRow.TableSection = TableRowSection.TableHeader;
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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
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
                        message.ForeColor = System.Drawing.Color.Red;
                    message.Text = "No of Pax should not exceed 9";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    if (j > 9)
                    {
                        message.Text = "No of Pax should not exceed 9";
                        message.ForeColor = System.Drawing.Color.Red;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                        leadEntity.FollowupDate = txtFollowUp.Text;
                        leadEntity.FollowupDesc = txtDescription.Text;
                        leadEntity.LeadDescription = txtDescription.Text;
                        leadEntity.CreatedBy = Convert.ToInt32(Session["ConsultantID"].ToString());
                        leadEntity.PackageId = "";
                        leadEntity.ProductID = "";
                        int result = leadBL.CUDLead(leadEntity, 'I');
                        if (result > 0)
                        {


                            leadEntity.AssignedBy = Convert.ToInt32(Session["ConsultantID"].ToString());
                            if (ddlSendEmail.SelectedValue == "3")
                            {
                                string Email = string.Empty;
                                leadEntity.AssignedTo = Convert.ToInt32(ddlConsultantsAction.SelectedValue);
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
                            else if (ddlAssignLead.SelectedValue == "2")
                            {
                                leadEntity.AssignedTo = Convert.ToInt32(Session["ConsultantID"].ToString());
                            }
                            else
                            {
                                leadEntity.AssignedTo = 0;
                            }
                            leadEntity.LeadStatus = 10;
                            leadEntity.LeadID = result;
                            int resultAssigned = leadBL.LeadAction(leadEntity);
                            if (resultAssigned == 1)
                            {

                                message.Text = "Lead Details saved Successfully!";
                                message.ForeColor = System.Drawing.Color.Green;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                                //SendMail(clName, txtEmail.Text, txtMobile.Text, ddlPackage.SelectedItem.Text);
                                Clear();
                                LeadList.Visible = true;
                                newlead.Visible = false;
                                imgbtnAddLead.Visible = true;
                                GetLeadsList();
                                if (gvAssignedList.Rows.Count > 0)
                                {
                                    gvAssignedList.HeaderRow.TableSection = TableRowSection.TableHeader;
                                }
                                if (gvLeadList.Rows.Count > 0)
                                {
                                    gvLeadList.HeaderRow.TableSection = TableRowSection.TableHeader;
                                }
                                if (gvReminders.Rows.Count > 0)
                                {
                                    gvReminders.HeaderRow.TableSection = TableRowSection.TableHeader;
                                }
                                GetAssinedLeadsList();
                            }
                            else
                            {
                                message.Text = "Please try again!";
                                message.ForeColor = System.Drawing.Color.Red;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            }
                        }
                        else
                        {
                            message.Text = "Please try again!";
                            message.ForeColor = System.Drawing.Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                }
            }
            else
            {
                message.Text = "No of infants should not exceed adults";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
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
                        message.ForeColor = System.Drawing.Color.Red;
                    message.Text = "No of Pax should not exceed 9";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    if (j > 9)
                    {
                        message.Text = "No of Pax should not exceed 9";
                        message.ForeColor = System.Drawing.Color.Red;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }
                    else
                    {
                        leadEntity.LeadID = Convert.ToInt32(ViewState["lsID"].ToString());
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
                        leadEntity.UpdatedBy = Convert.ToInt32(Session["ConsultantID"].ToString());
                        leadEntity.LeadStatus = Convert.ToInt32(ddlStatus.SelectedValue);
                        leadEntity.CreatedBy = 0;
                        leadEntity.LeadDescription = txtDescription.Text;
                        leadEntity.PackageId = "";
                        leadEntity.ProductID = "";
                        if (ddlStatus.SelectedValue == "4")
                        {
                            leadEntity.FollowupDate = txtFollowUp.Text;
                            leadEntity.FollowupDesc = txtDescription.Text;
                        }
                        else
                        {
                            leadEntity.FollowupDate = "";
                            leadEntity.FollowupDesc = "";
                        }
                        int result = leadBL.CUDLead(leadEntity, 'U');
                        if (result == 2)
                        {
                            message.Text = "Lead Details updated Successfully!";
                            message.ForeColor = System.Drawing.Color.Green;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            LeadList.Visible = true;
                            newlead.Visible = false;
                            GetLeadsList();
                            Clear();
                            imgbtnAddLead.Visible = true;

                        }
                        else
                        {
                            message.Text = "Please try again!";
                            message.ForeColor = System.Drawing.Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                }
            }
            else
            {
                message.Text = "No of infants should not exceed adults";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void btnReset_Click(object sender, ImageClickEventArgs e)
    {
        newlead.Visible = false;
        LeadList.Visible = true;
        imgbtnAddLead.Visible = true;
        if (gvAssignedList.Rows.Count > 0)
        {
            gvAssignedList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvLeadList.Rows.Count > 0)
        {
            gvLeadList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvReminders.Rows.Count > 0)
        {
            gvReminders.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void btnSure_Click(object sender, EventArgs e)
    {
        try
        {
            leadEntity.LeadID = Convert.ToInt32(ViewState["lsID"].ToString());
            leadEntity.SourceID = 0;
            leadEntity.SourceRef = "";
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
            leadEntity.Adult = 0;
            leadEntity.Child = 0;
            leadEntity.Infant = 0;
            leadEntity.ProductType = 0;
            leadEntity.Budget = 0;
            leadEntity.Notes = txtNotes.Text;
            leadEntity.QuotedPrice = 0;
            leadEntity.FinalPrice = 0;
            leadEntity.UpdatedBy = 0;
            leadEntity.LeadStatus = 0;
            leadEntity.CreatedBy = 0;
            leadEntity.PackageId = "";
            leadEntity.FollowupDate = "";
            leadEntity.FollowupDesc = "";
            leadEntity.LeadDescription = "";
            leadEntity.ProductID = "";
            int result = leadBL.CUDLead(leadEntity, 'D');
            if (result == 0)
            {
                message.Text = "Lead Details deleted Successfully!";
                message.ForeColor = System.Drawing.Color.Green;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                GetLeadsList();
                Clear();
                GetAssinedLeadsList();
            }
            else
            {
                message.Text = "Please try again!";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        catch (Exception ex)
        {
            message.ForeColor = System.Drawing.Color.Red;
            message.Text = "Something went wrong, please contact administrator";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFollowup.Text = "";
        if (ddlStatus.SelectedValue == "4")
        {
            DataSet ds = new DataSet();
            ds = leadBL.GetFollowupCount(Convert.ToInt32(ViewState["lsID"].ToString()));
            if (ds.Tables[0].Rows.Count == 3)
            {
                message.Text = "Maximum follow ups reached for this lead!";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (ds.Tables[0].Rows.Count > 0)
            {
                lblFollowup.Text = ds.Tables[0].Rows[0]["FollowupDate"].ToString();
                followupdate.Visible = true;
            }
            else
            {
                followupdate.Visible = true;
            }
        }
        else
        {
            txtDescription.Text = "";
            followupdate.Visible = false;
        }
        if (ddlStatus.SelectedValue == "2")
        {
            txtDescription.Text = "";
            desc.Visible = false;
        }
        else
        {
            txtDescription.Text = "";
            desc.Visible = true;
        }
        if (ddlStatus.SelectedValue == "6")
        {
            // Client File Id TextBox Show
            dvClientFileId.Visible = true;
            txtClientFileId.Text = "";
        }
        else
        {
            txtClientFileId.Text = "";
            dvClientFileId.Visible = false;
        }
    }
    protected void imgbtnAddLead_Click1(object sender, ImageClickEventArgs e)
    {
        newlead.Visible = true;
        LeadList.Visible = false;
        imgbtnAddLead.Visible = false;
        btnUpdate.Visible = false;
        ImageButton1.Visible = true;
        Clear();
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLeadsList();
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
    protected void ddlAssignedList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvAssignedList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Page")
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int RowIndex = row.RowIndex;
                ViewState["lsID"] = ((Label)row.FindControl("lblID")).Text.ToString();
                GetSourceDataEdit("U");
                if (e.CommandName == "EditLead")
                {
                    ViewState["lsLeadActionsID"] = ((Label)row.FindControl("lsLeadActionsID")).Text.ToString();
                    ClientName = encryptdecrypt.Encrypt(((Label)row.FindControl("lblFirstName")).Text.ToString() + " " + ((Label)row.FindControl("lblLastName")).Text.ToString());
                    product = encryptdecrypt.Encrypt(((Label)row.FindControl("lblProdType")).Text.ToString());
                    source = encryptdecrypt.Encrypt((((Label)row.FindControl("lblOrigin")).Text.ToString()));
                    toCity = encryptdecrypt.Encrypt(((Label)row.FindControl("lblDestination")).Text.ToString());
                    Email = encryptdecrypt.Encrypt(((Label)row.FindControl("lblEmailID")).Text.ToString());
                    encryptedparamleadid = encryptdecrypt.Encrypt(ViewState["lsID"].ToString());
                    encryptedparamlblProductID = encryptdecrypt.Encrypt(((Label)row.FindControl("lblProductID")).Text.ToString());
                    Edit(ViewState["lsLeadActionsID"].ToString());
                }
                else if (e.CommandName == "DeleteLead")
                {
                    lbldeletemessage.Text = "Are you sure, you want to delete Consultant Details?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDeleteModal();", true);
                }
                else if (e.CommandName == "Action")
                {
                    actions.Visible = true;
                    LeadList.Visible = false;
                    newlead.Visible = false;
                    imgbtnAddLead.Visible = false;
                }
            }
        }
        catch
        {
            message.ForeColor = System.Drawing.Color.Red;
            message.Text = "Something went wrong, please contact administrator";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }

    private void Edit(string lsLeadActionsID)
    {
        MailMessage.Text = "";
        _objComman.GetStatus(ddlStatus);
        newlead.Visible = false;
        LeadList.Visible = false;
        imgbtnAddLead.Visible = false;
        dvEdit.Visible = true;
        string strStatusId = ViewState["lsLeadActionsID"].ToString();
        Session["strStatusId"] = strStatusId;
        DataSet ds = leadBL.GetLeadInfo(Convert.ToInt32(ViewState["lsID"].ToString()));
        DataTable dtLead = ds.Tables[0];
        DataTable dtLeadHistory = ds.Tables[1];
        GetTemplateNames(ViewState["lsID"].ToString());
        if (dtLead.Rows.Count > 0)
        {
            txtEFirstName.Text = dtLead.Rows[0]["lsFirstName"].ToString();
            txtELastName.Text = dtLead.Rows[0]["lsLastName"].ToString();
            txtEMobile.Text = dtLead.Rows[0]["lsPhone"].ToString();
            txtEEmail.Text = dtLead.Rows[0]["lsEmailId"].ToString();
            txtEDepart.Text = String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["lsDepartureDate"]);
            txtEReturn.Text = String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["lsReturnDate"]);

            lblLName.Text = string.Format("{0} {1}", dtLead.Rows[0]["lsFirstName"].ToString(), dtLead.Rows[0]["lsLastName"].ToString());
            lblLEmail.Text = dtLead.Rows[0]["lsEmailId"].ToString();
            lblLDates.Text = string.Format("{0} / {1}", String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["lsDepartureDate"]), String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["lsReturnDate"]));
            lblLBudget.Text = dtLead.Rows[0]["lsBudget"].ToString();
            lblLPhone.Text = dtLead.Rows[0]["lsPhone"].ToString();
            if (dtLead.Rows[0]["lsProductId"].ToString() == "" || dtLead.Rows[0]["lsProductId"].ToString() == null)
            {
                lnkUrl.Text = "Lead not from Serendipity website";
            }
            else
            {
                lnkUrl.Text = "http://serendipitytravel.co.za/tour-detail.aspx?pid=" + dtLead.Rows[0]["lsProductId"].ToString();
                lnkUrl.NavigateUrl = "http://serendipitytravel.co.za/tour-detail.aspx?pid=" + dtLead.Rows[0]["lsProductId"].ToString();
            }
            lblLNotes.Text = dtLead.Rows[0]["lsNotes"].ToString();

            txtClientFileId.Text = dtLead.Rows[0]["lsClientFileId"].ToString();
            txtEConsultNotes.Text = dtLead.Rows[0]["lsConsultantNotes"].ToString();
            txtEReminder.Text = String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["lsReminder"]);
            txtERemindNotes.Text = dtLead.Rows[0]["lsReminderNotes"].ToString();

            txtToEmail.Text = dtLead.Rows[0]["lsEmailId"].ToString();
            txtEmailSubject.Text = "Serendipity Tours >> More Info Required";

            ddlESource.SelectedValue = dtLead.Rows[0]["lsSource"].ToString();
            if (ddlESource.SelectedValue == "10")
            {
                dvEOthers.Visible = true;
            }

            txtEOthers.Text = dtLead.Rows[0]["lsOthersInfo"].ToString();

            ddlStatus.SelectedValue = strStatusId;

            if (strStatusId == "6")
            {
                // Client File Id TextBox Show
                dvClientFileId.Visible = true;
                txtClientFileId.Text = dtLead.Rows[0]["lsClientFileId"].ToString();
            }
            else
            {
                txtClientFileId.Text = "";
                dvClientFileId.Visible = false;
            }

            if (ddlStatus.SelectedValue == "4")
            {
                if (Convert.ToInt32(dtLead.Rows[0]["Followups"]) == 3)
                {
                    message.Text = "Maximum follow ups reached!";
                    message.ForeColor = System.Drawing.Color.Red;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    lblFollowup.Text = String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["FollowupDate"]);
                    followupdate.Visible = true;
                }
                followupdate.Visible = true;
                lblFollowup.ForeColor = System.Drawing.Color.Red;
                lblFollowup.Text = "The last follow up date was : " + Convert.ToDateTime(dtLead.Rows[0]["FollowupDate"].ToString()).Date.ToString("dd-MM-yyyy"); ;
            }
            else
            {
                lblFollowup.Text = "";
                followupdate.Visible = false;
            }

        }

        if (ddlStatus.SelectedValue == "2")
        {
            desc.Visible = false;
        }
        else
        {
            desc.Visible = true;
        }
        // Lead Hostory
        LeadHistory(dtLeadHistory);
        // Email Template                    
        StringBuilder sb = new StringBuilder();
        string strHeading = string.Format("<p><strong>Dear {0},</strong></p>", lblLName.Text);
        sb.Append(strHeading);
        sb.Append("<p>Thank you so much for your enquiry I received today. In order to quote you accurately, I require the following additional information.</p>");
        sb.Append("<p>1.&nbsp;Dates of travel</p>");
        sb.Append("<p>2.&nbsp;Destination</p>");
        sb.Append("<p>3.&nbsp;Where will you be travelling from ie. Joburg, Durban or Cape Town</p>");
        sb.Append("<p>4.&nbsp;Estimated budget</p>");
        sb.Append("<p>5.&nbsp;How many people will be travelling incl. children (and their ages)</p>");
        sb.Append("<p>6.&nbsp;Are you travelling for a special occation ie. birthday, anniversary, honeymoon etc.</p>");
        sb.Append("<p>As soon as I receive the above information, I can work on some options for you.</p>");
        sb.Append("<p><strong>Kind regards</strong></p>");
        sb.Append("<p><strong>" + Session["Name"].ToString() + "</strong></p>");

        txtMailTemp.Text = sb.ToString();

        // Generate Quote URL


        string url = "Quote.aspx?id=" + Server.UrlEncode(encryptedparamleadid) + "&city=" + Server.UrlEncode(toCity) + "&client=" + Server.UrlEncode(ClientName) + "&source=" + Server.UrlEncode(source) + "&prod=" + Server.UrlEncode(product) + "&em=" + Server.UrlEncode(Email) + "&prodid=" + Server.UrlEncode(encryptedparamlblProductID);

        hdfQuoteUrl.Value = url;
        Session["QuoteUrl"] = hdfQuoteUrl.Value;
    }
    protected void GetLeadInfo()
    {
        try
        {
            dataset = leadBL.GetLeadsList(0);
            gvLeadList.DataSource = dataset;
            gvLeadList.DataBind();
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void gvAssignedList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Session["LoginID"].ToString() == "admin")
            {
                ImageButton deleteButton = e.Row.FindControl("btnDelete") as ImageButton;
                deleteButton.Visible = true;
            }
            string quote = ((Label)e.Row.FindControl("lblQuote")).Text.ToString();
            if (quote != "0")
            {
                ImageButton controlButton = e.Row.FindControl("imgbtnPDF") as ImageButton;
                controlButton.Visible = false;
            }
            e.Row.BackColor = ((Label)e.Row.FindControl("lblDuplicateLead")).Text.ToString() == "Y" ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
            e.Row.ForeColor = ((Label)e.Row.FindControl("lblDuplicateLead")).Text.ToString() == "Y" ? System.Drawing.Color.White : System.Drawing.Color.Black;
        }
    }
    protected void imgEUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            leadEntity.LeadID = Convert.ToInt32(ViewState["lsID"].ToString());
            leadEntity.SourceID = Convert.ToInt32(ddlESource.SelectedValue);
            leadEntity.SourceRef = ddlESource.SelectedItem.Text;
            leadEntity.Others = txtEOthers.Text;
            leadEntity.AssignedTo = 0;
            leadEntity.AssignedBy = 0;
            leadEntity.FirstName = txtEFirstName.Text;
            leadEntity.LastName = txtELastName.Text;
            leadEntity.Mobile = txtEMobile.Text;
            leadEntity.Email = txtEEmail.Text;
            leadEntity.OriginName = "";
            leadEntity.DestinationName = "";
            leadEntity.DepartureDate = txtEDepart.Text;
            leadEntity.ReturnDate = txtEReturn.Text;
            leadEntity.Adult = 0;
            leadEntity.Child = 0;
            leadEntity.Infant = 0;
            leadEntity.ProductType = 0;
            leadEntity.Budget = 0;
            leadEntity.Notes = "";
            leadEntity.QuotedPrice = 0;
            leadEntity.FinalPrice = 0;
            leadEntity.UpdatedBy = Convert.ToInt32(Session["ConsultantID"].ToString());
            leadEntity.LeadStatus = Convert.ToInt32(ddlStatus.SelectedValue);
            leadEntity.CreatedBy = 0;
            leadEntity.LeadDescription = txtDescription.Text;
            leadEntity.PackageId = "";
            leadEntity.ProductID = "";
            if (ddlStatus.SelectedValue == "4")
            {
                leadEntity.FollowupDate = txtFollowUp.Text;
                leadEntity.FollowupDesc = txtDescription.Text;
            }
            else
            {
                leadEntity.FollowupDate = "";
                leadEntity.FollowupDesc = "";
            }
            leadEntity.ClientFileId = txtClientFileId.Text;
            leadEntity.ConsultantNotes = txtEConsultNotes.Text;
            leadEntity.Reminder = txtEReminder.Text;
            leadEntity.ReminderNotes = txtERemindNotes.Text;

            int result = leadBL.UpdateLeadInfo(leadEntity, 'U');
            if (result == 2)
            {
                message.Text = "Lead Details updated Successfully!";
                message.ForeColor = System.Drawing.Color.Green;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                newlead.Visible = false;
                LeadList.Visible = false;
                dvEdit.Visible = true;
                actions.Visible = false;
                GetLeadsList();
                GetAssinedLeadsList();
                EditClear();
                imgbtnAddLead.Visible = false;
                Edit(ViewState["lsLeadActionsID"].ToString());
            }
            else
            {
                message.Text = "Please try again!";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    private void EditClear()
    {
        txtFollowUp.Text = "";
        txtDescription.Text = "";
        txtEFirstName.Text = "";
        txtELastName.Text = "";
        txtEEmail.Text = "";
        txtEDepart.Text = "";
        txtEReturn.Text = "";
        txtEMobile.Text = "";
        txtClientFileId.Text = "";
        txtEConsultNotes.Text = "";
        txtEReminder.Text = "";
        txtERemindNotes.Text = "";
        ddlESource.SelectedValue = "-1";
        txtEOthers.Text = "";
    }
    protected void imgECancel_Click(object sender, ImageClickEventArgs e)
    {
        dvEdit.Visible = false;
        LeadList.Visible = true;
        imgbtnAddLead.Visible = true;
        if (gvAssignedList.Rows.Count > 0)
        {
            gvAssignedList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvLeadList.Rows.Count > 0)
        {
            gvLeadList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvReminders.Rows.Count > 0)
        {
            gvReminders.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        string strEmail = txtToEmail.Text;
        string strCC = txtCCEmail.Text;
        string strSubject = txtEmailSubject.Text;
        string strBody = txtMailTemp.Text;
        SendMail(strEmail, strCC, strSubject, strBody);
    }
    public void SendMail(string clEmail, string strCC, string srtSubject, string strText)
    {
        try
        {
            DataSet ds = leadBL.GetMailInfo();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string SmtpServer = ds.Tables[0].Rows[0]["con_smtp_host"].ToString();
                int SmtpPort = Convert.ToInt32(ds.Tables[0].Rows[0]["con_smtp_port"].ToString());
                string MailFrom = ds.Tables[0].Rows[0]["con_mail_from"].ToString();
                string DisplayNameFrom = ds.Tables[0].Rows[0]["con_from_name"].ToString();
                string FromPassword = ds.Tables[0].Rows[0]["con_from_pwd"].ToString();
                string MailTo = clEmail;
                //string MailTo = "ramesh.palaparti@dinoosys.com";
                string DisplayNameTo = string.Empty;
                string MailCc = string.Empty;
                string DisplayNameCc = string.Empty;
                string MailBcc = string.Empty;
                string Subject = string.Empty;
                string MailText = string.Empty;
                string Attachment = string.Empty;

                try
                {
                    Subject = srtSubject;
                    MailCc = !string.IsNullOrEmpty(strCC) ? strCC : "";

                    MailText = strText;

                    bool mailSent = CommanClass.UpdateMail(SmtpServer, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo, DisplayNameTo, MailCc, "", "", "", DisplayNameCc, MailBcc, Subject, MailText, Attachment);

                    if (mailSent)
                    {
                        MailMessage.Text = "Email sent successfully.";
                        MailMessage.ForeColor = System.Drawing.Color.Green;
                        CommanClass.MailStatusLog(Convert.ToInt32(ViewState["lsID"].ToString()), "MI001", "Success", "", "");
                        DataSet dsInfo = leadBL.GetLeadInfo(Convert.ToInt32(ViewState["lsID"].ToString()));
                        DataTable dtLeadHistory = dsInfo.Tables[1];
                        // Lead History
                        LeadHistory(dtLeadHistory);
                    }
                    else
                    {
                        MailMessage.Text = "Email not sent.";
                        MailMessage.ForeColor = System.Drawing.Color.Red;
                        CommanClass.MailStatusLog(Convert.ToInt32(ViewState["lsID"].ToString()), "MI001", "Fail", "", "");
                        DataSet dsInfo = leadBL.GetLeadInfo(Convert.ToInt32(ViewState["lsID"].ToString()));
                        DataTable dtLeadHistory = dsInfo.Tables[1];
                        // Lead History
                        LeadHistory(dtLeadHistory);
                    }

                }
                catch (Exception ex)
                {
                    MailMessage.Text = "Email not sent.";
                    MailMessage.ForeColor = System.Drawing.Color.Red;
                    CommanClass.MailStatusLog(Convert.ToInt32(ViewState["lsID"].ToString()), "MI001", "Fail", ex.Message, "");

                    DataSet dsInfo = leadBL.GetLeadInfo(Convert.ToInt32(ViewState["lsID"].ToString()));
                    DataTable dtLeadHistory = dsInfo.Tables[1];
                    // Lead Hostory
                    LeadHistory(dtLeadHistory);
                }

            }
        }
        catch
        { }
    }
    public void LeadHistory_Old(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();

        if (dt.Rows.Count > 0)
        {
            sb.Append(" <table border='1'>");
            sb.Append("<tr>");
            sb.Append("<th>LEAD HISTORY</th>");
            sb.Append("<th>DATE</th>");
            sb.Append("<th>VIEW</th>");
            sb.Append("<th>EDIT</th>");
            sb.Append("</tr>");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    sb.Append("<td>");
                    sb.Append(row[column.ColumnName]);
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
        }
    }
    public void LeadHistory(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();

        if (dt.Rows.Count > 0)
        {
            gvHistory.DataSource = dt;
            gvHistory.DataBind();
        }
    }
    protected void imgQuoteSubmit_Click(object sender, ImageClickEventArgs e)
    {
        string strValue = ddlQuoteDetails.SelectedValue;
        string strTemp = ddlTemplateNames.SelectedValue;
        string LeadStatus = string.Empty;
        if (ddlStatus.SelectedValue == "6")
        {
            LeadStatus = txtClientFileId.Text;
        }
        else
        {
            LeadStatus = "";
        }

        string url = Session["QuoteUrl"].ToString() + "&qtype=" + strValue + "&temp=" + strTemp + "&QuoteID=&flag=1" + "&status=" + LeadStatus;
        Response.Redirect(url);
    }
    protected void ddlQuoteDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlQuoteDetails.SelectedValue == "2")
        {
            GetTemplateNames(ViewState["lsID"].ToString());
            dvTemplates.Visible = true;
            imgQuoteSubmit.Style.Add("margin-top", "15px;");
        }
        else
        {
            dvTemplates.Visible = false;
            imgQuoteSubmit.Style.Add("margin-top", "0px;");
        }
    }
    protected void GetTemplateNames(string strLeadId)
    {
        ddlTemplateNames.Items.Clear();
        try
        {
            DataSet ds = leadBL.GetTemplateNames(Convert.ToInt32(strLeadId));
            ddlTemplateNames.DataSource = ds;
            ddlTemplateNames.DataTextField = "TemplateName";
            ddlTemplateNames.DataValueField = "ID";
            ddlTemplateNames.DataBind();
            ddlTemplateNames.Items.Insert(0, new ListItem("--Select Template --", "-1"));
        }
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void GetSourceDataEdit(string Opeartion)
    {
        try
        {
            dataset = leadBL.GetSourceData(Opeartion);
            ddlESource.DataSource = dataset;
            ddlESource.DataTextField = "SourceType";
            ddlESource.DataValueField = "SourceTypeID";
            ddlESource.DataBind();
            ddlESource.Items.Insert(0, new ListItem("--Select Source --", "-1"));
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void ddlESource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlESource.SelectedValue == "10")
        {
            txtEOthers.Text = "";
            dvEOthers.Visible = true;
        }
        else
        {
            txtEOthers.Text = "";
            dvEOthers.Visible = false;
        }
    }
    protected void gvHistory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Page")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int RowIndex = row.RowIndex;

                if (e.CommandName == "View")
                {
                    string path = "http://tcrm.askswg.co.za/QuotePDF/";
                    string strQuoteNumber = ((Label)row.FindControl("lblHistoryQuote")).Text.ToString();
                    string fileName = path + "\\" + strQuoteNumber + ".pdf";
                    string s = "window.open('" + fileName + "', '_blank');";
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }
                else if (e.CommandName == "Edit")
                {
                    string strQuoteNumber = ((Label)row.FindControl("lblHistoryQuote")).Text.ToString();
                    string url = Session["QuoteUrl"].ToString() + "&qtype=&temp=&QuoteID=" + strQuoteNumber + "&flag=2";
                    //string s = "window.open('" + url + "', '_blank');";
                    //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                    Response.Redirect(url);
                }
                else if (e.CommandName == "SendSMS")
                {
                    hdfSMS.Value = ((Label)row.FindControl("lblHistoryQuote")).Text.ToString();
                    if (txtEMobile.Text.Substring(0, 1) == "0" && txtEMobile.Text.Length == 10)
                    {
                        txtSendSMS.Text = txtEMobile.Text.Substring(1);
                    }
                    else if (txtEMobile.Text.Length == 9)
                    {
                        txtSendSMS.Text = txtEMobile.Text;
                    }
                    else {
                        message.ForeColor = System.Drawing.Color.Red;
                        message.Text = "Mobile number format is wrong";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openSMSModal();", true);
                }
                else if (e.CommandName == "Convert")
                {
                    string strQuoteNumber = ((Label)row.FindControl("lblHistoryQuote")).Text.ToString();
                    string url = Session["QuoteUrl"].ToString() + "&qtype=&temp=&QuoteID=" + strQuoteNumber + "&flag=2";
                    string s = "window.open('" + url + "', '_blank');";
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }

            }
        }
        catch
        {
            message.ForeColor = System.Drawing.Color.Red;
            message.Text = "Something went wrong, please contact administrator";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void gvHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblQuote = (Label)e.Row.FindControl("lblHistoryQuote");
            Label lblClientFileId = (Label)e.Row.FindControl("lblClientFileId");
            LinkButton lnkView = (LinkButton)e.Row.FindControl("btnViewHistory");
            LinkButton lnkEdit = (LinkButton)e.Row.FindControl("btnEditHistory");
            LinkButton lnkSMS = (LinkButton)e.Row.FindControl("btnSendSMS");
            LinkButton lnkConvert = (LinkButton)e.Row.FindControl("btnConvert");

            lnkView.Visible = lblQuote.Text == "" ? false : true;
            lnkEdit.Visible = lblQuote.Text == "" ? false : true;
            lnkSMS.Visible = lblQuote.Text == "" ? false : true;

            lnkConvert.Visible = lblClientFileId.Text == "" ? false : true;
        }
    }
    protected void gvHistory_RowEditing(object sender, GridViewEditEventArgs e)
    {

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
    protected void backToLead_Click(object sender, ImageClickEventArgs e)
    {
        LeadList.Visible = true;
        newlead.Visible = false;
        dvEdit.Visible = false;
        imgbtnAddLead.Visible = true;
        if (gvAssignedList.Rows.Count > 0)
        {
            gvAssignedList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvLeadList.Rows.Count > 0)
        {
            gvLeadList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gvReminders.Rows.Count > 0)
        {
            gvReminders.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void btnSMS_Click(object sender, EventArgs e)
    {
        ISmsMessageBuilder messageBuilder;
        Guid productToken = new Guid("2DA19A96-5885-40CB-9098-F7A58B1C298E");
        //string phoneNo = "0027724766939"; 
        string phoneNo = "0027" + txtSendSMS.Text;
        //Use XML or JSON per your own preference
        //messageBuilder = new JsonSmsMessageBuilder();
        messageBuilder = new XmlSmsMessageBuilder();
        var request = messageBuilder.CreateMessage(productToken,
                "Serendipity",
                phoneNo,
                txtResp.Text
               );

        var response = doHttpPost(messageBuilder.GetTargetUrl(),
                                  messageBuilder.GetContentType(),
                                  request);

        txtResp.Text = response.ToString();
        //    Console.WriteLine($"Response: {response}");
        //    Console.ReadKey();
        leadBL.SetSMSStatus(hdfSMS.Value);
        DataSet ds = leadBL.GetLeadInfo(Convert.ToInt32(ViewState["lsID"].ToString()));
        DataTable dtLeadHistory = ds.Tables[1];
        LeadHistory(dtLeadHistory);
    }
    /// <summary>
    /// Sends a string via HTTP POST to a url
    /// </summary>
    /// <param name="url">The target url to send the string to</param>
    /// <param name="requestString">The string to send</param>
    /// <returns>The response of the url or the error text in case of an error</returns>
    private static string doHttpPost(string url, string contentType, string requestString)
    {
        try
        {
            //Console.WriteLine($"Sending request to: {url}");
            var webClient = new WebClient();
            webClient.Headers["Content-Type"] = contentType;
            webClient.Encoding = System.Text.Encoding.UTF8;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            return webClient.UploadString(url, requestString);
        }
        catch (WebException wex)
        {
            return string.Format("{0} - {1}", wex.Status, wex.Message);
        }
    }
}

/// <summary>
/// The gateway accepts both the XML amd JSON formats
/// </summary>
public interface ISmsMessageBuilder
{

    /// <summary>
    /// Creates a string according to the technical requirements
    /// of the CM MT gateway for sending a simple SMS text message
    /// </summary>
    /// <param name="productToken">Your product token</param>
    /// <param name="sender">A sendername/shortcode the SMS message</param>
    /// <param name="message">The text to be sent</param>
    /// <param name="recipient">The recipient's MSISDN</param>
    /// <returns>A string according to the technical requirements of the CM MT gateway,
    /// based on the provided parameters</returns>
    string CreateMessage(Guid productToken,
        string sender,
        string recipient,
        string message);

    /// <summary>
    /// The XML and JSON gateways use different URLs
    /// </summary>
    /// <returns>The target URL of either the XML or JSON gateway</returns>
    string GetTargetUrl();

    /// <summary>
    /// The JSON gateway requires you to set the content type to "application/json"
    /// </summary>
    /// <returns>The string of the content type to be used in the HTTP header </returns>
    string GetContentType();
}
public class XmlSmsMessageBuilder : ISmsMessageBuilder
{
    public string CreateMessage(Guid productToken,
                                string sender,
                                string recipient,
                                string message)
    {
        return
           new XElement("MESSAGES",
               new XElement("AUTHENTICATION",
                   new XElement("PRODUCTTOKEN", productToken)
           ),
           new XElement("MSG",
               new XElement("FROM", sender),
               new XElement("TO", recipient),
               new XElement("BODY", message)
           )
        ).ToString();
    }

    public string GetContentType()
    { return "application/xml"; }

    public string GetTargetUrl()
    { return "https://sgw01.cm.nl/gateway.ashx"; }
}

/// <summary>
/// For JSON string building we recommend the Newtonsoft JSON NuGet package
/// Feel free to substitute with your own preference in .net JSON library
/// </summary>
//public class JsonSmsMessageBuilder : ISmsMessageBuilder
//{
//    public string CreateMessage(Guid productToken,
//                        string sender,
//                        string recipient,
//                        string message) {
//        return new JObject {
//                            ["Messages"] = new JObject {
//                                                        ["Authentication"] = new JObject {
//                                                                                            ["ProductToken"] = productToken
//                                                                                          },
//                                                        ["Msg"] = new JArray {
//                                                                                new JObject { 
//                                                                                                ["From"] = sender,
//                                                                                                 ["To"] = new JArray {
//                                                                                 new JObject { ["Number"] = recipient }
//                                                           },
//                                        ["Body"] = new JObject {
//                                            ["Content"] = message
//                                        }    
//                    }
//                }
//            }
//        }.ToString();
//    }

//    public string GetContentType()
//    { return "application/json"; }

//    public string GetTargetUrl()
//    { return "https://gw.cmtelecom.com/v1.0/message"; }
//}