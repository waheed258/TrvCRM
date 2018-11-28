<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="CustomersList.aspx.cs" Inherits="CustomersList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.19/css/dataTables.jqueryui.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="customerslist" runat="server">
        <div class="panel">
            <div class="panel-heading">
                <h3 class="panel-title" style="font-weight: bold; color: #00AAFF">Customers List</h3>
            </div>
            <div class="panel-body">
                <div style="text-align: center">
                    <asp:Label ID="lblMessage" runat="server" Style="font-size: 20px;"></asp:Label>
                </div>
                <div class="tables">
                    <div class="table table-responsive">
                        <asp:GridView ID="gvCustomerList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                            EmptyDataText="There are no data records to display."
                             OnRowCommand="gvCustomerList_RowCommand" 
                             ForeColor="Black">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTravellerId" Text='<%#Eval("TravellerId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTravellerTitel" Text='<%#Eval("TravellerTitel") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="First Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTravellerFirstName" Text='<%#Eval("TravellerFirstName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTravellerLastName" Text='<%#Eval("TravellerLastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTravellerName" Text='<%#Eval("TravellerFirstName")+ " " + Eval("TravellerLastName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email ID">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTravellerMailId" Text='<%#Eval("TravellerMailId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TravellerPhone" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTravellerPhone" Text='<%#Eval("TravellerPhone") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTravellerMobile" Text='<%#Eval("TravellerMobile") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TravellerAddress" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTravellerAddress" Text='<%#Eval("TravellerAddress") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Passport No">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblTravellerPassPortNo" Text='<%#Eval("TravellerPassPortNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Passport Issue Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPassportIssueDate" Text='<%#Eval("PassportIssueDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Passport Expiry Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPassportExpiryDate" Text='<%#Eval("PassportExpiryDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CreatedBy" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCreatedBy" Text='<%#Eval("CreatedBy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UpdatedBy" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblUpdatedBy" Text='<%#Eval("UpdatedBy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CompanyId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCompanyId" Text='<%#Eval("CompanyId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" Width="23px" Height="23px" ImageUrl="~/assets/img/edit-user.png"
                                            CommandName="EditCustomer" ToolTip="Edit" />
                                        <asp:ImageButton ID="btnDelete" runat="server" Width="23px" Height="23px" ImageUrl="~/assets/img/garbage.png"
                                            CommandName="DeleteCustomer" ToolTip="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="editCustomer" runat="server">
        <div class="panel">
            <div class="panel-heading">
                <h3 class="panel-title" style="font-weight: bold; color: #00AAFF">Update Customer</h3>
            </div>
            <div class="panel-body">
                <div style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Style="font-size: 20px;"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-1">
                        <label class="control-label">Title</label>
                        <asp:DropDownList ID="ddlTitle" runat="server" class="form-control" Style="padding: 0px">
                            <asp:ListItem Value="-1">Title</asp:ListItem>
                            <asp:ListItem Value="0">Mr</asp:ListItem>
                            <asp:ListItem Value="1">Miss</asp:ListItem>
                            <asp:ListItem Value="2">Mrs</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="ddlTitle" ForeColor="#d0582e"
                            ErrorMessage="Please select Title" ValidationGroup="Consultant" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-2" id="others" runat="server">
                        <label class="control-label">First Name</label>
                        <asp:TextBox ID="txtFirstName" runat="server" class="form-control" placeholder="Given Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName" ForeColor="#d0582e"
                            ErrorMessage="Please Enter First Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Last Name</label>
                        <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Surname"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Last Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Email" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="#d0582e" Display="Dynamic" ErrorMessage="Please check Email Format"
                            ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Consultant">
                        </asp:RegularExpressionValidator>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Mobile</label>
                        <asp:TextBox ID="txtMobile" runat="server" class="form-control" placeholder="Mobile" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobile" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Mobile" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                            ControlToValidate="txtMobile" ForeColor="#d0582e" ValidationGroup="Consultant"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label class="control-label">Phone</label>
                        <asp:TextBox ID="txtPhone" runat="server" class="form-control" placeholder="Phone" MaxLength="10"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="rgvPhone" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                            ControlToValidate="txtPhone" ForeColor="#d0582e" ValidationGroup="Consultant"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Passport No</label>
                        <asp:TextBox ID="txtPassportNo" class="form-control" runat="server" placeholder="Passport" MaxLength="9"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassport" runat="server" ControlToValidate="txtPassportNo" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Passport" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Passport Issue Date</label>
                        <asp:TextBox ID="txtIssueDate" class="form-control" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvIssueDate" runat="server" ControlToValidate="txtIssueDate" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Issue Date" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <label class="control-label">Passport Expiry Date</label>
                        <asp:TextBox ID="txtExpiry" class="form-control" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvExpiry" runat="server" ControlToValidate="txtExpiry" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Expiry Date" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <label class="control-label">Address</label>
                        <asp:TextBox ID="txtAddress" class="form-control" TextMode="MultiLine" runat="server" placeholder="Address"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Address" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <p></p>
                <div class="row" style="margin-left: 1px;">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" ValidationGroup="Consultant" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="Back" CssClass="btn btn-primary"  OnClick="btnReset_Click" />
                </div>
            </div>
        </div>

    </div>
    <div class="modal fade" id="delete" tabindex="-1" role="dialog" data-keyboard="false" data-backdrop="static" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content" style="height: 150px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h2 class="modal-title">Delete Customer!</h2>
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
            $('#ContentPlaceHolder1_gvCustomerList').DataTable();
        });
    </script>
</asp:Content>

