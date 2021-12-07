using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace User_Login_CS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void ValidateUser(object sender, EventArgs e)
        {
            if (TextBox_UserName.Text != "" && TextBox_Password.Text != "")
            {
                int userId = 0;
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.Validate_User"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", TextBox_UserName.Text);
                        cmd.Parameters.AddWithValue("@Password", TextBox_Password.Text);
                        cmd.Connection = con;
                        con.Open();
                        userId = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }
                    switch (userId)
                    {
                        case -1:
                            Failure.Text = "Felhasználó és/vagy jelszó hibás.";
                            break;
                        default:
                            FormsAuthentication.RedirectFromLoginPage(TextBox_UserName.Text, true);
                            break;
                    }
                }
            }
            else Failure.Text = "Nem adtál meg felhasználót és/vagy jelszót.";
        }

        protected void InsertUser(object sender, EventArgs e)
        {
            if (TextBox_UserName.Text != "" && TextBox_Password.Text != "")
            {
                int userId = 0;
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.Insert_User"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", TextBox_UserName.Text);
                        cmd.Parameters.AddWithValue("@Password", TextBox_Password.Text);
                        cmd.Connection = con;
                        con.Open();
                        userId = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }
                    switch (userId)
                    {
                        case -1:
                            Failure.Text = "Felhasználó már létezik ilyen névvel.";
                            break;
                        default:
                            Failure.Text = "Felhasználó elkészült.";
                            break;
                    }
                }
            } else Failure.Text = "Nem adtál meg felhasználót és/vagy jelszót.";
        }

        protected void Login1_Click(object sender, EventArgs e)
        {
            ValidateUser(sender, e);
        }

        protected void Register1_Click(object sender, EventArgs e)
        {
            InsertUser(sender, e);
        }
    }
}