<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="LeadList.aspx.cs" Inherits="LeadList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="js/jquery-2.1.4.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#ContentPlaceHolder1_txtDepart').datepicker({
                startDate: 'today',
                minDate: 0,
                numberOfMonths: 1,
                autoclose: true,
                dateFormat: 'dd-mm-yy',
                onSelect: function (selected) {
                    $("#ContentPlaceHolder1_txtReturnDate").val('');
                    var date = $(this).datepicker('getDate');
                    if (date) {
                        date.setDate(date.getDate());
                    }
                    $("#ContentPlaceHolder1_txtReturnDate").datepicker("option", "minDate", date)
                }
            });
            $("#ContentPlaceHolder1_txtReturnDate").datepicker({
                startDate: 'today',
                numberOfMonths: 1,
                dateFormat: 'dd-mm-yy',
                autoclose: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="inner_content">
        <div class="w3l_agileits_breadcrumbs">
            <div class="w3l_agileits_breadcrumbs_inner">
                <ul>
                    <li><a href="main-page.html">Dashboard</a><span>«</span></li>
                    <li>Lead<span>«</span></li>
                    <li>Leads List</li>
                </ul>
            </div>
        </div>
        <div class="inner_content_w3_agile_info two_in" style="margin-top: 0em;">
            <div class="agile-tables">
                <div class="w3l-table-info agile_info_shadow">
                    <asp:GridView ID="gvLeadList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                        EmptyDataText="There are no data records to display."
                        AllowPaging="true" PageSize="100" OnRowCommand="gvLeadList_RowCommand"
                        Style="font-size: 100%;" ForeColor="Black">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblID" Text='<%#Eval("lsId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="First Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFirstName" Text='<%#Eval("lsFirstName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Name">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblLastName" Text='<%#Eval("lsLastName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblMobile" Text='<%#Eval("lsPhone") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email ID">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblEmailID" Text='<%#Eval("lsEmailId") %>'></asp:Label>
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
                            <asp:TemplateField HeaderText="From">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblOrigin" Text='<%#Eval("lsOriginName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To">
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
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" Width="23px" Height="23px" ImageUrl="~/images/edit_new.png"
                                        CommandName="EditLead" ToolTip="Edit" />
                                    <asp:ImageButton ID="btnDelete" runat="server" Width="23px" Height="23px" ImageUrl="~/images/Delete.png"
                                        CommandName="DeleteLead" ToolTip="Delete" />
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
            <div class="modal-content" style="height: 150px;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h2 class="modal-title">Delete Lead!</h2>
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
                    <h2 class="modal-title">Update Lead</h2>
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
                            <label class="control-label">From</label>
                            <asp:TextBox ID="txtSource" class="form-control" runat="server" placeholder="Source"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSource" runat="server" ControlToValidate="txtSource" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Source" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4 form-group1">
                            <label class="control-label">To</label>
                            <asp:TextBox ID="txtDestination" class="form-control" runat="server" placeholder="Destination"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDestination" runat="server" ControlToValidate="txtDestination" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Destination" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="vali-form vali-form1">
                        <div class="col-md-4 form-group2 group-mail">
                            <label class="control-label">Depart</label>
                            <asp:TextBox ID="txtDepart" class="form-control" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDepart" runat="server" ControlToValidate="txtDepart" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Depart Date" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4 form-group2 group-mail">
                            <label class="control-label">Return</label>
                            <asp:TextBox ID="txtReturnDate" class="form-control" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
                        </div>
                        <div class="col-md-4 form-group2 group-mail">
                            <label class="control-label">Product</label>
                            <asp:DropDownList ID="ddlPackage" class="form-control" runat="server" Style="padding: 0px; margin-top: 0em;">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPackage" runat="server" ControlToValidate="ddlPackage" ForeColor="#d0582e"
                                ErrorMessage="Please Select Product" ValidationGroup="Consultant" InitialValue="-1" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4 form-group2 group-mail">
                            <label class="control-label">Budget</label>
                            <asp:TextBox ID="txtBudget" runat="server" class="form-control" placeholder="Budget"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBudget" runat="server" ControlToValidate="txtBudget" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Budget" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-2 form-group2 group-mail">
                            <label class="control-label">Adult</label>
                            <asp:DropDownList ID="ddlAdults" class="form-control" runat="server" Style="padding: 0px; margin-top: 0em;">
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                                <asp:ListItem Value="7">7</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="9">9</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 form-group2 group-mail">
                            <label class="control-label">Child</label>
                            <asp:DropDownList ID="ddlChild" class="form-control" runat="server" Style="padding: 0px; margin-top: 0em;">
                                <asp:ListItem Value="0">0</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 form-group2 group-mail">
                            <label class="control-label">Infant</label>
                            <asp:DropDownList ID="ddlInfant" class="form-control" runat="server" Style="padding: 0px; margin-top: 0em;">
                                <asp:ListItem Value="0">0</asp:ListItem>
                                <asp:ListItem Value="1">1</asp:ListItem>
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="3">3</asp:ListItem>
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="6">6</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="vali-form vali-form1">
                        <div class="col-md-4 form-group2 group-mail">
                            <label class="control-label">Notes</label>
                            <asp:TextBox ID="txtNotes" runat="server" class="form-control" placeholder="Notes" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="vali-form vali-form1" style="margin-top: 20px; text-align: center">
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

