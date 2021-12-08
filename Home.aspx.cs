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
                if (Request.QueryString["deleteID_Model"] != null)
                {
                    int RespId = 0;
                    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.Delete_Model"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Id", Request.QueryString["deleteID_Model"]);
                            cmd.Connection = con;
                            con.Open();
                            RespId = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                        }
                        switch (RespId)
                        {
                            case -1:
                                Model_Del.Text = "Nem létezik ez a model, így nem törölhető.";
                                break;
                            default:
                                Model_Del.Text = "Model törölve.";
                                break;
                        }
                    }
                }
                if (Request.QueryString["deleteID_Gem"] != null)
                {
                    int RespId = 0;
                    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.Delete_Gem"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Id", Request.QueryString["deleteID_Gem"]);
                            cmd.Connection = con;
                            con.Open();
                            RespId = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                        }
                        switch (RespId)
                        {
                            case -1:
                                Gem_Del.Text = "Nem létezik ez a drágakő, így nem törölhető.";
                                break;
                            default:
                                Gem_Del.Text = "Drágakő törölve.";
                                break;
                        }
                    }
                }
                if (Request.QueryString["deleteID_Mat"] != null)
                {
                    int RespId = 0;
                    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.Delete_Mat"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Id", Request.QueryString["deleteID_Mat"]);
                            cmd.Connection = con;
                            con.Open();
                            RespId = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                        }
                        switch (RespId)
                        {
                            case -1:
                                Mat_Del.Text = "Nem létezik ez a fém, így nem törölhető.";
                                break;
                            default:
                                Mat_Del.Text = "Fém törölve.";
                                break;
                        }
                    }
                }

                BindData();
                if (Is_User_Admin())
                {
                    BindData_Models();
                    BindData_Gem();
                    BindData_Mat();
                }

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
        }

        protected bool Is_User_Admin()
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = '" + this.Page.User.Identity.Name + "' AND admin=1";

                using (var cmd = new SqlCommand(query, con))
                {
                    int rowsAmount = (int)cmd.ExecuteScalar();
                    if (rowsAmount > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
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

        protected void InsertModel(object sender, EventArgs e)
        {
            if (Is_User_Admin())
            {
                if (TextBox_ModelName.Text != "")
                {
                    int RespId = 0;
                    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.Insert_Model"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Name", TextBox_ModelName.Text);
                            cmd.Parameters.AddWithValue("@Img", TextBox_ModelImg.Text);
                            cmd.Connection = con;
                            con.Open();
                            RespId = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                        }
                        switch (RespId)
                        {
                            default:
                                Model_Resp.Text = "Model hozzáadva.";
                                BindData_Models();
                                break;
                        }
                    }
                }
                else Model_Resp.Text = "Nem adtál meg nevet a modelnek.";
            }
        }

        protected void InsertGem(object sender, EventArgs e)
        {
            if (Is_User_Admin())
            {
                int RespId = 0;
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.Insert_Gem"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Colorr", TextBox_DFColorr.Text);
                        cmd.Parameters.AddWithValue("@Colorg", TextBox_DFColorg.Text);
                        cmd.Parameters.AddWithValue("@Colorb", TextBox_DFColorb.Text);
                        cmd.Parameters.AddWithValue("@Hexcolor", TextBox_DFHexcolor.Text);
                        cmd.Connection = con;
                        con.Open();
                        RespId = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }
                    switch (RespId)
                    {
                        default:
                            BindData_Gem();
                            GemMat_Resp.Text = "Drágakő hozzáadva.";
                            break;
                    }
                }
            }
        }

        protected void InsertMat(object sender, EventArgs e)
        {
            if (Is_User_Admin())
            {
                int RespId = 0;
                string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.Insert_Mat"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Colorr", TextBox_DFColorr.Text);
                        cmd.Parameters.AddWithValue("@Colorg", TextBox_DFColorg.Text);
                        cmd.Parameters.AddWithValue("@Colorb", TextBox_DFColorb.Text);
                        cmd.Parameters.AddWithValue("@Hexcolor", TextBox_DFHexcolor.Text);
                        cmd.Connection = con;
                        con.Open();
                        RespId = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }
                    switch (RespId)
                    {
                        default:
                            BindData_Mat();
                            GemMat_Resp.Text = "Fém hozzáadva.";
                            break;
                    }
                }
            }
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

        private void BindData_Models()
        {
            // specify the data source for the GridView
            GridView2.DataSource = GetData_Models();
            // bind the data now
            GridView2.DataBind();
        }

        protected void PaginateGridView_Models(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            this.BindData_Models();
        }

        protected void SortRecords_Models(object sender, GridViewSortEventArgs e)
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
            DataTable table = GetData_Models();
            table.DefaultView.Sort = sortExpression + direction;
            GridView2.DataSource = table;
            GridView2.DataBind();
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

        private void BindData_Mat()
        {
            // specify the data source for the GridView
            GridView3.DataSource = GetData_Mat();
            // bind the data now
            GridView3.DataBind();
        }

        protected void PaginateGridView_Mat(object sender, GridViewPageEventArgs e)
        {
            GridView3.PageIndex = e.NewPageIndex;
            this.BindData_Models();
        }

        protected void SortRecords_Mat(object sender, GridViewSortEventArgs e)
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
            DataTable table = GetData_Mat();
            table.DefaultView.Sort = sortExpression + direction;
            GridView3.DataSource = table;
            GridView3.DataBind();
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

        private void BindData_Gem()
        {
            // specify the data source for the GridView
            GridView4.DataSource = GetData_Gem();
            // bind the data now
            GridView4.DataBind();
        }

        protected void PaginateGridView_Gem(object sender, GridViewPageEventArgs e)
        {
            GridView4.PageIndex = e.NewPageIndex;
            this.BindData_Gem();
        }

        protected void SortRecords_Gem(object sender, GridViewSortEventArgs e)
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
            DataTable table = GetData_Gem();
            table.DefaultView.Sort = sortExpression + direction;
            GridView4.DataSource = table;
            GridView4.DataBind();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            InsertSavedJewelry(sender, e);
        }

        protected void Model_Add_Click(object sender, EventArgs e)
        {
            InsertModel(sender, e);
        }

        protected void GemMat_Add_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue == "1") InsertGem(sender, e);
            else InsertMat(sender, e);
        }
    }
}