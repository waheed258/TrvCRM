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
                _objComman.getRecordsPerPage(DropPage);
                GetCustomerList();
                customerList.Visible = true;
                newCustomer.Visible = false;
            }
        }
        catch { }
    }
    protected void GetCustomerList()
    {
        try
        {
            gvCustomerList.PageSize = Convert.ToInt32(DropPage.SelectedValue);
            dataset = cusomerBL.GetCustomerList(0);
            gvCustomerList.DataSource = dataset;
            gvCustomerList.DataBind();

        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
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
                message.Text = "Customer details saved Successfully!";
                message.ForeColor = System.Drawing.Color.Green;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                Clear();
                customerList.Visible = true;
                newCustomer.Visible = false;
                GetCustomerList();
            }
            else
            {
                message.Text = "Please try again!";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                Clear();
            }
        }
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            customerEntity.TravellerId = Convert.ToInt32(ViewState["TravellerId"].ToString());
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

            int result = cusomerBL.CUDCustomer(customerEntity, 'U');
            if (result == 1)
            {
                message.Text = "Customer details updated Successfully!";
                message.ForeColor = System.Drawing.Color.Green;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                GetCustomerList();
                customerList.Visible = true;
                newCustomer.Visible = false;
                imgbtnAddCustomer.Visible = true;
                Clear();
            }
            else
            {
                message.Text = "Please try again!";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            }
        }
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
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
    protected void gvCustomerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Page")
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int RowIndex = row.RowIndex;
                ViewState["TravellerId"] = ((Label)row.FindControl("lblTravellerId")).Text.ToString();
                if (e.CommandName == "EditCustomer")
                {
                    customerList.Visible = false;
                    newCustomer.Visible = true;
                    imgbtnAddCustomer.Visible = false;
                    btnUpdate.Visible = true;
                    ImageButton1.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openEditModal();", true);
                    if (((Label)row.FindControl("lblTravellerTitel")).Text.ToString() == "Mr")
                    {
                        ddlTitle.SelectedValue = "0";
                    }
                    else if (((Label)row.FindControl("lblTravellerTitel")).Text.ToString() == "Miss")
                    {
                        ddlTitle.SelectedValue = "1";
                    }
                    else if (((Label)row.FindControl("lblTravellerTitel")).Text.ToString() == "Mrs")
                    {
                        ddlTitle.SelectedValue = "2";
                    }
                    txtFirstName.Text = ((Label)row.FindControl("lblTravellerFirstName")).Text.ToString();
                    txtLastName.Text = ((Label)row.FindControl("lblTravellerLastName")).Text.ToString();
                    txtMobile.Text = ((Label)row.FindControl("lblTravellerMobile")).Text.ToString();
                    txtEmail.Text = ((Label)row.FindControl("lblTravellerMailId")).Text.ToString();
                    txtPhone.Text = ((Label)row.FindControl("lblTravellerPhone")).Text.ToString();
                    txtAddress.Text = ((Label)row.FindControl("lblTravellerAddress")).Text.ToString();
                    txtPassportNo.Text = ((Label)row.FindControl("lblTravellerPassPortNo")).Text.ToString();
                    txtIssueDate.Text = ((Label)row.FindControl("lblPassportIssueDate")).Text.ToString();
                    txtExpiry.Text = ((Label)row.FindControl("lblPassportExpiryDate")).Text.ToString();
                }
                else if (e.CommandName == "DeleteCustomer")
                {
                    lbldeletemessage.Text = "Are you sure, you want to delete Customer Details?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDeleteModal();", true);
                }
            }
        }
        catch
        {
            message.ForeColor = System.Drawing.Color.Red;
            message.Text = "Something went wrong, please contact administrator";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

    }
    protected void btnReset_Click(object sender, ImageClickEventArgs e)
    {
        customerList.Visible = true;
        newCustomer.Visible = false;
        imgbtnAddCustomer.Visible = true;
        ImageButton1.Visible = true;
        Clear();
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCustomerList();
    }
    protected void gvCustomerList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCustomerList.PageIndex = e.NewPageIndex;
        GetCustomerList();
    }
    protected void btnSure_Click(object sender, EventArgs e)
    {
        try
        {
            customerEntity.TravellerId = Convert.ToInt32(ViewState["TravellerId"].ToString());
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

            int result = cusomerBL.CUDCustomer(customerEntity, 'D');
            if (result == 1)
            {
                message.Text = "Customer details deleted Successfully!";
                message.ForeColor = System.Drawing.Color.Green;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                GetCustomerList();
            }
            else
            {
                message.Text = "Please try again!";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            }
        }
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void imgbtnAddCustomer_Click(object sender, ImageClickEventArgs e)
    {
        customerList.Visible = false;
        newCustomer.Visible = true;
        imgbtnAddCustomer.Visible = false;
        btnUpdate.Visible = false;
    }
}