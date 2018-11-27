<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="EditLead.aspx.cs" Inherits="EditLead" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="assets/css/jquery-ui.css" rel="stylesheet" />
    <script src="ckeditor/ckeditor.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdfQuoteUrl" runat="server" />
    <div>
        <asp:Button ID="backToLead" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="backToLead_Click" />
    </div>
    <p></p>
    <div class="panel">
        <div class="panel-heading">
            <h3 class="panel-title" style="font-weight: bold; color: #00AAFF">Edit Lead</h3>
        </div>
        <div class="panel-body">
            <div style="text-align: center">
                <asp:Label ID="lblMessage" runat="server" Style="font-size: 20px;"></asp:Label>
            </div>
            <div class="col-md-12 text-center">
                <asp:Label ID="lblFollowup" runat="server"></asp:Label>
            </div>
            <div class="row">
                <div class="col-md-7">
                    <!-- PANEL SCROLLING -->
                    <div class="panel">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="control-label">Lead Source</label>
                                    <asp:DropDownList ID="ddlESource" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlESource_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlESource" runat="server" ControlToValidate="ddlESource" ForeColor="#d0582e"
                                        ErrorMessage="Please Select Source" ValidationGroup="LeadEdit" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6" id="dvEOthers" runat="server">
                                    <label class="control-label">Others</label>
                                    <asp:TextBox ID="txtEOthers" runat="server" class="form-control" placeholder="Description" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtEOthers" runat="server" ControlToValidate="txtEOthers" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Others Description" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-4">
                                    <label class="control-label">Lead Status</label>
                                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" ForeColor="#d0582e"
                                        ErrorMessage="Please select status" ValidationGroup="LeadEdit" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4" id="followupdate" runat="server">
                                    <label class="control-label">Follow up Date</label>
                                    <asp:TextBox ID="txtFollowUp" class="form-control" runat="server" placeholder="dd-mm-yyyy"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFollowupdate" runat="server" ControlToValidate="txtFollowUp" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Follow up Date" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4" id="desc" runat="server">
                                    <label class="control-label">Description</label>
                                    <asp:TextBox ID="txtDescription" runat="server" class="form-control" placeholder="Description" MaxLength="200"></asp:TextBox>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="control-label">First Name</label>
                                    <asp:TextBox ID="txtEFirstName" runat="server" class="form-control" placeholder="Given Name" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtEFirstName" runat="server" ControlToValidate="txtEFirstName" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter First Name" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <label class="control-label">Last Name</label>
                                    <asp:TextBox ID="txtELastName" runat="server" class="form-control" placeholder="Surname" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtELastName" runat="server" ControlToValidate="txtELastName" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Last Name" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="control-label">Email</label>
                                    <asp:TextBox ID="txtEEmail" runat="server" class="form-control" placeholder="Email" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtEEmail" runat="server" ControlToValidate="txtEEmail" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Email" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rgtxtEEmail" runat="server" ForeColor="#d0582e" Display="Dynamic" ErrorMessage="Please check Email Format"
                                        ControlToValidate="txtEEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="LeadEdit">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-6">
                                    <label class="control-label">Tel</label>
                                    <asp:TextBox ID="txtEMobile" runat="server" class="form-control" placeholder="Mobile" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtEMobile" runat="server" ControlToValidate="txtEMobile" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Mobile" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="rgtxtEMobile" runat="server" ErrorMessage="Please enter 10 digits" ValidationExpression="[0-9]{10}" Display="Dynamic"
                                        ControlToValidate="txtEMobile" ForeColor="#d0582e" ValidationGroup="LeadEdit"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-6" id="dvClientFileId" runat="server">
                                    <label class="control-label">Client file Id: </label>
                                    <asp:TextBox ID="txtClientFileId" runat="server" class="form-control" placeholder="Client file Id" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvClientFileID" runat="server" ControlToValidate="txtClientFileId" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter File ID" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-12" style="margin-top: 15px;">
                                    <label class="control-label"><strong>Final Travel Dates</strong></label>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="control-label">Depart</label>
                                    <asp:TextBox ID="txtEDepart" class="form-control" runat="server" placeholder="dd-mm-yyyy" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtEDepart" runat="server" ControlToValidate="txtEDepart" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Depart Date" ValidationGroup="LeadEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <label class="control-label">Return</label>
                                    <asp:TextBox ID="txtEReturn" class="form-control" runat="server" placeholder="dd-mm-yyyy" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-12">
                                    <label class="control-label">Consultant Notes</label>
                                    <asp:TextBox ID="txtEConsultNotes" runat="server" class="form-control" placeholder="Notes" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-12" style="margin-top: 15px;">
                                    <label class="control-label" style="float: left;">Set Reminder</label>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtEReminder" runat="server" class="form-control" placeholder="dd-mm-yyyy" MaxLength="10" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-12">
                                    <label class="control-label">Reminder Notes</label>
                                    <asp:TextBox ID="txtERemindNotes" runat="server" class="form-control" placeholder="Notes" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                </div>
                            </div>
                            <p></p>
                            <div class="row">
                                <div class="col-md-12 text-center" style="margin-top: 15px;">
                                    <asp:Button ID="imgEUpdate" runat="server" Text="Update" CssClass="btn btn-primary" ValidationGroup="Consultant" OnClick="imgEUpdate_Click1" />
                                    <asp:Button ID="imgECancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="imgECancel_Click1" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="row">
                        <%--Lead Data Section--%>
                        <div class="col-md-12">
                            <div class="panel">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Lead Data</h3>
                                </div>
                                <div class="panel-body">
                                    <div>
                                        <label class="control-label">Client Name:</label>
                                        <asp:Label ID="lblLName" runat="server"></asp:Label>
                                    </div>
                                    <div>
                                        <label class="control-label">Email: </label>
                                        <asp:Label ID="lblLEmail" runat="server"></asp:Label>
                                    </div>
                                    <div>
                                        <label class="control-label">Travel Dates:</label>
                                        <asp:Label ID="lblLDates" runat="server"></asp:Label>
                                    </div>
                                    <div>
                                        <label class="control-label">Budget:</label>
                                        <asp:Label ID="lblLBudget" runat="server"></asp:Label>
                                    </div>
                                    <div>
                                        <label class="control-label">Tel:</label>
                                        <asp:Label ID="lblLPhone" runat="server"></asp:Label>
                                    </div>
                                    <div>
                                        <label class="control-label">URL:</label>
                                        <asp:HyperLink ID="lnkUrl" runat="server" Target="_blank"></asp:HyperLink>
                                    </div>
                                    <div>
                                        <label class="control-label">Notes:</label>
                                        <asp:Label ID="lblLNotes" runat="server"></asp:Label>
                                    </div>
                                    <div>
                                        <label class="control-label">Package Name:</label>
                                        <asp:Label ID="lblPackageName" runat="server"></asp:Label>
                                    </div>
                                    <div>
                                        <label class="control-label">Consultant Notes:</label>
                                        <asp:Label ID="lblConsultantNotes" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--End Lead Data Section--%>

                        <%--Email Section--%>
                        <div class="col-md-12">
                            <div class="panel">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Send Emails</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-6">
                                        <asp:Button ID="btnSendEmail" runat="server" Text="Send More Info Email" CssClass="btn btn-primary" OnClick="btnSendEmail_Click" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Button ID="btnSendFUEmail" runat="server" Text="Send Follow up Email" CssClass="btn btn-primary" OnClick="btnSendFUEmail_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--End Email Section--%>

                        <%--Generate Quote Section--%>
                        <div class="col-md-12">
                            <div class="panel">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Generate Quote</h3>
                                </div>
                                <div class="panel-body">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="ddlQuoteDetails" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlQuoteDetails_SelectedIndexChanged" runat="server">
                                                <asp:ListItem Value="1">Quote from New</asp:ListItem>
                                                <asp:ListItem Value="2">Quote from Template</asp:ListItem>
                                                <asp:ListItem Value="3">Quote Custom</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-6" runat="server" id="dvTemplates" visible="false">
                                            <asp:DropDownList ID="ddlTemplateNames" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rqfddlTemplateNames" runat="server" ControlToValidate="ddlTemplateNames" ForeColor="#d0582e"
                                                ErrorMessage="Please select Template" ValidationGroup="TemplateName" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <p></p>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Button ID="imgQuoteSubmit" runat="server" Text="Generate Quote" OnClick="imgQuoteSubmit_Click" CssClass="btn btn-primary" ValidationGroup="TemplateName" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <%--End Generate Quote Section--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--Lead History--%>
    <asp:HiddenField ID="hdfSMS" runat="server" />
    <div class="panel">
        <div class="panel-heading">
            <h3 class="panel-title" style="font-weight: bold; color: #00AAFF">Lead History</h3>
        </div>
        <div class="panel-body">
            <div class="tables">
                <div class="table table-responsive">
                    <asp:GridView ID="gvHistory" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                        EmptyDataText="There are no data records to display."
                        PageSize="100" OnRowCommand="gvHistory_RowCommand" OnRowDataBound="gvHistory_RowDataBound" OnRowEditing="gvHistory_RowEditing"
                        Style="" ForeColor="Black">
                        <PagerStyle CssClass="pagination_grid" />
                        <Columns>
                            <asp:TemplateField HeaderText="Lead History" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("HistoryDesc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("EventDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QuoteNumber" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblHistoryQuote" Text='<%#Eval("ViewData") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ClientFileId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblClientFileId" Text='<%#Eval("ClientFileId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="View" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton CommandName="View" ID="btnViewHistory" runat="server" ToolTip="View">View</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton CommandName="Edit" ID="btnEditHistory" runat="server" ToolTip="Edit">Edit</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Send SMS" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton CommandName="SendSMS" ID="btnSendSMS" runat="server" ToolTip="Send SMS">Send SMS</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SMS Status" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%#Eval("SmsStatus").ToString() == "Y" ? "Sent" : "" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Convert Quote To Booking" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:LinkButton CommandName="Convert" ID="btnConvert" runat="server" ToolTip="Convert Quote To Booking">Convert</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>


        </div>
    </div>
    <%--End Lead History--%>

    <%--SMS Modal Popup--%>
    <div class="modal fade" id="smsModal" tabindex="-1" data-keyboard="false" data-backdrop="static" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h2 class="modal-title">Send SMS</h2>

                </div>

                <div class="modal-body">

                    <div class="col-md-12">
                        <div class="col-md-4">
                            <label class="control-label">Phone no</label>
                            <asp:TextBox ID="txtSendSMS" class="form-control" runat="server" MaxLength="10" ReadOnly="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSendSMS" runat="server" ControlToValidate="txtSendSMS" ForeColor="#d0582e"
                                ErrorMessage="Please enter Phone no" ValidationGroup="SMS" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-8">
                            <label class="control-label">Message</label>
                            <asp:TextBox ID="txtResp" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtResp" ForeColor="#d0582e"
                                ErrorMessage="Please enter message" ValidationGroup="SMS" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-12 text-center" style="margin-top: 15px; margin-bottom: 15px;">
                        <asp:Button ID="btnSMS" Text="Send SMS" runat="server" OnClick="btnSMS_Click" ValidationGroup="SMS" />
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%-- End SMS Modal Popup--%>

    <%--Email Modal Popup--%>
    <div class="modal fade" id="EmailModal" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h5 class="modal-title">Email Template</h5>
                </div>
                <div class="modal-body">

                    <div class="col-md-12">

                        <div class="col-md-4">
                            <label class="control-label">To</label>
                            <asp:TextBox ID="txtToEmail" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqftxtToEmail" runat="server" ControlToValidate="txtToEmail" ForeColor="#d0582e"
                                ErrorMessage="Please enter Email Id" ValidationGroup="Email" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">CC</label>
                            <asp:TextBox ID="txtCCEmail" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Subject</label>
                            <asp:TextBox ID="txtEmailSubject" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqftxtEmailSubject" runat="server" ControlToValidate="txtEmailSubject" ForeColor="#d0582e"
                                ErrorMessage="Please Email Subject" ValidationGroup="Email" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-12 text-center" style="margin-top: 15px;">
                        <asp:TextBox ID="txtMailTemp" runat="server" TextMode="MultiLine" ValidationGroup="Email"></asp:TextBox>
                        <asp:Button ID="btnSendMail" Text="Send Mail" runat="server" OnClick="btnSendMail_Click" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--End Email Modal Popup--%>

    <%--Follow up Email Modal Popup--%>
    <div class="modal fade" id="FollowupModal" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h5 class="modal-title">Follow up Email Template</h5>
                </div>
                <div class="modal-body">

                    <div class="col-md-12">

                        <div class="col-md-4">
                            <label class="control-label">To</label>
                            <asp:TextBox ID="txtToEmailFU" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToEmailFU" ForeColor="#d0582e"
                                ErrorMessage="Please enter Email Id" ValidationGroup="EmailFU" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">CC</label>
                            <asp:TextBox ID="txtCCEmailFU" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Subject</label>
                            <asp:TextBox ID="txtEmailSubjectFU" class="form-control" runat="server" MaxLength="100" Text="Serendipity Travel >> Follow up"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmailSubjectFU" ForeColor="#d0582e"
                                ErrorMessage="Please Email Subject" ValidationGroup="EmailFU" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-12 text-center" style="margin-top: 15px;">
                        <asp:TextBox ID="txtMailTempFU" runat="server" TextMode="MultiLine" ValidationGroup="EmailFU"></asp:TextBox>
                        <asp:Button ID="btnSendMailFU" Text="Send Mail" runat="server" OnClick="btnSendMailFU_Click" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--End Follow up Email Modal Popup--%>


    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script src="assets/vendor/bootstrap/js/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#ContentPlaceHolder1_txtReturnDate").prop('disabled', true);
            $('#ContentPlaceHolder1_txtDepart').datepicker({
                startDate: 'today',
                minDate: 0,
                numberOfMonths: 1,
                autoclose: true,
                dateFormat: 'dd-mm-yy',
                onSelect: function (selected) {
                    $("#ContentPlaceHolder1_txtReturnDate").prop('disabled', false);
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
            $("#ContentPlaceHolder1_txtFollowUp").datepicker({
                startDate: 'today',
                numberOfMonths: 1,
                //minDate: 0,
                dateFormat: 'dd-mm-yy',
                autoclose: true
            });
            $("#ContentPlaceHolder1_txtEReturn").prop('disabled', true);
            $('#ContentPlaceHolder1_txtEDepart').datepicker({
                startDate: 'today',
                minDate: 0,
                numberOfMonths: 1,
                autoclose: true,
                dateFormat: 'dd-mm-yy',
                onSelect: function (selected) {
                    $("#ContentPlaceHolder1_txtEReturn").prop('disabled', false);
                    $("#ContentPlaceHolder1_txtEReturn").val('');
                    var date = $(this).datepicker('getDate');
                    if (date) {
                        date.setDate(date.getDate());
                    }
                    $("#ContentPlaceHolder1_txtEReturn").datepicker("option", "minDate", date)
                }
            });
            $("#ContentPlaceHolder1_txtEReturn").datepicker({
                startDate: 'today',
                numberOfMonths: 1,
                dateFormat: 'dd-mm-yy',
                autoclose: true
            });

            $("#ContentPlaceHolder1_txtEReminder").datepicker({
                startDate: 'today',
                minDate: 0,
                numberOfMonths: 1,
                autoclose: true,
                dateFormat: 'dd-mm-yy'
            });

            CKEDITOR.disableAutoInline = true;
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtMailTemp', {
                toolbar:
            [
                { name: 'basicstyles', items: ['Bold', 'Italic'] },
                { name: 'paragraph', items: ['NumberedList', 'BulletedList'] },
                { name: 'tools', items: ['Maximize', '-', 'About'] }
            ],
                height: '300px'
            });
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtMailTempFU', {
                toolbar:
            [
                { name: 'basicstyles', items: ['Bold', 'Italic'] },
                { name: 'paragraph', items: ['NumberedList', 'BulletedList'] },
                { name: 'tools', items: ['Maximize', '-', 'About'] }
            ],
                height: '300px'
            });
        });
    </script>
    <script>
        function openSMSModal() {
            $('#smsModal').modal('show');
        }
        function openEmailModal() {
            $('#EmailModal').modal('show');
        }
        function openFUEmailModal() {
            $('#FollowupModal').modal('show');
        }
    </script>
</asp:Content>

