<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--custom-widgets-->
    <h3>Total Leads per Day / Week / Month</h3>
    <div class="custom-widgets" style="margin-top: 25px;">
        <div class="row-one">
            <div class="col-md-3 widget">
                <div class="stats-left ">
                    <h5>Today</h5>
                    <h5>Leads</h5>
                </div>
                <div class="stats-right">
                    <h5 id="today" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-md-3 widget states-mdl">
                <div class="stats-left">
                    <h5>This</h5>
                    <h5>Week</h5>
                </div>
                <div class="stats-right">
                    <h5 id="thisweek" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-md-3 widget states-thrd">
                <div class="stats-left">
                    <h5>This</h5>
                    <h5>Month</h5>
                </div>
                <div class="stats-right">
                    <h5 id="thismonth" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
            <hr />
        </div>
    </div>
    <h3>Leads By Consultant per Day / Week / Month</h3>
    <div class="col-md-3">
        <label class="control-label">Consultant</label>
        <asp:DropDownList ID="ddlConsultantsAction" runat="server" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlConsultantsAction_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
    </div>
    <div class="clearfix"></div>
    <div class="custom-widgets" style="margin-top: 25px;">
        <div class="row-one">
            <div class="col-md-3 widget">
                <div class="stats-left ">
                    <h5>Today</h5>
                    <h5>Leads</h5>
                </div>
                <div class="stats-right">
                    <h5 id="todayByConsultant" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-md-3 widget states-mdl">
                <div class="stats-left">
                    <h5>This</h5>
                    <h5>Week</h5>
                </div>
                <div class="stats-right">
                    <h5 id="thisweekByConsultant" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-md-3 widget states-thrd">
                <div class="stats-left">
                    <h5>This</h5>
                    <h5>Month</h5>
                </div>
                <div class="stats-right">
                    <h5 id="thismonthByConsultant" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
            <hr />
        </div>
    </div>
    <h3>Leads converted from Quote to Booking  per Day / Week / Month</h3>
    <div class="clearfix"></div>
    <div class="custom-widgets" style="margin-top: 25px;">
        <div class="row-one">
            <div class="col-md-3 widget">
                <div class="stats-left ">
                    <h5>Today</h5>
                    <h5>Leads</h5>
                </div>
                <div class="stats-right">
                    <h5 id="todayQB" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-md-3 widget states-mdl">
                <div class="stats-left">
                    <h5>This</h5>
                    <h5>Week</h5>
                </div>
                <div class="stats-right">
                    <h5 id="thisweekQB" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-md-3 widget states-thrd">
                <div class="stats-left">
                    <h5>This</h5>
                    <h5>Month</h5>
                </div>
                <div class="stats-right">
                    <h5 id="thismonthQB" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
            <hr />
        </div>
    </div>
    <h3>Leads by Source per Day / Week / Month</h3>
    <div class="col-md-3">
        <label class="control-label">Lead Source</label>
        <asp:DropDownList ID="ddlSource" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlSource_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <div class="clearfix"></div>
    <div class="custom-widgets" style="margin-top: 25px;">
        <div class="row-one">
            <div class="col-md-3 widget">
                <div class="stats-left ">
                    <h5>Today</h5>
                    <h5>Leads</h5>
                </div>
                <div class="stats-right">
                    <h5 id="todaySource" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-md-3 widget states-mdl">
                <div class="stats-left">
                    <h5>This</h5>
                    <h5>Week</h5>
                </div>
                <div class="stats-right">
                    <h5 id="thisweekSource" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="col-md-3 widget states-thrd">
                <div class="stats-left">
                    <h5>This</h5>
                    <h5>Month</h5>
                </div>
                <div class="stats-right">
                    <h5 id="thismonthSource" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
            <hr />
        </div>
    </div>
    <h3>Open Leads (Leads not yet Quoted)</h3>
    <div class="clearfix"></div>
    <div class="custom-widgets" style="margin-top: 25px;">
        <div class="row-one">
            <div class="col-md-3 widget">
                <div class="stats-left ">
                    <h5>Today</h5>
                    <h5>Leads</h5>
                </div>
                <div class="stats-right">
                    <h5 id="todayOpen" runat="server"></h5>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="clearfix"></div>
            <hr />
        </div>
    </div>
    <!--//custom-widgets-->
</asp:Content>

