# Henrybooks-in-ASP-C
This project is personal and just to showcase the product of my work.
A project I made to manipulate MySQL databases in a web page. The web page's backend is built in C# and the frontend is built in ASP.NET.

Testing and Running the Web Application:
------------------------------------------

The HenryBooks folder contains all Visual Studio setup to run the website correctly.
The Database query folder contains simple query so that anyone is able to create the database that is used in the project.
Extract the folder in any place, install Visual Studio 2019 with the Web development settings (I had Community edition), install ODBC connector in the official MySQL website (this will give you the ODBC library), Open Visual Studio and open a project by selecting the .sln file from my HenryBooks folder ( HenryBooks.sln ), Modify the Web.config file (the line <connectionStrings>) to match your MySQL database information (my setup was Database=henrybooks;Server=localhost;UID=root;PWD=<password used for the master user>;), get to the Default.aspx and click the run button (the green arrow pointing to the right which is located at the top.)
