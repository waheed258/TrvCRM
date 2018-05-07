using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;
using System.IO;
using System.Text;

public partial class Quote : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    QuoteBL qtBL = new QuoteBL();
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();
    int LeadID = 0;
    string city = string.Empty;
    string clEmail = string.Empty;
    LeadBL leadBL = new LeadBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            city = encryptdecrypt.Decrypt(Request.QueryString["city"]);
            LeadID = Convert.ToInt32(encryptdecrypt.Decrypt(Request.QueryString["id"]));
            clEmail = encryptdecrypt.Decrypt(Request.QueryString["em"]);

            if (!IsPostBack)
            {               
                lblClientName.Text = encryptdecrypt.Decrypt(Request.QueryString["client"]);
                lblProduct.Text = encryptdecrypt.Decrypt(Request.QueryString["prod"]);
                lblSource.Text = encryptdecrypt.Decrypt(Request.QueryString["source"]);
                lblDestination.Text = encryptdecrypt.Decrypt(Request.QueryString["city"]);
                GetCostTypeDataAdult();
                GetCostTypeDataChild();
                
                string result = GetQuoteData();

                if (result == "0")
                {
                    Clear();
                    GetIncludeExcludeData();
                }

            }            

            if (txtAdultPrice.Text != "")
            {
                ddlAdultPersons.Enabled = true;
            }
            else
            {
                ddlAdultPersons.Enabled = false;
            }

            if (txtChildPrice.Text != "")
            {
                ddlChildPersons.Enabled = true;
            }
            else
            {
                ddlChildPersons.Enabled = false;
            }

        }
        catch
        {  }
    }
    
    protected void GetCostTypeDataAdult()
    {
        try
        {            
            dataset = qtBL.GetTypeData("A");
            ddlAdultType.DataSource = dataset;
            ddlAdultType.DataTextField = "CostTypeDescription";
            ddlAdultType.DataValueField = "CostTypeID";
            ddlAdultType.DataBind();
            ddlAdultType.Items.Insert(0, new ListItem("--Select Cost Type --", "-1"));
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }

    protected void GetCostTypeDataChild()
    {
        try
        {
            dataset = qtBL.GetTypeData("C");
            ddlChildType.DataSource = dataset;
            ddlChildType.DataTextField = "CostTypeDescription";
            ddlChildType.DataValueField = "CostTypeID";
            ddlChildType.DataBind();
            ddlChildType.Items.Insert(0, new ListItem("--Select Cost Type --", "-1"));
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }

    protected string GetQuoteData()
    {
        string strResult = string.Empty;
        try
        {
            dataset = qtBL.GetQuotePDFData(LeadID);

            if (dataset.Tables.Count > 0)
            {
               
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    strResult = "1";
                    txtDate.Text = dataset.Tables[0].Rows[0]["QuoteDate"].ToString();
                    ddlAdultType.SelectedValue = dataset.Tables[0].Rows[0]["CostForAdultType"].ToString();

                    txtAdultPrice.Text = dataset.Tables[0].Rows[0]["CostForAdult"].ToString();
                    ddlAdultPersons.SelectedValue = dataset.Tables[0].Rows[0]["NoOfAdults"].ToString();
                    lblAdultTotPrice.Text = dataset.Tables[0].Rows[0]["AdultTotal"].ToString();

                    ddlChildType.SelectedValue = dataset.Tables[0].Rows[0]["CostForChildType"].ToString();
                    txtChildPrice.Text = dataset.Tables[0].Rows[0]["CostForChild"].ToString();
                    ddlChildPersons.SelectedValue = dataset.Tables[0].Rows[0]["NoOfChildren"].ToString();
                    lblChildTotPrice.Text = dataset.Tables[0].Rows[0]["ChildTotal"].ToString();

                    txtFlightDetails.Text = dataset.Tables[0].Rows[0]["FlightDetails"].ToString();
                    txtCarHireDetails.Text = dataset.Tables[0].Rows[0]["CarHireDetails"].ToString();
                    txtHotelInfo.Text = dataset.Tables[0].Rows[0]["HotelInfo"].ToString();
                    txtItinerary.Text = dataset.Tables[0].Rows[0]["ItineraryDetails"].ToString();
                    txtIncludes.Text = dataset.Tables[0].Rows[0]["Includes"].ToString();
                    txtExcludes.Text = dataset.Tables[0].Rows[0]["Excludes"].ToString();
                    txtTravelInsur.Text = dataset.Tables[0].Rows[0]["TravelInsurance"].ToString();


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
                else
                {
                    strResult = "0";
                }
            }
            

        }
        catch
        { strResult = "0"; }

        return strResult;
    }

    protected void GetIncludeExcludeData()
    {
        try
        {
            dataset = qtBL.GetIncudeExcludes();

           txtIncludes.Text = dataset.Tables[0].Rows[0]["IncludesDescription"].ToString();
           txtExcludes.Text = dataset.Tables[1].Rows[0]["ExcludesDescription"].ToString();

        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
        }
    }
    protected void ddlAdultPersons_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAdultPrice.Text != "" && ddlAdultPersons.SelectedValue != "0")
        {
            lblAdultTotPrice.Text = Convert.ToString(Convert.ToInt32(txtAdultPrice.Text) * Convert.ToInt32(ddlAdultPersons.SelectedValue));
        }
    }
    protected void imgbtnSubmitAssign_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            QuoteEntity qtEntity = new QuoteEntity();

            qtEntity.CarHireDetails = txtHotelInfo.Text;
            qtEntity.ConsultantName = Session["Name"].ToString();
            qtEntity.CostForAdult = txtAdultPrice.Text;
            qtEntity.CostForAdultType = Convert.ToInt32(ddlAdultType.SelectedValue);
            qtEntity.CostForChild = txtChildPrice.Text;
            qtEntity.CostForChildType = Convert.ToInt32(ddlChildType.SelectedValue);
            qtEntity.Excludes = txtExcludes.Text;
            qtEntity.FlightDetails = txtFlightDetails.Text;
            qtEntity.HotelInfo = txtHotelInfo.Text;
            qtEntity.Includes = txtIncludes.Text;
            qtEntity.ItineraryDetails = txtItinerary.Text;
            qtEntity.LeadID = LeadID;
            qtEntity.NoOfAdults = Convert.ToInt32(ddlAdultPersons.SelectedValue);
            qtEntity.NoOfChildren = Convert.ToInt32(ddlChildPersons.SelectedValue);
            qtEntity.QuoteDate = txtDate.Text;
            qtEntity.ToCity = city;
            qtEntity.TravelInsurance = txtTravelInsur.Text;
            qtEntity.AdultTotal = lblAdultTotPrice.Text;
            qtEntity.ChildTotal = lblChildTotPrice.Text;
            qtEntity.IsMailSent = "N";
            qtEntity.QuoteNumber = "";
            qtEntity.Operation = "I";


            //string result = qtBL.CUDQuote(qtEntity);

            string QuoteNumber = qtBL.CUOperationQuote(qtEntity);

            if (QuoteNumber != "")
            {
                message.Text = "Quote Details saved Successfully!";
                message.ForeColor = System.Drawing.Color.Green;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);               
                GetPdf(QuoteNumber);
                Clear();
            }
            else
            {
                message.Text = "Please try again!";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
  

    private void Clear()
    {
        txtDate.Text = "";
        ddlAdultType.SelectedValue = "-1";
        txtAdultPrice.Text = "";
        ddlAdultPersons.SelectedValue = "0";
        ddlChildType.SelectedValue = "-1";
        txtChildPrice.Text = "";
        ddlChildPersons.SelectedValue = "0";
        lblChildTotPrice.Text = "";
        lblAdultTotPrice.Text = "";
        txtFlightDetails.Text = "";
        txtCarHireDetails.Text = "";
        txtHotelInfo.Text = "";
        txtItinerary.Text = "";
        //txtIncludes.Text = "";
        //txtExcludes.Text = "";
        txtTravelInsur.Text = "";

        dvAdultPersons.Visible = false;
        dvChildPersons.Visible = false;
        dvAdultTot.Visible = false;
        dvChildTotalPrice.Visible = false;
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
    }


    private void GetPdf(string QuoteNumber)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = qtBL.GetQuotePDFData(LeadID);
            StreamReader reader = new StreamReader(Server.MapPath("~/QuotePDF.html"));
            string readFile = reader.ReadToEnd();
            reader.Close();

            StringBuilder sbMainrow = new StringBuilder();

            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dtlRow in ds.Tables[0].Rows)
                    {
                        readFile = readFile.Replace("{QuoteNumber}", dtlRow["QuoteNumber"].ToString());
                        readFile = readFile.Replace("{QuoteDate}", dtlRow["QuoteDate"].ToString());
                        readFile = readFile.Replace("{DestinationCity}", dtlRow["DestinationCity"].ToString());
                        readFile = readFile.Replace("{TravelMonth}", dtlRow["QuoteDate"].ToString());
                        readFile = readFile.Replace("{FlightDetails}", dtlRow["FlightDetails"].ToString());
                        readFile = readFile.Replace("{Includes}", dtlRow["Includes"].ToString());
                        readFile = readFile.Replace("{Excludes}", dtlRow["Excludes"].ToString());
                        readFile = readFile.Replace("{TravelInsurance}", dtlRow["TravelInsurance"].ToString());
                        readFile = readFile.Replace("{ConsultantName}", dtlRow["ConsultantName"].ToString());
                        readFile = readFile.Replace("{HotelDetails}", dtlRow["HotelInfo"].ToString());
                        readFile = readFile.Replace("{CarHireDetails}", dtlRow["CarHireDetails"].ToString());
                        readFile = readFile.Replace("{Itinerary}", dtlRow["ItineraryDetails"].ToString());

                        if (dtlRow["CostForAdultType"].ToString() == "1")
                        {
                            readFile = readFile.Replace("{AdultPrice}", "COST PER PERSON SHARING R " + dtlRow["CostForAdult"].ToString() + " x " + dtlRow["NoOfAdults"].ToString() + " adults");
                        }
                        else if (dtlRow["CostForAdultType"].ToString() == "2")
                        {
                            readFile = readFile.Replace("{AdultPrice}", "COST PER PERSON INDIVIDUAL R " + dtlRow["CostForAdult"].ToString() + " x 1 adult");
                        }

                        if (dtlRow["CostForChildType"].ToString() == "3")
                        {
                            readFile = readFile.Replace("{ChildPrice}", "COST PER CHILD SHARING R " + dtlRow["CostForChild"].ToString() + " x " + dtlRow["NoOfChildren"].ToString() + " child");
                        }
                        else
                        {
                            readFile = readFile.Replace("{ChildPrice}", "");
                        }
                    }
                }

                string StrContent = readFile;

                string filepath = Server.MapPath("~/QuotePDF");


              bool pdf =  GenerateHTML_TO_PDF(StrContent, true, filepath, false, QuoteNumber);
              if (pdf)
              {
                  string consultName = Session["Name"].ToString();
                  SendMail(lblClientName.Text, clEmail, lblDestination.Text, consultName, QuoteNumber);
              }              

            }
        }
        catch
        { }

    }

    private bool GenerateHTML_TO_PDF(string HtmlString, bool ResponseShow, string Path, bool SaveFileDir, string QuoteNumber)
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

            string FileName = Path + "/" + QuoteNumber + ".pdf";

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            doc.Save(FileName);

            doc.Close();  
         
            return true;

        }
        catch
        { return false; }
    }

    public void SendMail(string clName, string clEmail, string clDestinationCity, string consultName, string QuoteNumber)
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
                string MailTo = "ramesh.palaparti@dinoosys.com";
                string DisplayNameTo = string.Empty;
                string MailCc = string.Empty;
                string DisplayNameCc = string.Empty;
                string MailBcc = string.Empty;
                string Subject = string.Empty;
                string MailText = string.Empty;
                string Attachment = string.Empty;

                string filepath = Server.MapPath("~/QuotePDF");
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
                    MailText += "Kind regards, <br/>";
                    MailText += "(" + consultName + ")";                    

                  bool mailSent =   CommanClass.UpdateMail(SmtpServer, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo, DisplayNameTo, MailCc, "", "", "", DisplayNameCc, MailBcc, Subject, MailText, Attachment);

                  if (mailSent)
                  {
                      MailSentSatatus(QuoteNumber);
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
            qtEntity.NoOfChildren =0;
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
        {   }
    }

}