﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Web;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Configuration;
using System.Net.Mail;
public partial class Quote : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    QuoteBL qtBL = new QuoteBL();
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();
    int LeadID = 0;
    string city = string.Empty;
    string clEmail = string.Empty;
    string QuoteType = string.Empty;
    string TempId = string.Empty;
    string flag = string.Empty;
    string quoteno = string.Empty;
    string lStatus = string.Empty;
    LeadBL leadBL = new LeadBL();
    ProductBL productBL = new ProductBL();
    List<QuoteEntity> lstQuoteEntity = new List<QuoteEntity>();
    StringBuilder QuoteBuilder = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            city = encryptdecrypt.Decrypt(Request.QueryString["city"]);
            LeadID = Convert.ToInt32(encryptdecrypt.Decrypt(Request.QueryString["id"]));
            quoteno = Request.QueryString["QuoteID"];
            flag = Request.QueryString["flag"];
            clEmail = encryptdecrypt.Decrypt(Request.QueryString["em"]);
            QuoteType = Request.QueryString["qtype"];
            TempId = Request.QueryString["temp"];
            lStatus = Request.QueryString["status"];
            if (!IsPostBack)
            {
                lblClientName.Text = encryptdecrypt.Decrypt(Request.QueryString["client"]);
                //lblProduct.Text = encryptdecrypt.Decrypt(Request.QueryString["prod"]);
                txtSource.Text = encryptdecrypt.Decrypt(Request.QueryString["source"]);
                txtDestination.Text = encryptdecrypt.Decrypt(Request.QueryString["city"]);
                GetCostTypeDataAdult();
                GetCostTypeDataChild();
                GetProducts();
                ddlPackage.SelectedValue = encryptdecrypt.Decrypt(Request.QueryString["prodid"]);
                emailsection.Style.Add("display", "none");
                quotesection.Style.Add("display", "unset");
                backToLead.Visible = true;
                imgbtnBackQuote.Visible = false;

                if (flag == "1")
                {
                    if (QuoteType == "2")
                    {
                        GetTemplateQuoteData(TempId);
                    }
                    else if (QuoteType == "3")
                    {
                        Clear();
                        dvProdct.Visible = false;
                        dvCustomProduct.Visible = true;
                        txtTravelInsur.Text = "We recommend: Travel Policy @ R 350.00pp,  R12 million medical cover, max 30 days.";
                    }
                    else if (QuoteType == "1")
                    {
                        Clear();
                        dvProdct.Visible = true;
                        dvCustomProduct.Visible = false;
                        txtTravelInsur.Text = "We recommend: Travel Policy @ R 350.00pp,  R12 million medical cover, max 30 days.";
                    }
                }
                else
                {
                    GetQuoteData();
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

            if (QuoteType == "2")
            {
                btnTemplageName.Visible = false;
            }
        }
        catch
        { }
    }
    protected void GetProducts()
    {
        try
        {
            dataset = productBL.GetProduct();
            ViewState["products"] = dataset;
            ddlPackage.DataSource = dataset;
            ddlPackage.DataTextField = "package_short_des";
            ddlPackage.DataValueField = "package_id";
            ddlPackage.DataBind();
            ddlPackage.Items.Insert(0, new ListItem("--Select Product --", "-1"));

            if (encryptdecrypt.Decrypt(Request.QueryString["prodid"]) == "" || encryptdecrypt.Decrypt(Request.QueryString["prodid"]) == null)
            {
                txtIncludes.Text = "";
                txtExcludes.Text = "";
            }
            else
            {
                int prodid = Convert.ToInt32(encryptdecrypt.Decrypt(Request.QueryString["prodid"]));
                var includesExcludes = (from products in dataset.Tables[0].AsEnumerable()
                                        where products.Field<int>("package_id") == prodid
                                        select new
                                        {
                                            Includes = products.Field<string>("package_includes"),
                                            Excludse = products.Field<string>("package_excludes")
                                        }).First();

                txtIncludes.Text = includesExcludes.Includes.Replace("\n", "<br/>");
                txtExcludes.Text = includesExcludes.Excludse.Replace("\n", "<br/>");
            }
        }
        catch
        {
            message.Text = "Something went wrong. Please contact administrator!";
            message.ForeColor = System.Drawing.Color.Red;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
    public class ProductBL
    {
        DataManager dataManager = new DataManager();
        public DataSet GetProduct()
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("inOperationName", "SELECT");
            DataSet ds = dataManager.ExecuteDataSet("get_packages", hashtable);
            return ds;
        }
    }
    public class DataManager
    {
        #region SqlConnection

        /// <summary>
        /// This method gets the connection string.
        /// </summary>
        /// <returns>Connection String</returns>
        public string GetConnectionString()
        {
            string str = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            /* This code takes connection string from the web.config file.*/

            //return "Data Source=209.222.108.170;Database=trvcrm_dev;User Id=trvcrm_dev_user;Password=Dino@321;";
            //return "server=67.225.171.204;user id=seren_web_padmin; password=Dino@123;database=seren_web_prod";
            //return "Data Source=67.225.171.204;Database=seren_web_prod;User Id=seren_web_padmin;Password=Dino@123;Connect Timeout=12000";
            return str;
        }


        /// <summary>
        /// This method returns SqlConnection object.
        /// </summary>
        /// <returns>SqlConnection</returns>
        public MySqlConnection GetSqlConnection()
        {
            string strConnection = GetConnectionString();
            if (strConnection == null)
                return null;
            var objSqlConnection = new MySqlConnection(strConnection);
            return objSqlConnection;
        }

        #endregion


        #region EXECUTE DATASET

        /// <summary>
        /// This method returns the data in dataset form. 
        /// </summary>
        /// <param name="commandText">Command text</param>
        /// <returns>Data in the form of Dataset.</returns>
        public DataSet ExecuteDataSet(string commandText, Hashtable htParameters)
        {

            var dsData = new DataSet();
            var objMyDataAdapter = new MySqlDataAdapter();
            MySqlConnection objMySqlConn = GetSqlConnection();
            try
            {
                if (objMySqlConn == null)
                {
                    return null;
                }
                var objMyCommand = new MySqlCommand
                {
                    Connection = objMySqlConn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = commandText
                };
                foreach (DictionaryEntry parameter in htParameters)
                {
                    objMyCommand.Parameters.AddWithValue(parameter.Key.ToString(), parameter.Value);
                }
                objMyDataAdapter.SelectCommand = objMyCommand;

                objMyDataAdapter.Fill(dsData);
                //objMySqlConn.Close();
                //objMySqlConn.Dispose();

                return dsData;
            }
            catch
            {
                return null;
            }
            finally
            {
                objMySqlConn.Close();
            }
        }

        #endregion
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
            dataset = qtBL.GetQuotePDFData(quoteno);

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
                    lblChildTotPrice.Text = dataset.Tables[0].Rows[0]["ChildTotal"].ToString().TrimEnd();

                    txtFlightDetails.Text = dataset.Tables[0].Rows[0]["FlightDetails"].ToString();
                    txtCarHireDetails.Text = dataset.Tables[0].Rows[0]["CarHireDetails"].ToString();
                    txtHotelInfo.Text = dataset.Tables[0].Rows[0]["HotelInfo"].ToString();
                    txtItinerary.Text = dataset.Tables[0].Rows[0]["ItineraryDetails"].ToString();
                    txtIncludes.Text = dataset.Tables[0].Rows[0]["Includes"].ToString();
                    txtExcludes.Text = dataset.Tables[0].Rows[0]["Excludes"].ToString();
                    txtTravelInsur.Text = dataset.Tables[0].Rows[0]["TravelInsurance"].ToString();

                    lblGrandTotal.Text = Convert.ToString(Convert.ToInt32(lblAdultTotPrice.Text) + Convert.ToInt32(lblChildTotPrice.Text));

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

                    if (QuoteType == "3")
                    {
                        dvProdct.Visible = false;
                        dvCustomProduct.Visible = true;
                        txtProduct.Text = dataset.Tables[0].Rows[0]["PackageId"].ToString();
                    }
                    else
                    {
                        dvProdct.Visible = true;
                        dvCustomProduct.Visible = false;
                        ddlPackage.SelectedValue = string.IsNullOrEmpty(dataset.Tables[0].Rows[0]["PackageId"].ToString()) ? "-1" : dataset.Tables[0].Rows[0]["PackageId"].ToString();
                    }


                }
                else
                {
                    strResult = "0";
                    if (QuoteType == "3")
                    {
                        dvProdct.Visible = false;
                        dvCustomProduct.Visible = true;
                    }
                    else
                    {
                        dvProdct.Visible = true;
                        dvCustomProduct.Visible = false;
                    }
                }
            }


        }
        catch
        { strResult = "0"; }

        return strResult;
    }
    protected string GetTemplateQuoteData(string id)
    {
        string strResult = string.Empty;
        try
        {
            dataset = qtBL.GetTemplateData(Convert.ToInt32(id));

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

                    string strCustom = dataset.Tables[0].Rows[0]["IsCustomTemplate"].ToString();

                    if (strCustom == "Y")
                    {
                        dvProdct.Visible = false;
                        dvCustomProduct.Visible = true;
                        txtProduct.Text = dataset.Tables[0].Rows[0]["PackageId"].ToString();
                    }
                    else
                    {
                        dvProdct.Visible = true;
                        dvCustomProduct.Visible = false;
                        ddlPackage.SelectedValue = dataset.Tables[0].Rows[0]["PackageId"].ToString();
                    }


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
            if (lblAdultTotPrice.Text == "")
            {
                lblGrandTotal.Text = Convert.ToString(Convert.ToInt32(txtAdultPrice.Text) + Convert.ToInt32(lblChildTotPrice.Text));
            }
            else
                lblGrandTotal.Text = Convert.ToString(Convert.ToInt32(lblAdultTotPrice.Text) + Convert.ToInt32(lblChildTotPrice.Text));
        }
    }
    protected void ddlAdultPersons_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtAdultPrice.Text != "" && ddlAdultPersons.SelectedValue != "0")
        {
            lblAdultTotPrice.Text = Convert.ToString(Convert.ToInt32(txtAdultPrice.Text) * Convert.ToInt32(ddlAdultPersons.SelectedValue));
            lblGrandTotal.Text = lblAdultTotPrice.Text;
        }
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
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Clear();
    }
    private void GetPdfMultipleOptions(List<QuoteEntity> lstQuEnt)
    {
        try
        {
            int cnt = 1;
            StringBuilder sbMainrow = new StringBuilder();
            StreamReader reader = new StreamReader(Server.MapPath("~/QuotePDFMultiple.html"));
            string readFile = reader.ReadToEnd();
            reader.Close();
            foreach (QuoteEntity qt in lstQuEnt)
            {
                DataSet ds = new DataSet();
                ds = qtBL.GetQuotePDFData(qt.QuoteNumber);

                if (ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region Quotation Info

                        sbMainrow.Append("<table style='width:100%;margin-bottom:15px; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td colspan='4' width='100%' style='font-weight:700;background-color:#00aeef; width:100%; padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>QUOTATION " + cnt + " </td>");
                        sbMainrow.Append("</tr>");

                        foreach (DataRow dtlRow in ds.Tables[0].Rows)
                        {

                            if (!string.IsNullOrEmpty(dtlRow["FlightDetails"].ToString()))
                            {
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='4' width='100%' style='font-weight:700; width:100%; padding:5px 10px; font-size:3.56mm; text-transform:uppercase;'>FLIGHT QUOTATION</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>" + dtlRow["FlightDetails"].ToString() + "</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("</table>");
                                sbMainrow.Append("</tr>");
                            }

                            if (!string.IsNullOrEmpty(dtlRow["HotelInfo"].ToString()))
                            {
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='4' width='100%' style='font-weight:700; width:100%; padding:5px 10px; font-size:3.56mm; text-transform:uppercase;'>HOTEL QUOTATION</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>" + dtlRow["HotelInfo"].ToString() + "</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("</table>");
                                sbMainrow.Append("</tr>");
                            }

                            if (!string.IsNullOrEmpty(dtlRow["CarHireDetails"].ToString()))
                            {
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='4' width='100%' style='font-weight:700; width:100%; padding:5px 10px; font-size:3.56mm; text-transform:uppercase;'>CAR QUOTATION</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>" + dtlRow["CarHireDetails"].ToString() + "</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("</table>");
                                sbMainrow.Append("</tr>");
                            }

                            if (!string.IsNullOrEmpty(dtlRow["ItineraryDetails"].ToString()))
                            {

                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='4' width='100%' style='font-weight:700; width:100%; padding:5px 10px; font-size:3.56mm; text-transform:uppercase;'>ITINERARY</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>" + dtlRow["ItineraryDetails"].ToString() + "</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("</table>");
                                sbMainrow.Append("</tr>");

                            }

                            if (!string.IsNullOrEmpty(dtlRow["Includes"].ToString()) && !string.IsNullOrEmpty(dtlRow["Excludes"].ToString()))
                            {
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='55%' style='font-weight:700;background-color:#00aeef;  padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Include</td>");
                                sbMainrow.Append("<td width='45%' style='font-weight:700;background-color:#00aeef;  padding:5px 10px;  color:#fff; font-size:3.56mm; text-transform:uppercase;'>Exclude</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append(" <td width='55%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>" + dtlRow["Includes"].ToString() + "</td>");
                                sbMainrow.Append(" <td width='55%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9; '>" + dtlRow["Excludes"].ToString() + "</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("</table>");
                                sbMainrow.Append("</tr>");
                            }
                            else if (!string.IsNullOrEmpty(dtlRow["Includes"].ToString()) || !string.IsNullOrEmpty(dtlRow["Excludes"].ToString()))
                            {
                                string strHeading = !string.IsNullOrEmpty(dtlRow["Includes"].ToString()) ? "Include" : "Exclude";
                                string strData = !string.IsNullOrEmpty(dtlRow["Includes"].ToString()) ? dtlRow["Includes"].ToString() : dtlRow["Excludes"].ToString();
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='4' width='100%' style='font-weight:700; width:100%; padding:5px 10px; font-size:3.56mm; text-transform:uppercase;'>'" + strHeading + "'</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='100%' style='padding:0px 10px 0px;  border-right:1px solid #b9b9b9;'>'" + strData + "'</td>");
                                sbMainrow.Append("</tr>");
                                sbMainrow.Append("</table>");
                                sbMainrow.Append("</tr>");
                            }

                            string adultText = string.Empty;
                            if (dtlRow["CostForAdultType"].ToString() == "1")
                            {
                                adultText = "COST PER PERSON SHARING R " + dtlRow["CostForAdult"].ToString() + " x " + dtlRow["NoOfAdults"].ToString() + " adults";
                            }
                            else if (dtlRow["CostForAdultType"].ToString() == "2")
                            {
                                adultText = "COST PER PERSON INDIVIDUAL R " + dtlRow["CostForAdult"].ToString() + " x 1 adult";
                            }

                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<table style='width:100%; border:1px solid #b9b9b9; border-spacing:0; margin:0 0 3mm;'>");
                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<td width='30%' style='padding:10px 10px 5px; border-bottom:1px solid #b9b9b9; border-right:1px solid #b9b9b9;'><h5 style='font-size:3.05mm; margin:0;  color:#00aeef; width:100%;'>COST PER ADULT</h5></td>");
                            sbMainrow.Append("<td width='55%' style='padding:10px 10px 5px; border-bottom:1px solid #b9b9b9;border-right:1px solid #b9b9b9;'><p style='font-size:2.5mm; margin:0; width:50%;'>" + adultText + "</p></td>");
                            sbMainrow.Append("<td width='15%' style='padding:10px 10px 5px; border-bottom:1px solid #b9b9b9;'><p style='font-size:2.5mm; margin:0; width:100%; font-weight:bold;'>" + dtlRow["AdultTotal"].ToString() + "</p></td>");
                            sbMainrow.Append("</tr>");
                            if (!string.IsNullOrEmpty(dtlRow["CostForChild"].ToString()))
                            {
                                string childText = string.Empty;
                                if (dtlRow["CostForChildType"].ToString() == "3")
                                {
                                    childText = "COST PER CHILD SHARING R " + dtlRow["CostForChild"].ToString() + " x " + dtlRow["NoOfChildren"].ToString() + " child";
                                }
                                else
                                {
                                    childText = "";
                                }

                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td width='30%' style='padding:10px 10px 5px; border-bottom:1px solid #b9b9b9; border-right:1px solid #b9b9b9;'><h5 style='font-size:3.05mm; margin:0;  color:#00aeef; width:100%;'>COST PER CHILD</h5></td>");
                                sbMainrow.Append("<td width='55%' style='padding:10px 10px 5px; border-bottom:1px solid #b9b9b9;border-right:1px solid #b9b9b9;'><p style='font-size:2.5mm; margin:0; width:50%;'>" + childText + "</p></td>");
                                sbMainrow.Append("<td width='15%' style='padding:10px 10px 5px; border-bottom:1px solid #b9b9b9;'><p style='font-size:2.5mm; margin:0; width:100%; font-weight:bold;'>" + dtlRow["ChildTotal"].ToString() + "</p></td>");
                                sbMainrow.Append("</tr>");
                            }

                            sbMainrow.Append("</table>");
                            sbMainrow.Append("</tr>");
                            readFile = readFile.Replace("{QuoteDate}", dtlRow["QuoteDate"].ToString());
                            readFile = readFile.Replace("{TravelInsurance}", dtlRow["TravelInsurance"].ToString());
                            readFile = readFile.Replace("{ConsultantName}", dtlRow["ConsultantName"].ToString());
                            readFile = readFile.Replace("{ClientName}", lblClientName.Text.ToString());
                        }
                        sbMainrow.Append("</table>");

                        #endregion

                        cnt += 1;
                    }
                }


            }
            readFile = readFile.Replace("{Details}", sbMainrow.ToString());

            string StrContent = readFile;

            string filepath = Server.MapPath("~/QuotePDF");

            var quotes = lstQuEnt.Select(q => q.QuoteNumber).ToList();
            string QuoteNumbers = string.Join("_", quotes);

            bool pdf = GenerateHTML_TO_PDF(StrContent, true, filepath, false, QuoteNumbers);
            if (pdf)
            {
                string consultName = Session["Name"].ToString();
                SendMail(lblClientName.Text, txtToEmailNew.Text, txtDestination.Text, consultName, QuoteNumbers);
                Session.Remove("lstQuoteEntity");
            }



        }
        catch (Exception ex)
        { }

    }
    private void GetPdf(string QuoteNumber)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = qtBL.GetQuotePDFData(QuoteNumber);
            StreamReader reader = new StreamReader(Server.MapPath("~/QuotePDF.html"));
            string readFile = reader.ReadToEnd();
            reader.Close();

            StringBuilder sbMainrow = new StringBuilder();

            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dtlRow in ds.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(dtlRow["FlightDetails"].ToString()))
                        {
                            sbMainrow.Append(" <div class='col-md-12'> <h4>Flight Details:</h4>" + dtlRow["FlightDetails"].ToString() + "</div>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["HotelInfo"].ToString()))
                        {
                            sbMainrow.Append(" <div class='col-md-12'> <h4>Hotel Details:</h4>" + dtlRow["HotelInfo"].ToString() + "</div>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["CarHireDetails"].ToString()))
                        {
                            sbMainrow.Append(" <div class='col-md-12'> <h4>Car Hire:</h4>" + dtlRow["CarHireDetails"].ToString() + "</div>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["ItineraryDetails"].ToString()))
                        {
                            sbMainrow.Append(" <div class='col-md-12'> <h4>Itinerary:</h4>" + dtlRow["ItineraryDetails"].ToString() + "</div>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["Includes"].ToString()))
                        {
                            sbMainrow.Append(" <div class='col-md-12'> <h4>Includes:</h4>" + dtlRow["Includes"].ToString() + "</div>");
                        }

                        if (!string.IsNullOrEmpty(dtlRow["Excludes"].ToString()))
                        {
                            sbMainrow.Append(" <div class='col-md-12'> <h4>Excluded:</h4>" + dtlRow["Excludes"].ToString() + "</div>");
                        }

                        readFile = readFile.Replace("{Details}", sbMainrow.ToString());

                        readFile = readFile.Replace("{QuoteNumber}", dtlRow["QuoteNumber"].ToString());
                        readFile = readFile.Replace("{QuoteDate}", dtlRow["QuoteDate"].ToString());
                        readFile = readFile.Replace("{DestinationCity}", dtlRow["DestinationCity"].ToString());
                        readFile = readFile.Replace("{TravelMonth}", dtlRow["QuoteDate"].ToString());
                        readFile = readFile.Replace("{TravelInsurance}", dtlRow["TravelInsurance"].ToString());
                        readFile = readFile.Replace("{ConsultantName}", dtlRow["ConsultantName"].ToString());
                        readFile = readFile.Replace("{ClientName}", lblClientName.Text.ToString());
                        readFile = readFile.Replace("{AdultTotal}", dtlRow["AdultTotal"].ToString());
                        readFile = readFile.Replace("{ChildTotal}", dtlRow["ChildTotal"].ToString());
                        readFile = readFile.Replace("{Includes}", dtlRow["Includes"].ToString());
                        readFile = readFile.Replace("{Excludes}", dtlRow["Excludes"].ToString());
                        readFile = readFile.Replace("{FlightDetails}", dtlRow["FlightDetails"].ToString());
                        readFile = readFile.Replace("{HotelDetails}", dtlRow["HotelInfo"].ToString());
                        readFile = readFile.Replace("{CarDetails}", dtlRow["CarHireDetails"].ToString());
                        readFile = readFile.Replace("{GrandTotal}", lblGrandTotal.Text.ToString());
                        readFile = readFile.Replace("{LeadStatus}", lStatus);



                        if (dtlRow["CostForAdultType"].ToString() == "1")
                        {
                            readFile = readFile.Replace("{AdultPrice}", "COST PER PERSON SHARING R " + dtlRow["CostForAdult"].ToString() + " x " + dtlRow["NoOfAdults"].ToString() + " adults");
                        }
                        else if (dtlRow["CostForAdultType"].ToString() == "2")
                        {
                            readFile = readFile.Replace("{AdultPrice}", "COST PER PERSON INDIVIDUAL R " + dtlRow["CostForAdult"].ToString() + " x 1 adult");
                        }

                        if (dtlRow["CostForChildType"].ToString() == "3")
                        {
                            readFile = readFile.Replace("{ChildPrice}", "COST PER CHILD SHARING R " + dtlRow["CostForChild"].ToString() + " x " + dtlRow["NoOfChildren"].ToString() + " child");
                        }
                        else
                        {
                            readFile = readFile.Replace("{ChildPrice}", "");
                        }
                    }
                }

                string StrContent = readFile;

                string filepath = Server.MapPath("~/QuotePDF");


                bool pdf = GenerateHTML_TO_PDF(StrContent, true, filepath, false, QuoteNumber);
                if (pdf)
                {
                    string consultName = Session["Name"].ToString();
                    SendMail(lblClientName.Text, txtToEmailNew.Text, txtDestination.Text, consultName, QuoteNumber);
                    Session.Remove("lstQuoteEntity");
                }

            }
        }
        catch
        { }

    }
    private bool GenerateHTML_TO_PDF(string HtmlString, bool ResponseShow, string Path, bool SaveFileDir, string QuoteNumber)
    {
        try
        {
            string pdf_page_size = "A4";
            SelectPdf.PdfPageSize pageSize = (SelectPdf.PdfPageSize)Enum.Parse(typeof(SelectPdf.PdfPageSize),
                pdf_page_size, true);

            string pdf_orientation = "Portrait";
            SelectPdf.PdfPageOrientation pdfOrientation =
                (SelectPdf.PdfPageOrientation)Enum.Parse(typeof(SelectPdf.PdfPageOrientation),
                pdf_orientation, true);

            int webPageWidth = 1024;
            int webPageHeight = 0;

            // instantiate a html to pdf converter object
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            // create a new pdf document converting an url
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(HtmlString, "");

            // save pdf document      

            //if (!SaveFileDir)
            //    doc.Save(Response, ResponseShow, Path);
            //else
            //    doc.Save(Path);

            string FileName = Path + "/" + QuoteNumber + ".pdf";

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            else
            {
                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }
            }

            doc.Save(FileName);

            doc.Close();

            return true;

        }
        catch
        { return false; }
    }
    public void SendMail(string clName, string clEmail, string clDestinationCity, string consultName, string QuoteNumber)
    {
        try
        {
            DataSet ds = leadBL.GetMailInfo();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string SmtpServer = ds.Tables[0].Rows[0]["con_smtp_host"].ToString();
                int SmtpPort = Convert.ToInt32(ds.Tables[0].Rows[0]["con_smtp_port"].ToString());
                //int SmtpPort = 587;
                string MailFrom = ds.Tables[0].Rows[0]["con_mail_from"].ToString();
                string DisplayNameFrom = ds.Tables[0].Rows[0]["con_from_name"].ToString();
                string FromPassword = ds.Tables[0].Rows[0]["con_from_pwd"].ToString();
                string MailTo = clEmail;
                //string MailTo = "karen@serendipitytours.co.za";
                string DisplayNameTo = string.Empty;
                string MailCc = string.Empty;
                string DisplayNameCc = string.Empty;
                string MailBcc = string.Empty;
                string Subject = string.Empty;
                string MailText = string.Empty;
                string Attachment = string.Empty;
                string cusAttachment = string.Empty;

                string filepath = Server.MapPath("~/QuotePDF");
                string FileName = filepath + "\\" + QuoteNumber + ".pdf";

                if (File.Exists(FileName))
                {
                    Attachment = FileName;
                }

                try
                {
                    Subject = "Serendipity Tours quote to " + clDestinationCity;
                    MailCc = "";

                    MailText = "Dear " + clName + ", <br/><br/>";
                    MailText += "Thank you for the opportunity to quote for your holiday to <b>" + clDestinationCity + "</b> <br/><br/>";
                    MailText += "Please find attached the options as discussed. Should you require any changes or amendments, please do not hesitate to contact me. I will be contacting you shortly to discuss the quote. <br/><br/>";
                    MailText += "Kind regards, <br/>";
                    MailText += "(" + consultName + ")";

                    bool mailSent = UpdateCustomMail(SmtpServer, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo, DisplayNameTo, MailCc, "", "", "", DisplayNameCc, MailBcc, Subject, MailText, Attachment);

                    if (mailSent)
                    {
                        MailSentSatatus(QuoteNumber);
                        //emailsection.Style.Add("display", "none");
                        //quotesection.Style.Add("display", "unset");
                        Response.Redirect("Lead.aspx?t=quote&idq=" + LeadID);
                    }

                }
                catch
                { }

            }
        }
        catch
        { }
    }
    public bool UpdateCustomMail(string SmtpHost, int SmtpPort, string MailFrom, string DisplayNameFrom, string FromPassword, string MailTo, string DisplayNameTo, string MailCc, string mailCc2, string mailCc3, string mailCc4, string DisplayNameCc, string MailBcc, string Subject, string MailText, string Attachment)
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

            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile PostedFile = Request.Files[i];
                    if (PostedFile.ContentLength > 0)
                    {
                        myMessage.Attachments.Add(new Attachment(PostedFile.InputStream, PostedFile.FileName));
                    }
                }
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

    public void MailSentSatatus(string QuoteNumber)
    {
        try
        {
            QuoteEntity qtEntity = new QuoteEntity();

            qtEntity.CarHireDetails = "";
            qtEntity.ConsultantName = "";
            qtEntity.CostForAdult = "";
            qtEntity.CostForAdultType = 0;
            qtEntity.CostForChild = "";
            qtEntity.CostForChildType = 0;
            qtEntity.Excludes = "";
            qtEntity.FlightDetails = "";
            qtEntity.HotelInfo = "";
            qtEntity.Includes = "";
            qtEntity.ItineraryDetails = "";
            qtEntity.LeadID = 0;
            qtEntity.NoOfAdults = 0;
            qtEntity.NoOfChildren = 0;
            qtEntity.QuoteDate = "";
            qtEntity.ToCity = "";
            qtEntity.TravelInsurance = "";
            qtEntity.AdultTotal = "";
            qtEntity.ChildTotal = "";
            qtEntity.IsMailSent = "Y";
            qtEntity.QuoteNumber = QuoteNumber;
            qtEntity.Operation = "U";

            string strQuoteNumber = qtBL.CUOperationQuote(qtEntity);
        }
        catch
        { }
    }
    protected void btnTemplageName_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "TemplageModal();", true);
    }
    protected void btnSaveTemplate_Click(object sender, EventArgs e)
    {
        try
        {
            QuoteEntity qtEntity = new QuoteEntity();

            qtEntity.CarHireDetails = txtCarHireDetails.Text;
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
            qtEntity.IsMailSent = "N";
            qtEntity.QuoteNumber = "";
            qtEntity.Operation = "I";
            if (QuoteType == "3")
            {
                qtEntity.PackageId = txtProduct.Text;
                qtEntity.IsCustomTemplate = "Y";
            }
            else
            {
                qtEntity.PackageId = ddlPackage.SelectedValue;
                qtEntity.IsCustomTemplate = "N";
            }

            qtEntity.TemplateName = txtTemplateName.Text;


            int result = qtBL.CreateQuoteTemplate(qtEntity);

            if (result == 1)
            {
                message.Text = "Template Details saved Successfully!";
                message.ForeColor = System.Drawing.Color.Green;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                //GetPdf(QuoteNumber);
                //Clear();
            }
            else
            {
                message.Text = "Please try again!";
                message.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        lstQuoteEntity = (List<QuoteEntity>)Session["lstQuoteEntity"];
        if (lstQuoteEntity == null)
        {
            GetPdf(ViewState["QuoteNumber"].ToString());
            CommanClass.MailStatusLog(LeadID, "QT001", "Success", "", ViewState["QuoteNumber"].ToString());
        }
        else
        {
            GetPdfMultipleOptions(lstQuoteEntity);
            CommanClass.MailStatusLog(LeadID, "QT001", "Success", "", QuoteBuilder.ToString());
        }
    }
    protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = (DataSet)ViewState["products"];
        int prodid = Convert.ToInt32(ddlPackage.SelectedValue);
        var includesExcludes = (from products in ds.Tables[0].AsEnumerable()
                                where products.Field<int>("package_id") == prodid
                                select new
                                {
                                    Includes = products.Field<string>("package_includes"),
                                    Excludse = products.Field<string>("package_excludes")
                                }).First();

        txtIncludes.Text = includesExcludes.Includes.Replace("\n", "<br/>");
        txtExcludes.Text = includesExcludes.Excludse.Replace("\n", "<br/>");
    }
    protected void imgbtnSubmitAssign_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            backToLead.Visible = false;
            imgbtnBackQuote.Visible = true;
            quotesection.Style.Add("display", "none");
            emailsection.Style.Add("display", "unset");
            lstQuoteEntity = (List<QuoteEntity>)Session["lstQuoteEntity"];
            if (lstQuoteEntity == null)
            {
                QuoteEntity qtEntity = new QuoteEntity();
                qtEntity.CarHireDetails = txtCarHireDetails.Text;
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
                qtEntity.IsMailSent = "N";
                qtEntity.QuoteNumber = "";
                qtEntity.Operation = "I";

                if (QuoteType == "3")
                {
                    qtEntity.PackageId = txtProduct.Text;
                }
                else
                {
                    qtEntity.PackageId = ddlPackage.SelectedValue;
                }
                string QuoteNumber = qtBL.CUOperationQuote(qtEntity);
                ViewState["QuoteNumber"] = QuoteNumber;
                if (QuoteNumber != "")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "EmailModal();", true);
                    txtToEmailNew.Text = clEmail;
                    txtCLientNameNew.Text = lblClientName.Text;
                    txtEmailSubjectNew.Text = "Serendipity Tours quote";

                    // Email Template                    
                    StringBuilder sb = new StringBuilder();
                    string strHeading = string.Format("<p><strong>Dear {0},</strong></p>", lblClientName.Text);
                    sb.Append(strHeading);
                    sb.Append("<p>Thank you for the opportunity to quote for your holiday to" + ddlPackage.SelectedItem.Text + ". Please find attached the options as discussed. Should you require any changes or amendments, please do not hesitate to contact me. I will be contacting you shortly to discuss the quote.</p>");
                    sb.Append("<p><strong>Kind regards</strong></p>");
                    sb.Append("<p><strong>" + Session["Name"].ToString() + "</strong></p>");
                    txtMailTempNew.Text = sb.ToString();

                    Clear();
                }
                else
                {
                    message.Text = "Please try again!";
                    message.ForeColor = System.Drawing.Color.Red;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            else
            {
                QuoteEntity qtEntity = new QuoteEntity();
                qtEntity.CarHireDetails = txtCarHireDetails.Text;
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
                qtEntity.IsMailSent = "N";
                qtEntity.QuoteNumber = "";
                qtEntity.Operation = "I";

                if (QuoteType == "3")
                {
                    qtEntity.PackageId = txtProduct.Text;
                }
                else
                {
                    qtEntity.PackageId = ddlPackage.SelectedValue;
                }
                string QuoteNumber = qtBL.CUOperationQuote(qtEntity);
                qtEntity.QuoteNumber = QuoteNumber;
                lstQuoteEntity.Add(qtEntity);
                if (QuoteNumber != "")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "EmailModal();", true);
                    txtToEmailNew.Text = clEmail;
                    txtCLientNameNew.Text = lblClientName.Text;
                    txtEmailSubjectNew.Text = "Serendipity Tours quote";

                    // Email Template                    
                    StringBuilder sb = new StringBuilder();
                    string strHeading = string.Format("<p><strong>Dear {0},</strong></p>", lblClientName.Text);
                    sb.Append(strHeading);
                    sb.Append("<p>Thank you for the opportunity to quote for your holiday to" + ddlPackage.SelectedItem.Text + ". Please find attached the options as discussed. Should you require any changes or amendments, please do not hesitate to contact me. I will be contacting you shortly to discuss the quote.</p>");
                    sb.Append("<p><strong>Kind regards</strong></p>");
                    sb.Append("<p><strong>" + Session["Name"].ToString() + "</strong></p>");
                    txtMailTempNew.Text = sb.ToString();

                    Clear();
                }
                else
                {
                    message.Text = "Please try again!";
                    message.ForeColor = System.Drawing.Color.Red;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void backToLead_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Lead.aspx?t=quote&idq=" + LeadID);
    }
    protected void imgbtnAddMultipleOptions_Click(object sender, ImageClickEventArgs e)
    {
        QuoteEntity qtEntity = new QuoteEntity();
        try
        {
            qtEntity.CarHireDetails = txtCarHireDetails.Text;
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
            qtEntity.IsMailSent = "N";
            qtEntity.QuoteNumber = "";
            qtEntity.Operation = "I";

            if (QuoteType == "3")
            {
                qtEntity.PackageId = txtProduct.Text;
            }
            else
            {
                qtEntity.PackageId = ddlPackage.SelectedValue;
            }
            string QuoteNumber = qtBL.CUOperationQuote(qtEntity);
            qtEntity.QuoteNumber = QuoteNumber;
            if (Session["lstQuoteEntity"] == null)
            {
                lstQuoteEntity.Add(qtEntity);
                Session["lstQuoteEntity"] = lstQuoteEntity;
            }
            else
            {
                lstQuoteEntity = (List<QuoteEntity>)Session["lstQuoteEntity"];
                lstQuoteEntity.Add(qtEntity);
                Session["lstQuoteEntity"] = lstQuoteEntity;
            }
            Clear();
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnSendMailNew_Click(object sender, EventArgs e)
    {
        lstQuoteEntity = (List<QuoteEntity>)Session["lstQuoteEntity"];
        if (lstQuoteEntity == null)
        {
            GetPdf(ViewState["QuoteNumber"].ToString());
            CommanClass.MailStatusLog(LeadID, "QT001", "Success", "", ViewState["QuoteNumber"].ToString());
        }
        else
        {
            GetPdfMultipleOptions(lstQuoteEntity);
            CommanClass.MailStatusLog(LeadID, "QT001", "Success", "", QuoteBuilder.ToString());
        }
    }
    protected void imgbtnBackQuote_Click(object sender, ImageClickEventArgs e)
    {
        imgbtnBackQuote.Visible = false;
        backToLead.Visible = true;
        quotesection.Style.Add("display", "unset");
        emailsection.Style.Add("display", "none");
    }
}