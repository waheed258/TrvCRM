<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="AssignedLeads.aspx.cs" Inherits="AssignedLeads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.19/css/dataTables.jqueryui.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel">
        <div class="panel-heading">
            <h3 class="panel-title" style="font-weight: bold; color: #00AAFF">Assigned Leads</h3>
        </div>
        <div class="panel-body">
            <div style="text-align: center">
                <asp:Label ID="lblMessage" runat="server" Style="font-size: 20px;"></asp:Label>
            </div>
            <div class="tables">
                <div class="table table-responsive">
                    <div class="row text-center">
                        <div class="col-lg-4"></div>
                        <div class="col-lg-4">
                            <asp:DropDownList ID="ddlConsultantsFilter" runat="server" Style="padding: 0px" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlConsultantsFilter_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <p></p>
                    <asp:GridView ID="gvAssignedList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                        EmptyDataText="There are no data records to display. Please Add Lead." OnRowDataBound="gvAssignedList_RowDataBound" OnRowCommand="gvAssignedList_RowCommand"
                         ForeColor="Black">
                        <Columns>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quote" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblQuote" Text='<%#Eval("Quote") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QuoteNumber" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblQuoteNumber" Text='<%#Eval("QuoteNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblID" Text='<%#Eval("lsId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDuplicateLead" Text='<%#Eval("lsDuplicateLead") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Source">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbllsSourceRef" Text='<%#Eval("lsSourceRef") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created By" Visible="false">
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

                            <asp:TemplateField HeaderText="Product" Visible="false">
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
                            <asp:TemplateField HeaderText="Return Date" Visible="false">
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

                            <asp:TemplateField HeaderText="Assigned By" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblAssignedby" Text='<%#Eval("AssignedBy").ToString() == "" ? "Not assigned by anyone" : Eval("AssignedBy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStatus" Text='<%#Eval("LeadStatusAction") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created On">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbllsCreatedOn" Text='<%#Eval("lsCreatedOn", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ProductID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblProductID" Text='<%#Eval("ProductID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDescription" Text='<%#Eval("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="110px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" Width="23px" Height="23px" ImageUrl="~/assets/img/edit-user.png"
                                        CommandName="EditLead" ToolTip="Edit" />
                                    <asp:ImageButton ID="btnDelete" runat="server" Width="23px" Height="23px" ImageUrl="~/assets/img/garbage.png"
                                        CommandName="DeleteLead" ToolTip="Delete" Visible="false" />
                                    <asp:ImageButton ID="imgbtnStaus" runat="server" Width="23px" Height="23px" ImageUrl="~/assets/img/Status1.png"
                                        CommandName="Action" ToolTip="Actions" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
      <div class="modal fade" id="delete" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content" style="height: 150px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h2 class="modal-title">Delete Lead!</h2>
                </div>
                <div class="modal-body">
                    <div class="col-md-12 form-group user-form-group">
                        <asp:Label ID="lbldeletemessage" runat="server" class="control-label" />
                        <div class="pull-right">
                            <asp:Button ID="btnSure" runat="server" Text="YES" CssClass="btn btn-add btn-sm btn-primary" OnClick="btnSure_Click" Style="margin-top: 0em;"></asp:Button>
                        </div>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <script type="text/javascript">
        function openDeleteModal() {
            $('#delete').modal('show');
        }
    </script>
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/dataTables.jqueryui.min.js"></script>
     
    <script>
        $(document).ready(function () {
            $('#ContentPlaceHolder1_gvAssignedList').DataTable();
        });
    </script>
</asp:Content>

