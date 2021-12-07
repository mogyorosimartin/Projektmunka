using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;

namespace User_Login_CS
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                if (Request.QueryString["deleteID"] != null)
                {
                    int RespId = 0;
                    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.Delete_Saved_Jewelry"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Username", this.Page.User.Identity.Name);
                            cmd.Parameters.AddWithValue("@Id", Request.QueryString["deleteID"]);
                            cmd.Connection = con;
                            con.Open();
                            RespId = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                        }
                        switch (RespId)
                        {
                            case -1:
                                Failure.Text = "Nem létezik vagy nem hozzád tartozik ez a mentés, így nem törölhető.";
                                break;
                            default:
                                Failure.Text = "Mentés törölve.";
                                break;
                        }
                    }
                }


                BindData();
            }
        }

        protected void InsertSavedJewelry(object sender, EventArgs e)
        {
            if (TextBox_JewName.Text != "")
            {
                int RespId = 0;
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.Insert_Saved_Jewelry"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", this.Page.User.Identity.Name);
                        cmd.Parameters.AddWithValue("@Name", TextBox_JewName.Text);
                        cmd.Parameters.AddWithValue("@Model", model.Value);
                        cmd.Parameters.AddWithValue("@Color1r", color1r.Value);
                        cmd.Parameters.AddWithValue("@Color1g", color1g.Value);
                        cmd.Parameters.AddWithValue("@Color1b", color1b.Value);
                        cmd.Parameters.AddWithValue("@Color2r", color2r.Value);
                        cmd.Parameters.AddWithValue("@Color2g", color2g.Value);
                        cmd.Parameters.AddWithValue("@Color2b", color2b.Value);
                        cmd.Connection = con;
                        con.Open();
                        RespId = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }
                    switch (RespId)
                    {
                        case -1:
                            FailureSave.Text = "Valami hiba történt. A felhasználód nem létezik.";
                            break;
                        default:
                            BindData();
                            FailureSave.Text = "Mentés kész.";
                            break;
                    }
                }
            }
            else FailureSave.Text = "Nem adtál meg nevet a mentésnek.";
        }

        private DataTable GetData()
        {
            DataTable table = new DataTable();
            // get the connection
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                // write the sql statement to execute
                string sql = "select * from saved_jewelry where username='" + this.Page.User.Identity.Name + "'";
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

        private void BindData()
        {
            // specify the data source for the GridView
            GridView1.DataSource = GetData();
            // bind the data now
            GridView1.DataBind();
        }

        protected void PaginateGridView(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void SortRecords(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string direction = string.Empty;
            if (SortDirection == SortDirection.Ascending)
            {
                SortDirection = SortDirection.Descending;
                direction = " DESC";
            }
            else
            {
                SortDirection = SortDirection.Ascending;
                direction = " ASC";
            }
            DataTable table = GetData();
            table.DefaultView.Sort = sortExpression + direction;
            GridView1.DataSource = table;
            GridView1.DataBind();
        }

        public SortDirection SortDirection
        {
            get
            {
                if (ViewState["SortDirection"] == null)
                {
                    ViewState["SortDirection"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortDirection"];
            }
            set
            {
                ViewState["SortDirection"] = value;
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            InsertSavedJewelry(sender, e);
        }
    }
}