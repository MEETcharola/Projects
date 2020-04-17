<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BikeDekho_Client.Views.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 308px;
        }
        .auto-style3 {
            width: 220px;
        }
        .auto-style4 {
            margin-left: 0px;
        }
        .auto-style5 {
            width: 221px;
        }
    </style>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3">
                        <asp:Button ID="Button1" runat="server" Text="Home" Enabled="False" />
                    </td>
                    <td class="auto-style5">
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="My Bikes" />
                    </td>
                    <td class="auto-style2">
                        <asp:Button ID="Button3" runat="server" CssClass="auto-style4" OnClick="Button3_Click" Text="Upload Bike" />
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
    <script>
        function Buy() {
            alert("You Bought This Item!");
        }
    </script>
</body>
</html>
