using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BikeDekho_Client.Views
{
    public partial class Register : System.Web.UI.Page
    {
        BikeDekhoService.ServiceClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new BikeDekhoService.ServiceClient();
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            client.Register(TextBox1.Text, TextBox2.Text, TextBox3.Text);
            Response.Redirect("Login.aspx");
        }
    }
}