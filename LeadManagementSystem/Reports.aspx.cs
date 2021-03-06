﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class Reports : System.Web.UI.Page
{
    DataSet dataset = new DataSet();
    LeadBL leadBL = new LeadBL();
    LeadEntity leadEntity = new LeadEntity();
    CommanClass _objComman = new CommanClass();
    ConsultantBL consultantBL = new ConsultantBL();
    FollowupEntity followupEntity = new FollowupEntity();
    EncryptDecrypt encryptdecrypt = new EncryptDecrypt();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                _objComman.getRecordsPerPage(DropPage);
                dataset = leadBL.GetLeadsReport(hdfSearchBy.Value, hdfSearchValue.Value, hdfDates.Value);
                bindGrid(dataset);
                GetProducts();
                GetSourceData("A");
                GetConsultants();
            }
        }
        catch { }
    }
    protected void GetProducts()
    {
        try
        {
            dataset = leadBL.GetProduct();
            ddlProduct.DataSource = dataset;
            ddlProduct.DataTextField = "ProductType";
            ddlProduct.DataValueField = "ProductTypeID";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void GetSourceData(string Opeartion)
    {
        try
        {
            dataset = leadBL.GetSourceData(Opeartion);
            ddlSource.DataSource = dataset;
            ddlSource.DataTextField = "SourceType";
            ddlSource.DataValueField = "SourceTypeID";
            ddlSource.DataBind();
            ddlSource.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void GetConsultants()
    {
        try
        {
            dataset = consultantBL.GetConsultants(0);
            ddlConsultants.DataSource = dataset;
            ddlConsultants.DataTextField = "Name";
            ddlConsultants.DataValueField = "ConsultantID";
            ddlConsultants.DataBind();
            ddlConsultants.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All", "0"));
        }
        catch
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void GetLeadsList()
    {
        try
        {
            gvLeadList.PageSize = Convert.ToInt32(DropPage.SelectedValue);
            dataset = leadBL.GetLeadsList(0);

            gvLeadList.DataSource = dataset;
            gvLeadList.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        dataset = leadBL.GetLeadsReport(hdfSearchBy.Value, hdfSearchValue.Value, hdfDates.Value);
        bindGrid(dataset);
    }
    protected void gvLeadList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLeadList.PageIndex = e.NewPageIndex;
        dataset = leadBL.GetLeadsReport(hdfSearchBy.Value, hdfSearchValue.Value, hdfDates.Value);
        bindGrid(dataset);
    }
    protected void gvLeadList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void imgbtnExcel_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            gvLeadList.Visible = true;
            ExportGridToExcel();
        }
        catch { }
    }
    protected void imgpdf_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            PdfPTable pdfptable = new PdfPTable(gvLeadList.HeaderRow.Cells.Count);

            foreach (TableCell headerCell in gvLeadList.HeaderRow.Cells)
            {

                Font font = new Font();
                font.Color = GrayColor.BLUE;
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                pdfptable.AddCell(pdfCell);

            }
            foreach (GridViewRow gridviewrow in gvLeadList.Rows)
            {
                foreach (TableCell tableCell in gridviewrow.Cells)
                {

                    tableCell.BackColor = gvLeadList.HeaderStyle.BackColor;
                    PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text.Trim()));
                    pdfptable.AddCell(pdfCell);

                }

            }
            Document pdfDocument = new Document(PageSize.A4.Rotate(), 0, 0, 10, 0);
            PdfWriter.GetInstance(pdfDocument, Response.OutputStream);
            pdfDocument.Open();
            pdfDocument.Add(pdfptable);
            pdfDocument.Close();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("content-disposition", "attachment;filename=ServiceWiseReport.pdf");
            Response.Write(pdfDocument);
            Response.Flush();
            Response.End();
        }
        catch { }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    private void ExportGridToExcel()
    {
        gvExcel.AllowPaging = false;
        dataset = leadBL.GetLeadsReport(hdfSearchBy.Value, hdfSearchValue.Value, hdfDates.Value);
        bindExcel(dataset);

        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Lead_Report - " + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        gvExcel.GridLines = GridLines.Both;
        gvExcel.HeaderStyle.Font.Bold = true;
        gvExcel.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            gvLeadList.PageSize = Convert.ToInt32(DropPage.SelectedValue);

            string strSearchBy = ddlSearch.SelectedValue;
            hdfSearchBy.Value = strSearchBy;

            if (strSearchBy == "0")
            {
                dataset = leadBL.GetLeadsReport(strSearchBy, "", txtFrom.Text + "," + txtTo.Text);
                hdfSearchValue.Value = "";
            }
            else if (strSearchBy == "1")
            {
                dataset = leadBL.GetLeadsReport(strSearchBy, ddlProduct.SelectedValue, txtFrom.Text + "," + txtTo.Text);
                hdfSearchValue.Value = ddlProduct.SelectedValue;
            }
            else if (strSearchBy == "2")
            {
                dataset = leadBL.GetLeadsReport(strSearchBy, ddlSource.SelectedValue, txtFrom.Text + "," + txtTo.Text);
                hdfSearchValue.Value = ddlSource.SelectedValue;
            }
            else if (strSearchBy == "3")
            {
                dataset = leadBL.GetLeadsReport(strSearchBy, ddlConsultants.SelectedValue, txtFrom.Text + "," + txtTo.Text);
                hdfSearchValue.Value = ddlConsultants.SelectedValue;
            }
            hdfDates.Value = txtFrom.Text + "," + txtTo.Text;

            bindGrid(dataset);

        }
        catch (Exception ex)
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void bindGrid(DataSet ds)
    {
        try
        {
            gvLeadList.PageSize = Convert.ToInt32(DropPage.SelectedValue);
            DataSet dsData = ds;
            gvLeadList.DataSource = dsData;
            gvLeadList.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void bindExcel(DataSet ds)
    {
        try
        {
            DataSet dsData = ds;
            gvExcel.DataSource = dsData;
            gvExcel.DataBind();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Something went wrong. Please contact administrator!";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}