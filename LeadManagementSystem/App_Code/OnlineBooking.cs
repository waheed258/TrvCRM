﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BusinessEntities;
using System.Web.Script.Serialization;
using BusinessLogic;
using System.Data;
using System.Globalization;

/// <summary>
/// Summary description for OnlineBooking
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class OnlineBooking : System.Web.Services.WebService {
    LeadBL leadBL = new LeadBL();
    public OnlineBooking () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void LeadInfoFromWebsite(List<Object> aray)
    {
        string leadInfo = "";

        try
        {
            foreach (Dictionary<string, object> item in aray)
            {
                leadInfo = new JavaScriptSerializer().Serialize(item);
            };
            JavaScriptSerializer js = new JavaScriptSerializer();
            LeadEntity leadObject = js.Deserialize<LeadEntity>(leadInfo);
            LeadEntity leadEntity = new LeadEntity();

            string deptdt = DateTime.ParseExact(leadObject.DepartureDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");
            string rettdt = DateTime.ParseExact(leadObject.DepartureDate, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");

            leadEntity.SourceID = 1;
            leadEntity.SourceRef = "Website";
            leadEntity.Others = "";
            leadEntity.AssignedTo = 0;
            leadEntity.AssignedBy = 0;
            leadEntity.FirstName = leadObject.FirstName;
            leadEntity.LastName = leadObject.LastName;
            leadEntity.Mobile = leadObject.Mobile;
            leadEntity.Email = leadObject.Email;
            leadEntity.OriginName = leadObject.OriginName;
            leadEntity.DestinationName = leadObject.DestinationName;
            leadEntity.DepartureDate = deptdt;
            leadEntity.ReturnDate = rettdt;
            leadEntity.Adult = leadObject.Adult;
            leadEntity.Child = leadObject.Child;
            leadEntity.Infant = leadObject.Infant;
            leadEntity.ProductType = leadObject.ProductType;
            leadEntity.Budget = leadObject.Budget;
            leadEntity.Notes = leadObject.Notes;
            leadEntity.QuotedPrice = 0;
            leadEntity.FinalPrice = 0;
            leadEntity.UpdatedBy = 0;
            leadEntity.LeadStatus = 2;
            leadEntity.CreatedBy = 2;
            leadEntity.FollowupDate = "";
            leadEntity.FollowupDesc = "";
            leadEntity.LeadDescription = "";
            leadEntity.PackageId = leadObject.PackageId;
            leadEntity.ProductID = leadObject.ProductID;
            leadEntity.WebsiteConsultantNotes = leadObject.WebsiteConsultantNotes;
            int result = leadBL.CUDLead(leadEntity, 'I');
            if (result > 1)
            {
                string clName = leadObject.FirstName + " " + leadObject.LastName;
                SendMail(clName, leadObject.Email, leadObject.Mobile, leadObject.PackageId);
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }
    }
    public void SendMail(string clName, string clEmail, string clPhone, string clPackageName)
    {
        try
        {
            DataSet ds = leadBL.GetMailInfo();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string SmtpServer = ds.Tables[0].Rows[0]["con_smtp_host"].ToString();
                int SmtpPort = Convert.ToInt32(ds.Tables[0].Rows[0]["con_smtp_port"].ToString());
                string MailFrom = ds.Tables[0].Rows[0]["con_mail_from"].ToString();
                //string MailFrom = "active8crm.sa@gmail.com";
                string DisplayNameFrom = ds.Tables[0].Rows[0]["con_from_name"].ToString();
                string FromPassword = ds.Tables[0].Rows[0]["con_from_pwd"].ToString();
                // string FromPassword = "Active@321#";
                //string MailTo = "ramesh.palaparti@dinoosys.com";
                string MailTo = string.Empty;
                string DisplayNameTo = string.Empty;
                string MailCc = string.Empty;
                string DisplayNameCc = string.Empty;
                string MailBcc = string.Empty;
                string Subject = string.Empty;

                string Attachment = string.Empty;

                try
                {
                    string MailText = string.Empty;
                    Subject = "New website enquiry submitted";
                    MailCc = "";
                    //MailTo = "saipramod.balatrapu@dinoosys.com";
                    MailTo = "consultants@serendipitytours.co.za";
                    MailText = "Hi, <br/><br/><b> New enquiry created : </b><br/><br/><br/>";
                    MailText += "<table border='1'><tbody>";
                    MailText += "<tr><td>Name</td><td>" + clName + "</td></tr>";
                    MailText += "<tr><td>Email</td><td>" + clEmail + "</td></tr>";
                    MailText += "<tr><td>Phone</td><td>" + clPhone + "</td></tr>";
                    //if (hdfpid.Value != null && hdfpid.Value.ToString() != "")
                    //{
                    //    MailText += "<tr><td>Enquiry URL</td><td>http://serendipitytravel.co.za/tour-detail.aspx?pid=" + hdfpid.Value.ToString() + "</td></tr>";
                    //}
                    //else
                    //{
                    //    MailText += "<tr><td>Enquiry </td><td>From contact page</td></tr>";
                    //}
                    MailText += "<tr><td>View Lead</td><td>http://tcrm.askswg.co.za/NewLead</td></tr>";
                    MailText += "";
                    MailText += "</tbody></table>";

                    CommanClass.UpdateMail(SmtpServer, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo, DisplayNameTo, MailCc, "", "", "", DisplayNameCc, MailBcc, Subject, MailText, Attachment);

                }
                catch
                { }

                try
                {
                    string MailText = string.Empty;
                    Subject = "Serendipity Tours >> Thank you for your travel enquiry";
                    MailTo = clEmail;
                    //MailTo = "saipramod.balatrapu@dinoosys.com";
                    MailCc = "";

                    //MailText = "<img src='http://www.serendipitytravel.co.za/Images/Signature.jpg' /> <br/>";

                    //MailText += "<b> Dear " + clName + ", </b><br/><br/>";

                    //MailText += "Thank you for travel enquiry<br/><br/>";
                    //MailText += "A consultant will be in touch with you shortly. Please check your inbox (don’t forget to check your spam folder too) <br/><br/>";

                    //MailText += "Have a Serendipitous Day! <br/>";
                    //MailText += "The Serendipity Travel team <br/><br/>";

                    //MailText += "<b>Did you know:</b><br/>";
                    //MailText += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;We have a Corporate Travel division<br/>";
                    //MailText += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;Contact us for any incentive / group travel<br/>";
                    //MailText += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;We can assist with Visa applications";


                    MailText = "<div style='width:100%; display:inline-block; margin-bottom:20px; clear:both;'><img style='max-width:100%; display:block;' src='http://tcrm.askswg.co.za/images/head.png' alt='head' /></div>";
                    MailText += "<div class='body_cnt' style='width:100%; padding:0 25px;'>";
                    MailText += "<h1 style='font-size:14px; color:#00aeef; margin:0 0 10px;'>Dear " + clName + ",</h1>";
                    MailText += "<p style='margin:0 0 10px;'>Thank you for travel enquiry.</p>";
                    MailText += "<p style='margin:0 0 10px;'>A consultant will be in touch with you shortly. Please check your inbox <br />(Don’t forget to check your spam folder too)</p>";
                    MailText += "<p style='margin:0 0 10px; color:#00aeef;'>Have a Serendipitous Day!</p>";
                    MailText += "<h2 style='font-size:14px; color:#00aeef; margin:0 0 10px;'>Did you know :</h2>";
                    MailText += "<ul style='padding:0; margin:0 0 10px;'>";
                    MailText += "<li style='list-style-position:inside; margin:0 0 5px;'>We have a Corporate Travel division</li>";
                    MailText += "<li style='list-style-position:inside; margin:0 0 5px;'>We can help you with incentive / group travel</li>";
                    MailText += "<li style='list-style-position:inside; margin:0 0 5px;'>We can assist with Visa applications</li>";
                    MailText += "</ul>";
                    MailText += "<h3 style='font-size:18px; margin:0 0 10px;'>Regards,</h3>";
                    MailText += "<p style='margin:0 0 10px; color:#00aeef;'>The Serendipity Travel team</p>";
                    MailText += "<p style='margin:0 0 5px; line-height:16px;'>Physical Address Suite 3, 2nd floor Silver Oaks, 36 Silverton Road, <br />Musgrave, Durban, South Africa, 4001</p>";
                    MailText += "<p style='margin:0 0 5px;'>Tel: <a style='color:#00aeef; text-decoration:none;' href='tel:27312010630'>+27 31 2010 630</a> </p>";
                    MailText += "<p style='margin:0 0 5px;'>Email: <a style='color:#00aeef; text-decoration:none;' href='mailto:media@serendipitytours.co.za'>media@serendipitytours.co.za</a>  </p>";
                    MailText += "<p style='margin:0 0 15px;'>Web: <a style='color:#00aeef; text-decoration:none;' href='http://www.serendipitytravel.co.za' target='_blank'>www.serendipitytravel.co.za</a></p>";
                    MailText += "<p><a style='margin-right:20px;' href='#' target='_blank'><img src='http://tcrm.askswg.co.za/images/facebook1.png' alt='facebook' /></a><a style='margin-right:20px;' href='#' target='_blank'><img src='http://tcrm.askswg.co.za/images/google.png' alt='googleplus' /></a><a style='margin-right:20px;' href='#' target='_blank'><img src='http://tcrm.askswg.co.za/images/twitter1.png' alt='twitter' /></a></p>";
                    MailText += "</div>";
                    MailText += "<div style='width:100%; display:inline-block; margin-top:0;'><img style='max-width:100%; display:block;' src='http://tcrm.askswg.co.za/images/foot.png' alt='foot' /></div>";

                    CommanClass.UpdateMail(SmtpServer, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo, DisplayNameTo, MailCc, "", "", "", DisplayNameCc, MailBcc, Subject, MailText, Attachment);


                }
                catch
                { }
            }
        }
        catch
        { }
    }
    
}
