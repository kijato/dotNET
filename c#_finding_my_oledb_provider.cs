
using System;
using System.Data;
using System.Data.OleDb;

class Program {
	
	static void Main() {
		
		var oleEnum = new OleDbEnumerator();
		var elems = oleEnum.GetElements();

/*
	https://stackoverflow.com/questions/37849262/how-to-get-a-list-of-installed-ole-db-providers#61328613
*/
		if (elems != null && elems.Rows != null)

		foreach (System.Data.DataRow row in elems.Rows)
			if (!row.IsNull("SOURCES_NAME") && row["SOURCES_NAME"] is string)
				Console.WriteLine(row["SOURCES_NAME"]);

		Console.WriteLine("##################################");
		DisplayData(elems);  
	
	
	}

/*
	https://docs.microsoft.com/en-us/dotnet/api/system.data.oledb.oledbenumerator.getelements?view=netframework-4.8
*/

	static void DisplayData(DataTable table) {
		foreach (DataRow row in table.Rows) {
			foreach (DataColumn col in table.Columns) {
				Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
			}
			Console.WriteLine("==================================");
		}
	}
	
}
