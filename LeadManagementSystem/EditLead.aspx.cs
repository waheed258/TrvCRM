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

public partial class EditLead : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    LeadBL leadBL = new LeadBL();
    LeadEntity leadEntity = new LeadEntity();
    CommanClass _objComman = new CommanClass();
    ConsultantBL consultantBL = new ConsultantBL();
    FollowupEntity followupEntity = new FollowupEntity();
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();
    BasicDropdownBL _objBasicDropdownBL = new BasicDropdownBL();

    string ClientName = string.Empty;
    string product = string.Empty;
    string source = string.Empty;
    string toCity = string.Empty;
    string Email = string.Empty;
    string encryptedparamleadid = string.Empty;
    string encryptedparamlblProductID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["t"] == null)
            {
                Session["lsLeadActionsID"] = Request.QueryString["LeadActionID"];
            }
            ClientName = Request.QueryString["ClientName"];
            product = Request.QueryString["product"];
            source = Request.QueryString["source"];
            toCity = Request.QueryString["toCity"];
            Email = Request.QueryString["Email"];
            encryptedparamleadid = Request.QueryString["encryptedparamleadid"];
            encryptedparamlblProductID = Request.QueryString["encryptedparamlblProductID"];

            GetLeadInfo();
            GetStatus();
            GetSourceDataEdit("U");
            BindEMailTemplate();
            BindFollowupEmailTemplate();
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
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    private void GetLeadInfo()
    {
        dvEOthers.Visible = false;
        string strStatusId = Session["lsLeadActionsID"].ToString();
        Session["strStatusId"] = strStatusId;
        DataSet ds = leadBL.GetLeadInfo(Convert.ToInt32(Session["lsID"].ToString()));
        DataTable dtLead = ds.Tables[0];
        DataTable dtLeadHistory = ds.Tables[1];
        GetTemplateNames(Session["lsID"].ToString());
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
                lnkUrl.Text = "http://serendipitytravel.co.za/holiday_packages/" + dtLead.Rows[0]["lsPackageId"].ToString().Replace(" ","-");
                lnkUrl.NavigateUrl = "http://serendipitytravel.co.za/holiday_packages/" + dtLead.Rows[0]["lsPackageId"].ToString().Replace(" ", "-");
            }
            lblLNotes.Text = dtLead.Rows[0]["lsNotes"].ToString();
            if (dtLead.Rows[0]["lsPackageId"].ToString() == "" || dtLead.Rows[0]["lsPackageId"].ToString() == null)
            {
                lblPackageName.Text = "No Package name available";
            }
            else
            {
                lblPackageName.Text = dtLead.Rows[0]["lsPackageId"].ToString();
            }

            if (dtLead.Rows[0]["lsWebSiteConsultantNotes"].ToString() == "" || dtLead.Rows[0]["lsWebSiteConsultantNotes"].ToString() == null)
            {
                lblConsultantNotes.Text = "Not Available";
            }
            else
            {
                lblConsultantNotes.Text = dtLead.Rows[0]["lsWebSiteConsultantNotes"].ToString();
            }

            txtClientFileId.Text = dtLead.Rows[0]["lsClientFileId"].ToString();
            txtEConsultNotes.Text = dtLead.Rows[0]["lsConsultantNotes"].ToString();
            txtEReminder.Text = String.Format("{0:dd-MM-yyyy}", dtLead.Rows[0]["lsReminder"]);
            txtERemindNotes.Text = dtLead.Rows[0]["lsReminderNotes"].ToString();

            txtToEmail.Text = dtLead.Rows[0]["lsEmailId"].ToString();
            txtToEmailFU.Text = dtLead.Rows[0]["lsEmailId"].ToString();
            txtEmailSubject.Text = "Serendipity Tours >> More Info Required";
            txtEmailSubjectFU.Text = "Serendipity Travel >> Follow up";
            ddlESource.SelectedValue = dtLead.Rows[0]["lsSource"].ToString();
            if (ddlESource.SelectedValue == "10")
            {
                dvEOthers.Visible = true;
            }

            txtEOthers.Text = dtLead.Rows[0]["lsOthersInfo"].ToString();

            ddlStatus.SelectedValue = dtLead.Rows[0]["lsLeadStatus"].ToString();

            if (dtLead.Rows[0]["lsLeadStatus"].ToString() == "6")
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
                    lblMessage.Text = "Maximum follow ups reached!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
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
        BindEMailTemplate();

        //Followup Email Template
        BindFollowupEmailTemplate();

        // Generate Quote URL

        string url = "Quote.aspx?id=" + encryptedparamleadid + "&city=" + toCity + "&client=" + ClientName + "&source=" + source + "&prod=" + product + "&em=" + Email + "&prodid=" + encryptedparamlblProductID;
        hdfQuoteUrl.Value = url;
        Session["QuoteUrl"] = hdfQuoteUrl.Value;

    }

    private void BindEMailTemplate()
    {
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
        //sb.Append("<div style='float:left; width:10%; border-right:3px solid #03F; padding:0 20px; margin-right:50px;'><img style='width:100%; display:block;' src='http://tcrm.askswg.co.za/images/logoEmail.png' /></div><div><h1 style='color:#3fa9df; margin:0 0 5px; font-size:12px;'>" + Session["Name"].ToString() + "</h1><h3 style='color:#25377b; margin:0 0 5px; font-size:12px; font-weight:400;'>Travel Consultant</h3><h5 style='color:#25377b; margin:0 0 5px; font-size:12px; font-weight:400;'>+27 31 2010 630 <span style='color:#3fa9df;'>|</span>" + Session["ConsultantEmail"].ToString() + "</h5><p style='color:#25377b; margin:0 0 0px; font-size:12px; font-weight:400;margin-left:165px;'><a href='#'><img src='http://tcrm.askswg.co.za/images/facebook.png' style='width:3%' /></a>&nbsp; <a href='#'><img src='http://tcrm.askswg.co.za/images/twitter.png' style='width:3%' /></a>&nbsp; <a href='#'><img src='http://tcrm.askswg.co.za/images/linkedin.png' style='width:3%' /></a>&nbsp; &nbsp; &nbsp;Suite 3, 2nd floor Silver Oaks, 36 Silverton Road, Musgruve, Durban</p></div>");

        txtMailTemp.Text = sb.ToString();
    }

    private void BindFollowupEmailTemplate()
    {
        StringBuilder sbFU = new StringBuilder();
        string strHeadingFU = string.Format("<p><strong>Dear " + lblLName.Text + "</strong></p>");
        sbFU.Append(strHeadingFU);
        sbFU.Append("<p>Thank you for coming through to me at Serendipity Travel, re. your travel request.</p>");
        sbFU.Append("<p>This is a courtesy email to follow up on the quote I have sent.  </p>");
        sbFU.Append("<p>Please advise if I can assist you further by changing anything on the quote sent or quoting you on alternative options.</p>");

        sbFU.Append("<p><strong>Kind regards</strong></p>");
        sbFU.Append("<p><strong>" + Session["Name"].ToString() + "</strong></p>");
        //sbFU.Append("<div style='float:left; width:10%; border-right:3px solid #03F; padding:0 20px; margin-right:50px;'><img style='width:100%; display:block;' src='http://tcrm.askswg.co.za/images/logoEmail.png' /></div><div><h1 style='color:#3fa9df; margin:0 0 5px; font-size:12px;'>" + Session["Name"].ToString() + "</h1><h3 style='color:#25377b; margin:0 0 5px; font-size:12px; font-weight:400;'>Travel Consultant</h3><h5 style='color:#25377b; margin:0 0 5px; font-size:12px; font-weight:400;'>+27 31 2010 630 <span style='color:#3fa9df;'>|</span>" + Session["ConsultantEmail"].ToString() + "</h5><p style='color:#25377b; margin:0 0 0px; font-size:12px; font-weight:400;margin-left:165px;'><a href='#'><img src='http://tcrm.askswg.co.za/images/facebook.png' style='width:3%' /></a>&nbsp; <a href='#'><img src='http://tcrm.askswg.co.za/images/twitter.png' style='width:3%' /></a>&nbsp; <a href='#'><img src='http://tcrm.askswg.co.za/images/linkedin.png' style='width:3%' /></a>&nbsp; &nbsp; &nbsp;Suite 3, 2nd floor Silver Oaks, 36 Silverton Road, Musgruve, Durban</p></div>");
        txtMailTempFU.Text = sbFU.ToString();
    }

    private void GetStatus()
    {
        try
        {
            DataSet dsStatus = new DataSet();
            dsStatus = _objBasicDropdownBL.GetStatus();
            ddlStatus.DataSource = dsStatus;
            ddlStatus.DataTextField = "LeadAction";
            ddlStatus.DataValueField = "LeadActionsID";
            ddlStatus.DataBind();
        }
        catch
        {

        }
    }
    protected void GetTemplateNames(string strLeadId)
    {
        ddlTemplateNames.Items.Clear();
        try
        {
            DataSet ds = leadBL.GetTemplateNames(Convert.ToInt32(strLeadId), Session["Name"].ToString());
            ddlTemplateNames.DataSource = ds;
            ddlTemplateNames.DataTextField = "TemplateName";
            ddlTemplateNames.DataValueField = "ID";
            ddlTemplateNames.DataBind();
            ddlTemplateNames.Items.Insert(0, new ListItem("--Select Template --", "-1"));
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
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
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFollowup.Text = "";
        if (ddlStatus.SelectedValue == "4")
        {
            DataSet ds = new DataSet();
            ds = leadBL.GetFollowupCount(Convert.ToInt32(Session["lsID"].ToString()));
            if (ds.Tables[0].Rows.Count == 3)
            {
                lblMessage.Text = "Maximum follow ups reached for this lead!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
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
    protected void imgEUpdate_Click1(object sender, EventArgs e)
    {
        try
        {
            leadEntity.LeadID = Convert.ToInt32(Session["lsID"].ToString());
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
                lblMessage.Text = "Lead Details updated Successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                DataSet dsInfo = leadBL.GetLeadInfo(Convert.ToInt32(Session["lsID"].ToString()));
                DataTable dtLeadHistory = dsInfo.Tables[1];
                LeadHistory(dtLeadHistory);
            }
            else
            {
                lblMessage.Text = "Please try again!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void imgECancel_Click1(object sender, EventArgs e)
    {
        Response.Redirect("AssignedLeads.aspx");
    }
    protected void imgQuoteSubmit_Click(object sender, EventArgs e)
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
            GetTemplateNames(Session["lsID"].ToString());
            dvTemplates.Visible = true;
        }
        else
        {
            dvTemplates.Visible = false;
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
                    else
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Mobile number format is wrong";
                    }
                    txtResp.Text = "Hi " + lblLName.Text + ", I have sent you the quote for " + lblPackageName.Text + ". Please check your spam folders incase you do not see the mail. Regards " + Session["Name"].ToString() + ".";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openSMSModal();", true);
                }
                else if (e.CommandName == "Convert")
                {
                    string strQuoteNumber = ((Label)row.FindControl("lblHistoryQuote")).Text.ToString();
                    string clientFieldID = txtClientFileId.Text;
                    string leadid = Session["lsID"].ToString();
                    Response.Redirect("ConvertQuote.aspx?id=" + leadid + "&Quotenum=" + strQuoteNumber + "&clientFieldID=" + clientFieldID, false);
                }

            }
        }
        catch
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Something went wrong, please contact administrator";
        }
    }
    protected void gvHistory_RowEditing(object sender, GridViewEditEventArgs e)
    {

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
    public void LeadHistory(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();

        if (dt.Rows.Count > 0)
        {
            gvHistory.DataSource = dt;
            gvHistory.DataBind();
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
        DataSet ds = leadBL.GetLeadInfo(Convert.ToInt32(Session["lsID"].ToString()));
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
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        string strEmail = txtToEmail.Text;
        string strCC = txtCCEmail.Text;
        string strSubject = txtEmailSubject.Text;
        string strBody = txtMailTemp.Text;
        SendMail(strEmail, strCC, strSubject, strBody, 2);
    }
    protected void btnSendMailFU_Click(object sender, EventArgs e)
    {
        string strEmail = txtToEmailFU.Text;
        string strCC = txtCCEmailFU.Text;
        string strSubject = txtEmailSubjectFU.Text;
        string strBody = txtMailTempFU.Text;
        SendMail(strEmail, strCC, strSubject, strBody, 1);
    }
    public void SendMail(string clEmail, string strCC, string srtSubject, string strText, int Type)
    {
        try
        {
            DataSet ds = leadBL.GetMailInfo();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string SmtpServer = ds.Tables[0].Rows[0]["con_smtp_host"].ToString();
                int SmtpPort = Convert.ToInt32(ds.Tables[0].Rows[0]["con_smtp_port"].ToString());
                //int SmtpPort = 587;
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
                    //if (Type == 1)
                    //{

                    //    MailText = "<p><strong>Dear Valued Client,</strong></p>";
                    //    MailText += "<p>Thanks for submitting a travel request through to Serendipity Travel.</p>";
                    //    MailText += "<p>I trust that our consultant has reverted to you with a suitable quotation specifict to your travel needs. Should you not have received a response, please do let us know.</p>";
                    //    MailText += "<p>Please advise if we can assist you further by tailor making a suitable package for you should the package sent through not be suitable enough.</p>";

                    //    MailText += "<p><strong>Kind regards</strong></p>";
                    //    MailText += "<div style='float:left; width:10%; border-right:3px solid #03F; padding:0 20px; margin-right:50px;'><img style='width:100%; display:block;' src='http://tcrm.askswg.co.za/images/logoEmail.png' /></div><div><h1 style='color:#3fa9df; margin:0 0 5px; font-size:12px;'>" + Session["Name"].ToString() + "</h1><h3 style='color:#25377b; margin:0 0 5px; font-size:12px; font-weight:400;'>Travel Consultant</h3><h5 style='color:#25377b; margin:0 0 5px; font-size:12px; font-weight:400;'>+27 31 2010 630 <span style='color:#3fa9df;'>|</span>" + Session["ConsultantEmail"].ToString() + "</h5><p style='color:#25377b; margin:0 0 0px; font-size:12px; font-weight:400;margin-left:165px;'><a href='#'><img src='http://tcrm.askswg.co.za/images/facebook.png' style='width:3%' /></a>&nbsp; <a href='#'><img src='http://tcrm.askswg.co.za/images/twitter.png' style='width:3%' /></a>&nbsp; <a href='#'><img src='http://tcrm.askswg.co.za/images/linkedin.png' style='width:3%' /></a>&nbsp; &nbsp; &nbsp;Suite 3, 2nd floor Silver Oaks, 36 Silverton Road, Musgruve, Durban</p></div>";
                    //}
                    //else
                    //{
                    //    MailText = "<p><strong>Dear ,</strong></p>" + lblLName.Text;
                    //    MailText += "<p>Thank you so much for your enquiry I received today. In order to quote you accurately, I require the following additional information.</p>";
                    //    MailText += "<p>1.&nbsp;Dates of travel</p>";
                    //    MailText += "<p>2.&nbsp;Destination</p>";
                    //    MailText += "<p>3.&nbsp;Where will you be travelling from ie. Joburg, Durban or Cape Town</p>";
                    //    MailText += "<p>4.&nbsp;Estimated budget</p>";
                    //    MailText += "<p>5.&nbsp;How many people will be travelling incl. children (and their ages)</p>";
                    //    MailText += "<p>6.&nbsp;Are you travelling for a special occation ie. birthday, anniversary, honeymoon etc.</p>";
                    //    MailText += "<p>As soon as I receive the above information, I can work on some options for you.</p>";
                    //    MailText += "<p><strong>Kind regards</strong></p>";
                    //    MailText += "<div style='float:left; width:10%; border-right:3px solid #03F; padding:0 20px; margin-right:50px;'><img style='width:100%; display:block;' src='http://tcrm.askswg.co.za/images/logoEmail.png' /></div><div><h1 style='color:#3fa9df; margin:0 0 5px; font-size:12px;'>" + Session["Name"].ToString() + "</h1><h3 style='color:#25377b; margin:0 0 5px; font-size:12px; font-weight:400;'>Travel Consultant</h3><h5 style='color:#25377b; margin:0 0 5px; font-size:12px; font-weight:400;'>+27 31 2010 630 <span style='color:#3fa9df;'>|</span>" + Session["ConsultantEmail"].ToString() + "</h5><p style='color:#25377b; margin:0 0 0px; font-size:12px; font-weight:400;margin-left:165px;'><a href='#'><img src='http://tcrm.askswg.co.za/images/facebook.png' style='width:3%' /></a>&nbsp; <a href='#'><img src='http://tcrm.askswg.co.za/images/twitter.png' style='width:3%' /></a>&nbsp; <a href='#'><img src='http://tcrm.askswg.co.za/images/linkedin.png' style='width:3%' /></a>&nbsp; &nbsp; &nbsp;Suite 3, 2nd floor Silver Oaks, 36 Silverton Road, Musgruve, Durban</p></div>";
                    //}

                    bool mailSent = CommanClass.UpdateMail(SmtpServer, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo, DisplayNameTo, MailCc, "", "", "", DisplayNameCc, MailBcc, Subject, MailText, Attachment);

                    if (mailSent)
                    {
                        lblMessage.Text = "Email sent successfully.";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        if (Type == 1)
                        {
                            CommanClass.MailStatusLog(Convert.ToInt32(Session["lsID"].ToString()), "FP001", "Success", "", "");
                        }
                        else
                        {
                            CommanClass.MailStatusLog(Convert.ToInt32(Session["lsID"].ToString()), "MI001", "Success", "", "");
                        }
                        DataSet dsInfo = leadBL.GetLeadInfo(Convert.ToInt32(Session["lsID"].ToString()));
                        DataTable dtLeadHistory = dsInfo.Tables[1];
                        // Lead History
                        LeadHistory(dtLeadHistory);
                    }
                    else
                    {
                        lblMessage.Text = "Email not sent.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        if (Type == 1)
                        {
                            CommanClass.MailStatusLog(Convert.ToInt32(Session["lsID"].ToString()), "FP001", "Fail", "", "");
                        }
                        else
                        {
                            CommanClass.MailStatusLog(Convert.ToInt32(Session["lsID"].ToString()), "MI001", "Fail", "", "");
                        }
                        DataSet dsInfo = leadBL.GetLeadInfo(Convert.ToInt32(Session["lsID"].ToString()));
                        DataTable dtLeadHistory = dsInfo.Tables[1];
                        // Lead History
                        LeadHistory(dtLeadHistory);
                    }

                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Email not sent.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    CommanClass.MailStatusLog(Convert.ToInt32(Session["lsID"].ToString()), "MI001", "Fail", ex.Message, "");

                    DataSet dsInfo = leadBL.GetLeadInfo(Convert.ToInt32(Session["lsID"].ToString()));
                    DataTable dtLeadHistory = dsInfo.Tables[1];
                    // Lead Hostory
                    LeadHistory(dtLeadHistory);
                }

            }
        }
        catch
        { }
    }
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openEmailModal();", true);
    }
    protected void btnSendFUEmail_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openFUEmailModal();", true);
    }
    protected void backToLead_Click(object sender, EventArgs e)
    {
        Response.Redirect("AssignedLeads.aspx");
    }
}