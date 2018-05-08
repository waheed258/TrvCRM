<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="outter-wp">
        <div class="graph" id="LeadList" runat="server">
            <div class="row" id="search" runat="server">
                <div class="col-lg-12">
                    <div class="col-lg-1 form-group">
                        <asp:DropDownList CssClass="form-control" ID="DropPage" runat="server" Style="padding: 0px" OnSelectedIndexChanged="DropPage_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-2 form-group">
                        <label class="control-label">
                            Records per page</label>
                    </div>
                    <div class="col-lg-6 form-group text-right">
                        <asp:ImageButton ID="imgbtnExcel" ImageUrl="images/icon-excel.png" runat="server" Height="35px"
                            ToolTip="Export To Excel" OnClick="imgbtnExcel_Click" />
                        <asp:ImageButton ID="imgpdf" ImageUrl="images/PDFIcon.png" runat="server" Height="35px"
                            ToolTip="Export To PDf" OnClick="imgpdf_Click" />
                    </div>
                    <div class="col-lg-3 form-group">
                        <input id="target" type="text" class="form-control" placeholder="Text To Search" />
                    </div>

                </div>
            </div>
            <div class="tables">
                <div class="table table-responsive">
                    <asp:GridView ID="gvLeadList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                        EmptyDataText="There are no data records to display. Please Add Lead." AllowPaging="true"
                        PageSize="100" OnRowCommand="gvLeadList_RowCommand" OnPageIndexChanging="gvLeadList_PageIndexChanging"
                        Style="font-size: 110%;" ForeColor="Black">
                        <PagerStyle CssClass="pagination_grid" />
                        <Columns>
                            <asp:BoundField DataField="Quote" HeaderText="Quote No" ReadOnly="true" />
                            <asp:BoundField DataField="lsSourceRef" HeaderText="Source" ReadOnly="true" />
                            <asp:BoundField DataField="ConsultantName" HeaderText="Created By" ReadOnly="true" />
                            <asp:BoundField DataField="ClientName" HeaderText="Client Name" ReadOnly="true" />
                            <%--<asp:BoundField DataField="CREATEDDATE" HeaderText="Date & Time" ReadOnly="true" />
                            <asp:BoundField DataField="CREATEDBY" HeaderText="Lead Created By" ReadOnly="true" />
                            <asp:BoundField DataField="ASSIGNEDTO" HeaderText="Lead Allocated To(TC)" ReadOnly="true" />
                            <asp:BoundField DataField="STATUS" HeaderText="Lead Status" ReadOnly="true" />
                            <asp:BoundField DataField="SERVICES" HeaderText="Services" ReadOnly="true" />
                            <asp:BoundField DataField="CLASS" HeaderText="Flight Class" ReadOnly="true" />--%>                          
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

