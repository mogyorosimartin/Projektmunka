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

        protected void Page_Load(object sender, EventArgs e)
        {
            ListRefresh();
        }

        protected void ListRefresh()
        {
            DataTable ModelL = GetData_Models();
            string mlist = "";
            foreach (DataRow dtRow in ModelL.Rows)
            {
                string js = String.Format("createRing(scene,\"{0}\",\"-1\",\"-1\");setHiddenInputValues();", dtRow[1].ToString());
                mlist += String.Format("<button type = 'button'> <img src = '{0}' width = '100%' height = '100%' alt = '{1}' onclick = '{2}'/></button>", dtRow[2].ToString(), dtRow[1].ToString(), js);
            }
            ModelList.Text = mlist;

            DataTable MetalL = GetData_Mat();
            string melist = "";
            foreach (DataRow dtRow in MetalL.Rows)
            {
                string js = String.Format("createRing(scene,\"\",new BABYLON.Color3({0},{1},{2}),\"-1\");setHiddenInputValues();", dtRow[1].ToString().Replace(",", "."), dtRow[2].ToString().Replace(",", "."), dtRow[3].ToString().Replace(",", "."));
                melist += String.Format("<button type='button' style='background-color:{0}; ' onclick='{1}'></button>", dtRow[4].ToString(), js);
            }
            MetalList.Text = melist;

            DataTable GemL = GetData_Gem();
            string glist = "";
            foreach (DataRow dtRow in GemL.Rows)
            {
                string js = String.Format("createRing(scene,\"\",\"-1\",new BABYLON.Color3({0},{1},{2}));setHiddenInputValues();", dtRow[1].ToString().Replace(",", "."), dtRow[2].ToString().Replace(",", "."), dtRow[3].ToString().Replace(",", "."));
                glist += String.Format("<button type='button' style='background-color:{0};background-size:cover;' onclick='{1}'></button>", dtRow[4].ToString(), js);
            }
            GemList.Text = glist;
        }

        private DataTable GetData_Models()
        {
            DataTable table = new DataTable();
            // get the connection
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                // write the sql statement to execute
                string sql = "select * from models";
                // instantiate the command object to fire
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // get the adapter object and attach the command object to it
                    using (SqlDataAdapter sdt = new SqlDataAdapter(cmd))
                    {
                        // fire Fill method to fetch the data and fill into DataTable
                        sdt.Fill(table);
                    }
                }
            }

            return table;
        }
        private DataTable GetData_Mat()
        {
            DataTable table = new DataTable();
            // get the connection
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                // write the sql statement to execute
                string sql = "select * from materials";
                // instantiate the command object to fire
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // get the adapter object and attach the command object to it
                    using (SqlDataAdapter sdt = new SqlDataAdapter(cmd))
                    {
                        // fire Fill method to fetch the data and fill into DataTable
                        sdt.Fill(table);
                    }
                }
            }

            return table;
        }
        private DataTable GetData_Gem()
        {
            DataTable table = new DataTable();
            // get the connection
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                // write the sql statement to execute
                string sql = "select * from gems";
                // instantiate the command object to fire
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // get the adapter object and attach the command object to it
                    using (SqlDataAdapter sdt = new SqlDataAdapter(cmd))
                    {
                        // fire Fill method to fetch the data and fill into DataTable
                        sdt.Fill(table);
                    }
                }
            }

            return table;
        }
    }
}