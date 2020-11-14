using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Drawing;
using System.Configuration;

namespace AlumniiUT
{
    public partial class forgot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        SqlConnection con = new SqlConnection("Data Source=ERMIRA\\ERMIRA;Initial Catalog=AlumniUT;Integrated Security=True");
        
        protected void SendEmail(object sender, EventArgs e)
        {
            string sqlsuery = "select name,  password, username from admin where email=@email";
            SqlCommand cmd = new SqlCommand(sqlsuery, con);
            cmd.Parameters.AddWithValue("@email", TextBox1.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string username = dr["username"].ToString();
                string password = dr["password"].ToString();
                string name = dr["name"].ToString();
                MailMessage mm = new MailMessage("ermira62@gmail.com", TextBox1.Text);
                mm.Subject = "Your AlumniUT Info";
                mm.Body = string.Format("Hello {2} : Your username is: <h1> {0} </h1>  <br/> Your password is: <h1> {1} </h1>", username, password, name);
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;

                NetworkCredential nc = new NetworkCredential();
                nc.UserName = "ENTER YOUR GMAIL HERE";
                nc.Password = "ENTER YOUR GMAILS PASSWORD HERE";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = nc;
                smtp.Port = 587;
                smtp.Send(mm);
                Label1.Text = "Your AlumniUT username and password has been sent to your email!";
            }
            else
            {
                Label1.Text = "This email does not exist in our system!";
            }
            con.Close();







        }
    }
    }
