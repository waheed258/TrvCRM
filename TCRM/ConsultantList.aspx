<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="ConsultantList.aspx.cs" Inherits="ConsultantList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="inner_content">
        <div class="w3l_agileits_breadcrumbs">
            <div class="w3l_agileits_breadcrumbs_inner">
                <ul>
                    <li><a href="main-page.html">Dashboard</a><span>«</span></li>
                    <li>Consultants <span>«</span></li>
                    <li>Consultant List</li>
                </ul>
            </div>
        </div>
        <div class="inner_content_w3_agile_info two_in" style="margin-top:0em;">
            <div class="agile-tables">
                <div class="w3l-table-info agile_info_shadow">                    
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                        EmptyDataText="There are no data records to display."
                        AllowPaging="true" PageSize="100" OnRowCommand="GridView1_RowCommand"
                        Style="font-size: 100%;" ForeColor="Black">
                        <Columns>
                            <asp:TemplateField HeaderText="Advisor ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblConsultantID" Text='<%#Eval("ConsultantID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="First Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("FirstName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLastName" Text='<%#Eval("LastName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile">
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
                                    <asp:ImageButton ID="btnEdit" runat="server" Width="23px" Height="23px" ImageUrl="~/images/edit_new.png"
                                        CommandName="EditConsultant" ToolTip="Edit" />
                                    <asp:ImageButton ID="btnDelete" runat="server" Width="23px" Height="23px" ImageUrl="~/images/Delete.png"
                                        CommandName="DeleteConsultant" ToolTip="Delete" />
                                </ItemTemplate>
                            </asp:TemplateField>
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
                    <h2 class="modal-title">Thank you!</h2>
                </div>
                <div class="modal-body">
                    <asp:Label ID="message" runat="server"></asp:Label>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="delete" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content" style="height:150px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h2 class="modal-title">Delete Consultant!</h2>
                </div>
                <div class="modal-body">
                    <div class="col-md-12 form-group user-form-group">
                        <asp:Label ID="lbldeletemessage" runat="server" class="control-label" Style="color: green" />
                        <div class="pull-right">
                            <asp:Button ID="btnSure" runat="server" Text="YES" CssClass="btn btn-add btn-sm" OnClick="btnSure_Click"></asp:Button>
                        </div>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="Edit" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h2 class="modal-title">Update Consultant</h2>
                </div>
                <div class="modal-body">
                    <div class="vali-form">
                        <div class="col-md-4 form-group1">
                            <label class="control-label">First Name</label>
                            <asp:TextBox ID="txtFirstName" runat="server" placeholder="Given Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ForeColor="#d0582e"
                                ErrorMessage="Please Enter First Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4 form-group1 form-last">
                            <label class="control-label">Last Name</label>
                            <asp:TextBox ID="txtLastName" runat="server" placeholder="Surname"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Last Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4 form-group1 form-last">
                            <label class="control-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmailId" runat="server" ControlToValidate="txtEmail" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Email" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmailId" runat="server" ForeColor="#d0582e" Display="Dynamic" ErrorMessage="Please check Email Format"
                                ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Consultant">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="vali-form vali-form1">
                        <div class="col-md-4 form-group1 form-last">
                            <label class="control-label">Mobile</label>
                            <asp:TextBox ID="txtMobile" runat="server" placeholder="Mobile"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMobileNum" runat="server" ControlToValidate="txtMobile" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Mobile" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revMobileNum" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                                ControlToValidate="txtMobile" ForeColor="#d0582e" ValidationGroup="Consultant"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-4 form-group1">
                            <label class="control-label">Login ID</label>
                            <asp:TextBox ID="txtLoginId" ReadOnly="true" class="form-control" runat="server" placeholder="Enter LoginId" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLoginId" runat="server" ControlToValidate="txtLoginId" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Login ID" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4 form-group1">
                            <label class="control-label">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" placeholder="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Password" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="vali-form vali-form1">
                        <div class="col-md-4 form-group2 group-mail">
                            <label class="control-label">Branch</label>
                            <asp:DropDownList ID="ddlBranch" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" ForeColor="#d0582e"
                                ErrorMessage="Please Select Branch" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4 form-group2 group-mail">
                            <label class="control-label">Designation</label>
                            <asp:DropDownList ID="ddlDesignation" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation" ForeColor="#d0582e"
                                ErrorMessage="Please Select Designation" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4 form-group2 group-mail">
                            <label class="control-label">Status</label>
                            <asp:DropDownList ID="ddlStatus" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" ForeColor="#d0582e"
                                ErrorMessage="Please Select Status" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="vali-form vali-form1" style="margin-top: 20px">
                        <div class="col-md-12 form-group">
                            <asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="btn btn-primary" ValidationGroup="Consultant" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnReset" runat="server" Text="Close" CssClass="btn btn-default" />
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }

    </script>
    <script type="text/javascript">
        function openEditModal() {
            $('#Edit').modal('show');
        }
        function openDeleteModal() {
            $('#delete').modal('show');
        }
    </script>   
</asp:Content>

