using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BikeDekho_Client.Views
{
    public partial class Home : System.Web.UI.Page
    {
        BikeDekhoService.ServiceClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            client = new BikeDekhoService.ServiceClient();
            CreateTable();
        }

        void CreateTable()
        {
            if (!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Model", typeof(string)),
                    new DataColumn("Company", typeof(string)),
                    new DataColumn("Price", typeof(int)),
                    new DataColumn("Details", typeof(string)),
                    new DataColumn("Photo", typeof(string)),
                    new DataColumn("Operation", typeof(string)) });

                var list = client.FetchAllUserBikes();
                foreach (var i in list)
                {
                    dt.Rows.Add(i.Model, i.Company, i.Price, i.Details, "<a style= 'color:blue;text-decoration:none;' href= 'Image.aspx?id=" + i.Id + "'>View Photo</ a >", "<button onclick=Buy()>Buy</button>");
                }

                StringBuilder sb = new StringBuilder();
                //Table start.
                sb.Append("<div id='t'><table align='center' style='width: 80%' cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 9pt;font-family:Arial'>");

                //Adding HeaderRow.
                sb.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.ColumnName + "</th>");
                }
                sb.Append("</tr>");


                //Adding DataRow.
                foreach (DataRow row in dt.Rows)
                {
                    sb.Append("<tr>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + row[column.ColumnName].ToString() + "</td>");
                    }
                    sb.Append("</tr>");
                }

                //Table end.
                sb.Append("</table></div>");
                Literal1.Text = sb.ToString();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyBikes.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("UploadBike.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}