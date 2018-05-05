<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="ProfilePage.aspx.cs" Inherits="ProfilePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="outter-wp">
        <div class="forms-main">
            <div class="graph-form">
                <div class="validation-form">
                    <div id="dvEdit" runat="server">
                        <div class="vali-form">
                            <div class="col-md-4">
                                <label class="control-label" for="txtFirstName">First Name</label>
                                <asp:TextBox ID="txtFirstName" runat="server" class="form-control" placeholder="Given Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter First Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4 form-last">
                                <label class="control-label" for="txtLastName">Last Name</label>
                                <asp:TextBox ID="txtLastName" runat="server" class="form-control" placeholder="Surname"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Last Name" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4 form-last">
                                <label class="control-label" for="txtEmail">Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmailId" runat="server" ControlToValidate="txtEmail" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Email" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmailId" runat="server" ForeColor="#d0582e" Display="Dynamic" ErrorMessage="Please check Email Format"
                                    ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Consultant">
                                </asp:RegularExpressionValidator>
                            </div>
                           
                            <div class="clearfix"></div>
                        </div>

                        <div class="vali-form">
                             <div class="col-md-4 form-last">
                                <label class="control-label" for="txtMobile">Mobile</label>
                                <asp:TextBox ID="txtMobile" runat="server" class="form-control" placeholder="Mobile" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvMobileNum" runat="server" ControlToValidate="txtMobile" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Mobile" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revMobileNum" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                                    ControlToValidate="txtMobile" ForeColor="#d0582e" ValidationGroup="Consultant"></asp:RegularExpressionValidator>
                            </div>
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
                        </div>
                        <div class="clearfix"></div>
                        <div class="vali-form">
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
                            <div class="col-md-12 form-group button-2">
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
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>
</asp:Content>

