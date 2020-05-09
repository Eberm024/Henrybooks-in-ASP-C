using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Data.Odbc;
using System.Configuration;


namespace HenryBooks
{
    public partial class SearchTitle : System.Web.UI.Page
    {

        protected string Keyword = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //Retrieving contents from textbox from previous page.
            
            if (Application["SearchTitle"] != null)
            {
                Keyword = Application["SearchTitle"].ToString();
            }
        }

        /** 
         * Function whose only goal is to display the word
         * input from the user from the previous page
         */
         protected void displayUserTextbox()
        {
            Response.Write("\"" + Keyword + "\"");
        }

        /**
         * displayTitles will display the result of the searched titles and
         * all of the realted information, such as onHand inventory, authorname,
         * branch information, publisher information. It will only display
         * the result from the textbox from the previous page.
         */
        protected void displayTitles()
         {

            //end of retrieving contents from textbox from previous page

            /* I installed the MySQL Connector/ODBC then I had to configure the Web.config and add the following piece in there
             * <connectionStrings>
             * <add name="MySQLConnStr" connectionString="DRIVER={MySQL ODBC 3.51 Driver};Database=henrybooks;Server=localhost;UID=root;PWD=pilo;"/>
             * </connectionStrings>
             * 
             * include the following libraries:
             * using System.Data.Odbc;
             * using System.Configuration; // just for the ConfigurationManager
             * 
             * I also modified the default password by typing a query in MySQL
             * 
             * ALTER USER 'yourusername'@'localhost' IDENTIFIED WITH mysql_native_password BY 'yourpassword';
             * 
             * 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY 'pilo';
             * 
             * then the code below...
             */

            // test with obdc...
            string sqlSearchQuery = "SELECT DISTINCT B.title, I.onHand, BR.branchName, BR.branchLocation, P.publisherName, P.city, A.authorLast, A.authorFirst " +
                "FROM Book as B, Inventory as I, Branch as BR, Publisher as P, Author as A, Wrote as W, Copy as C " +
                "WHERE B.bookcode = I.bookcode AND I.branchNum = BR.branchNum AND B.bookCode = C.bookCode AND " +
                "C.branchNum = BR.branchNum AND B.publisherCode = P.publisherCode AND B.bookCode = W.bookCode AND W.authorNum = A.authorNum AND " +
                "INSTR(title, '" + Keyword + "')>0" + "";

            try
            {
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    DataTable table = new DataTable();
                    OdbcCommand command = new OdbcCommand(sqlSearchQuery, connection);
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
                        foreach (DataColumn column in table.Columns)
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

        protected void btn_GoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

    }
}