<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="Reminders.aspx.cs" Inherits="Reminders" %>

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
                    <asp:GridView ID="gvReminders" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                        EmptyDataText="There are no data records to display."
                         ForeColor="Black">
                        <Columns>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblClientName" Text='<%#Eval("ClientName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbllsPhone" Text='<%#Eval("lsPhone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reminder Date">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblReminderon" Text='<%#Eval("Reminderon", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Assigned To">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblAssignedTo" Text='<%#Eval("AssignedTo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lead Status">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLeadStatusAction" Text='<%#Eval("LeadStatusAction") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/dataTables.jqueryui.min.js"></script>

    <script>
        $(document).ready(function () {
            $('#ContentPlaceHolder1_gvReminders').DataTable();
        });
    </script>
</asp:Content>

