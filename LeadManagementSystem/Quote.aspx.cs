using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Web;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Configuration;
using System.Net.Mail;
using System.Net;

public partial class Quote : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    QuoteBL qtBL = new QuoteBL();
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();
    int LeadID = 0;
    string city = string.Empty;
    string clEmail = string.Empty;
    string QuoteType = string.Empty;
    string TempId = string.Empty;
    string flag = string.Empty;
    string quoteno = string.Empty;
    string lStatus = string.Empty;
    LeadBL leadBL = new LeadBL();
    //ProductBL productBL = new ProductBL();
    List<QuoteEntity> lstQuoteEntity = new List<QuoteEntity>();
    StringBuilder QuoteBuilder = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            city = Request.QueryString["city"];
            LeadID = Convert.ToInt32(Request.QueryString["id"]);
            quoteno = Request.QueryString["QuoteID"];
            flag = Request.QueryString["flag"];
            clEmail = Request.QueryString["em"];
            QuoteType = Request.QueryString["qtype"];
            TempId = Request.QueryString["temp"];
            lStatus = Request.QueryString["status"];
            if (!IsPostBack)
            {
                lblClientName.Text = Request.QueryString["client"];
                txtSource.Text = Request.QueryString["source"];
                txtDestination.Text = Request.QueryString["city"];
                GetCostTypeDataAdult();
                GetCostTypeDataChild();
                emailsection.Style.Add("display", "none");
                quotesection.Style.Add("display", "unset");
                backToLead.Visible = true;
                imgbtnBackQuote.Visible = false;
                if (flag == "1")
                {
                    if (QuoteType == "2")
                    {
                        GetTemplateQuoteData(TempId);
                    }
                    else if (QuoteType == "3")
                    {
                        Clear();
                        dvProdct.Visible = false;
                        dvCustomProduct.Visible = true;
                        txtTravelInsur.Text = "We recommend: Travel Policy @ R 350.00pp,  R12 million medical cover, max 30 days.";
                    }
                    else if (QuoteType == "1")
                    {
                        Clear();
                        dvProdct.Visible = true;
                        dvCustomProduct.Visible = false;
                        txtTravelInsur.Text = "We recommend: Travel Policy @ R 350.00pp,  R12 million medical cover, max 30 days.";
                    }
                }
                else
                {
                    GetQuoteData();
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

            if (QuoteType == "2")
            {
                btnTemplageName.Visible = false;
            }
        }
        catch
        { }
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
        dvAdultPersons.Visible = false;
        dvChildPersons.Visible = false;
        dvAdultTot.Visible = false;
        dvChildTotalPrice.Visible = false;
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
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
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
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected string GetQuoteData()
    {
        string strResult = string.Empty;
        try
        {
            dataset = qtBL.GetQuotePDFData(quoteno);

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
                    lblChildTotPrice.Text = dataset.Tables[0].Rows[0]["ChildTotal"].ToString().TrimEnd();

                    txtFlightDetails.Text = dataset.Tables[0].Rows[0]["FlightDetails"].ToString();
                    txtCarHireDetails.Text = dataset.Tables[0].Rows[0]["CarHireDetails"].ToString();
                    txtHotelInfo.Text = dataset.Tables[0].Rows[0]["HotelInfo"].ToString();
                    txtItinerary.Text = dataset.Tables[0].Rows[0]["ItineraryDetails"].ToString();
                    txtIncludes.Text = dataset.Tables[0].Rows[0]["Includes"].ToString();
                    txtExcludes.Text = dataset.Tables[0].Rows[0]["Excludes"].ToString();
                    txtTravelInsur.Text = dataset.Tables[0].Rows[0]["TravelInsurance"].ToString();

                    lblGrandTotal.Text = Convert.ToString(Convert.ToInt32(lblAdultTotPrice.Text) + Convert.ToInt32(lblChildTotPrice.Text));

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

                    if (QuoteType == "3")
                    {
                        dvProdct.Visible = false;
                        dvCustomProduct.Visible = true;
                        txtProduct.Text = dataset.Tables[0].Rows[0]["PackageId"].ToString();
                    }
                    else
                    {
                        dvProdct.Visible = true;
                        dvCustomProduct.Visible = false;
                        ddlPackage.SelectedValue = string.IsNullOrEmpty(dataset.Tables[0].Rows[0]["PackageId"].ToString()) ? "-1" : dataset.Tables[0].Rows[0]["PackageId"].ToString();
                    }


                }
                else
                {
                    strResult = "0";
                    if (QuoteType == "3")
                    {
                        dvProdct.Visible = false;
                        dvCustomProduct.Visible = true;
                    }
                    else
                    {
                        dvProdct.Visible = true;
                        dvCustomProduct.Visible = false;
                    }
                }
            }


        }
        catch
        { strResult = "0"; }

        return strResult;
    }
    protected string GetTemplateQuoteData(string id)
    {
        string strResult = string.Empty;
        try
        {
            dataset = qtBL.GetTemplateData(Convert.ToInt32(id));

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

                    string strCustom = dataset.Tables[0].Rows[0]["IsCustomTemplate"].ToString();

                    if (strCustom == "Y")
                    {
                        dvProdct.Visible = false;
                        dvCustomProduct.Visible = true;
                        txtProduct.Text = dataset.Tables[0].Rows[0]["PackageId"].ToString();
                    }
                    else
                    {
                        dvProdct.Visible = true;
                        dvCustomProduct.Visible = false;
                        ddlPackage.SelectedValue = dataset.Tables[0].Rows[0]["PackageId"].ToString();
                    }


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
    protected void ddlAdultPersons_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAdultPrice.Text != "" && ddlAdultPersons.SelectedValue != "0")
        {
            lblAdultTotPrice.Text = Convert.ToString(Convert.ToInt32(txtAdultPrice.Text) * Convert.ToInt32(ddlAdultPersons.SelectedValue));
            lblGrandTotal.Text = lblAdultTotPrice.Text;
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
    protected void imgbtnViewQuote_Click(object sender, EventArgs e)
    {
        StringBuilder sbMainrow = new StringBuilder();

        StringBuilder sbFlight = new StringBuilder();
        StringBuilder sbHotel = new StringBuilder();
        StringBuilder sbCar = new StringBuilder();

        if (!string.IsNullOrEmpty(txtFlightDetails.Text))
        {
            sbFlight.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
            sbFlight.Append("<tr>");
            sbFlight.Append("<td colspan='4' width='100%' style='font-weight:700;background-color:#00aeef; width:100%; padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Flight Quotation </td>");
            sbFlight.Append("</tr>");
            sbFlight.Append("<tr>");
            sbFlight.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>");
            sbFlight.Append(txtFlightDetails.Text);
            sbFlight.Append("</td>");
            sbFlight.Append("</tr>");
            sbFlight.Append("</table>");

        }
        if (!string.IsNullOrEmpty(txtHotelInfo.Text))
        {
            sbHotel.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
            sbHotel.Append("<tr>");
            sbHotel.Append("<td colspan='4' width='100%' style='font-weight:700;background-color:#00aeef; width:100%; padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Flight Quotation </td>");
            sbHotel.Append("</tr>");
            sbHotel.Append("<tr>");
            sbHotel.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>");
            sbHotel.Append(txtHotelInfo.Text);
            sbHotel.Append("</td>");
            sbHotel.Append("</tr>");
            sbHotel.Append("</table>");
        }

        if (!string.IsNullOrEmpty(txtCarHireDetails.Text))
        {
            sbCar.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
            sbCar.Append("<tr>");
            sbCar.Append("<td colspan='4' width='100%' style='font-weight:700;background-color:#00aeef; width:100%; padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Flight Quotation </td>");
            sbCar.Append("</tr>");
            sbCar.Append("<tr>");
            sbCar.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>");
            sbCar.Append(txtCarHireDetails.Text);
            sbCar.Append("</td>");
            sbCar.Append("</tr>");
            sbCar.Append("</table>");
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


        StreamReader reader = new StreamReader(Server.MapPath("~/QuotePDF.html"));
        string readFile = reader.ReadToEnd();
        reader.Close();

        readFile = readFile.Replace("{QuoteDate}", txtDate.Text);
        readFile = readFile.Replace("{DestinationCity}", txtDestination.Text);
        readFile = readFile.Replace("{TravelInsurance}", txtTravelInsur.Text);
        readFile = readFile.Replace("{ConsultantName}", Session["Name"].ToString());
        readFile = readFile.Replace("{ClientName}", lblClientName.Text.ToString());
        readFile = readFile.Replace("{ChildTotal}", lblChildTotPrice.Text);
        readFile = readFile.Replace("{Includes}", txtIncludes.Text);
        readFile = readFile.Replace("{Excludes}", txtExcludes.Text);
        readFile = readFile.Replace("{FlightDetails}", sbFlight.ToString());
        readFile = readFile.Replace("{HotelDetails}", sbHotel.ToString());
        readFile = readFile.Replace("{CarDetails}", sbCar.ToString());
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

        string filepath = Server.MapPath("~/QuotePDF");


        bool pdf = GenerateHTML_TO_PDF1(StrContent, true, filepath, false, "");

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
    protected void btnTemplageName_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "TemplageModal();", true);
    }
    protected void imgbtnSubmitAssign_Click(object sender, EventArgs e)
    {
        try
        {
            backToLead.Visible = false;
            imgbtnBackQuote.Visible = true;
            quotesection.Style.Add("display", "none");
            emailsection.Style.Add("display", "unset");
            lstQuoteEntity = (List<QuoteEntity>)Session["lstQuoteEntity"];
            if (lstQuoteEntity == null)
            {
                QuoteEntity qtEntity = new QuoteEntity();
                qtEntity.CarHireDetails = txtCarHireDetails.Text;
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

                if (QuoteType == "3")
                {
                    qtEntity.PackageId = txtProduct.Text;
                }
                else
                {
                    qtEntity.PackageId = ddlPackage.SelectedValue;
                }
                string QuoteNumber = qtBL.CUOperationQuote(qtEntity);
                ViewState["QuoteNumber"] = QuoteNumber;
                if (QuoteNumber != "")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "EmailModal();", true);
                    txtToEmailNew.Text = clEmail;
                    txtCLientNameNew.Text = lblClientName.Text;
                    txtEmailSubjectNew.Text = "Serendipity Tours quote";

                    // Email Template                    
                    StringBuilder sb = new StringBuilder();
                    string strHeading = string.Format("<p><strong>Dear {0},</strong></p>", lblClientName.Text);
                    sb.Append(strHeading);
                    sb.Append("<p>Thank you for the opportunity to quote for your holiday to" + ddlPackage.SelectedItem.Text + ". Please find attached the options as discussed. Should you require any changes or amendments, please do not hesitate to contact me. I will be contacting you shortly to discuss the quote.</p>");
                    sb.Append("<p><strong>Kind regards</strong></p>");
                    sb.Append("<p><strong>" + Session["Name"].ToString() + "</strong></p>");
                    txtMailTempNew.Text = sb.ToString();

                    Clear();
                }
                else
                {
                    lblMessage.Text = "Please try again!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                QuoteEntity qtEntity = new QuoteEntity();
                qtEntity.CarHireDetails = txtCarHireDetails.Text;
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

                if (QuoteType == "3")
                {
                    qtEntity.PackageId = txtProduct.Text;
                }
                else
                {
                    qtEntity.PackageId = ddlPackage.SelectedValue;
                }
                string QuoteNumber = qtBL.CUOperationQuote(qtEntity);
                qtEntity.QuoteNumber = QuoteNumber;
                lstQuoteEntity.Add(qtEntity);
                if (QuoteNumber != "")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "EmailModal();", true);
                    txtToEmailNew.Text = clEmail;
                    txtCLientNameNew.Text = lblClientName.Text;
                    txtEmailSubjectNew.Text = "Serendipity Tours quote";

                    // Email Template                    
                    StringBuilder sb = new StringBuilder();
                    string strHeading = string.Format("<p><strong>Dear {0},</strong></p>", lblClientName.Text);
                    sb.Append(strHeading);
                    sb.Append("<p>Thank you for the opportunity to quote for your holiday to" + ddlPackage.SelectedItem.Text + ". Please find attached the options as discussed. Should you require any changes or amendments, please do not hesitate to contact me. I will be contacting you shortly to discuss the quote.</p>");
                    sb.Append("<p><strong>Kind regards</strong></p>");
                    sb.Append("<p><strong>" + Session["Name"].ToString() + "</strong></p>");
                    txtMailTempNew.Text = sb.ToString();

                    Clear();
                }
                else
                {
                    lblMessage.Text = "Please try again!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void imgbtnAddMultipleOptions_Click(object sender, EventArgs e)
    {
        QuoteEntity qtEntity = new QuoteEntity();
        try
        {
            qtEntity.CarHireDetails = txtCarHireDetails.Text;
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

            if (QuoteType == "3")
            {
                qtEntity.PackageId = txtProduct.Text;
            }
            else
            {
                qtEntity.PackageId = ddlPackage.SelectedValue;
            }
            string QuoteNumber = qtBL.CUOperationQuote(qtEntity);
            qtEntity.QuoteNumber = QuoteNumber;
            if (Session["lstQuoteEntity"] == null)
            {
                lstQuoteEntity.Add(qtEntity);
                Session["lstQuoteEntity"] = lstQuoteEntity;
            }
            else
            {
                lstQuoteEntity = (List<QuoteEntity>)Session["lstQuoteEntity"];
                lstQuoteEntity.Add(qtEntity);
                Session["lstQuoteEntity"] = lstQuoteEntity;
            }
            Clear();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSaveTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            QuoteEntity qtEntity = new QuoteEntity();

            qtEntity.CarHireDetails = txtCarHireDetails.Text;
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
            if (QuoteType == "3")
            {
                qtEntity.PackageId = txtProduct.Text;
                qtEntity.IsCustomTemplate = "Y";
            }
            else
            {
                qtEntity.PackageId = ddlPackage.SelectedValue;
                qtEntity.IsCustomTemplate = "N";
            }

            qtEntity.TemplateName = txtTemplateName.Text;


            int result = qtBL.CreateQuoteTemplate(qtEntity);

            if (result == 1)
            {
                lblMessage.Text = "Template Details saved Successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "Please try again!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        lstQuoteEntity = (List<QuoteEntity>)Session["lstQuoteEntity"];
        if (lstQuoteEntity == null)
        {
            GetPdf(ViewState["QuoteNumber"].ToString());
            CommanClass.MailStatusLog(LeadID, "QT001", "Success", "", ViewState["QuoteNumber"].ToString());
        }
        else
        {
            GetPdfMultipleOptions(lstQuoteEntity);
            CommanClass.MailStatusLog(LeadID, "QT001", "Success", "", QuoteBuilder.ToString());
        }
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
            else
            {
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }
            }

            doc.Save(FileName);

            doc.Close();

            return true;

        }
        catch
        { return false; }
    }
    private void GetPdf(string QuoteNumber)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = qtBL.GetQuotePDFData(QuoteNumber);
            StreamReader reader = new StreamReader(Server.MapPath("~/QuotePDF.html"));
            string readFile = reader.ReadToEnd();
            reader.Close();

            StringBuilder sbFlight = new StringBuilder();
            StringBuilder sbHotel = new StringBuilder();
            StringBuilder sbCar = new StringBuilder();

            StringBuilder sbMainrow = new StringBuilder();

            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dtlRow in ds.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(dtlRow["FlightDetails"].ToString()))
                        {
                            sbFlight.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                            sbFlight.Append("<tr>");
                            sbFlight.Append("<td colspan='4' width='100%' style='font-weight:700;background-color:#00aeef; width:100%; padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Flight Quotation </td>");
                            sbFlight.Append("</tr>");
                            sbFlight.Append("<tr>");
                            sbFlight.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>");
                            sbFlight.Append(txtFlightDetails.Text);
                            sbFlight.Append("</td>");
                            sbFlight.Append("</tr>");
                            sbFlight.Append("</table>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["HotelInfo"].ToString()))
                        {
                            sbHotel.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                            sbHotel.Append("<tr>");
                            sbHotel.Append("<td colspan='4' width='100%' style='font-weight:700;background-color:#00aeef; width:100%; padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Flight Quotation </td>");
                            sbHotel.Append("</tr>");
                            sbHotel.Append("<tr>");
                            sbHotel.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>");
                            sbHotel.Append(txtHotelInfo.Text);
                            sbHotel.Append("</td>");
                            sbHotel.Append("</tr>");
                            sbHotel.Append("</table>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["CarHireDetails"].ToString()))
                        {
                            sbCar.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                            sbCar.Append("<tr>");
                            sbCar.Append("<td colspan='4' width='100%' style='font-weight:700;background-color:#00aeef; width:100%; padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Flight Quotation </td>");
                            sbCar.Append("</tr>");
                            sbCar.Append("<tr>");
                            sbCar.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>");
                            sbCar.Append(txtCarHireDetails.Text);
                            sbCar.Append("</td>");
                            sbCar.Append("</tr>");
                            sbCar.Append("</table>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["ItineraryDetails"].ToString()))
                        {
                            sbMainrow.Append(" <div class='col-md-12'> <h4>Itinerary:</h4>" + dtlRow["ItineraryDetails"].ToString() + "</div>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["Includes"].ToString()))
                        {
                            sbMainrow.Append(" <div class='col-md-12'> <h4>Includes:</h4>" + dtlRow["Includes"].ToString() + "</div>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["Excludes"].ToString()))
                        {
                            sbMainrow.Append(" <div class='col-md-12'> <h4>Excluded:</h4>" + dtlRow["Excludes"].ToString() + "</div>");
                        }

                        readFile = readFile.Replace("{Details}", sbMainrow.ToString());

                        readFile = readFile.Replace("{QuoteNumber}", dtlRow["QuoteNumber"].ToString());
                        readFile = readFile.Replace("{QuoteDate}", dtlRow["QuoteDate"].ToString());
                        readFile = readFile.Replace("{DestinationCity}", dtlRow["DestinationCity"].ToString());
                        readFile = readFile.Replace("{TravelMonth}", dtlRow["QuoteDate"].ToString());
                        readFile = readFile.Replace("{TravelInsurance}", dtlRow["TravelInsurance"].ToString());
                        readFile = readFile.Replace("{ConsultantName}", dtlRow["ConsultantName"].ToString());
                        readFile = readFile.Replace("{ClientName}", lblClientName.Text.ToString());
                        readFile = readFile.Replace("{AdultTotal}", dtlRow["AdultTotal"].ToString());
                        readFile = readFile.Replace("{ChildTotal}", dtlRow["ChildTotal"].ToString());
                        readFile = readFile.Replace("{Includes}", dtlRow["Includes"].ToString());
                        readFile = readFile.Replace("{Excludes}", dtlRow["Excludes"].ToString());
                        readFile = readFile.Replace("{FlightDetails}", sbFlight.ToString());
                        readFile = readFile.Replace("{HotelDetails}", sbHotel.ToString());
                        readFile = readFile.Replace("{CarDetails}", sbCar.ToString());
                        readFile = readFile.Replace("{GrandTotal}", lblGrandTotal.Text.ToString());
                        readFile = readFile.Replace("{LeadStatus}", lStatus);



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

                        readFile = readFile.Replace("{ConsultantEmail}", Session["ConsultantEmail"].ToString());
                    }
                }

                string StrContent = readFile;

                string filepath = Server.MapPath("~/QuotePDF");


                bool pdf = GenerateHTML_TO_PDF(StrContent, true, filepath, false, QuoteNumber);
                if (pdf)
                {
                    string consultName = Session["Name"].ToString();
                    SendMail(lblClientName.Text, txtToEmailNew.Text, txtDestination.Text, consultName, QuoteNumber);
                    Session.Remove("lstQuoteEntity");
                }

            }
        }
        catch
        { }

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
                    MailText += "Kind regards, <br/><br/>";
                    //MailText += "(" + consultName + ")";

                    MailText += "<div style='float:left; width:10%; border-right:3px solid #03F; padding:0 20px; margin-right:50px;'><img style='width:100%; display:block;' src='http://tcrm.askswg.co.za/images/logoEmail.png' /></div><div><h1 style='color:#3fa9df; margin:0 0 5px; font-size:12px;'>" + Session["Name"].ToString() + "</h1><h3 style='color:#25377b; margin:0 0 5px; font-size:12px; font-weight:400;'>Travel Consultant</h3><h5 style='color:#25377b; margin:0 0 5px; font-size:12px; font-weight:400;'>+27 31 2010 630 <span style='color:#3fa9df;'>|</span>" + Session["ConsultantEmail"].ToString() + "</h5><p style='color:#25377b; margin:0 0 0px; font-size:12px; font-weight:400;margin-left:165px;'><a href='#'><img src='http://tcrm.askswg.co.za/images/facebook.png' style='width:3%' /></a>&nbsp; <a href='#'><img src='http://tcrm.askswg.co.za/images/twitter.png' style='width:3%' /></a>&nbsp; <a href='#'><img src='http://tcrm.askswg.co.za/images/linkedin.png' style='width:3%' /></a>&nbsp; &nbsp; &nbsp;Suite 3, 2nd floor Silver Oaks, 36 Silverton Road, Musgruve, Durban</p></div>";
                    bool mailSent = UpdateCustomMail(SmtpServer, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo, DisplayNameTo, MailCc, "", "", "", DisplayNameCc, MailBcc, Subject, MailText, Attachment);

                    if (mailSent)
                    {
                        MailSentSatatus(QuoteNumber);
                        CommanClass.MailStatusLog(LeadID, "QT001", "Success", "", QuoteNumber);
                        emailsection.Style.Add("display", "none");
                        quotesection.Style.Add("display", "unset");
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

            if (chbkBookingForm.Checked)
            {
                string filepath = Server.MapPath("~/PreDefinedDocs");
                string FileName = filepath + "\\BookingForm.pdf";
                Attachment attch = new Attachment(FileName);
                myMessage.Attachments.Add(attch);
            }
            if (chbkBankingDetails.Checked)
            {
                string filepath = Server.MapPath("~/PreDefinedDocs");
                string FileName = filepath + "\\BANKINGDETAILS.pdf";
                Attachment attch = new Attachment(FileName);
                myMessage.Attachments.Add(attch);
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
    private void GetPdfMultipleOptions(List<QuoteEntity> lstQuEnt)
    {
        try
        {
            int cnt = 1;
            StringBuilder sbMainrow = new StringBuilder();
            StreamReader reader = new StreamReader(Server.MapPath("~/QuotePDFMultiple.html"));
            string readFile = reader.ReadToEnd();
            reader.Close();
            foreach (QuoteEntity qt in lstQuEnt)
            {
                DataSet ds = new DataSet();
                ds = qtBL.GetQuotePDFData(qt.QuoteNumber);

                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region Quotation Info

                        sbMainrow.Append("<table style='width:100%;margin-bottom:15px; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td colspan='4' width='100%' style='font-weight:700;background-color:#00aeef; width:100%; padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>OPTION " + cnt + " </td>");
                        sbMainrow.Append("</tr>");

                        foreach (DataRow dtlRow in ds.Tables[0].Rows)
                        {

                            if (!string.IsNullOrEmpty(dtlRow["FlightDetails"].ToString()))
                            {
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='4' width='100%' style='font-weight:700; width:100%; padding:5px 10px; font-size:3.56mm; text-transform:uppercase;'>FLIGHT Quotation</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>" + dtlRow["FlightDetails"].ToString() + "</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("</table>");
                                sbMainrow.Append("</tr>");
                            }

                            if (!string.IsNullOrEmpty(dtlRow["HotelInfo"].ToString()))
                            {
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='4' width='100%' style='font-weight:700; width:100%; padding:5px 10px; font-size:3.56mm; text-transform:uppercase;'>HOTEL Quotation</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>" + dtlRow["HotelInfo"].ToString() + "</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("</table>");
                                sbMainrow.Append("</tr>");
                            }

                            if (!string.IsNullOrEmpty(dtlRow["CarHireDetails"].ToString()))
                            {
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='4' width='100%' style='font-weight:700; width:100%; padding:5px 10px; font-size:3.56mm; text-transform:uppercase;'>CAR Quotation</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>" + dtlRow["CarHireDetails"].ToString() + "</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("</table>");
                                sbMainrow.Append("</tr>");
                            }

                            if (!string.IsNullOrEmpty(dtlRow["ItineraryDetails"].ToString()))
                            {

                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='4' width='100%' style='font-weight:700; width:100%; padding:5px 10px; font-size:3.56mm; text-transform:uppercase;'>ITINERARY</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>" + dtlRow["ItineraryDetails"].ToString() + "</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("</table>");
                                sbMainrow.Append("</tr>");

                            }

                            if (!string.IsNullOrEmpty(dtlRow["Includes"].ToString()) && !string.IsNullOrEmpty(dtlRow["Excludes"].ToString()))
                            {
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='55%' style='font-weight:700;background-color:#00aeef;  padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Includes</td>");
                                sbMainrow.Append("<td width='45%' style='font-weight:700;background-color:#00aeef;  padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Excludes</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append(" <td width='55%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>" + dtlRow["Includes"].ToString() + "</td>");
                                sbMainrow.Append(" <td width='55%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>" + dtlRow["Excludes"].ToString() + "</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("</table>");
                                sbMainrow.Append("</tr>");
                            }
                            else if (!string.IsNullOrEmpty(dtlRow["Includes"].ToString()) || !string.IsNullOrEmpty(dtlRow["Excludes"].ToString()))
                            {
                                string strHeading = !string.IsNullOrEmpty(dtlRow["Includes"].ToString()) ? "Includes" : "Excludes";
                                string strData = !string.IsNullOrEmpty(dtlRow["Includes"].ToString()) ? dtlRow["Includes"].ToString() : dtlRow["Excludes"].ToString();
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='4' width='100%' style='font-weight:700; width:100%; padding:5px 10px; font-size:3.56mm; text-transform:uppercase;'>'" + strHeading + "'</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>'" + strData + "'</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("</table>");
                                sbMainrow.Append("</tr>");
                            }

                            string adultText = string.Empty;
                            if (dtlRow["CostForAdultType"].ToString() == "1")
                            {
                                adultText = "COST PER PERSON SHARING R " + dtlRow["CostForAdult"].ToString() + " x " + dtlRow["NoOfAdults"].ToString() + " adults";
                            }
                            else if (dtlRow["CostForAdultType"].ToString() == "2")
                            {
                                adultText = "COST PER PERSON INDIVIDUAL R " + dtlRow["CostForAdult"].ToString() + " x 1 adult";
                            }

                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<td width='30%' style='padding:10px 10px 5px; border-bottom:1px solid #b9b9b9; border-right:1px solid #b9b9b9;'><h5 style='font-size:3.05mm; margin:0;  color:#00aeef; width:100%;'>COST PER ADULT</h5></td>");
                            sbMainrow.Append("<td width='55%' style='padding:10px 10px 5px; border-bottom:1px solid #b9b9b9;border-right:1px solid #b9b9b9;'><p style='font-size:2.5mm; margin:0; width:50%;'>" + adultText + "</p></td>");
                            sbMainrow.Append("<td width='15%' style='padding:10px 10px 5px; border-bottom:1px solid #b9b9b9;'><p style='font-size:2.5mm; margin:0; width:100%; font-weight:bold;'>" + dtlRow["AdultTotal"].ToString() + "</p></td>");
                            sbMainrow.Append("</tr>");
                            if (!string.IsNullOrEmpty(dtlRow["CostForChild"].ToString()))
                            {
                                string childText = string.Empty;
                                if (dtlRow["CostForChildType"].ToString() == "3")
                                {
                                    childText = "COST PER CHILD SHARING R " + dtlRow["CostForChild"].ToString() + " x " + dtlRow["NoOfChildren"].ToString() + " child";
                                }
                                else
                                {
                                    childText = "";
                                }

                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='30%' style='padding:10px 10px 5px; border-bottom:1px solid #b9b9b9; border-right:1px solid #b9b9b9;'><h5 style='font-size:3.05mm; margin:0;  color:#00aeef; width:100%;'>COST PER CHILD</h5></td>");
                                sbMainrow.Append("<td width='55%' style='padding:10px 10px 5px; border-bottom:1px solid #b9b9b9;border-right:1px solid #b9b9b9;'><p style='font-size:2.5mm; margin:0; width:50%;'>" + childText + "</p></td>");
                                sbMainrow.Append("<td width='15%' style='padding:10px 10px 5px; border-bottom:1px solid #b9b9b9;'><p style='font-size:2.5mm; margin:0; width:100%; font-weight:bold;'>" + dtlRow["ChildTotal"].ToString() + "</p></td>");
                                sbMainrow.Append("</tr>");
                            }

                            sbMainrow.Append("</table>");
                            sbMainrow.Append("</tr>");
                            readFile = readFile.Replace("{QuoteDate}", dtlRow["QuoteDate"].ToString());
                            readFile = readFile.Replace("{TravelInsurance}", dtlRow["TravelInsurance"].ToString());
                            readFile = readFile.Replace("{ConsultantName}", dtlRow["ConsultantName"].ToString());
                            readFile = readFile.Replace("{ClientName}", lblClientName.Text.ToString());
                            readFile = readFile.Replace("{ConsultantEmail}", Session["ConsultantEmail"].ToString());
                        }
                        sbMainrow.Append("</table>");

                        #endregion

                        cnt += 1;
                    }
                }


            }
            readFile = readFile.Replace("{Details}", sbMainrow.ToString());

            string StrContent = readFile;

            string filepath = Server.MapPath("~/QuotePDF");

            var quotes = lstQuEnt.Select(q => q.QuoteNumber).ToList();
            string QuoteNumbers = string.Join("_", quotes);

            bool pdf = GenerateHTML_TO_PDF(StrContent, true, filepath, false, QuoteNumbers);
            if (pdf)
            {
                string consultName = Session["Name"].ToString();
                SendMail(lblClientName.Text, txtToEmailNew.Text, txtDestination.Text, consultName, QuoteNumbers);
                Session.Remove("lstQuoteEntity");
            }



        }
        catch (Exception ex)
        { }

    }
    protected void imgbtnBackQuote_Click1(object sender, EventArgs e)
    {
        imgbtnBackQuote.Visible = false;
        backToLead.Visible = true;
        quotesection.Style.Add("display", "unset");
        emailsection.Style.Add("display", "none");
    }
    protected void backToLead_Click1(object sender, EventArgs e)
    {
        Response.Redirect("EditLead.aspx?t=quote&idq=" + LeadID);
    }
    protected void btnSendMailNew_Click(object sender, EventArgs e)
    {
        lstQuoteEntity = (List<QuoteEntity>)Session["lstQuoteEntity"];
        if (lstQuoteEntity == null)
        {
            GetPdf(ViewState["QuoteNumber"].ToString());
            CommanClass.MailStatusLog(LeadID, "QT001", "Success", "", ViewState["QuoteNumber"].ToString());
        }
        else
        {
            GetPdfMultipleOptions(lstQuoteEntity);
            CommanClass.MailStatusLog(LeadID, "QT001", "Success", "", QuoteBuilder.ToString());
        }
    }
}