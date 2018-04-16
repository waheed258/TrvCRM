using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class NewConsultant : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    consultantEntity consultant = new consultantEntity();
    ConsultantBL consultantBL = new ConsultantBL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GetStatus();
                GetDesignation();
                GetBranch();
            }
        }
        catch { }
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
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
        ddlBranch.SelectedValue = "-1";
        ddlDesignation.SelectedValue = "-1";
        ddlStatus.SelectedValue = "-1";
        txtLoginId.Text = "";
        txtPassword.Text = "";
        txtConfirmPassword.Text = "";

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            consultant.FirstName = txtFirstName.Text;
            consultant.LastName = txtLastName.Text;
            consultant.Mobile = txtMobile.Text;
            consultant.Email = txtEmail.Text;
            consultant.LoginID = txtLoginId.Text;
            consultant.Password = txtPassword.Text.Trim();
            consultant.Designation = Convert.ToInt32(ddlDesignation.SelectedValue);
            consultant.Branch = Convert.ToInt32(ddlBranch.SelectedValue);
            consultant.Status = Convert.ToInt32(ddlStatus.SelectedValue);

            int result = consultantBL.CUDConsultant(consultant, 'I');
            if (result == 1)
            {
                message.Text = "Consultant details saved Successfully!";
                message.ForeColor = System.Drawing.Color.Green;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                Clear();
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
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
    }
}