using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;

public partial class NewConsultant : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    consultantEntity consultant = new consultantEntity();
    ConsultantBL consultantBL = new ConsultantBL();
    EncryptDecrypt encrypydecrypt = new EncryptDecrypt();
    CommanClass _objComman = new CommanClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtPassword.Attributes["type"] = "password";
                GetStatus();
                GetDesignation();
                GetBranch();
            }
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void GetDesignation()
    {
        try
        {

            dataset = consultantBL.GetDesignation();
            ddlDesignation.DataSource = dataset;
            ddlDesignation.DataTextField = "Designation";
            ddlDesignation.DataValueField = "DesignationID";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("--Select Designation --", "-1"));
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void GetBranch()
    {
        try
        {
            dataset = consultantBL.GetBranch();
            ddlBranch.DataSource = dataset;
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("--Select Branch --", "-1"));
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void GetStatus()
    {
        try
        {
            dataset = consultantBL.GetStatus();
            ddlStatus.DataSource = dataset;
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("--Select Status --", "-1"));
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            consultant.FirstName = txtFirstName.Text;
            consultant.LastName = txtLastName.Text;
            consultant.Mobile = "";
            consultant.Email = txtEmail.Text;
            consultant.LoginID = txtLoginId.Text;
            consultant.Password = encrypydecrypt.Encrypt(txtPassword.Text.Trim());
            consultant.Designation = Convert.ToInt32(ddlDesignation.SelectedValue);
            consultant.Branch = Convert.ToInt32(ddlBranch.SelectedValue);
            consultant.Status = Convert.ToInt32(ddlStatus.SelectedValue);

            int result = consultantBL.CUDConsultant(consultant, 'I');
            if (result == 1)
            {
                lblMessage.Text = "Consultant details saved Successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                Clear();
                btnSave.Visible = true;
                dvRPwd.Visible = true;
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard.aspx");
    }
    public void Clear()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmail.Text = "";
        ddlBranch.SelectedValue = "-1";
        ddlDesignation.SelectedValue = "-1";
        ddlStatus.SelectedValue = "-1";
        txtLoginId.Text = "";
        txtPassword.Text = "";
        txtConfirmPassword.Text = "";

    }
}