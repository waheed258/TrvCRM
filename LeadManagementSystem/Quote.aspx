<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.master" AutoEventWireup="true" CodeFile="Quote.aspx.cs" Inherits="Quote" ValidateRequest="false" %>

<%--<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <script src="js/jquery-2.2.3.min.js"></script>
    <script src="ckeditor/ckeditor.js"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <style>
        label {
            /*font-size: 1.5em !important;*/
            margin-top: 15px;
        }

        #ui-datepicker-div {
            display: none;
        }
    </style>
    <style>
        @font-face {
            font-family: 'MarkOT-Light';
            src: url('fonts/MarkOT-Light.eot?#iefix') format('embedded-opentype'), url('fonts/MarkOT-Light.otf') format('opentype'), url('fonts/MarkOT-Light.woff') format('woff'), url('fonts/MarkOT-Light.ttf') format('truetype'), url('fonts/MarkOT-Light.svg#MarkOT-Light') format('svg');
            font-weight: normal;
            font-style: normal;
        }

        @font-face {
            font-family: 'MarkOT-Medium';
            src: url('fonts/MarkOT-Medium.eot?#iefix') format('embedded-opentype'), url('fonts/MarkOT-Medium.otf') format('opentype'), url('fonts/MarkOT-Medium.woff') format('woff'), url('fonts/MarkOT-Medium.ttf') format('truetype'), url('fonts/MarkOT-Medium.svg#MarkOT-Medium') format('svg');
            font-weight: normal;
            font-style: normal;
        }

        @font-face {
            font-family: 'MarkOT-Heavy';
            src: url('fonts/MarkOT-Heavy.eot?#iefix') format('embedded-opentype'), url('fonts/MarkOT-Heavy.otf') format('opentype'), url('fonts/MarkOT-Heavy.woff') format('woff'), url('fonts/MarkOT-Heavy.ttf') format('truetype'), url('fonts/MarkOT-Heavy.svg#MarkOT-Heavy') format('svg');
            font-weight: normal;
            font-style: normal;
        }

        @font-face {
            font-family: 'MarkOT-Bold';
            src: url('fonts/MarkOT-Bold.eot?#iefix') format('embedded-opentype'), url('fonts/MarkOT-Bold.otf') format('opentype'), url('fonts/MarkOT-Bold.woff') format('woff'), url('fonts/MarkOT-Bold.ttf') format('truetype'), url('fonts/MarkOT-Bold.svg#MarkOT-Bold') format('svg');
            font-weight: normal;
            font-style: normal;
        }
    </style>
    <script type="text/javascript">
        var counter = 0;
        function AddFileUpload() {
            var div = document.createElement('DIV');
            div.innerHTML = '<input style="float:left;" id="file' + counter + '" name = "file' + counter +
                            '" type="file" />' +
                            '<input id="Button' + counter + '" type="button" ' +
                            'value="Remove" onclick = "RemoveFileUpload(this)" />';
            document.getElementById("FileUploadContainer").appendChild(div);
            counter++;
        }
        function RemoveFileUpload(div) {

            document.getElementById("FileUploadContainer").removeChild(div.parentNode);

        }

        function SetTarget() {

            document.forms[0].target = "_blank";

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="outter-wp">
        <div>
            <asp:ImageButton ID="backToLead" runat="server" OnClick="backToLead_Click" ImageUrl="~/images/Back.png" Height="35px" />
        </div>
        <div class="forms-main" runat="server" id="quotesection">
            <div class="graph-form">
                <div class="validation-form">
                    <div class="vali-form">
                        <label class="control-label" style="color: #58a2e6; font-size: 20px;">Lead Details</label>
                        <div class="graph-form">
                            <div class="row">
                                <div class="col-md-3">
                                    <label class="control-label">Client Name</label>
                                    <asp:Label ID="lblClientName" class="form-control" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-3" id="dvProdct" runat="server">
                                    <label class="control-label">Product</label>
                                    <asp:DropDownList ID="ddlPackage" runat="server" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlPackage" runat="server" ControlToValidate="ddlPackage" ForeColor="#d0582e"
                                        ErrorMessage="Please select Product" ValidationGroup="Quote" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="dvCustomProduct" runat="server" visible="false">
                                    <label class="control-label">Product</label>
                                    <asp:TextBox ID="txtProduct" runat="server" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rqftxtProduct" runat="server" ControlToValidate="txtProduct" ForeColor="#d0582e"
                                        ErrorMessage="Please Enter Product" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label">Departing from</label>
                                    <asp:TextBox ID="txtSource" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="control-label">Travelling to</label>
                                    <asp:TextBox ID="txtDestination" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <label class="control-label">Date</label>
                                <asp:TextBox ID="txtDate" class="form-control" runat="server" placeholder="DD-MM-YYYY"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDate" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Date" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="vali-form">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="control-label">Cost Type for Adult</label>
                                <asp:DropDownList ID="ddlAdultType" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlAdultType_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rvAdult" runat="server" ControlToValidate="ddlAdultType" ForeColor="#d0582e"
                                    ErrorMessage="Please select Cost Type" ValidationGroup="Quote" Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3" id="dvAdultPrice" runat="server">
                                <label class="control-label">Price</label>
                                <asp:TextBox ID="txtAdultPrice" runat="server" class="form-control" placeholder="Adult Price" MaxLength="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAdultPice" runat="server" ControlToValidate="txtAdultPrice" ForeColor="#d0582e"
                                    ErrorMessage="Please Enter Price" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3" id="dvAdultPersons" runat="server" visible="false">
                                <label class="control-label">No Of Persons</label>
                                <asp:DropDownList ID="ddlAdultPersons" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlAdultPersons_SelectedIndexChanged">
                                    <asp:ListItem Value="0">No Of Persons</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="8">8</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="rfvAdultQty" runat="server" ControlToValidate="ddlAdultPersons" ForeColor="#d0582e"
                                    ErrorMessage="Please Select No Of Persons" ValidationGroup="Quote" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3" runat="server" id="dvAdultTot" visible="false">
                                <label class="control-label">Total Price</label>
                                <asp:Label ID="lblAdultTotPrice" runat="server" class="form-control"></asp:Label>
                            </div>
                        </div>

                    </div>
                    <div class="clearfix"></div>
                    <div class="vali-form">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="control-label">Cost Type for Child</label>
                                <asp:DropDownList ID="ddlChildType" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlChildType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3" id="dvChildPrice">
                                <label class="control-label">Price</label>
                                <asp:TextBox ID="txtChildPrice" runat="server" class="form-control" placeholder="Child Price" MaxLength="9"></asp:TextBox>
                            </div>
                            <div class="col-md-3" id="dvChildPersons" runat="server" visible="false">
                                <label class="control-label">No Of Childrens</label>
                                <asp:DropDownList ID="ddlChildPersons" runat="server" AutoPostBack="true" Style="padding: 0px" CssClass="form-control" OnSelectedIndexChanged="ddlChildPersons_SelectedIndexChanged">
                                    <asp:ListItem Value="0">No Of Childrens</asp:ListItem>
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6">6</asp:ListItem>
                                    <asp:ListItem Value="7">7</asp:ListItem>
                                    <asp:ListItem Value="8">8</asp:ListItem>
                                    <asp:ListItem Value="9">9</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="rfvChildPersons" runat="server" ControlToValidate="ddlChildPersons" ForeColor="#d0582e"
                                    ErrorMessage="Please Select No Of Childrens" ValidationGroup="Quote" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3" id="dvChildTotalPrice" runat="server" visible="false">
                                <label class="control-label">Total Price</label>
                                <asp:Label ID="lblChildTotPrice" runat="server" class="form-control"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="vali-form">
                        <div class="row text-right">
                            <asp:Label ID="lblGrand" runat="server" CssClass="control-label" Text="Grand Total :" Style="font-weight: bold"></asp:Label>
                            <asp:Label ID="lblGrandTotal" runat="server" CssClass="control-label" Style="font-weight: bold"></asp:Label>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="vali-form" style="margin-top: 30px">
                        <div class="row">
                            <div class="col-lg-6">
                                <label class="control-label" style="color: #58a2e6; font-size: 20px;">Flight Details</label>
                            </div>
                            <div class="col-lg-6 text-right">
                                <asp:ImageButton ID="imgbtnGetDots" runat="server" ImageUrl="~/images/button_get-from-dots.png" Height="35px" />
                            </div>
                        </div>
                        <asp:TextBox ID="txtFlightDetails" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="vali-form">
                        <label class="control-label" style="color: #58a2e6; font-size: 20px;">Car Hire</label>
                        <asp:TextBox ID="txtCarHireDetails" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="vali-form">
                        <label class="control-label" style="color: #58a2e6; font-size: 20px;">Hotel Info</label>
                        <asp:TextBox ID="txtHotelInfo" TextMode="MultiLine" runat="server" class="form-control" placeholder="No Of Childrens"></asp:TextBox>
                    </div>
                    <div class="vali-form">
                        <label class="control-label" style="color: #58a2e6; font-size: 20px;">Itinerary</label>
                        <asp:TextBox ID="txtItinerary" TextMode="MultiLine" runat="server" class="form-control" placeholder="No Of Childrens"></asp:TextBox>
                    </div>
                    <div class="vali-form">
                        <label class="control-label" style="color: #58a2e6; font-size: 20px;">INCLUDES</label>
                        <asp:TextBox ID="txtIncludes" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="vali-form">
                        <label class="control-label" style="color: #58a2e6; font-size: 20px;">EXCLUDES</label>
                        <asp:TextBox ID="txtExcludes" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="vali-form">
                        <label class="control-label" style="color: #58a2e6; font-size: 20px;">Travel Insurance</label>
                        <asp:TextBox ID="txtTravelInsur" TextMode="MultiLine" runat="server" class="form-control" placeholder="Travel Insurance Details" MaxLength="500" Columns="30"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTravelInsur" ForeColor="#d0582e"
                            ErrorMessage="Please Enter Travel Insurance Details" ValidationGroup="Quote" Display="Dynamic"></asp:RequiredFieldValidator>

                    </div>
                    <div class="clearfix"></div>
                    <div class="vali-form">
                        <div class="col-md-10" style="margin-bottom: 20px; margin-top: 10px;">
                            <asp:ImageButton ID="imgbtnViewQuote" runat="server" ImageUrl="~/images/ViewQuote.png" OnClick="imgbtnViewQuote_Click" ValidationGroup="Quote" Height="35px" OnClientClick="SetTarget();" />
                            <asp:ImageButton ID="btnTemplageName" runat="server" ValidationGroup="Quote" ImageUrl="~/images/button_save-as-template.png" OnClick="btnTemplageName_Click" Height="35px" />
                            <asp:ImageButton ID="imgbtnSubmitAssign" runat="server" ImageUrl="~/images/Save.png" OnClick="imgbtnSubmitAssign_Click1" ValidationGroup="Quote" Height="35px" />
                            <asp:ImageButton ID="imgbtnAddMultipleOptions" runat="server" ImageUrl="~/images/add-multiple-options.png" OnClick="imgbtnAddMultipleOptions_Click" Height="35px" />
                            <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/images/clear.png" OnClick="imgbtnClear_Click" Height="35px" />
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <asp:ImageButton ID="imgbtnBackQuote" runat="server" OnClick="imgbtnBackQuote_Click" ImageUrl="~/images/Back.png" Height="35px" />
        </div>
        <div class="forms-main" runat="server" id="emailsection">
            <div class="graph-form">
                <div class="validation-form">
                    <div class="vali-form">
                        <label class="control-label" style="color: #58a2e6; font-size: 20px;">Send Quote</label>
                        <div class="graph-form">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label class="control-label">To</label>
                                        <asp:TextBox ID="txtToEmailNew" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToEmailNew" ForeColor="#d0582e"
                                            ErrorMessage="Please Email Id" ValidationGroup="EmailNew" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="control-label">Client Name</label>
                                        <asp:TextBox ID="txtCLientNameNew" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label class="control-label">Subject</label>
                                        <asp:TextBox ID="txtEmailSubjectNew" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmailSubjectNew" ForeColor="#d0582e"
                                            ErrorMessage="Please Email Subject" ValidationGroup="EmailNew" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <label class="control-label">Click to add files</label>
                                    <%--<asp:FileUpload ID="customAttachmentNew" runat="server" AllowMultiple="true" />--%>
                                    <%--<span style="font-family: Arial">Click to add files</span>--%>&nbsp;&nbsp;

    <input id="Button1" type="button" value="add" onclick="AddFileUpload()" />
                                    <br />
                                    <br />
                                    <div id="FileUploadContainer">
                                        <!--FileUpload Controls will be added here -->
                                    </div>
                                    <br />
                                </div>
                                <div class="col-md-12">
                                    <asp:CheckBox ID="chbkBookingForm" runat="server" Text="Booking Form" />
                                    <asp:CheckBox ID="chbkBankingDetails" runat="server" Text="Banking Details" />
                                </div>
                                <div class="col-md-12 text-center" style="margin-top: 15px;">
                                    <asp:TextBox ID="txtMailTempNew" runat="server" TextMode="MultiLine" ValidationGroup="EmailNew"></asp:TextBox>
                                    <asp:Button ID="btnSendMailNew" Text="Send Mail" runat="server" ValidationGroup="EmailNew" OnClick="btnSendMailNew_Click" />
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>

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
                    <h2 class="modal-title">Alert!</h2>
                </div>
                <div class="modal-body">
                    <asp:Label ID="message" runat="server"></asp:Label>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="TemplateModal" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h5 class="modal-title">Template Name</h5>
                </div>
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <label class="control-label">Template Name</label>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtTemplateName" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtTemplateName" runat="server" ControlToValidate="txtTemplateName" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Template Name" ValidationGroup="Template" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnSaveTemplate" runat="server" CssClass="btn btn-default" ValidationGroup="Template" OnClick="btnSaveTemplate_Click" Text="Save Template" Style="margin: 4px;" />
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="EmailModal" data-keyboard="false" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h5 class="modal-title">Send Email</h5>
                </div>
                <div class="modal-body">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <label class="control-label">To</label>
                            <asp:TextBox ID="txtToEmail" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqftxtToEmail" runat="server" ControlToValidate="txtToEmail" ForeColor="#d0582e"
                                ErrorMessage="Please Email Id" ValidationGroup="Email" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Client Name</label>
                            <asp:TextBox ID="txtCLientName" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label class="control-label">Subject</label>
                            <asp:TextBox ID="txtEmailSubject" class="form-control" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqftxtEmailSubject" runat="server" ControlToValidate="txtEmailSubject" ForeColor="#d0582e"
                                ErrorMessage="Please Email Subject" ValidationGroup="Email" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <label class="control-label">Subject</label>
                        <%--<asp:FileUpload ID="customAttachment" runat="server" AllowMultiple="true" />--%>
                    </div>
                    <div class="col-md-12 text-center" style="margin-top: 15px;">
                        <asp:TextBox ID="txtMailTemp" runat="server" TextMode="MultiLine" ValidationGroup="Email"></asp:TextBox>
                        <asp:Button ID="btnSendMail" Text="Send Mail" runat="server" ValidationGroup="Email" OnClick="btnSendMail_Click" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <script>
        $(document).ready(function () {
            $('#ContentPlaceHolder1_txtDate').datepicker({
                startDate: 'today',
                minDate: 0,
                numberOfMonths: 1,
                autoclose: true,
                dateFormat: 'dd-mm-yy'
            });

            $("#ContentPlaceHolder1_txtDate").datepicker("setDate", new Date());

            CKEDITOR.disableAutoInline = true;
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtFlightDetails', { contentsCss: "p { margin:0px 0px 0px 0px; }" });
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtCarHireDetails', { contentsCss: "p { margin:0px 0px 0px 0px; }" });
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtHotelInfo', { contentsCss: "p { margin:0px 0px 0px 0px; }" });
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtItinerary', { contentsCss: "p { margin:0px 0px 0px 0px; }" });
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtIncludes', { contentsCss: "p { margin:0px 0px 0px 0px; }" });
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtExcludes', { contentsCss: "p { margin:0px 0px 0px 0px; }" });
            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtTravelInsur', { height: '100px', contentsCss: "p { margin:0px 0px 0px 0px; }" });

            //CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtVFlightDetails', { contentsCss: "p { margin:0px 0px 0px 0px; }" });
            //CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtVCarHire', { contentsCss: "p { margin:0px 0px 0px 0px; }" });
            //CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtVHotelInfo', { contentsCss: "p { margin:0px 0px 0px 0px; }" });
            //CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtVItinerary', { contentsCss: "p { margin:0px 0px 0px 0px; }" });
            //CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtVIncludes', { contentsCss: "p { margin:0px 0px 0px 0px; }" });
            //CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtVExcludes', { contentsCss: "p { margin:0px 0px 0px 0px; }" });
            //CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtVTravelInsurance', { height: '100px', contentsCss: "p { margin:0px 0px 0px 0px; }" });

            CKEDITOR.on('instanceReady', function () {
                $.each(CKEDITOR.instances, function (instance) {
                    CKEDITOR.instances[instance].on("change",
                        function (e) {
                            for (instance in CKEDITOR.instances)
                                CKEDITOR.instances[instance].updateElement();
                        });
                });
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

            CKEDITOR.replace('ctl00$ContentPlaceHolder1$txtMailTempNew', {
                toolbar:
            [
                { name: 'basicstyles', items: ['Bold', 'Italic'] },
                { name: 'paragraph', items: ['NumberedList', 'BulletedList'] },
                { name: 'tools', items: ['Maximize', '-', 'About'] }
            ],
                height: '300px'
            });



            $("#ContentPlaceHolder1_txtAdultPrice").blur(function () {
                var value = $(this).val();
                if (value != '') {
                    $('#ContentPlaceHolder1_ddlAdultPersons').removeAttr("disabled");
                    $('#ContentPlaceHolder1_lblGrandTotal').html($(this).val());
                }
                else {
                    alert("Enter Adult Price");
                    $('#ContentPlaceHolder1_ddlAdultPersons').attr("disabled", "disabled");
                }
            });



            $("#ContentPlaceHolder1_txtChildPrice").blur(function () {
                var value = $(this).val();
                if (value != '') {
                    $('#ContentPlaceHolder1_ddlChildPersons').removeAttr("disabled");
                }
                else {
                    alert("Enter Child Sharing Price");
                    $('#ContentPlaceHolder1_ddlChildPersons').attr("disabled", "disabled");
                }
            });

            $("#ContentPlaceHolder1_txtAdultPrice,#ContentPlaceHolder1_txtChildPrice").bind('keypress', function (e) {
                if (e.keyCode == '9' || e.keyCode == '16') {
                    return;
                }
                var code;
                if (e.keyCode) code = e.keyCode;
                else if (e.which) code = e.which;
                if (e.which == 46)
                    return false;
                if (code == 8 || code == 46)
                    return true;
                if (code < 48 || code > 57)
                    return false;
            });
        });

        function openModal() {
            $('#myModal').modal('show');
        }

        function TemplageModal() {
            $('#TemplateModal').modal('show');
        }
        function EmailModal() {
            $('#EmailModal').modal('show');
        }

    </script>
</asp:Content>

