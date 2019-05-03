<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeOutPage.aspx.cs" Inherits="HijoPortal.TimeOutPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <style>
        body {
            margin: 0;
            padding: 0;
            background-color: rgb(230,239,247);
            background: url('hijosign.png');
            background-repeat: repeat;
            /*font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;*/
            font-family: 'Open Sans';
            overflow: hidden;
            background-color: rgb(230,239,247);
        }

            body label {
                color: white;
                font-size: 12px;
                /*font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;*/
                font-family: 'Open Sans';
            }
    </style>
    <title>Session Expired</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding:10px;">
            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text=" Sorry, your Session has expired." Theme="Moderno"></dx:ASPxLabel>
            <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" NavigateUrl="default.aspx" Text="Go to Log In Page" Theme="Moderno">
            </dx:ASPxHyperLink>
        </div>
    </form>
</body>
</html>
