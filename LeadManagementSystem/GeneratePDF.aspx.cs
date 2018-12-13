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


public partial class GeneratePDF : System.Web.UI.Page
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

    }
    protected void btnGenerateQuote_Click(object sender, EventArgs e)
    {
        try
        {
            GetPdf(txtQuoteNumber.Text);
        }
        catch
        {
            lblMessage.Text = "Something went wrong";
        }
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
            StringBuilder sbItinerary = new StringBuilder();

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
                            sbFlight.Append(dtlRow["FlightDetails"].ToString());
                            sbFlight.Append("</td>");
                            sbFlight.Append("</tr>");
                            sbFlight.Append("</table>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["HotelInfo"].ToString()))
                        {
                            sbHotel.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                            sbHotel.Append("<tr>");
                            sbHotel.Append("<td colspan='4' width='100%' style='font-weight:700;background-color:#00aeef; width:100%; padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Hotel Quotation </td>");
                            sbHotel.Append("</tr>");
                            sbHotel.Append("<tr>");
                            sbHotel.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>");
                            sbHotel.Append(dtlRow["HotelInfo"].ToString());
                            sbHotel.Append("</td>");
                            sbHotel.Append("</tr>");
                            sbHotel.Append("</table>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["CarHireDetails"].ToString()))
                        {
                            sbCar.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                            sbCar.Append("<tr>");
                            sbCar.Append("<td colspan='4' width='100%' style='font-weight:700;background-color:#00aeef; width:100%; padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Car Quotation </td>");
                            sbCar.Append("</tr>");
                            sbCar.Append("<tr>");
                            sbCar.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>");
                            sbCar.Append(dtlRow["CarHireDetails"].ToString());
                            sbCar.Append("</td>");
                            sbCar.Append("</tr>");
                            sbCar.Append("</table>");
                        }
                        if (!string.IsNullOrEmpty(dtlRow["ItineraryDetails"].ToString()))
                        {
                            sbItinerary.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                            sbItinerary.Append("<tr>");
                            sbItinerary.Append("<td colspan='4' width='100%' style='font-weight:700;background-color:#00aeef; width:100%; padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Itinerary </td>");
                            sbItinerary.Append("</tr>");
                            sbItinerary.Append("<tr>");
                            sbItinerary.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>");
                            sbItinerary.Append(dtlRow["ItineraryDetails"].ToString());
                            sbItinerary.Append("</td>");
                            sbItinerary.Append("</tr>");
                            sbItinerary.Append("</table>");
                        }
                        if (!string.IsNullOrEmpty(dtlRow["ItineraryDetails"].ToString()))
                        {
                            sbMainrow.Append(" <div class='col-md-12'> <h4>Itinerary:</h4>" + dtlRow["ItineraryDetails"].ToString() + "</div>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["Includes"].ToString().TrimEnd()))
                        {
                            sbMainrow.Append(" <div class='col-md-12'> <h4>Includes:</h4>" + dtlRow["Includes"].ToString() + "</div>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["Excludes"].ToString().TrimEnd()))
                        {
                            sbMainrow.Append(" <div class='col-md-12'> <h4>Excluded:</h4>" + dtlRow["Excludes"].ToString() + "</div>");
                        }

                        readFile = readFile.Replace("{Details}", sbMainrow.ToString());

                        readFile = readFile.Replace("{QuoteNumber}", dtlRow["QuoteNumber"].ToString());
                        readFile = readFile.Replace("{QuoteDate}", dtlRow["QuoteDate"].ToString());
                        readFile = readFile.Replace("{DestinationCity}", dtlRow["DestinationCity"].ToString());
                        readFile = readFile.Replace("{TravelMonth}", dtlRow["QuoteDate"].ToString());
                        readFile = readFile.Replace("{TravelInsurance}", dtlRow["TravelInsurance"].ToString().Replace("<p>&nbsp;</p>", "").TrimEnd());
                        readFile = readFile.Replace("{ConsultantName}", dtlRow["ConsultantName"].ToString());
                        readFile = readFile.Replace("{ClientName}", dtlRow["ClientName"].ToString());
                        readFile = readFile.Replace("{AdultTotal}", dtlRow["AdultTotal"].ToString());
                        readFile = readFile.Replace("{ChildTotal}", dtlRow["ChildTotal"].ToString());
                        readFile = readFile.Replace("{Includes}", dtlRow["Includes"].ToString().Replace("<p>&nbsp;</p>", "").TrimEnd());
                        readFile = readFile.Replace("{Excludes}", dtlRow["Excludes"].ToString().Replace("<p>&nbsp;</p>", "").TrimEnd());
                        readFile = readFile.Replace("{FlightDetails}", sbFlight.ToString().Replace("<p>&nbsp;</p>", "").TrimEnd());
                        readFile = readFile.Replace("{HotelDetails}", sbHotel.ToString().Replace("<p>&nbsp;</p>", "").TrimEnd());
                        readFile = readFile.Replace("{CarDetails}", sbCar.ToString().Replace("<p>&nbsp;</p>", "").TrimEnd());
                        readFile = readFile.Replace("{GrandTotal}", dtlRow["GrandTotal"].ToString());
                        readFile = readFile.Replace("{LeadStatus}", lStatus);
                        readFile = readFile.Replace("{ItineraryDetails}", sbItinerary.ToString().Replace("<p>&nbsp;</p>", "").TrimEnd());



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

                        readFile = readFile.Replace("{ConsultantEmail}", dtlRow["Email"].ToString());
                    }
                }

                string StrContent = readFile;

                string filepath = Server.MapPath("~/QuotePDF");


                bool pdf = GenerateHTML_TO_PDF(StrContent, true, filepath, false, QuoteNumber);
                if (pdf)
                {
                    lblMessage.Text = "Created Successfully";
                }

            }
        }
        catch
        {
            lblMessage.Text = "Something went wrong";
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
    protected void btnGenerateMultipleQuotes_Click(object sender, EventArgs e)
    {
        try
        {
            string[] quotes = txtMultipleQuotes.Text.Split('_');
            int cnt = 1;
            StringBuilder sbMainrow = new StringBuilder();
            StreamReader reader = new StreamReader(Server.MapPath("~/QuotePDFMultiple.html"));
            string readFile = reader.ReadToEnd();
            reader.Close();
            foreach (string quoteno in quotes)
            {
                DataSet ds = new DataSet();
                ds = qtBL.GetQuotePDFData(quoteno);

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
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>" + dtlRow["FlightDetails"].ToString().Replace("<p>&nbsp;</p>", "").TrimEnd() + "</td>");
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
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>" + dtlRow["HotelInfo"].ToString().Replace("<p>&nbsp;</p>", "").TrimEnd() + "</td>");
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
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>" + dtlRow["CarHireDetails"].ToString().Replace("<p>&nbsp;</p>", "").TrimEnd() + "</td>");
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
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>" + dtlRow["ItineraryDetails"].ToString().Replace("<p>&nbsp;</p>", "").TrimEnd() + "</td>");
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
                                sbMainrow.Append(" <td width='55%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>" + dtlRow["Includes"].ToString().Replace("<p>&nbsp;</p>", "").TrimEnd() + "</td>");
                                sbMainrow.Append(" <td width='55%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>" + dtlRow["Excludes"].ToString().Replace("<p>&nbsp;</p>", "").TrimEnd() + "</td>");
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
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>'" + strData.Replace("<p>&nbsp;</p>", "").TrimEnd() + "'</td>");
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
                            readFile = readFile.Replace("{TravelInsurance}", dtlRow["TravelInsurance"].ToString().Replace("<p>&nbsp;</p>", "").TrimEnd());
                            readFile = readFile.Replace("{ConsultantName}", dtlRow["ConsultantName"].ToString());
                            readFile = readFile.Replace("{ClientName}", dtlRow["ClientName"].ToString());
                            readFile = readFile.Replace("{ConsultantEmail}", dtlRow["Email"].ToString());
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

            var quoteslist = quotes.Select(q => q).ToList();
            string QuoteNumbers = string.Join("_", quotes);

            bool pdf = GenerateHTML_TO_PDF(StrContent, true, filepath, false, QuoteNumbers);
            if (pdf)
            {
                lblMessage.Text = "Created successfully";
            }



        }
        catch (Exception ex)
        { lblMessage.Text = "Something went wrong"; }


    }
}