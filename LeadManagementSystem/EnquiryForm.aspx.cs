using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;

public partial class EnquiryForm : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    LeadBL leadBL = new LeadBL();
    LeadEntity leadEntity = new LeadEntity();
    CommanClass _objComman = new CommanClass();
    int j = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {         

            if (!IsPostBack)
            {
                GetProducts();

                hdfpackid.Value = Request.QueryString["packid"];
                hdfpacktype.Value = Request.QueryString["ptype"];
                ddlPackage.SelectedValue = hdfpacktype.Value;
                txtBudget.Text = Request.QueryString["price"];
            }
        }
        catch { }
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
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
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
        ddlAdults.SelectedValue = "";
        ddlChild.SelectedValue = "";
        ddlInfant.SelectedValue = "";
        ddlPackage.SelectedValue = "-1";
        txtBudget.Text = "";
        txtNotes.Text = "";
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int c, i;
            int a = Convert.ToInt32(ddlAdults.SelectedValue);
            if (ddlChild.SelectedValue == "-1")
            {
                c = 0;
            }
            else
            {
                 c = Convert.ToInt32(ddlChild.SelectedValue);
            }
            if (ddlInfant.SelectedValue == "-1")
            {
               i = 0;
            }
            else
            {
                 i = Convert.ToInt32(ddlInfant.SelectedValue);
            }
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
                        leadEntity.SourceID = 1;
                        leadEntity.SourceRef = "Website";
                        leadEntity.Others = "";
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
                        leadEntity.CreatedBy = 2;
                        leadEntity.FollowupDate = "";
                        leadEntity.FollowupDesc = "";
                        leadEntity.LeadDescription = "";
                        leadEntity.PackageId = hdfpackid.Value;
                        int result = leadBL.CUDLead(leadEntity, 'I');
                        if (result == 1)
                        {
                            message.Text = "Thank you...! One of our Consultants will contact you soon.";
                            message.ForeColor = System.Drawing.Color.Orange;
                            string clName = txtFirstName.Text + " " + txtLastName.Text;

                            SendMail(clName, txtEmail.Text, txtMobile.Text, ddlPackage.SelectedItem.Text);

                            //Clear();

                            //Response.Redirect("http://www.serendipitytravel.co.za/");
                            Response.Redirect("Thankyou");

                        }
                        else
                        {
                            message.Text = "Please try again!";
                            message.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            else
            {
                message.Text = "No of infants should not exceed adults";
                message.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch
        {
            message.Text = "Something went wrong. Please try again!";
            message.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
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
                     //MailTo = "consultants@serendipitytours.co.za";
                     MailTo = "karen@serendipitytours.co.za";
                     MailText = "Hi, <br/><br/><b> New enquiry created : </b><br/><br/><br/>";
                     MailText += "<table border='1'><tbody>";
                     MailText += "<tr><td>Name</td><td>" + clName + "</td></tr>";
                     MailText += "<tr><td>Email</td><td>" + clEmail + "</td></tr>";
                     MailText += "<tr><td>Phone</td><td>" + clPhone + "</td></tr>";
                     MailText += "<tr><td>Enquiry URL</td><td>http://tcrm.askswg.co.za/EnquiryForm</td></tr>";
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
                     Subject = "Thank You for Enquiring with Serendipity Tours !!";
                     //MailTo = clEmail;
                     MailTo = "karen@serendipitytours.co.za";
                     MailCc = "";

                     MailText = "<b> Dear " + clName + ", </b><br/>";

                     MailText += "Thank you for your travel enquiry. <br/><br/>";

                     MailText += "Query / Package : " + clPackageName + "<br/><br/>";
                     MailText += "A consultant will be in touch shortly. <br/><br/>";

                     MailText += "Have Serendipitous Day! <br/><br/><br/>";
                     
                     MailText += "<img src='http://tcrm.askswg.co.za/images/Regards.jpg' /> <br/>";
                     MailText += "<strong style='color:#0b5394;'> Serendipity​ Travel </strong>​ <br/>";
                     MailText += "<img src='http://tcrm.askswg.co.za/images/Footer.png' width='300' /> <br/>";

                     CommanClass.UpdateMail(SmtpServer, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo, DisplayNameTo, MailCc, "", "", "", DisplayNameCc, MailBcc, Subject, MailText, Attachment);


                 }
                 catch
                 { }
             }
        }
        catch 
        {  }
    }

}