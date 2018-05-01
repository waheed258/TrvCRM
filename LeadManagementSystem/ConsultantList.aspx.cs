using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;
public partial class ConsultantList : System.Web.UI.Page
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
                _objComman.getRecordsPerPage(DropPage);
                GetConsultants();

                GetStatus();
                GetDesignation();
                GetBranch();
            }
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }

    protected void GetConsultants()
    {
        try
        {
            dataset = consultantBL.GetConsultants(0);
            if (dataset.Tables[0].Rows.Count > 0)
            {
                search.Visible = true;
            }
            else
            {
                search.Visible = false;
            }
            GridView1.DataSource = dataset;
            GridView1.PageSize = Convert.ToInt32(DropPage.SelectedValue);
            GridView1.DataBind();
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
        catch
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
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }

    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetConsultants();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Page")
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int RowIndex = row.RowIndex;
                ViewState["ConsultantID"] = ((Label)row.FindControl("lblConsultantID")).Text.ToString();
                if (e.CommandName == "EditConsultant")
                {


                    imgAdd.Visible = false;
                    search.Visible = false;
                    dvGrid.Visible = false;
                    dvEdit.Visible = true;
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                    dvRPwd.Visible = false;
                    txtPassword.Attributes.Add("onfocus", "this.type='text';");

                    GetStatus();
                    GetDesignation();
                    GetBranch();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openEditModal();", true);
                    txtFirstName.Text = ((Label)row.FindControl("lblFirstName")).Text.ToString();
                    txtLastName.Text = ((Label)row.FindControl("lblLastName")).Text.ToString();
                    txtMobile.Text = ((Label)row.FindControl("lblMobile")).Text.ToString();
                    txtEmail.Text = ((Label)row.FindControl("lblEmailID")).Text.ToString();
                    txtLoginId.Text = ((Label)row.FindControl("lblLoginID")).Text.ToString();
                    txtPassword.Text = encrypydecrypt.Decrypt(((Label)row.FindControl("lblPwd")).Text.ToString());
                    ddlBranch.SelectedValue = ((Label)row.FindControl("lblBranch")).Text.ToString();
                    ddlDesignation.SelectedValue = ((Label)row.FindControl("lblDesignation")).Text.ToString();
                    ddlStatus.SelectedValue = ((Label)row.FindControl("lblAStatus")).Text.ToString();
                }
                else if (e.CommandName == "DeleteConsultant")
                {
                    lbldeletemessage.Text = "Are you sure, you want to delete Consultant Details?";
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
    protected void btnSure_Click(object sender, EventArgs e)
    {
        try
        {
            consultant.ConsultantID = Convert.ToInt32(ViewState["ConsultantID"].ToString());
            //need to change updated by after session created
            consultant.UpdatedBy = 0;
            consultant.FirstName = "";
            consultant.LastName = "";
            consultant.Mobile = "";
            consultant.Email = "";
            consultant.LoginID = "";
            consultant.Password = "";
            consultant.Designation = 0;
            consultant.Branch = 0;
            consultant.Status = 0;

            int result = consultantBL.CUDConsultant(consultant, 'D');
            if (result == 1)
            {
                message.ForeColor = System.Drawing.Color.Green;
                message.Text = "Consultant details deleted Successfully!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                GetConsultants();
            }
            else
            {
                message.ForeColor = System.Drawing.Color.Red;
                message.Text = "Please try again!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        catch
        {
            message.ForeColor = System.Drawing.Color.Red;
            message.Text = "Something went wrong, please contact administrator";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }


    protected void imgAdd_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
        imgAdd.Visible = false;
        search.Visible = false;
        dvGrid.Visible = false;
        dvEdit.Visible = true;
        btnUpdate.Visible = false;
        dvRPwd.Visible = true;
        txtPassword.Attributes.Add("onfocus", "this.type='password';");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GetConsultants();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            consultant.FirstName = txtFirstName.Text;
            consultant.LastName = txtLastName.Text;
            consultant.Mobile = txtMobile.Text;
            consultant.Email = txtEmail.Text;
            consultant.LoginID = txtLoginId.Text;
            consultant.Password = encrypydecrypt.Encrypt(txtPassword.Text.Trim());
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

                imgAdd.Visible = true;
                search.Visible = true;
                dvGrid.Visible = true;
                dvEdit.Visible = false;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                dvRPwd.Visible = true;

            }
            else
            {
                message.Text = "Please try again!";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                //Clear();
            }
        }
        catch (Exception ex)
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        imgAdd.Visible = true;
        search.Visible = true;
        dvGrid.Visible = true;
        dvEdit.Visible = false;
        btnUpdate.Visible = false;
        btnSave.Visible = true;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            consultant.ConsultantID = Convert.ToInt32(ViewState["ConsultantID"].ToString());
            //need to change updated by after session created
            consultant.UpdatedBy = 0;
            consultant.FirstName = txtFirstName.Text;
            consultant.LastName = txtLastName.Text;
            consultant.Mobile = txtMobile.Text;
            consultant.Email = txtEmail.Text;
            consultant.LoginID = txtLoginId.Text;
            consultant.Password = encrypydecrypt.Encrypt(txtPassword.Text.Trim());
            consultant.Designation = Convert.ToInt32(ddlDesignation.SelectedValue);
            consultant.Branch = Convert.ToInt32(ddlBranch.SelectedValue);
            consultant.Status = Convert.ToInt32(ddlStatus.SelectedValue);

            int result = consultantBL.CUDConsultant(consultant, 'U');
            if (result == 1)
            {
                message.ForeColor = System.Drawing.Color.Green;
                message.Text = "Consultant details updated Successfully!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                GetConsultants();

                imgAdd.Visible = true;
                search.Visible = true;
                dvGrid.Visible = true;
                dvEdit.Visible = false;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                dvRPwd.Visible = true;

            }
            else
            {
                message.ForeColor = System.Drawing.Color.Red;
                message.Text = "Please try again!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        catch (Exception ex)
        {
            message.ForeColor = System.Drawing.Color.Red;
            message.Text = "Something went wrong, please contact administrator";
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

}