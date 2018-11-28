using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessEntities;
using BusinessLogic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;

public partial class ConvertQuote : System.Web.UI.Page
{
    QuoteBL quoteBl = new QuoteBL();
    string strQuoteNumber = string.Empty;
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();
    QuoteBL qtBL = new QuoteBL();
    int LeadID = 0;
    string ClientFieldId = string.Empty;
    string EmailID = string.Empty;
    LeadBL leadBl = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        LeadID = Convert.ToInt32(Request.QueryString["id"]);
        strQuoteNumber = Request.QueryString["Quotenum"];
        ClientFieldId = Request.QueryString["clientFieldID"];

        if (!IsPostBack)
        {
            GetCostTypeDataAdult();
            GetCostTypeDataChild();
            GetGridData();
        }
    }
    private bool GenerateHTML_TO_PDF1(string HtmlString, bool ResponseShow, string Path, bool SaveFileDir, string QuoteNumber)
    {
        try
        {
            string pdf_page_size = "A4";
            SelectPdf.PdfPageSize pageSize = (SelectPdf.PdfPageSize)Enum.Parse(typeof(SelectPdf.PdfPageSize),
                pdf_page_size, true);

            string pdf_orientation = "Portrait";
            SelectPdf.PdfPageOrientation pdfOrientation =
                (SelectPdf.PdfPageOrientation)Enum.Parse(typeof(SelectPdf.PdfPageOrientation),
                pdf_orientation, true);

            int webPageWidth = 1024;
            int webPageHeight = 0;

            // instantiate a html to pdf converter object
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            // create a new pdf document converting an url
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(HtmlString, "");

            // save pdf document      

            //if (!SaveFileDir)
            //    doc.Save(Response, ResponseShow, Path);
            //else
            //    doc.Save(Path);

            string FileName = Path + "/" + "QuoteNumber" + ".pdf";

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            else
            {
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }
            }

            doc.Save(FileName);

            //doc.Close();

            //doc.Save(FileName);


            string FilePath = FileName;
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }


            //if (FileName != "")
            //    doc.Save(FileName);

            doc.Close();

            return true;

        }
        catch
        { return false; }
    }
    protected void btnConvertToBook_Click(object sender, EventArgs e)
    {
        StringBuilder sbMainrow = new StringBuilder();
        StreamReader reader = new StreamReader(Server.MapPath("~/ConvertQuotePDF.html"));
        string readFile = reader.ReadToEnd();
        reader.Close();

        if (!string.IsNullOrEmpty(txtFlightDetails.Text))
        {
            sbMainrow.Append(" <div class='col-md-12'> <h4>Flight Details:</h4>" + txtFlightDetails.Text + "</div>");
        }

        if (!string.IsNullOrEmpty(txtHotelInfo.Text))
        {
            sbMainrow.Append(" <div class='col-md-12'> <h4>Hotel Details:</h4>" + txtHotelInfo.Text + "</div>");
        }

        if (!string.IsNullOrEmpty(txtCarHireDetails.Text))
        {
            sbMainrow.Append(" <div class='col-md-12'> <h4>Car Hire:</h4>" + txtCarHireDetails.Text + "</div>");
        }

        if (!string.IsNullOrEmpty(txtItinerary.Text))
        {
            sbMainrow.Append(" <div class='col-md-12'> <h4>Itinerary:</h4>" + txtItinerary.Text + "</div>");
        }

        if (!string.IsNullOrEmpty(txtIncludes.Text))
        {
            sbMainrow.Append(" <div class='col-md-12'> <h4>Includes:</h4>" + txtIncludes.Text + "</div>");
        }

        if (!string.IsNullOrEmpty(txtExcludes.Text))
        {
            sbMainrow.Append(" <div class='col-md-12'> <h4>Excluded:</h4>" + txtExcludes.Text + "</div>");
        }

        readFile = readFile.Replace("{QuoteDate}", txtDate.Text);
        readFile = readFile.Replace("{lsClientFileId}", ClientFieldId);
        readFile = readFile.Replace("{DestinationCity}", txtDestination.Text);
        readFile = readFile.Replace("{TravelInsurance}", txtTravelInsur.Text);
        readFile = readFile.Replace("{ConsultantName}", Session["Name"].ToString());
        readFile = readFile.Replace("{ClientName}", Session["Name"].ToString());
        readFile = readFile.Replace("{ChildTotal}", lblChildTotPrice.Text);
        readFile = readFile.Replace("{Includes}", txtIncludes.Text);
        readFile = readFile.Replace("{Excludes}", txtExcludes.Text);
        readFile = readFile.Replace("{FlightDetails}", txtFlightDetails.Text);
        readFile = readFile.Replace("{HotelDetails}", txtHotelInfo.Text);
        readFile = readFile.Replace("{CarDetails}", txtCarHireDetails.Text);
        readFile = readFile.Replace("{GrandTotal}", lblGrandTotal.Text.ToString());
        //readFile = readFile.Replace("{LeadStatus}", lStatus);



        if (ddlAdultType.SelectedValue == "1")
        {
            readFile = readFile.Replace("{AdultPrice}", "COST PER PERSON SHARING R " + txtAdultPrice.Text + " x " + ddlAdultPersons.SelectedValue + " adults");
            readFile = readFile.Replace("{AdultTotal}", lblAdultTotPrice.Text);
        }
        else if (ddlAdultType.SelectedValue == "2")
        {
            readFile = readFile.Replace("{AdultPrice}", "COST PER PERSON INDIVIDUAL R " + txtAdultPrice.Text + " x 1 adult");
            readFile = readFile.Replace("{AdultTotal}", txtAdultPrice.Text);
        }

        if (ddlChildType.SelectedValue == "3")
        {
            readFile = readFile.Replace("{ChildPrice}", "COST PER CHILD SHARING R " + txtChildPrice.Text + " x " + ddlChildPersons.SelectedValue + " child");
        }
        else
        {
            readFile = readFile.Replace("{ChildPrice}", "");
        }

        readFile = readFile.Replace("{ConsultantEmail}", Session["ConsultantEmail"].ToString());

        string StrContent = readFile;

        string filepath = Server.MapPath("~/ConvertToBookPDF");


        bool pdf = GenerateHTML_TO_PDF1(StrContent, true, filepath, false, "");
        if (pdf)
        {
            string consultName = Session["Name"].ToString();
            SendMail(lblClientName.Text, EmailID, txtDestination.Text, consultName, strQuoteNumber);
        }

    }
    public void SendMail(string clName, string clEmail, string clDestinationCity, string consultName, string QuoteNumber)
    {
        try
        {
            DataSet ds = leadBl.GetMailInfo();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string SmtpServer = ds.Tables[0].Rows[0]["con_smtp_host"].ToString();
                int SmtpPort = Convert.ToInt32(ds.Tables[0].Rows[0]["con_smtp_port"].ToString());
                //int SmtpPort = 587;
                string MailFrom = ds.Tables[0].Rows[0]["con_mail_from"].ToString();
                string DisplayNameFrom = ds.Tables[0].Rows[0]["con_from_name"].ToString();
                string FromPassword = ds.Tables[0].Rows[0]["con_from_pwd"].ToString();
                string MailTo = clEmail;
                //string MailTo = "karen@serendipitytours.co.za";
                string DisplayNameTo = string.Empty;
                string MailCc = string.Empty;
                string DisplayNameCc = string.Empty;
                string MailBcc = string.Empty;
                string Subject = string.Empty;
                string MailText = string.Empty;
                string Attachment = string.Empty;
                string cusAttachment = string.Empty;

                string filepath = Server.MapPath("~/ConvertToBookPDF");
                string FileName = filepath + "\\" + QuoteNumber + ".pdf";

                if (File.Exists(FileName))
                {
                    Attachment = FileName;
                }

                try
                {
                    Subject = "Serendipity Tours quote to " + clDestinationCity;
                    MailCc = "";

                    MailText = "Dear " + clName + ", <br/><br/>";
                    MailText += "Thank you for the opportunity to quote for your holiday to <b>" + clDestinationCity + "</b> <br/><br/>";
                    MailText += "Please find attached the options as discussed. Should you require any changes or amendments, please do not hesitate to contact me. I will be contacting you shortly to discuss the quote. <br/><br/>";
                    MailText += "Kind regards, <br/><br/>";
                    //MailText += "(" + consultName + ")";

                    MailText += "<div style='float:left; width:10%; border-right:3px solid #03F; padding:0 20px; margin-right:50px;'><img style='width:100%; display:block;' src='http://tcrm.askswg.co.za/images/logoEmail.png' /></div><div><h1 style='color:#3fa9df; margin:0 0 5px; font-size:12px;'>" + Session["Name"].ToString() + "</h1><h3 style='color:#25377b; margin:0 0 5px; font-size:12px; font-weight:400;'>Travel Consultant</h3><h5 style='color:#25377b; margin:0 0 5px; font-size:12px; font-weight:400;'>+27 31 2010 630 <span style='color:#3fa9df;'>|</span>" + Session["ConsultantEmail"].ToString() + "</h5><p style='color:#25377b; margin:0 0 0px; font-size:12px; font-weight:400;margin-left:165px;'><a href='#'><img src='http://tcrm.askswg.co.za/images/facebook.png' style='width:3%' /></a>&nbsp; <a href='#'><img src='http://tcrm.askswg.co.za/images/twitter.png' style='width:3%' /></a>&nbsp; <a href='#'><img src='http://tcrm.askswg.co.za/images/linkedin.png' style='width:3%' /></a>&nbsp; &nbsp; &nbsp;Suite 3, 2nd floor Silver Oaks, 36 Silverton Road, Musgruve, Durban</p></div>";
                    bool mailSent = UpdateCustomMail(SmtpServer, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo, DisplayNameTo, MailCc, "", "", "", DisplayNameCc, MailBcc, Subject, MailText, Attachment);

                    if (mailSent)
                    {
                        MailSentSatatus(QuoteNumber);
                        CommanClass.MailStatusLog(LeadID, "BT001", "Success", "", QuoteNumber);
                        Response.Redirect("EditLead.aspx?t=quote&idq=" + LeadID);

                    }
                    else
                    {
                        CommanClass.MailStatusLog(LeadID, "QT001", "Fail", "", QuoteNumber);
                    }

                }
                catch
                { }

            }
        }
        catch
        { }
    }
    public void MailSentSatatus(string QuoteNumber)
    {
        try
        {
            QuoteEntity qtEntity = new QuoteEntity();

            qtEntity.CarHireDetails = "";
            qtEntity.ConsultantName = "";
            qtEntity.CostForAdult = "";
            qtEntity.CostForAdultType = 0;
            qtEntity.CostForChild = "";
            qtEntity.CostForChildType = 0;
            qtEntity.Excludes = "";
            qtEntity.FlightDetails = "";
            qtEntity.HotelInfo = "";
            qtEntity.Includes = "";
            qtEntity.ItineraryDetails = "";
            qtEntity.LeadID = 0;
            qtEntity.NoOfAdults = 0;
            qtEntity.NoOfChildren = 0;
            qtEntity.QuoteDate = "";
            qtEntity.ToCity = "";
            qtEntity.TravelInsurance = "";
            qtEntity.AdultTotal = "";
            qtEntity.ChildTotal = "";
            qtEntity.IsMailSent = "Y";
            qtEntity.QuoteNumber = QuoteNumber;
            qtEntity.Operation = "U";

            string strQuoteNumber = qtBL.CUOperationQuote(qtEntity);
        }
        catch
        { }
    }
    public bool UpdateCustomMail(string SmtpHost, int SmtpPort, string MailFrom, string DisplayNameFrom, string FromPassword, string MailTo, string DisplayNameTo, string MailCc, string mailCc2, string mailCc3, string mailCc4, string DisplayNameCc, string MailBcc, string Subject, string MailText, string Attachment)
    {
        MailMessage myMessage = new MailMessage();
        bool IsSucces = false;
        try
        {
            myMessage.From = new MailAddress(MailFrom, DisplayNameFrom);
            if (MailTo != "")
                myMessage.To.Add(new MailAddress(MailTo, DisplayNameTo));
            if (MailCc != "")
                myMessage.CC.Add(new MailAddress(MailCc, DisplayNameCc));
            if (mailCc2 != "")
                myMessage.CC.Add(new MailAddress(mailCc2, DisplayNameCc));
            if (mailCc3 != "")
                myMessage.CC.Add(new MailAddress(mailCc3, DisplayNameCc));
            if (mailCc4 != "")
                myMessage.CC.Add(new MailAddress(mailCc4, DisplayNameCc));

            if (MailBcc != "")
                myMessage.Bcc.Add(MailBcc);

            myMessage.Subject = Subject;
            myMessage.IsBodyHtml = true;
            myMessage.Body = MailText;

            //create Alrternative HTML view
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(MailText, null, "text/html");



            //Add view to the Email Message
            myMessage.AlternateViews.Add(htmlView);


            if (Attachment != "")
            {
                Attachment a = new Attachment(Attachment);
                myMessage.Attachments.Add(a);
            }

            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile PostedFile = Request.Files[i];
                    if (PostedFile.ContentLength > 0)
                    {
                        myMessage.Attachments.Add(new Attachment(PostedFile.InputStream, PostedFile.FileName));
                    }
                }
            }            
            SmtpClient mySmtpClient = new SmtpClient(SmtpHost, SmtpPort);
            mySmtpClient.Credentials = new System.Net.NetworkCredential(MailFrom, FromPassword);
            mySmtpClient.EnableSsl = true;
            mySmtpClient.Send(myMessage);
            IsSucces = true;
        }
        catch
        {
            IsSucces = false;
        }
        finally
        {
            myMessage = null;
        }
        return IsSucces;
    }
    private void GetGridData()
    {
        try
        {
            DataSet ds = quoteBl.GetConvertQuoteDetails(strQuoteNumber);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblClientName.Text = ds.Tables[0].Rows[0]["ClientName"].ToString();
                ddlPackage.SelectedItem.Text = ds.Tables[0].Rows[0]["lsProductId"].ToString();
                txtSource.Text = ds.Tables[0].Rows[0]["lsOriginName"].ToString();
                txtDestination.Text = ds.Tables[0].Rows[0]["lsDestinationName"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["QuoteDate1"].ToString();
                ddlAdultType.SelectedValue = ds.Tables[0].Rows[0]["CostForAdultType"].ToString();

                txtAdultPrice.Text = ds.Tables[0].Rows[0]["CostForAdult"].ToString();
                ddlAdultPersons.SelectedValue = ds.Tables[0].Rows[0]["NoOfAdults"].ToString();
                lblAdultTotPrice.Text = ds.Tables[0].Rows[0]["AdultTotal"].ToString();

                ddlChildType.SelectedValue = ds.Tables[0].Rows[0]["CostForChildType"].ToString();
                txtChildPrice.Text = ds.Tables[0].Rows[0]["CostForChild"].ToString();
                ddlChildPersons.SelectedValue = ds.Tables[0].Rows[0]["NoOfChildren"].ToString();
                lblChildTotPrice.Text = ds.Tables[0].Rows[0]["ChildTotal"].ToString().TrimEnd();

                txtFlightDetails.Text = ds.Tables[0].Rows[0]["FlightDetails"].ToString();
                txtCarHireDetails.Text = ds.Tables[0].Rows[0]["CarHireDetails"].ToString();
                txtHotelInfo.Text = ds.Tables[0].Rows[0]["HotelInfo"].ToString();
                txtItinerary.Text = ds.Tables[0].Rows[0]["ItineraryDetails"].ToString();
                txtIncludes.Text = ds.Tables[0].Rows[0]["Includes"].ToString();
                txtExcludes.Text = ds.Tables[0].Rows[0]["Excludes"].ToString();
                txtTravelInsur.Text = ds.Tables[0].Rows[0]["TravelInsurance"].ToString();
                lblGrandTotal.Text = Convert.ToString(Convert.ToInt32(lblAdultTotPrice.Text) + Convert.ToInt32(lblChildTotPrice.Text));
                EmailID = ds.Tables[0].Rows[0]["lsEmailId"].ToString();
                if (ddlAdultType.SelectedValue == "1")
                {
                    dvAdultPersons.Visible = true;
                    dvAdultTot.Visible = true;
                    ddlAdultPersons.Enabled = true;
                }

                if (ddlChildType.SelectedValue == "3")
                {
                    dvChildPersons.Visible = true;
                    ddlChildPersons.Enabled = true;
                    dvChildTotalPrice.Visible = true;
                }
            }

        }
        catch
        {

        }
    }
    protected void ddlAdultType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAdultPrice.Text = "";
        lblAdultTotPrice.Text = "";

        if (ddlAdultType.SelectedValue == "1")
        {
            dvAdultPersons.Visible = true;
            dvAdultTot.Visible = true;
        }
        else
        {
            dvAdultPersons.Visible = false;
            dvAdultTot.Visible = false;
            ddlAdultPersons.SelectedValue = "0";
            lblAdultTotPrice.Text = "";
        }
    }
    protected void ddlChildType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtChildPrice.Text = "";
        lblChildTotPrice.Text = "";
        if (ddlChildType.SelectedValue == "3")
        {
            dvChildPersons.Visible = true;
            dvChildTotalPrice.Visible = true;
        }
        else
        {
            dvChildPersons.Visible = false;
            dvChildTotalPrice.Visible = false;
            ddlChildPersons.SelectedValue = "0";
            lblChildTotPrice.Text = "";
        }
    }
    protected void ddlChildPersons_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtChildPrice.Text != "" && ddlChildPersons.SelectedValue != "0")
        {
            lblChildTotPrice.Text = Convert.ToString(Convert.ToInt32(txtChildPrice.Text) * Convert.ToInt32(ddlChildPersons.SelectedValue));
            if (lblAdultTotPrice.Text == "")
            {
                lblGrandTotal.Text = Convert.ToString(Convert.ToInt32(txtAdultPrice.Text) + Convert.ToInt32(lblChildTotPrice.Text));
            }
            else
                lblGrandTotal.Text = Convert.ToString(Convert.ToInt32(lblAdultTotPrice.Text) + Convert.ToInt32(lblChildTotPrice.Text));
        }
    }
    protected void ddlAdultPersons_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAdultPrice.Text != "" && ddlAdultPersons.SelectedValue != "0")
        {
            lblAdultTotPrice.Text = Convert.ToString(Convert.ToInt32(txtAdultPrice.Text) * Convert.ToInt32(ddlAdultPersons.SelectedValue));
            lblGrandTotal.Text = lblAdultTotPrice.Text;
        }
    }

    protected void backToLead_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditLead.aspx?t=quote&idq=" + LeadID);
    }
    protected void GetCostTypeDataAdult()
    {
        try
        {
            DataSet ds = qtBL.GetTypeData("A");
            ddlAdultType.DataSource = ds;
            ddlAdultType.DataTextField = "CostTypeDescription";
            ddlAdultType.DataValueField = "CostTypeID";
            ddlAdultType.DataBind();
            ddlAdultType.Items.Insert(0, new ListItem("--Select Cost Type --", "-1"));
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void GetCostTypeDataChild()
    {
        try
        {
            DataSet ds = qtBL.GetTypeData("C");
            ddlChildType.DataSource = ds;
            ddlChildType.DataTextField = "CostTypeDescription";
            ddlChildType.DataValueField = "CostTypeID";
            ddlChildType.DataBind();
            ddlChildType.Items.Insert(0, new ListItem("--Select Cost Type --", "-1"));
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}