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
                    <div class="col-lg-6 form-group">
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
                            <asp:TemplateField HeaderText="Quote" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblQuote" Text='<%#Eval("Quote") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblID" Text='<%#Eval("lsId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Source">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbllsSourceRef" Text='<%#Eval("lsSourceRef") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created By">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbllsConsultantName" Text='<%#Eval("ConsultantName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SourceID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbllsSource" Text='<%#Eval("lsSource") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Others" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbllsOthersInfo" Text='<%#Eval("lsOthersInfo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblName" Text='<%#Eval("lsFirstName") + " " +Eval("lsLastName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="First Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("lsFirstName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLastName" Text='<%#Eval("lsLastName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblMobile" Text='<%#Eval("lsPhone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblEmailID" Text='<%#Eval("lsEmailId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lsLeadActionsID" Text='<%#Eval("LeadActionsID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Product">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblProdType" Text='<%#Eval("ProductType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblProdID" Text='<%#Eval("lsProdType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Source">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblOrigin" Text='<%#Eval("lsOriginName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Destination">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDestination" Text='<%#Eval("lsDestinationName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Depart Date">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDepartDate" Text='<%#Eval("lsDepartureDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Return Date">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblReturnDate" Text='<%#Eval("lsReturnDate", "{0:dd-MM-yyyy}").ToString() == ""? "NA": Eval("lsReturnDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NoofAdults" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblAdult" Text='<%#Eval("lsAdults") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NoofChild" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblChildren" Text='<%#Eval("lsChildren") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NoofInfants" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblInfants" Text='<%#Eval("lsInfants") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Budget" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblBudget" Text='<%#Eval("lsBudget") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Notes" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblNotes" Text='<%#Eval("lsNotes") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quoted Price" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblQuotedPrice" Text='<%#Eval("lsQuotedPrice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Price" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFinalPrice" Text='<%#Eval("lsFinalPrice") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CreatedBy" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCreatedBy" Text='<%#Eval("lsCreatedBy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned To">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblAssignedto" Text='<%#Eval("AssignedTo").ToString() == "" ? "Not assigned to anyone" : Eval("AssignedTo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Assigned By">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblAssignedby" Text='<%#Eval("AssignedBy").ToString() == "" ? "Not assigned by anyone" : Eval("AssignedBy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStatus" Text='<%#Eval("LeadStatusAction") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDescription" Text='<%#Eval("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--  <asp:TemplateField HeaderText="Action" HeaderStyle-Width="110px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" Width="23px" Height="23px" ImageUrl="~/images/edit-user.png"
                                        CommandName="EditLead" ToolTip="Edit" />
                                    <asp:ImageButton ID="btnDelete" runat="server" Width="23px" Height="23px" ImageUrl="~/images/garbage.png"
                                        CommandName="DeleteLead" ToolTip="Delete" Visible="false" />
                                    <asp:ImageButton ID="imgbtnStaus" runat="server" Width="23px" Height="23px" ImageUrl="~/images/Status1.png"
                                        CommandName="Action" ToolTip="Actions" />
                                    <asp:ImageButton ID="imgbtnQuote" runat="server" Width="23px" Height="23px" ImageUrl="~/images/Quote.png"
                                        CommandName="Quote" ToolTip="Generate Quote" />
                                    <asp:ImageButton ID="imgbtnPDF" runat="server" Width="23px" Height="23px" ImageUrl="~/images/PDFIcon.png"
                                        CommandName="PDF" ToolTip="Download Quote" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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

