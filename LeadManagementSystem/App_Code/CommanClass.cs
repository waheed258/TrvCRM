using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Net.Mail;
using System.Globalization;

namespace BusinessLogic
{

    public class CommanClass
    {
        DataSet ds = new DataSet();
        BasicDropdownBL _objBasicDropdownBL = new BasicDropdownBL();
        public void GetCountry(DropDownList ddlCountry)
        {
            try
            {
                ds = _objBasicDropdownBL.GetCountry();
                ddlCountry.DataSource = ds;
                ddlCountry.DataTextField = "Country";
                ddlCountry.DataValueField = "CountryID";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("--Select Country--", "-1"));
            }
            catch
            {

            }
        }
        public void GetProvince(DropDownList ddlProvince)
        {
            try
            {
                ds = _objBasicDropdownBL.GetProvince();
                ddlProvince.DataSource = ds;
                ddlProvince.DataTextField = "Province";
                ddlProvince.DataValueField = "ProvinceID";
                ddlProvince.DataBind();
                ddlProvince.Items.Insert(0, new ListItem("--Select Province--", "-1"));
            }
            catch
            {

            }
        }

        public void GetAssigLeadOptions(DropDownList ddlAssignLeadOptions)
        {
            try
            {
                ds = _objBasicDropdownBL.GetAssigLeadOptions();
                ddlAssignLeadOptions.DataSource = ds;
                ddlAssignLeadOptions.DataTextField = "LeadStatus";
                ddlAssignLeadOptions.DataValueField = "LeadStatusID";
                ddlAssignLeadOptions.DataBind();
                ddlAssignLeadOptions.Items.Insert(0, new ListItem("--Select Option--", "-1"));
            }
            catch
            {

            }
        }


        public void GetStatus(DropDownList ddlStatus)
        {
            try
            {
                ds = _objBasicDropdownBL.GetStatus();
                ddlStatus.DataSource = ds;
                ddlStatus.DataTextField = "LeadAction";
                ddlStatus.DataValueField = "LeadActionsID";
                ddlStatus.DataBind();                
            }
            catch
            {

            }
        }
        public void GetCity(DropDownList ddlCity)
        {
            try
            {
                ds = _objBasicDropdownBL.GetCity();
                ddlCity.DataSource = ds;
                ddlCity.DataTextField = "City";
                ddlCity.DataValueField = "CityID";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("--Select City--", "-1"));
            }
            catch
            {

            }
        }
        public void GetAccountType(DropDownList ddlAccountType)
        {
            try
            {
                ds = _objBasicDropdownBL.GetAccountType();
                ddlAccountType.DataSource = ds;
                ddlAccountType.DataTextField = "AccountType";
                ddlAccountType.DataValueField = "AccountTypeID";
                ddlAccountType.DataBind();
                ddlAccountType.Items.Insert(0, new ListItem("--Select Account Type--", "-1"));
            }
            catch (Exception ex)
            {

            }
        }
        public void getRecordsPerPage(DropDownList ddlRpP)
        {
            try
            {
                ddlRpP.Items.Clear();
                Dictionary<int, string> objRp = new Dictionary<int, string>();                
                objRp.Add(20, "20");
                objRp.Add(50, "50");
                objRp.Add(100, "100");
                ddlRpP.DataSource = objRp;
                ddlRpP.DataTextField = "Value";
                ddlRpP.DataValueField = "Key";
                ddlRpP.DataBind();
            }
            catch { }
        }
        public static bool SendEmail(string SmtpHost, int SmtpPort, string MailFrom, string DisplayNameFrom, string FromPassword, string MailTo, string DisplayNameTo, string MailCc, string mailCc2, string mailCc3, string mailCc4, string DisplayNameCc, string MailBcc, string Subject, string MailText, string Attachment)
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

                //Add Image
                string image = System.Web.HttpContext.Current.Server.MapPath("/Admin") + "\\sign.jpg";
                LinkedResource theEmailImage = new LinkedResource(image);
                theEmailImage.ContentId = "myImageID";


                //Add the Image to the Alternate view
                htmlView.LinkedResources.Add(theEmailImage);

                //Add view to the Email Message
                myMessage.AlternateViews.Add(htmlView);

                if (Attachment != "")
                {
                    Attachment a = new Attachment(Attachment);
                    myMessage.Attachments.Add(a);
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


        public static bool UpdateMail(string SmtpHost, int SmtpPort, string MailFrom, string DisplayNameFrom, string FromPassword, string MailTo, string DisplayNameTo, string MailCc, string mailCc2, string mailCc3, string mailCc4, string DisplayNameCc, string MailBcc, string Subject, string MailText, string Attachment)
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

    }
}