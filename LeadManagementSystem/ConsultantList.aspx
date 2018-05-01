<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="ConsultantList.aspx.cs" Inherits="ConsultantList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#target").keyup(function () {
                if ($("[id *=target]").val() != "") {
                    $("[id *=ContentPlaceHolder1_GridView1]").children
                    ('tbody').children('tr').each(function () {
                        $(this).show();
                    });
                    $("[id *=ContentPlaceHolder1_GridView1]").children
                    ('tbody').children('tr').each(function () {
                        var match = false;
                        $(this).children('td').each(function () {
                            if ($(this).text().toUpperCase().indexOf($("[id *=target]").val().toUpperCase()) > -1) {
                                match = true;
                                return false;
                            }
                        });
                        if (match) {
                            $(this).show();
                            $(this).children('th').show();
                        }
                        else {
                            $(this).hide();
                            $(this).children('th').show();
                        }
                    });


                    $("[id *=ContentPlaceHolder1_GridView1]").children('tbody').
                            children('tr').each(function (index) {
                                if (index == 0)
                                    $(this).show();
                            });
                }
                else {
                    $("[id *=ContentPlaceHolder1_GridView1]").children('tbody').
                            children('tr').each(function () {
                                $(this).show();
                            });
                }
            });
        });
    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="outter-wp">
        <div class="row">
            <div class="col-md-8">
                <!--/sub-heard-part-->
                <%--<div class="sub-heard-part">
                    <ol class="breadcrumb m-b-0">
                        <li><a>Consultant</a></li>
                        <li class="active">Consultant List</li>
                    </ol>
                </div>--%>
                <asp:ImageButton ID="imgAdd" runat="server" ImageUrl="~/images/add-consultant.png" OnClick="imgAdd_Click" ToolTip="Add Consultant" />
            </div>
            <div class="col-md-4 text-right">
            </div>

        </div>

        <!--/sub-heard-part-->

        <div class="forms-main">
            <div class="graph-form">
                <div class="validation-form">
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
                            <div class="col-lg-6 form-group"></div>
                            <div class="col-lg-3 form-group">
                                <input id="target" type="text" class="form-control" placeholder="Text To Search" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="dvGrid" runat="server">
                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                            EmptyDataText="There are no data records to display." PageSize="2" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowCommand="GridView1_RowCommand"
                            Style="font-size: 110%;" ForeColor="Black">
                            <PagerStyle CssClass="pagination_grid" />
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
                                        <asp:ImageButton ID="btnEdit" runat="server" Width="23px" Height="23px" ImageUrl="~/images/edit-user.png"
                                            CommandName="EditConsultant" ToolTip="Edit" />
                                        <asp:ImageButton ID="btnDelete" runat="server" Width="23px" Height="23px" ImageUrl="~/images/garbage.png"
                                            CommandName="DeleteConsultant" ToolTip="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div id="dvEdit" runat="server" visible="false">
                        <div class="vali-form">
                            <div class="col-md-3">
                                <label class="control-label" for="txtFirstName">First Name</label>
                                <asp:TextBox ID="txtFirstName" runat="server" class="form-control" placeholder="Given Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter First Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3 form-last">
                                <label class="control-label" for="txtLastName">Last Name</label>
                                <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Surname"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Last Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3 form-last">
                                <label class="control-label" for="txtEmail">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmailId" runat="server" ControlToValidate="txtEmail" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Email" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmailId" runat="server" ForeColor="#d0582e" Display="Dynamic" ErrorMessage="Please check Email Format"
                                    ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Consultant">
                                </asp:RegularExpressionValidator>
                            </div>
                            <div class="col-md-3 form-last">
                                <label class="control-label" for="txtMobile">Mobile</label>
                                <asp:TextBox ID="txtMobile" runat="server" class="form-control" placeholder="Mobile" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvMobileNum" runat="server" ControlToValidate="txtMobile" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Mobile" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revMobileNum" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                                    ControlToValidate="txtMobile" ForeColor="#d0582e" ValidationGroup="Consultant"></asp:RegularExpressionValidator>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="vali-form">
                            <div class="form-group col-lg-3">
                                <label class="control-label">Login ID</label>
                                <asp:TextBox ID="txtLoginId" class="form-control" runat="server" placeholder="Enter LoginId" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLoginId" runat="server" ControlToValidate="txtLoginId" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Login ID" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3">
                                <label class="control-label" runat="server">Password</label>
                                <asp:TextBox ID="txtPassword" class="form-control" runat="server" placeholder="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Password" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3" id="dvRPwd" runat="server">
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
                        <div class="clearfix"></div>
                        <div class="vali-form">
                            <div class="form-group col-lg-3">
                                <label class="control-label">Branch</label>
                                <asp:DropDownList ID="ddlBranch" class="form-control" runat="server" Style="padding: 0px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" ForeColor="#d0582e"
                                    ErrorMessage="Please Select Branch" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3">
                                <label class="control-label">Designation</label>
                                <asp:DropDownList ID="ddlDesignation" class="form-control" runat="server" Style="padding: 0px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation" ForeColor="#d0582e"
                                    ErrorMessage="Please Select Designation" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3">
                                <label class="control-label">Status</label>
                                <asp:DropDownList ID="ddlStatus" class="form-control" runat="server" Style="padding: 0px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" ForeColor="#d0582e"
                                    ErrorMessage="Please Select Status" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-12 form-group button-2">
                                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/images/Save.png" ValidationGroup="Consultant" OnClick="btnSave_Click" />
                                <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/images/Update.png" ValidationGroup="Consultant" OnClick="btnUpdate_Click" />
                                <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/images/Back.png" OnClick="btnBack_Click" />

                            </div>
                        </div>

                        <div class="clearfix"></div>



                    </div>

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

