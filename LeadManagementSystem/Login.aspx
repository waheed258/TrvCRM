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
</head>
<body>
    <form id="form1" runat="server">
        <div class="agile-login">

            <div class="wrapper">
                <img id="logo" src="Scripts/LoginCSS/images/logo4.png" alt="Logo" style="height: 100px;" />
                <div class="w3ls-form">

                    <label>Username</label>
                    <input type="text" name="name" placeholder="Username" required />
                    <label>Password</label>
                    <input type="text" name="password" placeholder="Password" required />
                    <a href="#" class="pass">Forgot Password ?</a>
                    <input type="submit" value="LogIn" />

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
