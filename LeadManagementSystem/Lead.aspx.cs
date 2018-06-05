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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                _objComman.getRecordsPerPage(DropPage);
                _objComman.getRecordsPerPage(ddlAssignedList);
                GetProducts();
                GetSourceData("I");
                others.Visible = false;
                GetLeadsList();
                GetAssinedLeadsList();
                newlead.Visible = false;
                btnUpdate.Visible = false;
                _objComman.GetAssigLeadOptions(ddlAssignLead);
                consultant.Visible = false;
                actions.Visible = false;
                status.Visible = false;
                followupdate.Visible = false;
                desc.Visible = false;
            }
        }
        catch { }
    }
    protected void GetLeadsList()
    {
        try
        {
            gvLeadList.PageSize = Convert.ToInt32(DropPage.SelectedValue);
            dataset = leadBL.GetLeadsList(0);
            if (dataset.Tables[0].Rows.Count > 0)
            {
                search.Visible = true;
            }
            else
            {
                search.Visible = false;
            }
            gvLeadList.DataSource = dataset;
            gvLeadList.DataBind();
        }
        catch (Exception ex)
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
            gvAssignedList.PageSize = Convert.ToInt32(ddlAssignedList.SelectedValue);
            dataset = leadBL.GetAssignedLeadsList(0);
            if (dataset.Tables[0].Rows.Count > 0)
            {
                assignedLeadList.Visible = true;
            }
            else
            {
                assignedLeadList.Visible = false;
            }
            gvAssignedList.DataSource = dataset;
            gvAssignedList.DataBind();
        }
        catch (Exception ex)
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
                    status.Visible = true;
                    newlead.Visible = true;
                    LeadList.Visible = false;
                    imgbtnAddLead.Visible = false;
                    btnUpdate.Visible = true;
                    ImageButton1.Visible = false;
                    GetProducts();
                    DataSet ds = leadBL.GetFollowupCount(Convert.ToInt32(ViewState["lsID"].ToString()));

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
                    if (ddlStatus.SelectedValue == "4")
                    {
                        if (ds.Tables[0].Rows.Count == 3)
                        {
                            message.Text = "Maximum follow ups reached!";
                            message.ForeColor = System.Drawing.Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                        else
                        {
                            lblFollowup.Text = ds.Tables[0].Rows[0]["FollowupDate"].ToString();
                            followupdate.Visible = true;
                        }

                        followupdate.Visible = true;
                        lblFollowup.ForeColor = System.Drawing.Color.Red;
                        lblFollowup.Text = "The last follow up date was : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["FollowupDate"].ToString()).Date.ToString("dd-MM-yyyy"); ;
                    }
                    else
                    {
                        lblFollowup.Text = "";
                        followupdate.Visible = false;
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
                //else if (e.CommandName == "Quote")
                //{
                //    string ClientName = encryptdecrypt.Encrypt(((Label)row.FindControl("lblFirstName")).Text.ToString() + " " + ((Label)row.FindControl("lblLastName")).Text.ToString());
                //    string product = encryptdecrypt.Encrypt(((Label)row.FindControl("lblProdType")).Text.ToString());
                //    string source = encryptdecrypt.Encrypt((((Label)row.FindControl("lblOrigin")).Text.ToString()));
                //    string toCity = encryptdecrypt.Encrypt(((Label)row.FindControl("lblDestination")).Text.ToString());
                //    string Email = encryptdecrypt.Encrypt(((Label)row.FindControl("lblEmailID")).Text.ToString());
                //    string encryptedparamleadid = encryptdecrypt.Encrypt(ViewState["lsID"].ToString());
                //    string url = "Quote.aspx?id=" + Server.UrlEncode(encryptedparamleadid) + "&city=" + Server.UrlEncode(toCity) + "&client=" + Server.UrlEncode(ClientName) + "&source=" + Server.UrlEncode(source) + "&prod=" + Server.UrlEncode(product) + "&em=" + Server.UrlEncode(Email);
                //    string s = "window.open('" + url + "', '_blank');";
                //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                //}
                //else if (e.CommandName == "PDF")
                //{
                //    string quoteNumber = ((Label)row.FindControl("lblQuoteNumber")).Text.ToString();
                //    //string path = Server.MapPath("~/QuotePDF");
                //    //Process.Start(fileName);
                //    string path = "http://tcrm.askswg.co.za/QuotePDF/";
                //    string fileName = path + "\\" + quoteNumber + ".pdf";
                //    string s = "window.open('" + fileName + "', '_blank');";
                //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

                //}
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
    protected void gvLeadList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeadList.PageIndex = e.NewPageIndex;
        GetLeadsList();
    }
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
                    //MailTo = "karen@serendipitytours.co.za";

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
        leadEntity.LeadStatus = Convert.ToInt32(ddlAssignLead.SelectedValue);
        leadEntity.LeadID = Convert.ToInt32(ViewState["lsID"].ToString());
        int result = leadBL.LeadAction(leadEntity);
        if (result == 1)
        {
            message.Text = "Lead status changed Successfully!";
            message.ForeColor = System.Drawing.Color.Green;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            Clear();
            LeadList.Visible = true;
            newlead.Visible = false;
            actions.Visible = false;
            imgbtnAddLead.Visible = true;
            GetLeadsList();
            GetAssinedLeadsList();

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
            status.Visible = false;
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
                        int result = leadBL.CUDLead(leadEntity, 'I');
                        if (result == 1)
                        {
                            message.Text = "Lead Details saved Successfully!";
                            message.ForeColor = System.Drawing.Color.Green;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            Clear();
                            LeadList.Visible = true;
                            newlead.Visible = false;
                            imgbtnAddLead.Visible = true;
                            GetLeadsList();

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
        status.Visible = false;
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
            int result = leadBL.CUDLead(leadEntity, 'D');
            if (result == 1)
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
            else
            {
                lblFollowup.Text = ds.Tables[0].Rows[0]["FollowupDate"].ToString();
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
                GetSourceData("U");
                if (e.CommandName == "EditLead")
                {

                    _objComman.GetStatus(ddlStatus);
                    //status.Visible = true;
                    newlead.Visible = true;
                    LeadList.Visible = false;
                    imgbtnAddLead.Visible = false;
                    btnUpdate.Visible = true;
                    ImageButton1.Visible = false;
                    GetProducts();
                    DataSet ds = leadBL.GetFollowupCount(Convert.ToInt32(ViewState["lsID"].ToString()));

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
                    if (ddlStatus.SelectedValue == "4")
                    {
                        if (ds.Tables[0].Rows.Count == 3)
                        {
                            message.Text = "Maximum follow ups reached!";
                            message.ForeColor = System.Drawing.Color.Red;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                        else
                        {
                            lblFollowup.Text = ds.Tables[0].Rows[0]["FollowupDate"].ToString();
                            followupdate.Visible = true;
                        }

                        followupdate.Visible = true;
                        lblFollowup.ForeColor = System.Drawing.Color.Red;
                        lblFollowup.Text = "The last follow up date was : " + Convert.ToDateTime(ds.Tables[0].Rows[0]["FollowupDate"].ToString()).Date.ToString("dd-MM-yyyy"); ;
                    }
                    else
                    {
                        lblFollowup.Text = "";
                        followupdate.Visible = false;
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
                else if (e.CommandName == "DeleteLead")
                {
                    lbldeletemessage.Text = "Are you sure, you want to delete Consultant Details?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDeleteModal();", true);
                }

                else if (e.CommandName == "Quote")
                {
                    string ClientName = encryptdecrypt.Encrypt(((Label)row.FindControl("lblFirstName")).Text.ToString() + " " + ((Label)row.FindControl("lblLastName")).Text.ToString());
                    string product = encryptdecrypt.Encrypt(((Label)row.FindControl("lblProdType")).Text.ToString());
                    string source = encryptdecrypt.Encrypt((((Label)row.FindControl("lblOrigin")).Text.ToString()));
                    string toCity = encryptdecrypt.Encrypt(((Label)row.FindControl("lblDestination")).Text.ToString());
                    string Email = encryptdecrypt.Encrypt(((Label)row.FindControl("lblEmailID")).Text.ToString());
                    string encryptedparamleadid = encryptdecrypt.Encrypt(ViewState["lsID"].ToString());
                    string url = "Quote.aspx?id=" + Server.UrlEncode(encryptedparamleadid) + "&city=" + Server.UrlEncode(toCity) + "&client=" + Server.UrlEncode(ClientName) + "&source=" + Server.UrlEncode(source) + "&prod=" + Server.UrlEncode(product) + "&em=" + Server.UrlEncode(Email);
                    string s = "window.open('" + url + "', '_blank');";
                    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }
                else if (e.CommandName == "PDF")
                {
                    string quoteNumber = ((Label)row.FindControl("lblQuoteNumber")).Text.ToString();
                    //string path = Server.MapPath("~/QuotePDF");
                    //Process.Start(fileName);
                    string path = "http://tcrm.askswg.co.za/QuotePDF/";
                    string fileName = path + "\\" + quoteNumber + ".pdf";
                    string s = "window.open('" + fileName + "', '_blank');";
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
    protected void gvAssignedList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAssignedList.PageIndex = e.NewPageIndex;
        GetAssinedLeadsList();
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
                controlButton.Visible = true;
            }

            e.Row.BackColor = ((Label)e.Row.FindControl("lblDuplicateLead")).Text.ToString() == "Y" ? System.Drawing.Color.LightBlue : System.Drawing.Color.White;
            e.Row.ForeColor = ((Label)e.Row.FindControl("lblDuplicateLead")).Text.ToString() == "Y" ? System.Drawing.Color.White : System.Drawing.Color.Black;

           
            //e.Row.Attributes.CssStyle.Value = ((Label)e.Row.FindControl("lblDuplicateLead")).Text.ToString() == "Y" ? "color: Red" : "color: White";

        }
    }
}