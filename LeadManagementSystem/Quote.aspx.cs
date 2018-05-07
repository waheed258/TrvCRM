using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;

public partial class Quote : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    QuoteBL qtBL = new QuoteBL();
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();
    int LeadID = 0;
    string city = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            city = encryptdecrypt.Decrypt(Request.QueryString["city"]);
            LeadID = Convert.ToInt32(encryptdecrypt.Decrypt(Request.QueryString["id"]));

            if (!IsPostBack)
            {               
                lblClientName.Text = encryptdecrypt.Decrypt(Request.QueryString["client"]);
                lblProduct.Text = encryptdecrypt.Decrypt(Request.QueryString["prod"]);
                lblSource.Text = encryptdecrypt.Decrypt(Request.QueryString["source"]);
                lblDestination.Text = encryptdecrypt.Decrypt(Request.QueryString["city"]);
                GetCostTypeDataAdult();
                GetCostTypeDataChild();
                
                string result = GetQuoteData();

                if (result == "0")
                {
                    Clear();
                    GetIncludeExcludeData();
                }

            }            

            if (txtAdultPrice.Text != "")
            {
                ddlAdultPersons.Enabled = true;
            }
            else
            {
                ddlAdultPersons.Enabled = false;
            }

            if (txtChildPrice.Text != "")
            {
                ddlChildPersons.Enabled = true;
            }
            else
            {
                ddlChildPersons.Enabled = false;
            }

        }
        catch
        {  }
    }
    
    protected void GetCostTypeDataAdult()
    {
        try
        {            
            dataset = qtBL.GetTypeData("A");
            ddlAdultType.DataSource = dataset;
            ddlAdultType.DataTextField = "CostTypeDescription";
            ddlAdultType.DataValueField = "CostTypeID";
            ddlAdultType.DataBind();
            ddlAdultType.Items.Insert(0, new ListItem("--Select Cost Type --", "-1"));
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }

    protected void GetCostTypeDataChild()
    {
        try
        {
            dataset = qtBL.GetTypeData("C");
            ddlChildType.DataSource = dataset;
            ddlChildType.DataTextField = "CostTypeDescription";
            ddlChildType.DataValueField = "CostTypeID";
            ddlChildType.DataBind();
            ddlChildType.Items.Insert(0, new ListItem("--Select Cost Type --", "-1"));
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }

    protected string GetQuoteData()
    {
        string strResult = string.Empty;
        try
        {
            dataset = qtBL.GetQuotePDFData(LeadID);

            if (dataset.Tables.Count > 0)
            {
               
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    strResult = "1";
                    txtDate.Text = dataset.Tables[0].Rows[0]["QuoteDate"].ToString();
                    ddlAdultType.SelectedValue = dataset.Tables[0].Rows[0]["CostForAdultType"].ToString();

                    txtAdultPrice.Text = dataset.Tables[0].Rows[0]["CostForAdult"].ToString();
                    ddlAdultPersons.SelectedValue = dataset.Tables[0].Rows[0]["NoOfAdults"].ToString();
                    lblAdultTotPrice.Text = dataset.Tables[0].Rows[0]["AdultTotal"].ToString();

                    ddlChildType.SelectedValue = dataset.Tables[0].Rows[0]["CostForChildType"].ToString();
                    txtChildPrice.Text = dataset.Tables[0].Rows[0]["CostForChild"].ToString();
                    ddlChildPersons.SelectedValue = dataset.Tables[0].Rows[0]["NoOfChildren"].ToString();
                    lblChildTotPrice.Text = dataset.Tables[0].Rows[0]["ChildTotal"].ToString();

                    txtFlightDetails.Text = dataset.Tables[0].Rows[0]["FlightDetails"].ToString();
                    txtCarHireDetails.Text = dataset.Tables[0].Rows[0]["CarHireDetails"].ToString();
                    txtHotelInfo.Text = dataset.Tables[0].Rows[0]["HotelInfo"].ToString();
                    txtItinerary.Text = dataset.Tables[0].Rows[0]["ItineraryDetails"].ToString();
                    txtIncludes.Text = dataset.Tables[0].Rows[0]["Includes"].ToString();
                    txtExcludes.Text = dataset.Tables[0].Rows[0]["Excludes"].ToString();
                    txtTravelInsur.Text = dataset.Tables[0].Rows[0]["TravelInsurance"].ToString();


                    if (ddlAdultType.SelectedValue == "1")
                    {
                        dvAdultPersons.Visible = true;
                        dvAdultTot.Visible = true;
                        ddlAdultPersons.Enabled = true;
                    }

                    if (ddlChildType.SelectedValue == "3")
                    {
                        dvChildPersons.Visible = true;
                        ddlChildPersons.Enabled = true;
                        dvChildTotalPrice.Visible = true;
                    }

                }
                else
                {
                    strResult = "0";
                }
            }
            

        }
        catch
        { strResult = "0"; }

        return strResult;
    }

    protected void GetIncludeExcludeData()
    {
        try
        {
            dataset = qtBL.GetIncudeExcludes();

           txtIncludes.Text = dataset.Tables[0].Rows[0]["IncludesDescription"].ToString();
           txtExcludes.Text = dataset.Tables[1].Rows[0]["ExcludesDescription"].ToString();

        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }

    protected void ddlAdultType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtAdultPrice.Text = "";
        lblAdultTotPrice.Text = "";

        if (ddlAdultType.SelectedValue == "1")
        {
            dvAdultPersons.Visible = true;
            dvAdultTot.Visible = true;
        }
        else
        {
            dvAdultPersons.Visible = false;
            dvAdultTot.Visible = false;
            ddlAdultPersons.SelectedValue = "0";
            lblAdultTotPrice.Text = "";
        }
    }
    protected void ddlChildType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtChildPrice.Text = "";
        lblChildTotPrice.Text = "";
        if (ddlChildType.SelectedValue == "3")
        {
            dvChildPersons.Visible = true;
            dvChildTotalPrice.Visible = true;
        }
        else
        {
            dvChildPersons.Visible = false;
            dvChildTotalPrice.Visible = false;
            ddlChildPersons.SelectedValue = "0";
            lblChildTotPrice.Text = "";
        }
    }
    protected void ddlChildPersons_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtChildPrice.Text != "" && ddlChildPersons.SelectedValue != "0") 
        {
            lblChildTotPrice.Text = Convert.ToString(Convert.ToInt32(txtChildPrice.Text) * Convert.ToInt32(ddlChildPersons.SelectedValue));
        }
    }
    protected void ddlAdultPersons_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAdultPrice.Text != "" && ddlAdultPersons.SelectedValue != "0")
        {
            lblAdultTotPrice.Text = Convert.ToString(Convert.ToInt32(txtAdultPrice.Text) * Convert.ToInt32(ddlAdultPersons.SelectedValue));
        }
    }
    protected void imgbtnSubmitAssign_Click(object sender, ImageClickEventArgs e)
    {
        QuoteEntity qtEntity = new QuoteEntity();

        qtEntity.CarHireDetails = txtHotelInfo.Text;
        qtEntity.ConsultantName = Session["Name"].ToString();
        qtEntity.CostForAdult = txtAdultPrice.Text;
        qtEntity.CostForAdultType = Convert.ToInt32(ddlAdultType.SelectedValue);
        qtEntity.CostForChild = txtChildPrice.Text;
        qtEntity.CostForChildType = Convert.ToInt32(ddlChildType.SelectedValue);
        qtEntity.Excludes = txtExcludes.Text;
        qtEntity.FlightDetails = txtFlightDetails.Text;
        qtEntity.HotelInfo = txtHotelInfo.Text;
        qtEntity.Includes = txtIncludes.Text;
        qtEntity.ItineraryDetails = txtItinerary.Text;
        qtEntity.LeadID = LeadID;
        qtEntity.NoOfAdults = Convert.ToInt32(ddlAdultPersons.SelectedValue);
        qtEntity.NoOfChildren = Convert.ToInt32(ddlChildPersons.SelectedValue);
        qtEntity.QuoteDate = txtDate.Text;
        qtEntity.ToCity = city;
        qtEntity.TravelInsurance = txtTravelInsur.Text;
        qtEntity.AdultTotal = lblAdultTotPrice.Text;
        qtEntity.ChildTotal = lblChildTotPrice.Text;


        int result = qtBL.CUDQuote(qtEntity);

        if (result == 1)
        {
            message.Text = "Quote Details saved Successfully!";
            message.ForeColor = System.Drawing.Color.Green;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            Clear();
        }
        else
        {
            message.Text = "Please try again!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }


    }
    protected void imgbtnBackAssign_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void Clear()
    {
        txtDate.Text = "";
        ddlAdultType.SelectedValue = "-1";
        txtAdultPrice.Text = "";
        ddlAdultPersons.SelectedValue = "0";
        ddlChildType.SelectedValue = "-1";
        txtChildPrice.Text = "";
        ddlChildPersons.SelectedValue = "0";
        lblChildTotPrice.Text = "";
        lblAdultTotPrice.Text = "";
        txtFlightDetails.Text = "";
        txtCarHireDetails.Text = "";
        txtHotelInfo.Text = "";
        txtItinerary.Text = "";
        //txtIncludes.Text = "";
        //txtExcludes.Text = "";
        txtTravelInsur.Text = "";

        dvAdultPersons.Visible = false;
        dvChildPersons.Visible = false;
        dvAdultTot.Visible = false;
        dvChildTotalPrice.Visible = false;
    }
}