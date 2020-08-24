/*
#!/usr/bin/mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll
*/

/*

cd \oracle\product\11.2.0\client_64\BIN
c:\Windows\system32\regsvr32.exe OraOLEDB11.dll

set path=c:\Programs\ODAC122010Xcopy_x64;c:\Programs\ODAC122010Xcopy_x64\bin
C:\Windows\SysWOW64\Regsvr32.exe bin\OraOLEDB12.dll

*/

using System;
using System.Drawing;
using System.Windows.Forms;

using System.Data;
using System.Data.OleDb;

public class HelloWorld : Form
{
    static public void Main ()
    {
        Application.Run (new HelloWorld ());
    }

    public HelloWorld ()
    {
        Button b = new Button ();
        b.Text = "Click Me!";
        b.Location = new Point(10, 10);
        b.Click += new EventHandler (Button_Click);
        Controls.Add (b);

        Button b2 = new Button {Text="Teszt OraOLEDB",  Location = new Point(10, 50), Width = 100 };
        b2.Click += new EventHandler (Button_Click2);
        Controls.Add (b2);

    }

    private void Button_Click (object sender, EventArgs e)
    {
        MessageBox.Show ("Button Clicked!");
    }

    private void Button_Click2 (object sender, EventArgs e)
    {

		String oraclePath=@"c:\Programs\ODAC122010Xcopy_x64";
		if( !Environment.GetEnvironmentVariable("PATH").Contains(oraclePath) )
		{
			Environment.SetEnvironmentVariable("PATH", oraclePath+";"+oraclePath+"\\bin;"+Environment.GetEnvironmentVariable("PATH"));
			Environment.SetEnvironmentVariable("ORACLE_HOME", @"c:\Programs\ODAC122010Xcopy_x64");
		}
		MessageBox.Show (Environment.GetEnvironmentVariable("ORACLE_HOME")+"\n"+
		                 "----------------------------------------------------\n"+
		                 string.Join("\n",Environment.GetEnvironmentVariable("PATH").Split(';')));

		String sConnectionString = "Provider=OraOLEDB.Oracle; User ID=...; password=...; Data Source=192.168.101..../...; Persist Security Info=False";
		//String sConnectionString = "Provider=OraOLEDB.Oracle; User ID=...; password=...; Data Source=192.168.101..../...; Persist Security Info=False";
		String mySelectQuery = "";
		       //mySelectQuery = "SELECT * FROM dt_terkep where nev LIKE ?";
		       mySelectQuery = "SELECT kulcs, ertek FROM takaros.parameterek WHERE kulcs LIKE 'FH%' ORDER BY 1";
		//String mySelectQuery = "alter session set nls_date_format='YYYY.MM.DD HH24:MI:SS'";
		//       mySelectQuery = "select current_date, current_timestamp from dual";

		OleDbConnection myConnection = new OleDbConnection(sConnectionString);
		OleDbCommand myCommand = new OleDbCommand(mySelectQuery, myConnection);

//https://docs.microsoft.com/en-us/dotnet/api/system.data.oledb.oledbdatareader?view=dotnet-plat-ext-3.1

		//myCommand.Parameters.Add("@p1", OleDbType.Char, 9).Value = "%_jogerős";
		myConnection.Open();
		OleDbDataReader myReader = myCommand.ExecuteReader();
		try
		{
			if (myReader.RecordsAffected == 0)
			{
				MessageBox.Show("No data returned");
			}
			else
			{
				MessageBox.Show("Number of records returned: " + myReader.RecordsAffected);
			}
			while (myReader.Read())
			{
				for(int i=0;i<myReader.FieldCount;i++)
				{
					Console.Write(myReader[i].ToString()+"\t");
				}
				Console.WriteLine();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}
		finally
		{
		myReader.Close();
		myConnection.Close();
		}

    }
}

/*

If you save this code as hello.cs, you would compile it like this:

csc hello.cs -r:System.Windows.Forms.dll

The compiler will create “hello.exe”, which you can run using:

mono hello.exe

*/
