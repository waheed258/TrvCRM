<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--custom-widgets-->
    <div class="custom-widgets" style="margin-top: 25px;">
        <div class="row-one">
            <div class="col-md-3 widget">
                <div class="stats-left ">
                    <h5>Today</h5>
                    <h5>Leads</h5>
                </div>
                <div class="stats-right">
                    <h5>90</h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-md-3 widget states-mdl">
                <div class="stats-left">
                    <h5>Today</h5>
                    <h5>Visitors</h5>
                </div>
                <div class="stats-right">
                   <h5>85</h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-md-3 widget states-thrd">
                <div class="stats-left">
                    <h5>Today</h5>
                    <h5>Open Leads</h5>
                </div>
                <div class="stats-right">
                    <h5>51</h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-md-3 widget states-last">
                <div class="stats-left">
                    <h5>Today</h5>
                    <h5>Alerts</h5>
                </div>
                <div class="stats-right">
                    <h5>30</h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
    <!--//custom-widgets-->
</asp:Content>

