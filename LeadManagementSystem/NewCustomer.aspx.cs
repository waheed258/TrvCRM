using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;
public partial class NewCustomer : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    customerBL cusomerBL = new customerBL();
    customerEntity customerEntity = new customerEntity();
    CommanClass _objComman = new CommanClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
            }
        }
        catch { }
    }
  
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        try
        {
            customerEntity.TravellerTitel = ddlTitle.SelectedItem.Text;
            customerEntity.TravellerFirstName = txtFirstName.Text;
            customerEntity.TravellerLastName = txtLastName.Text;
            customerEntity.TravellerMobile = txtMobile.Text;
            customerEntity.TravellerMailId = txtEmail.Text;
            customerEntity.TravellerPhone = txtPhone.Text;
            customerEntity.TravellerPassPortNo = txtPassportNo.Text;
            customerEntity.PassportIssueDate = txtIssueDate.Text;
            customerEntity.PassportExpiryDate = txtExpiry.Text;
            customerEntity.TravellerAddress = txtAddress.Text;
            customerEntity.CompanyId = 0;

            int result = cusomerBL.CUDCustomer(customerEntity, 'I');
            if (result == 1)
            {
                lblMessage.Text = "Customer details saved Successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                Clear();                
            }
            else
            {
                lblMessage.Text = "Please try again!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                Clear();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        customerEntity.TravellerId = Convert.ToInt32(ViewState["TravellerId"].ToString());
    //        customerEntity.TravellerTitel = ddlTitle.SelectedItem.Text;
    //        customerEntity.TravellerFirstName = txtFirstName.Text;
    //        customerEntity.TravellerLastName = txtLastName.Text;
    //        customerEntity.TravellerMobile = txtMobile.Text;
    //        customerEntity.TravellerMailId = txtEmail.Text;
    //        customerEntity.TravellerPhone = txtPhone.Text;
    //        customerEntity.TravellerPassPortNo = txtPassportNo.Text;
    //        customerEntity.PassportIssueDate = txtIssueDate.Text;
    //        customerEntity.PassportExpiryDate = txtExpiry.Text;
    //        customerEntity.TravellerAddress = txtAddress.Text;
    //        customerEntity.CompanyId = 0;

    //        int result = cusomerBL.CUDCustomer(customerEntity, 'U');
    //        if (result == 1)
    //        {
    //            lblMessage.Text = "Customer details updated Successfully!";
    //            lblMessage.ForeColor = System.Drawing.Color.Green;
    //            Clear();
    //        }
    //        else
    //        {
    //            lblMessage.Text = "Please try again!";
    //            lblMessage.ForeColor = System.Drawing.Color.Red;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = "Something went wrong. Please contact administrator!";
    //        lblMessage.ForeColor = System.Drawing.Color.Red;
    //    }
    //}
    public void Clear()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtMobile.Text = "";
        txtEmail.Text = "";
        ddlTitle.SelectedValue = "-1";
        txtPhone.Text = "";
        txtPassportNo.Text = "";
        txtIssueDate.Text = "";
        txtExpiry.Text = "";
        txtAddress.Text = "";
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {       
        Clear();
    }
}