<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyBikes.aspx.cs" Inherits="BikeDekho_Client.Views.MyBikes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td>
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Home" />
                    </td>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="My Bikes" Enabled="False" />
                    </td>
                    <td>
                        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Upload Bike" />
                    </td>
                    <td>
                        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Logout" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <br />
        </div>
    </form>
    
</body>
</html>
