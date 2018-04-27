<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Meta tags -->
    <title>Login</title>
    <meta name="keywords" content="Apps Login Form Responsive widget, Flat Web Templates, Android Compatible web template, 
	Smartphone Compatible web template, free webdesigns for Nokia, Samsung, LG, SonyEricsson, Motorola web design" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- stylesheets -->
    <link rel="stylesheet" href="Scripts/LoginCSS/css/font-awesome.css" />

    <link rel="stylesheet" href="Scripts/LoginCSS/css/style.css" />
    <!-- google fonts  -->
    <link href="//fonts.googleapis.com/css?family=Open+Sans" rel="stylesheet" />
    <link href="//fonts.googleapis.com/css?family=Raleway:400,500,600,700" rel="stylesheet" />
    <style>
        input[type="password"] {
            outline: 0;
            padding: 12px 15px;
            border: 1px solid #000;
            width: 100%;
            letter-spacing: 1px;
            box-sizing: border-box;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="agile-login">

            <div class="wrapper">
                <img id="logo" src="Scripts/LoginCSS/images/logo4.png" alt="Logo" style="height: 100px;" />
                <div class="w3ls-form">
                    <label>Username</label>
                    <asp:TextBox ID="txtUserName" placeholder="Login ID" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Login ID" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    <label>Password</label>
                    <asp:TextBox ID="txtPassword" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ForeColor="#d0582e"
                                ErrorMessage="Please Enter Password" ValidationGroup="Consultant" Display="Dynamic"></asp:RequiredFieldValidator>
                    <div style="margin-top:15px;text-align:center;">
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    </div>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" ValidationGroup="Consultant"/>
                </div>

                <div class="agile-icons">
                    <a href="#"><span class="fa fa-twitter" aria-hidden="true"></span></a>
                    <a href="#"><span class="fa fa-facebook"></span></a>
                    <a href="#"><span class="fa fa-pinterest-p"></span></a>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
