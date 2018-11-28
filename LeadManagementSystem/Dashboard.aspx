<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-headline">
        <div class="panel-heading">
            <h3 class="panel-title">Total Leads</h3>
            <h4>Per Day/Week/Month</h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="today" runat="server"></span>
                            <span class="title">Leads today</span>
                        </p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="thisweek" runat="server"></span>
                            <span class="title">Leads this week</span>
                        </p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="thismonth" runat="server"></span>
                            <span class="title">Leads this month</span>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="panel panel-headline">
        <div class="panel-heading">
            <h3 class="panel-title">Leads By Consultant</h3>
            <h4>Per Day/Week/Month</h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlConsultantsAction" runat="server" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlConsultantsAction_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
            <p></p>
            <div class="row">
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="todayByConsultant" runat="server"></span>
                            <span class="title">Leads today</span>
                        </p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="thisweekByConsultant" runat="server"></span>
                            <span class="title">Leads this week</span>
                        </p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="thismonthByConsultant" runat="server"></span>
                            <span class="title">Leads this month</span>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="panel panel-headline">
        <div class="panel-heading">
            <h3 class="panel-title">Leads converted from Quote to Booking</h3>
            <h4>Per Day/Week/Month</h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="todayQB" runat="server"></span>
                            <span class="title">Leads today</span>
                        </p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="thisweekQB" runat="server"></span>
                            <span class="title">Leads this week</span>
                        </p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="thismonthQB" runat="server"></span>
                            <span class="title">Leads this month</span>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="panel panel-headline">
        <div class="panel-heading">
            <h3 class="panel-title">Leads by Source</h3>
            <h4>Per Day/Week/Month</h4>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlSource" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlSource_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <p></p>
            <div class="row">
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="todaySource" runat="server"></span>
                            <span class="title">Leads today</span>
                        </p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="thisweekSource" runat="server"></span>
                            <span class="title">Leads this week</span>
                        </p>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="thismonthSource" runat="server"></span>
                            <span class="title">Leads this month</span>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="panel panel-headline">
        <div class="panel-heading">
            <h3 class="panel-title">Open Leads (Leads not yet Quoted)</h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-4">
                    <div class="metric">
                        <span class="icon"><i class="fa fa-file-image-o"></i></span>
                        <p>
                            <span class="number" id="todayOpen" runat="server"></span>
                            <span class="title">Leads today</span>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

