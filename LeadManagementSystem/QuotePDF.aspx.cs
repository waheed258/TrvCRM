using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;

public partial class QuotePDF : System.Web.UI.Page
{
    QuoteBL qtBL = new QuoteBL();
    int leadID = 0;
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            leadID = Convert.ToInt32(encryptdecrypt.Decrypt(Request.QueryString["id"]));
            GetPdf();
        }
        catch 
        {  }
      
    }


    private void GetPdf()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = qtBL.GetQuotePDFData(leadID);
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

                GenerateHTML_TO_PDF(StrContent, true, "", false);

            }
        }
        catch
        {   }
        
    }

    private void GenerateHTML_TO_PDF(string HtmlString, bool ResponseShow, string FileName, bool SaveFileDir)
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

            if (!SaveFileDir)
                doc.Save(Response, ResponseShow, FileName);
            else
                doc.Save(FileName);

            doc.Close();

        }
        catch
        {   }
    }

}