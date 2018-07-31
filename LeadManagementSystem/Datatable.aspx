<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="Datatable.aspx.cs" Inherits="Datatable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.19/css/dataTables.jqueryui.min.css" />

    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/dataTables.jqueryui.min.js"></script>
    <script>
        $(document).ready(function () {
            //$('#ContentPlaceHolder1_gvLeadList').DataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView ID="gvLeadList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
        EmptyDataText="There are no data records to display. Please Add Lead." AllowPaging="true"
        PageSize="100" OnRowCommand="gvLeadList_RowCommand" OnRowDataBound="gvLeadList_RowDataBound"
        Style="font-size: 110%;" ForeColor="Black">
        <Columns>
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
                    <asp:Label runat="server" ID="lblDuplicateLeadList" Text='<%#Eval("lsDuplicateLead") %>'></asp:Label>
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
            <asp:TemplateField HeaderText="Depart">
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
            <asp:TemplateField HeaderText="Description" Visible="false">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDescription" Text='<%#Eval("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action" HeaderStyle-Width="110px">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" Width="23px" Height="23px" ImageUrl="~/images/edit-user.png"
                        CommandName="EditLead" ToolTip="Edit"  />
                    <asp:ImageButton ID="imgbtnStaus" runat="server" Width="23px" Height="23px" ImageUrl="~/images/Status1.png"
                        CommandName="Action" ToolTip="Pickup/Assign" />
                    <asp:ImageButton ID="btnDelete" runat="server" Width="23px" Height="23px" ImageUrl="~/images/garbage.png"
                        CommandName="DeleteLead" ToolTip="Delete"  />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

