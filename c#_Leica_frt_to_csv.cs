/*
#!/usr/bin/mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll

set CSC=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\
set CSC=c:\Windows\Microsoft.NET\Framework64\v4.0.30319\
set path=%csc%;%path%

csc.exe -debug+ -target:winexe -r:System.Windows.Forms.dll -r:System.Drawing.dll C#_Leica_frt_to_csv.cs
csc.exe -debug+ -target:exe -r:System.Windows.Forms.dll,System.Drawing.dll -platform:x64 C#_Leica_frt_to_csv.cs && c#_leica_frt_to_csv.exe

*/

using System;
using System.Windows.Forms;
using System.Drawing;

using System.IO;
using System.Text.RegularExpressions;
using System.Collections; // for Hashtable()
using System.Collections.Generic; // for List()
using System.Text; // Encoding
using System.Data; // DataTable

using System.Reflection; // https://codecharm.com/blog/archive/2017-11-19-c-assembly-automatic-versioning/
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.1.123")]


public class Program
{

	//static Button myButton;
	static MenuStrip strip = new MenuStrip();
	static StatusStrip statusStrip = new StatusStrip();
	static ToolStripStatusLabel statusLabel = new ToolStripStatusLabel();	
	static ToolStripStatusLabel statusLabel2 = new ToolStripStatusLabel();	

	static List<Dictionary<String,String>> pontok;

	static DataGridView dataGridView = new DataGridView();
	
	[STAThread]
	public static void Main() {
		try {

		var f = new Form();
			f.Text = Assembly.GetExecutingAssembly().GetName().Name + " [v" +
					 Assembly.GetExecutingAssembly().GetName().Version +"]";
			f.Size = new Size(600, 400);
			/*
			myButton = new Button { Location = new Point(f.Width-100, f.Height-70), Text = "Megnyit..." , Anchor = ( AnchorStyles.Bottom | AnchorStyles.Right ) };
			f.Controls.Add(myButton);
			myButton.Click += new EventHandler (ButtonClick);
			*/
			ToolStripMenuItem fileItem = new ToolStripMenuItem("&Fájl");
			var openFileMenuItem = fileItem.DropDownItems.Add("&Megnyitás");
			openFileMenuItem.Click += new EventHandler (ButtonClick);
			var saveFileMenuItem = fileItem.DropDownItems.Add("M&entés");
				saveFileMenuItem.Enabled = false;
				// username.Click += (s, e) => SomeTextBox.Text = "test";
			var quitMenuItem = fileItem.DropDownItems.Add("&Kilépés");
				quitMenuItem.Click += new EventHandler (Quit);
			strip.Items.Add(fileItem);
			f.Controls.Add(strip);

			statusStrip.Items.AddRange(new ToolStripItem[] {statusLabel});
			statusStrip.Items.AddRange(new ToolStripItem[] {statusLabel2});
			statusLabel2.Alignment = ToolStripItemAlignment.Right;
			statusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			f.Controls.Add(statusStrip);

			dataGridView.Location = new Point(5, 30);
			dataGridView.Size = new Size(f.Width-25, f.Height-95);
			dataGridView.Anchor = ( AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right );
			dataGridView.ReadOnly = true;
			dataGridView.MultiSelect = true;
			dataGridView.AllowUserToAddRows = false;
			dataGridView.AutoResizeColumns();
			dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			f.Controls.Add(dataGridView);

			Application.Run(f);

		} catch (Exception e) {
			statusLabel.Text = e.Message;
		}
	}

	static private void Quit (object sender, EventArgs e) {
		Application.Exit();
	}

	static private void ButtonClick (object sender, EventArgs e) {

		OpenFileDialog openDialog = new OpenFileDialog();
		openDialog.Title = "Select A File";
		openDialog.Filter = "MJK Files (*.mjk)|*.mjk" + "|" + 
							"Text Files (*.txt)|*.txt" + "|" + 
							"All Files (*.*)|*.*";
		openDialog.InitialDirectory = Environment.CurrentDirectory;
		if ( openDialog.ShowDialog() == DialogResult.OK ) {

			statusLabel.Text = openDialog.FileName;

			int i=0;
			string row = String.Empty;
			try {
				//String file = File.ReadAllText(myLabel.Text);
				string[] lines = File.ReadAllLines(statusLabel.Text, Encoding.Default);

				pontok = new List<Dictionary<String,String>>();
				foreach (string line in lines) {
					
					i++;

					if ( line.Contains("Meghat") || line.Contains("Y=") || line.Contains("X=") || line.Contains("h=") || line.Contains("hib") || line.Contains("Dátum") ) {

						row+=line+";";

					} else if ( (Regex.Match(line,@"-{20,}")).Success ) {

						row = Regex.Replace(row, @"(\s{2,}|\t+)", ";");
						row = Regex.Replace(row, @"=", ":");

						//Hashtable pont = new Hashtable();
						 Dictionary<String,String> pont = new  Dictionary<String,String>();
						foreach ( string data in row.Split(';') ) {
							string[] kv = data.Split(':');
							if(kv.Length==2) pont.Add(kv[0],kv[1]);
						}
						 if(!pont.Count.Equals(0)) {
						 	pontok.Add(pont);
						 }
						row=String.Empty;
					}
	System.Console.Write("pontok feltöltése: "+i+"/"+lines.Length + "\r");
					statusLabel2.Text = i+"/"+lines.Length;
					statusStrip.Refresh();
				}
	System.Console.WriteLine("\nPontok száma: " + pontok.Count);
				
				// Adatok
				//dataGridView.AutoGenerateColumns = false;
				dataGridView.DataSource = null;
				//dataTable.Reset();
				//dataTable.Clear();
				DataTable dataTable = new DataTable();

				// Fejléc
				foreach ( var key in pontok[0].Keys ) {
					dataTable.Columns.Add(key.ToString(),typeof(string));
				}

				//foreach ( Hashtable p in pontok ) {
				i=0;
				foreach ( Dictionary<String,String>	 p in pontok ) {
					i++;
					//foreach (DictionaryEntry entry in p) {
					List<string> r = new List<string>();
					foreach (KeyValuePair<String,String> entry in p) {
						r.Add(entry.Value);
					}
					dataTable.Rows.Add(r.ToArray());
	System.Console.Write("dataTable feltöltés: "+i+"/"+pontok.Count + "\r");
				}
	System.Console.WriteLine("\ndataTable száma: " + dataTable.Rows.Count);
		        
	System.Console.WriteLine("table to grid..."+DateTime.Now.ToString("h:mm:ss tt"));
				dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Enélkül folyamatosan újraszámol, igazítja a cellákat, ami sokszorosára növeli az időigényt.
				dataGridView.DataSource = dataTable;
	System.Console.WriteLine("done."+DateTime.Now.ToString("h:mm:ss tt"));
				foreach (DataGridViewRow dgrow in dataGridView.Rows) {
		            dgrow.HeaderCell.Value = String.Format("{0}", dgrow.Index + 1);
                    dgrow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
		        }
				//dataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
				dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
				//dataGridView.Update();
				//dataGridView.Refresh();
	System.Console.WriteLine("->"+DateTime.Now.ToString("h:mm:ss tt"));

			} catch (Exception e2) {
				statusLabel.Text=e2.Message + " (sorszám: "+i+")";
				System.Console.WriteLine("["+row+"]");
			}
		}
		
    }

}