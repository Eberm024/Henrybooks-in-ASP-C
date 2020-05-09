using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HenryBooks
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ddl_ModifyTable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btn_ModifyTable_Click(object sender, EventArgs e)
        {
            Application["ModifyTable"] = ddl_ModifyTable.SelectedValue;
            Response.Redirect("ModifyTable.aspx");
        }

        protected void btn_ViewTable_Click(object sender, EventArgs e)
        {
            Application["ViewTable"] = ddl_ViewTable.SelectedValue;
            Response.Redirect("ViewTable.aspx");
        }

        protected void btn_SearchTitle_Click(object sender, EventArgs e)
        {
            Application["SearchTitle"] = tb_SearchTitle.Text;
            Response.Redirect("SearchTitle.aspx");
        }

        
    }
}