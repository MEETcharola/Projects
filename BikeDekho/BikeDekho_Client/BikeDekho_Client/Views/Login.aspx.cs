using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BikeDekho_Client.Views
{
    public partial class Login : System.Web.UI.Page
    {
        BikeDekhoService.ServiceClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new BikeDekhoService.ServiceClient();
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int id;
            id = client.Login(TextBox2.Text, TextBox3.Text);
            if (id != -1)
            {
                Session["id"] = id;
                Response.Redirect("Home.aspx");
            }
            else
            {
                Label.Text = "* Please Enter Correct Email and Password";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}