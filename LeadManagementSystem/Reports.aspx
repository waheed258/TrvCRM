<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script>
        $(function () {

            $('#ContentPlaceHolder1_txtFrom').datepicker({
                startDate: 'today',
                numberOfMonths: 1,
                autoclose: true,
                dateFormat: 'dd-mm-yy',
                onSelect: function (selected) {
                    $("#ContentPlaceHolder1_txtTo").prop('disabled', false);
                    $("#ContentPlaceHolder1_txtTo").val('');
                    var date = $(this).datepicker('getDate');
                    if (date) {
                        date.setDate(date.getDate());
                    }
                    $("#ContentPlaceHolder1_txtTo").datepicker("option", "minDate", date)
                }
            });
            $("#ContentPlaceHolder1_txtTo").datepicker({
                startDate: 'today',
                numberOfMonths: 1,
                dateFormat: 'dd-mm-yy',
                autoclose: true
            });

            $('#ContentPlaceHolder1_ddlSearch').change(function () {
                var val = $(this).val();

                $('#dvControls').find('div').removeClass("show").addClass("hide");
                $('#dvBtn').removeClass("hide").addClass("show");
                $('#dvDuration,#dvFrom,#dvTo').removeClass("hide").addClass("show");

                if (val != '0') {
                    val == "1" ? $('#dvProduct').removeClass("hide").addClass("show") : $('#dvProduct').removeClass("show").addClass("hide");
                    val == "2" ? $('#dvSource').removeClass("hide").addClass("show") : $('#dvSource').removeClass("show").addClass("hide");
                    val == "3" ? $('#dvConsultant').removeClass("hide").addClass("show") : $('#dvConsultant').removeClass("show").addClass("hide");
                    //val == "4" ? $('#dvWeek').removeClass("hide").addClass("show") : $('#dvWeek').removeClass("show").addClass("hide");
                    //val == "5" ? $('#dvMonth').removeClass("hide").addClass("show") : $('#dvMonth').removeClass("show").addClass("hide");
                    //val == "6" ? $('#dvDuration,#dvFrom,#dvTo').removeClass("hide").addClass("show") : $('#dvDuration,#dvFrom,#dvTo').removeClass("show").addClass("hide");
                }


            });

            var value = $('#ContentPlaceHolder1_ddlSearch').val();

            if (value != '0') {
                value == "1" ? $('#dvProduct').removeClass("hide").addClass("show") : $('#dvProduct').removeClass("show").addClass("hide");
                value == "2" ? $('#dvSource').removeClass("hide").addClass("show") : $('#dvSource').removeClass("show").addClass("hide");
                value == "3" ? $('#dvConsultant').removeClass("hide").addClass("show") : $('#dvConsultant').removeClass("show").addClass("hide");
                //value == "3" ? $('#dvDays').removeClass("hide").addClass("show") : $('#dvDays').removeClass("show").addClass("hide");
                //value == "4" ? $('#dvWeek').removeClass("hide").addClass("show") : $('#dvWeek').removeClass("show").addClass("hide");
                //value == "5" ? $('#dvMonth').removeClass("hide").addClass("show") : $('#dvMonth').removeClass("show").addClass("hide");
                //value == "6" ? $('#dvDuration,#dvFrom,#dvTo').removeClass("hide").addClass("show") : $('#dvDuration,#dvFrom,#dvTo').removeClass("show").addClass("hide");
            }


        });

        function dateValidation() {
            var from = $('#ContentPlaceHolder1_txtFrom').val();
            var to = $('#ContentPlaceHolder1_txtTo').val();
            debugger;
            if (from != '' && to == '')
            {
                alert('Please Select To Date');
                return false;
            } else if (to != '' && from == '') {
                alert('Please Select From Date');
                return false;
            } else {
                return true;
            }
        }

    </script>
    <style>
        .hide {
            display: none;
        }

        .show {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="outter-wp">
        <asp:HiddenField ID="hdfSearchValue" runat="server" Value="" />
        <asp:HiddenField ID="hdfSearchBy" runat="server" Value="0" />
        <asp:HiddenField ID="hdfDates" runat="server" Value="" />
        <h2 class="inner-tittle">Leads Report</h2>
        <div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="col-lg-1 form-group">
                        <label class="control-label">Records</label>
                        <asp:DropDownList CssClass="form-control" ID="DropPage" runat="server" Style="padding: 0px" OnSelectedIndexChanged="DropPage_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2">
                        <label class="control-label">Search By</label>
                        <asp:DropDownList runat="server" ID="ddlSearch" CssClass="form-control" Style="padding: 0px;">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Product</asp:ListItem>
                            <asp:ListItem Value="2">Lead Source</asp:ListItem>
                            <asp:ListItem Value="3">Consultant</asp:ListItem>
                           <%-- <asp:ListItem Value="3">Day wise Report</asp:ListItem>
                            <asp:ListItem Value="4">Weekly Report</asp:ListItem>
                            <asp:ListItem Value="5">Monthly Report</asp:ListItem>--%>
                            <%--<asp:ListItem Value="6">Duration wise Report</asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-8" id="dvControls">
                        <div class="col-lg-3 hide" id="dvProduct">
                            <label class="control-label">Product</label>
                            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" Style="padding: 0px;"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3 hide" id="dvSource">
                            <label class="control-label">Source</label>
                            <asp:DropDownList ID="ddlSource" runat="server" CssClass="form-control" Style="padding: 0px;"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3 hide" id="dvConsultant">
                            <label class="control-label">Consultants</label>
                            <asp:DropDownList ID="ddlConsultants" runat="server" CssClass="form-control" Style="padding: 0px;"></asp:DropDownList>
                        </div>
                        <div class="col-lg-3 hide" id="dvDays">
                            <label class="control-label">No of Days</label>
                            <asp:DropDownList ID="ddlDayWise" runat="server" CssClass="form-control" Style="padding: 0px;">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3 hide" id="dvWeek">
                            <label class="control-label">No of Weeks</label>
                            <asp:DropDownList ID="ddlWeek" runat="server" CssClass="form-control" Style="padding: 0px;">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3 hide" id="dvMonth">
                            <label class="control-label">Month</label>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" Style="padding: 0px;">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Jan</asp:ListItem>
                                <asp:ListItem Value="2">Feb</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">Jun</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">Aug</asp:ListItem>
                                <asp:ListItem Value="9">Sep</asp:ListItem>
                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                <asp:ListItem Value="12">Dec</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-8" id="dvDuration">
                            <div class="col-lg-6" id="dvFrom">
                                <label class="control-label">From</label>
                                <asp:TextBox runat="server" ID="txtFrom" placeholder="DD-MM-YYYY" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-6" id="dvTo">
                                <label class="control-label">To</label>
                                <asp:TextBox runat="server" ID="txtTo" placeholder="DD-MM-YYYY" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-1" id="dvBtn">
                            <label></label>
                            <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="btn-default" Style="height: 37px; width: 50px; border: 1px solid #ddd;" OnClick="btnSearch_Click" OnClientClick="return dateValidation();"  />
                        </div>

                    </div>

                    <%-- <div class="col-lg-4">--%>
                    <%--<div class="input-group">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search text"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="btnSearch" runat="server" Text="Go" Style="height: 37px; width: 50px; color: black; background-color: white; border: 1px solid #ddd;" OnClick="btnSearch_Click" />
                            </span>
                        </div>--%>
                    <%-- </div>--%>

                    <div class="col-lg-1 form-group text-right">
                        <label class="control-label">Export</label>
                        <asp:ImageButton ID="imgbtnExcel" ImageUrl="images/icon-excel.png" runat="server" Height="35px"
                            ToolTip="Export To Excel" OnClick="imgbtnExcel_Click" />
                        <%-- <asp:ImageButton ID="imgpdf" ImageUrl="images/PDFIcon.png" runat="server" Height="35px"
                            ToolTip="Export To PDf" OnClick="imgpdf_Click" />--%>
                    </div>
                </div>
            </div>
            <div class="tables">
                <div class="table table-responsive">
                    <asp:GridView ID="gvLeadList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                        EmptyDataText="There are no data records to display. Please Add Lead." AllowPaging="true"
                        PageSize="100" OnRowCommand="gvLeadList_RowCommand" OnPageIndexChanging="gvLeadList_PageIndexChanging"
                        Style="font-size: 110%;" ForeColor="Black">
                        <PagerStyle CssClass="pagination_grid" />
                        <Columns>
                            <asp:BoundField DataField="QuoteNumber" HeaderText="Quote No" ReadOnly="true" />
                            <asp:BoundField DataField="lsSourceRef" HeaderText="Source" ReadOnly="true" />
                            <asp:BoundField DataField="ProductType" HeaderText="Product" ReadOnly="true" />
                            <asp:BoundField DataField="ConsultantName" HeaderText="Created By" ReadOnly="true" Visible="false" />
                            <asp:BoundField DataField="ClientName" HeaderText="Client Name" ReadOnly="true" />
                            <asp:BoundField DataField="lsEmailId" HeaderText="Email" ReadOnly="true" Visible="false" />
                            <asp:BoundField DataField="lsPhone" HeaderText="Phone" ReadOnly="true" Visible="false" />
                            <asp:BoundField DataField="lsOriginName" HeaderText="Origin" ReadOnly="true" />
                            <asp:BoundField DataField="lsDestinationName" HeaderText="Destination" ReadOnly="true" />
                            <asp:BoundField DataField="lsDepartureDate" HeaderText="DepartureDate" ReadOnly="true" Visible="false" />
                            <asp:BoundField DataField="lsReturnDate" HeaderText="ReturnDate" ReadOnly="true" Visible="false" />
                            <asp:BoundField DataField="lsQuotedPrice" HeaderText="Quote Price" ReadOnly="true" />
                            <asp:BoundField DataField="AssignedTo" HeaderText="Assigned To" ReadOnly="true" />
                            <asp:BoundField DataField="AssignedBy" HeaderText="Assigned By" ReadOnly="true" Visible="false" />
                            <asp:BoundField DataField="LeadStatusAction" HeaderText="Status" ReadOnly="true" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="tables" style="display:none;">
                <div class="table table-responsive">
                    <asp:GridView ID="gvExcel" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                        EmptyDataText="There are no data records to display. Please Add Lead." AllowPaging="true"
                        PageSize="100"
                        Style="font-size: 110%;" ForeColor="Black">
                        <PagerStyle CssClass="pagination_grid" />
                        <Columns>
                            <asp:BoundField DataField="QuoteNumber" HeaderText="Quote No" ReadOnly="true" />
                            <asp:BoundField DataField="lsSourceRef" HeaderText="Source" ReadOnly="true" />
                            <asp:BoundField DataField="ProductType" HeaderText="Product" ReadOnly="true" />
                            <asp:BoundField DataField="ConsultantName" HeaderText="Created By" ReadOnly="true" />
                            <asp:BoundField DataField="ClientName" HeaderText="Client Name" ReadOnly="true" />
                            <asp:BoundField DataField="lsEmailId" HeaderText="Email" ReadOnly="true"/>
                            <asp:BoundField DataField="lsPhone" HeaderText="Phone" ReadOnly="true"/>
                            <asp:BoundField DataField="lsOriginName" HeaderText="Origin" ReadOnly="true" />
                            <asp:BoundField DataField="lsDestinationName" HeaderText="Destination" ReadOnly="true" />
                            <asp:BoundField DataField="lsDepartureDate" HeaderText="DepartureDate" ReadOnly="true" />
                            <asp:BoundField DataField="lsReturnDate" HeaderText="ReturnDate" ReadOnly="true" />
                            <asp:BoundField DataField="lsQuotedPrice" HeaderText="Quote Price" ReadOnly="true" />
                            <asp:BoundField DataField="AssignedTo" HeaderText="Assigned To" ReadOnly="true" />
                            <asp:BoundField DataField="AssignedBy" HeaderText="Assigned By" ReadOnly="true"/>
                            <asp:BoundField DataField="LeadStatusAction" HeaderText="Status" ReadOnly="true" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h2 class="modal-title">Alert!</h2>
                </div>
                <div class="modal-body">
                    <asp:Label ID="message" runat="server"></asp:Label>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
</asp:Content>

