using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BikeDekho_Client.Views
{
    public partial class Edit : System.Web.UI.Page
    {
        BikeDekhoService.ServiceClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new BikeDekhoService.ServiceClient();
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            Label5.Text = "";
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            byte[] data = FileUpload1.FileBytes;
            string fname = FileUpload1.FileName;
            if (data.Length < 20971520 && (fname.Contains(".jpg") || fname.Contains(".jpeg") || fname.Contains(".png")))
            {
                int id = int.Parse(Request.QueryString["id"].ToString());
                client.EditPost(TextBox1.Text, TextBox2.Text, Int32.Parse(TextBox4.Text), TextBox3.Text, data, id);
                Response.Redirect("MyBikes.aspx");
            }
            else if (data.Length > 20971520)
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