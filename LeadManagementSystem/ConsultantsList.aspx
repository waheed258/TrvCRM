<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="ConsultantsList.aspx.cs" Inherits="ConsultantsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.19/css/dataTables.jqueryui.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="consultantslist" runat="server">
        <div class="panel">
            <div class="panel-heading">
                <h3 class="panel-title" style="font-weight: bold; color: #00AAFF">Consultants List</h3>
            </div>
            <div class="panel-body">
                <div style="text-align: center">
                    <asp:Label ID="lblMessage" runat="server" Style="font-size: 20px;"></asp:Label>
                </div>
                <div class="tables">
                    <div class="table table-responsive">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                            EmptyDataText="There are no data records to display."
                            OnRowCommand="GridView1_RowCommand"
                            ForeColor="Black">
                            <Columns>
                                <asp:TemplateField HeaderText="Advisor ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblConsultantID" Text='<%#Eval("ConsultantID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblName" Text='<%#Eval("FirstName") + " " + Eval("LastName") %>'></asp:Label>
                                        <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("FirstName") %>' Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="lblLastName" Text='<%#Eval("LastName") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMobile" Text='<%#Eval("Mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email ID">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblEmailID" Text='<%#Eval("Email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDesignation" Text='<%#Eval("Designation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDesignationName" Text='<%#Eval("DesignationName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LoginID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblLoginID" Text='<%#Eval("LoginID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Password" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblPwd" Text='<%#Eval("Password") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBranch" Text='<%#Eval("Branch") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BranchName" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblBranchName" Text='<%#Eval("BranchName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAStatus" Text='<%#Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblAStatusName" Text='<%#Eval("StatusName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" Width="23px" Height="23px" ImageUrl="~/assets/img/edit-user.png"
                                            CommandName="EditConsultant" ToolTip="Edit" />
                                        <asp:ImageButton ID="btnDelete" runat="server" Width="23px" Height="23px" ImageUrl="~/assets/img/garbage.png"
                                            CommandName="DeleteConsultant" ToolTip="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="editConsultant" runat="server">
        <div class="panel">
            <div class="panel-heading">
                <h3 class="panel-title" style="font-weight: bold; color: #00AAFF">Update Consultant</h3>
            </div>
            <div class="panel-body">
                <div style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Style="font-size: 20px;"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label class="control-label" for="txtFirstName">First Name</label>
                        <asp:TextBox ID="txtFirstName" runat="server" class="form-control" placeholder="Given Name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFirstName" ForeColor="#d0582e"
                            ErrorMessage="Please Enter First Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4 form-last">
                        <label class="control-label" for="txtLastName">Last Name</label>
                        <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Surname"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Last Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4 form-last">
                        <label class="control-label" for="txtEmail">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Email" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="#d0582e" Display="Dynamic" ErrorMessage="Please check Email Format"
                            ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Consultant">
                        </asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-lg-4">
                        <label class="control-label">Login ID</label>
                        <asp:TextBox ID="txtLoginId" class="form-control" runat="server" placeholder="Enter LoginId" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLoginId" runat="server" ControlToValidate="txtLoginId" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Login ID" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group col-lg-4">
                        <label class="control-label" runat="server">Password</label>
                        <asp:TextBox ID="txtPassword" class="form-control" runat="server" placeholder="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Password" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-lg-4" id="dvRPwd" runat="server">
                        <label class="control-label">Repeated password</label>
                        <asp:TextBox ID="txtConfirmPassword" class="form-control" TextMode="Password" runat="server" placeholder="Confirm Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Password" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                            ControlToCompare="txtPassword" Operator="Equal" Text="Should match with Password"
                            ErrorMessage="Should match with Password" class="validationred"
                            ValidationGroup="Consultant" Display="Dynamic" ForeColor="#d0582e"></asp:CompareValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-lg-4">
                        <label class="control-label">Branch</label>
                        <asp:DropDownList ID="ddlBranch" class="form-control" runat="server" Style="padding: 0px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" ForeColor="#d0582e"
                            ErrorMessage="Please Select Branch" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-lg-4">
                        <label class="control-label">Designation</label>
                        <asp:DropDownList ID="ddlDesignation" class="form-control" runat="server" Style="padding: 0px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation" ForeColor="#d0582e"
                            ErrorMessage="Please Select Designation" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-lg-4">
                        <label class="control-label">Status</label>
                        <asp:DropDownList ID="ddlStatus" class="form-control" runat="server" Style="padding: 0px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" ForeColor="#d0582e"
                            ErrorMessage="Please Select Status" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <p></p>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" ValidationGroup="Consultant" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-danger" OnClick="btnBack_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="delete" tabindex="-1" role="dialog" data-keyboard="false" data-backdrop="static" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content" style="height: 150px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h2 class="modal-title">Delete Consultant!</h2>
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
            $('#ContentPlaceHolder1_GridView1').DataTable();
        });
    </script>
</asp:Content>

