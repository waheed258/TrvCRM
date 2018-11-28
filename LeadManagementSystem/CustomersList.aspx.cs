using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;

public partial class CustomersList : System.Web.UI.Page
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
                GetCustomerList();
                gvCustomerList.UseAccessibleHeader = true;
                customerslist.Visible = true;
                editCustomer.Visible = false;
            }
        }
        catch { }
    }
    protected void GetCustomerList()
    {
        try
        {
            dataset = cusomerBL.GetCustomerList(0);
            gvCustomerList.DataSource = dataset;
            gvCustomerList.DataBind();
            gvCustomerList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
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
                lblMessage.Text = "Customer details updated Successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                GetCustomerList();
                customerslist.Visible = true;
                editCustomer.Visible = false;
                Clear();
            }
            else
            {
                lblMessage.Text = "Please try again!";
                lblMessage.ForeColor = System.Drawing.Color.Red;

            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
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
                    customerslist.Visible = false;
                    editCustomer.Visible = true;
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
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Something went wrong, please contact administrator";
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        customerslist.Visible = true;
        editCustomer.Visible = false;
        Clear();
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
                lblMessage.Text = "Customer details deleted Successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                GetCustomerList();
            }
            else
            {
                lblMessage.Text = "Please try again!";
                lblMessage.ForeColor = System.Drawing.Color.Red;

            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}