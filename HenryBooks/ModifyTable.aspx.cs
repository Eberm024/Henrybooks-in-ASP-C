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

    public partial class ModifyTable : System.Web.UI.Page
    {

        protected string finalSelection = "";
        protected string primaryKeyName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            String Selection = Application["ModifyTable"].ToString();

            
           if (String.Equals(Selection, "Book"))
            {
                //here in books
                finalSelection = Selection;
                primaryKeyName = "bookCode";
            }
            else if (String.Equals(Selection, "Publisher"))
            {
                //here in publishers
                finalSelection = Selection;
                primaryKeyName = "publisherCode";
            }
            else if (String.Equals(Selection, "Author"))
            {
                //here in Author
                finalSelection = Selection;
                primaryKeyName = "authorNum";
            }
            else if (String.Equals(Selection, "Copy"))
            {
                //here in Copy
                finalSelection = Selection;
                primaryKeyName = "copyNum";
            }
            else if (String.Equals(Selection, "Branch"))
            {
                //here in Branch
                finalSelection = Selection;
                primaryKeyName = "branchNum";
            }
            else if (String.Equals(Selection, "Wrote"))
            {
                //here in Wrote
                finalSelection = Selection;
                primaryKeyName = "bookCode";
                //the other key is authorNum
            }
            else if (String.Equals(Selection, "Inventory"))
            {
                //here in Inventory
                finalSelection = Selection;
                primaryKeyName = "bookCode";
                //the other is BranchNum
            }
            else
            {
                Response.Write("This is an error, no item." + "<br />");
            }

            //dynamic items
            string removeRowSelectedValue = "";

            if (!IsPostBack)
            {
                ddl_RemoveRow_Creation();
                ddl_UpdateEntryPrimaryKey_Creation();
                ddl_UpdateEntryColumn_Creation();
            }
                
            else
            {
                removeRowSelectedValue += Request.Form["ddl_RemoveRow"];
            }
                

        }

        protected void CurrentViewName()
        {
            Response.Write(finalSelection);
        }

        /** Creates the intial table based on the dropdown list provided
         * in the previous page.
         */
        protected void tableCreation()
        {
            //code for database display
            try
            {
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    DataTable table = new DataTable();
                    OdbcCommand command = new OdbcCommand("SELECT * FROM " + finalSelection + "", connection);
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
                     * 
                     * 
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

        /** Purpose of this function is to adjust the textbox input
         * given by the user and transform it into a compatible
         * format for MySQL syntax.
         * 
         * In short, it turns the following string:
         * 
         * hello, kevin, how
         * 
         * into the result below:
         * 
         * 'hello','kevin','how'
         */
        protected string textboxStringAdjustor(string str)
        {
            string final = "";
           
            string[] Split = str.Split(new Char[] { ',' });
            string[] newSplit = new string[Split.Length]; 
            int arraySize = Split.Length - 1;
            int arrayTraverse = arraySize;
            ;
            //separates and adjust split strings
            // starting from item 2 and foward, the space will become part of the token, eliminate that...
            for (arrayTraverse = arraySize; arrayTraverse >= 0; arrayTraverse--)
            {
               newSplit[arraySize - arrayTraverse] = "'" + Split[arraySize - arrayTraverse] + "',";
                if((arraySize - arrayTraverse) >= 1 )
                {
                    newSplit[arraySize - arrayTraverse] = newSplit[arraySize - arrayTraverse].Remove(1, 1);
                }
            }

            newSplit[newSplit.Length - 1] = "'" + Split[Split.Length - 1] + "'";
            newSplit[newSplit.Length - 1] = newSplit[newSplit.Length - 1].Remove(1, 1);

            //joins every split into a string
            for (arrayTraverse = arraySize; arrayTraverse >= 0; arrayTraverse--)
            {
                final += newSplit[arraySize - arrayTraverse];
            }
            return final;
        }

        /** Button for Add Row
         * First it finds the column names of the table
         * to determine the number of tokens the textbox should have.
         * Then, it determines the primary key the textbox should have.
         * If the user inserted primary key is equal to any primary key
         * that already exists, then it will prevent the row to be added.
         * Finally, it executes the query based on the information the
         * user inserted.
         */
        protected void btn_AddRow_Click(object sender, EventArgs e)
        {
            string textboxString = tb_AddRow.Text;

            //connection and command setup
            try
            {
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {

                    //code for identifying the table column names
                    //columnNameString will be used for names of table column
                    //countTokenNum will have number of columns for that table
                    string columnNameString = "";
                    int countTokenNum = 0;
                        connection.Open();
                        DataTable table = new DataTable();
                        OdbcCommand command = new OdbcCommand("SELECT * FROM " + finalSelection + "", connection);
                        OdbcDataAdapter adapter = new OdbcDataAdapter(command);
                        OdbcCommandBuilder builder = new OdbcCommandBuilder(adapter);
                        adapter.Fill(table);
                        //just setting up the fields names
                        foreach (DataColumn column in table.Columns)
                        {
                           countTokenNum++;
                           columnNameString += column.ColumnName + ", ";
                        }
                  columnNameString = columnNameString.Remove(columnNameString.Length - 2, 2);
                    
                    //separating string for number of inputs the user provided
                    int inputTokenNum = 0;
                    inputTokenNum = (textboxString.Split(',').Length - 1);
                    inputTokenNum += 1;


                    //extracting database primarykeys
                    DataTable primaryKeyTable = new DataTable();
                    OdbcCommand primaryKeyCommand = new OdbcCommand("SELECT "+ primaryKeyName + " FROM " + finalSelection + "", connection);
                    OdbcDataAdapter primaryKeyAdapter = new OdbcDataAdapter(primaryKeyCommand);
                    primaryKeyAdapter.Fill(primaryKeyTable);

                    //checker for primaryKeys
                    int quitFlag = 0;
                    string[] splitString = textboxString.Split(',');


                    foreach (DataRow row in primaryKeyTable.Rows)
                    {

                        foreach (DataColumn column in primaryKeyTable.Columns)
                        {
                            if (String.Equals(row[column].ToString(), splitString[0]))
                            {
                                quitFlag = 1;
                                Response.Write("PrimaryKey and new inserted primarykey are equal... <br> query ignored due to constraint violation.");
                            }
                            else
                            {
                                //do nothing quitFlag is at 0
                               
                            }
                        }
 
                    }
                    // end of checking primaryKeys

                    if(quitFlag == 0)
                    {
                        if (countTokenNum != inputTokenNum)
                        {
                            Response.Write("insert the proper number of elements... <br> query ignored.");
                        } 
                        else
                        {
                           
                           //code for Insert
                           DataTable table2 = new DataTable();
                           textboxString = textboxStringAdjustor(textboxString);
                           OdbcCommand command2 = new OdbcCommand("INSERT INTO " + finalSelection + " VALUES( " + textboxString + " )" + "", connection);
                           OdbcDataAdapter adapter2 = new OdbcDataAdapter(command2);
                           adapter2.Fill(table2);

                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("An error occured: " + ex.Message);
            }
        }

        /** Creation of DropDown List RemoveRow.
         * This function creates a dynamic dropdown list for the Remove row
         * calls a mysql script to fill in the data into the dropdown list
         */
        protected void ddl_RemoveRow_Creation()
        {

            //code for database display
            try
            {
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    DataTable table = new DataTable();
                    OdbcCommand command = new OdbcCommand("SELECT " + primaryKeyName + " FROM " + finalSelection + "", connection);
                    OdbcDataAdapter adapter = new OdbcDataAdapter(command);
                    adapter.Fill(table);

                    ddl_RemoveRow.DataSource = table;
                    ddl_RemoveRow.DataTextField = primaryKeyName;
                    ddl_RemoveRow.DataValueField = primaryKeyName;
                    ddl_RemoveRow.DataBind();
                    connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                Response.Write("An error occured: " + ex.Message);
            }
            
        }

        /** Button for Remove.
         * Event function for the Remove Row button
         * this button will delete the row with the specified
         * value in the drop down list from the database.
         */
        protected void btn_RemoveConfirm_Click(object sender, EventArgs e)
        {
            
           // Response.Write("DELETE FROM " + finalSelection + " WHERE " + primaryKeyName + " = " + "'" + ddl_RemoveRow.SelectedValue.ToString() + "'" + "");
            //code for database display
            
            if(ddl_RemoveRow.SelectedIndex == 0)
            {
                Response.Write("no selected value was chosen. Please choose a value.");
            }
            else
            {
                try
                {
                    using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                    {
                        connection.Open();
                        DataTable table = new DataTable();
                        //different command...
                        OdbcCommand command = new OdbcCommand("DELETE FROM " + finalSelection + " WHERE " + primaryKeyName  + " = " + "'" + ddl_RemoveRow.SelectedValue.ToString() + "'" + "", connection);
                        OdbcDataAdapter adapter = new OdbcDataAdapter(command);
                        adapter.Fill(table);

                        connection.Close();
                    
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("An error occured: " + ex.Message);
                }
            }
            
        }

        /** Creation of Dropdown List for UpdatePrimaryKey.
         * This function creates a dynamic dropdown list for the Update row
         * calls a mysql script to fill in the data into the dropdown list
         * This data will contain primary keys
         */
        protected void ddl_UpdateEntryPrimaryKey_Creation()
        {

            //code for database display
            try
            {
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    DataTable table = new DataTable();
                    OdbcCommand command = new OdbcCommand("SELECT " + primaryKeyName + " FROM " + finalSelection + "", connection);
                    OdbcDataAdapter adapter = new OdbcDataAdapter(command);
                    adapter.Fill(table);

                    ddl_UpdateEntryPrimaryKey.DataSource = table;
                    ddl_UpdateEntryPrimaryKey.DataTextField = primaryKeyName;
                    ddl_UpdateEntryPrimaryKey.DataValueField = primaryKeyName;
                    ddl_UpdateEntryPrimaryKey.DataBind();
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                Response.Write("An error occured: " + ex.Message);
            }

        }



        /** Creation of Dropdown list for UpdateEntryColumn. 
         * This function creates a dynamic dropdown list for the Update row
         * calls a mysql script to fill in the data into the dropdown list
         * This data will contain primary keys
         */
        protected void ddl_UpdateEntryColumn_Creation()
        {  
            //code for database display
            try
            {
                using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                {
                    connection.Open();
                    DataTable table = new DataTable();
                    OdbcCommand command = new OdbcCommand("SELECT COLUMN_NAME FROM information_schema.columns WHERE table_name = " + "\"" + finalSelection + "\"" + "", connection);
                    OdbcDataAdapter adapter = new OdbcDataAdapter(command);
                    adapter.Fill(table);

                    ddl_UpdateEntryColumn.DataSource = table;
                    ddl_UpdateEntryColumn.DataTextField = "COLUMN_NAME";
                    ddl_UpdateEntryColumn.DataValueField = "COLUMN_NAME";
                    ddl_UpdateEntryColumn.DataBind();
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                Response.Write("An error occured: " + ex.Message);
            }
            
        }

        /** Button for UpdateEntry. It executes the query to update
         * an entry based on the selected values of PrimaryKeys
         * and Column Name dropdown lists and based on the
         * text entered in the UpdateInput textbox.
         */
        protected void btn_UpdateConfirm_Click(object sender, EventArgs e)
        {
            if (ddl_UpdateEntryPrimaryKey.SelectedIndex == 0 && ddl_UpdateEntryColumn.SelectedIndex == 0)
            {
                Response.Write("no selected value was chosen for the two dropdown boxes. Please choose a value for them.");
            }
            else if (ddl_UpdateEntryPrimaryKey.SelectedIndex == 0)
            {
                Response.Write("no selected value was chosen for the primary key. Please choose a value.");
            }
            else if (ddl_UpdateEntryColumn.SelectedIndex == 0)
            {
                Response.Write("no selected value was chosen for the column to change. Please choose a value.");
            }
            else
            {
                //code for database display

                try
                {
                    using (OdbcConnection connection = new OdbcConnection(ConfigurationManager.ConnectionStrings["MySQLConnStr"].ConnectionString))
                    {
                        connection.Open();
                        DataTable table = new DataTable();
                        //different command...
                        OdbcCommand command = new OdbcCommand("UPDATE " + finalSelection + " SET " + ddl_UpdateEntryColumn.SelectedValue.ToString() + " = " + "'" + tb_UpdateInput.Text + "'" + " WHERE " + primaryKeyName + " = " + "'" + ddl_UpdateEntryPrimaryKey.SelectedValue.ToString() + "'" + "", connection);
                        OdbcDataAdapter adapter = new OdbcDataAdapter(command);
                        adapter.Fill(table);

                        connection.Close();

                    }
                }
                catch (Exception ex)
                {
                    Response.Write("An error occured: " + ex.Message);
                }
            }

        }

        protected void btn_GoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        
    }
}