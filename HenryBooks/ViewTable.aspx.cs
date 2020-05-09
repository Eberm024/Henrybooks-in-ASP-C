using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Data.Odbc;
using System.Data;
using System.Configuration;


namespace HenryBooks
{
    public partial class ViewTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btn_GoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void btn_View_Click(object sender, EventArgs e)
        {
            if(String.Equals(DropDownTableList.SelectedValue, "No table selected..."))
            {
                // do nothing
            }
            else if(String.Equals(DropDownTableList.SelectedValue, "Book"))
            {
                //here in books
                Application["ViewTable"] = DropDownTableList.SelectedValue;
                Response.Redirect("ViewTable.aspx");
            }
            else if (String.Equals(DropDownTableList.SelectedValue, "Publisher"))
            {
                //here in publishers
                Application["ViewTable"] = DropDownTableList.SelectedValue;
                Response.Redirect("ViewTable.aspx");
            }
            else if (String.Equals(DropDownTableList.SelectedValue, "Author"))
            {
                //here in Author
                Application["ViewTable"] = DropDownTableList.SelectedValue;
                Response.Redirect("ViewTable.aspx");
            }
            else if (String.Equals(DropDownTableList.SelectedValue, "Copy"))
            {
                //here in Copy
                Application["ViewTable"] = DropDownTableList.SelectedValue;
                Response.Redirect("ViewTable.aspx");
            }
            else if (String.Equals(DropDownTableList.SelectedValue, "Branch"))
            {
                //here in Branch
                Application["ViewTable"] = DropDownTableList.SelectedValue;
                Response.Redirect("ViewTable.aspx");
            }
            else if (String.Equals(DropDownTableList.SelectedValue, "Wrote"))
            {
                //here in Wrote
                Application["ViewTable"] = DropDownTableList.SelectedValue;
                Response.Redirect("ViewTable.aspx");
            }
            else if (String.Equals(DropDownTableList.SelectedValue, "Inventory"))
            {
                //here in Inventory
                Application["ViewTable"] = DropDownTableList.SelectedValue;
                Response.Redirect("ViewTable.aspx");
            }
            else
            {
                Response.Write("This is an error, no dropdown item." + "<br />");
            }
        }

        protected void CurrentViewName()
        {
            //Retrieving contents from dropbox from previous page call. can be Default.aspx or ViewTable.aspx
            string viewSelectionName = "";
            if (Application["ViewTable"] != null)
            {
                viewSelectionName = Application["ViewTable"].ToString();
            }

            Response.Write(viewSelectionName);
        }

        protected void TableView()
        {
            //Retrieving contents from dropbox from previous page call. can be Default.aspx or ViewTable.aspx
            string viewSelection = "";
            if (Application["ViewTable"] != null)
            {
                viewSelection = Application["ViewTable"].ToString();
            }

            //code for database display
            try
            {
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    DataTable table = new DataTable();
                    OdbcCommand command = new OdbcCommand("SELECT * FROM " + viewSelection + "", connection);
                    OdbcDataAdapter adapter = new OdbcDataAdapter(command);
                    adapter.Fill(table);


                    Response.Write("<table>");
                    Response.Write("<tr>");
                    //just setting up the fields names
                    foreach (DataColumn column in table.Columns)
                    {
                        Response.Write("<td>" + column.ColumnName + "&nbsp;&nbsp;" + "</td>");
                    }
                    Response.Write("</tr>");

                    //lines below the column name
                    Response.Write("<tr>");
                    foreach (DataColumn column in table.Columns)
                    {
                        Response.Write("<td>" + "-------" + "&nbsp;&nbsp;" + "</td>");
                    }
                    Response.Write("</tr>");

                    /* Display is correct, now include the fields.
                     * each field will be added with this
                     */
                    foreach (DataRow row in table.Rows)
                    {
                        Response.Write("<tr>");
                        foreach(DataColumn column in table.Columns)
                        {
                            Response.Write("<td>" + row[column] + "&nbsp;&nbsp;" + "</td>");
                        }
                        Response.Write("</tr>");

                        //display separator
                        Response.Write("<tr>");
                        foreach (DataColumn column in table.Columns)
                        {
                            Response.Write("<td>" + "-------" + "&nbsp;&nbsp;" + "</td>");
                        }
                        Response.Write("</tr>");
                    }

                    Response.Write("</table>");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("An error occured: " + ex.Message);
            }
        }

        protected void DropDownTableList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}