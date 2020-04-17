using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BikeDekho_Client.Views
{
    public partial class UploadBike : System.Web.UI.Page
    {
        BikeDekhoService.ServiceClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new BikeDekhoService.ServiceClient();
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            Label5.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyBikes.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            byte[] data = FileUpload1.FileBytes;
            string fname = FileUpload1.FileName;
            if(data.Length < 20971520 && (fname.Contains(".jpg") || fname.Contains(".jpeg") || fname.Contains(".png")))
            {
                client.AddPost(TextBox1.Text, TextBox2.Text, Int32.Parse(TextBox4.Text), TextBox3.Text, data, (int)Session["Id"]);
                Response.Redirect("MyBikes.aspx");
            }
            else if(data.Length > 20971520)
            {
                Label5.Text = "*Please Enter a Photo < 20MB";
            }
            else
            {
                Label5.Text = "*Please Enter a Photo in type of jpg/jpeg/png";
            }
        }
    }
}