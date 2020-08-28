/*
#!/usr/bin/mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll

set CSC=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\
set CSC=c:\Windows\Microsoft.NET\Framework64\v4.0.30319\
set path=%csc%;%path%

csc.exe -debug+ -target:exe -r:System.Windows.Forms.dll,System.Drawing.dll -platform:x64 c#_properties_editor.cs && c#_properties_editor.exe

*/

using System;
using System.Windows.Forms;
using System.Drawing;

using System.IO;
using System.Text.RegularExpressions;
using System.Collections; // Hashtable()
using System.Collections.Generic; // List()
using System.Text; // Encoding
using System.Data; // DataTable
using System.Linq;

using System.Reflection; // https://codecharm.com/blog/archive/2017-11-19-c-assembly-automatic-versioning/
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0.002")]

[assembly: AssemblyTitle("WindowsFormsSkeletonApplication")]
//[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Kis János Tamás")]
[assembly: AssemblyProduct("WindowsFormsSkeletonApplication")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

public class Program : Form
{

	static MenuStrip menuStrip = new MenuStrip();
	static StatusStrip statusStrip = new StatusStrip();
	static ToolStripStatusLabel statusLabel = new ToolStripStatusLabel();
	static ToolStripStatusLabel statusLabel2 = new ToolStripStatusLabel();

	//static Dictionary<String,String> master = new Dictionary<String,String>();
	//static Dictionary<String,String> translated = new Dictionary<String,String>();
	static List<String> keys = new List<String>();

	//static DataGridView dataGridView = new DataGridView();
	static SplitContainer splitContainer = new SplitContainer();

	[STAThread]
	public static void Main() {
		try {
			//Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.Run(new Program());
		} catch (Exception e) {
			statusLabel.Text = e.Message;
		}
	}

	public Program() {
		Initcomponents();
	}

	private void Initcomponents() {
		Text = Assembly.GetExecutingAssembly().GetName().Name + " [v" + Assembly.GetExecutingAssembly().GetName().Version +"]";
		Size = new Size(600, 400);
		ToolStripMenuItem fileItem = new ToolStripMenuItem("&File");
		var openOriginalFileMenuItem = fileItem.DropDownItems.Add("Open &original...");
			openOriginalFileMenuItem.Click += new EventHandler (OpenMaster);
		var openTranslatedFileMenuItem = fileItem.DropDownItems.Add("Open &translated file...");
			openTranslatedFileMenuItem.Click += new EventHandler (OpenTranslated);
		var saveFileMenuItem = fileItem.DropDownItems.Add("&Save");
			//saveFileMenuItem.Enabled=false;
			saveFileMenuItem.Click += new EventHandler (SaveTranslated);

			// username.Click += (s, e) => SomeTextBox.Text = "test";
		var quitMenuItem = fileItem.DropDownItems.Add("&Quit");
			quitMenuItem.Click += new EventHandler (Quit);
		menuStrip.Items.Add(fileItem);

		ToolStripMenuItem editItem = new ToolStripMenuItem("&Edit");
		var convertToUTFMenuItem = editItem.DropDownItems.Add("to \\u00...");
			convertToUTFMenuItem.Click += new EventHandler (toUTF);
		var convertToNationalMenuItem = editItem.DropDownItems.Add("to national...");
			convertToNationalMenuItem.Click += new EventHandler (toNational);
		menuStrip.Items.Add(editItem);

		ToolStripMenuItem helpItem = new ToolStripMenuItem("&Help");
			helpItem.Enabled=false;
		var helpMenuItem = helpItem.DropDownItems.Add("About...");
			helpMenuItem.Enabled=false;
		menuStrip.Items.Add(helpItem);

		Controls.Add(menuStrip);

		statusStrip.Items.AddRange(new ToolStripItem[] {statusLabel});
		statusStrip.Items.AddRange(new ToolStripItem[] {statusLabel2});
		statusLabel2.Alignment = ToolStripItemAlignment.Right;
		statusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
		Controls.Add(statusStrip);

		// dataGridView.Location = new Point(5, 30);
		// dataGridView.Size = new Size(this.Width-25, this.Height-95);
		// dataGridView.Anchor = ( AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right );
		// dataGridView.ReadOnly = false;
		// dataGridView.MultiSelect = true;
		// dataGridView.AllowUserToAddRows = false;
		// dataGridView.AutoResizeColumns();
		// dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
		// dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		// Controls.Add(dataGridView);
		
		splitContainer.Location = new Point(5, 30);
		splitContainer.Size = new Size(this.Width-25, this.Height-95);
		splitContainer.Anchor = ( AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right );
		splitContainer.Panel1.BackColor = Color.LightGray;
		splitContainer.Panel2.BackColor = Color.GhostWhite;
		
		Controls.Add(splitContainer);
		
		this.Load += new System.EventHandler(this.Form1_Load);
		this.Click += new System.EventHandler(this.Form1_Click);
		this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
		
	}

	private void Quit (object sender, EventArgs e) {
		Application.Exit();
	}

	private void OpenMaster (object sender, EventArgs e) {
		OpenFileDialog openDialog = new OpenFileDialog();
		openDialog.Title = "Select A File";
		openDialog.Filter = "Properties Files (*.properties)|*.properties" + "|" +
							"All Files (*.*)|*.*";
		openDialog.InitialDirectory = Environment.CurrentDirectory;
		/*if ( openDialog.ShowDialog() == DialogResult.OK ) {
			statusLabel.Text = openDialog.FileName;
			master = openToDirectory(openDialog.FileName);
		}*/
    }

	private void OpenTranslated (object sender, EventArgs e) {
		OpenFileDialog openDialog = new OpenFileDialog();
		openDialog.Title = "Select A File";
		openDialog.Filter = "Properties Files (*.properties)|*.properties" + "|" +
							"All Files (*.*)|*.*";
		openDialog.InitialDirectory = Environment.CurrentDirectory;
		/*if ( openDialog.ShowDialog() == DialogResult.OK ) {
			statusLabel.Text = openDialog.FileName;
			translated = openToDirectory(openDialog.FileName);
		}*/

		/*dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

		dataGridView.Columns.Add("Keys", "Kulcs");
		foreach ( var k in keys ) {
			dataGridView.Rows.Add(k);
		}

		dataGridView.Columns.Add("masterCounter", "m"); int m=0;
		dataGridView.Columns.Add("masterValues", "Original");
		dataGridView.Columns.Add("translatedCounter", "t"); int t=0;
		dataGridView.Columns.Add("translatedValues", "Translated");
		foreach (DataGridViewRow dgrow in dataGridView.Rows) {
			dgrow.HeaderCell.Value = String.Format("{0}", dgrow.Index + 1);
			dgrow.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			if(  master.ContainsKey( dgrow.Cells["Keys"].Value.ToString() ) ) {
				dgrow.Cells["masterCounter"].Value = ++m ;
				dgrow.Cells["masterValues"].Value = master[ dgrow.Cells["Keys"].Value.ToString() ] ;
			} else {
				dgrow.Cells["masterValues"].Value = "" ;
				dgrow.Cells["masterValues"].Style.BackColor = Color.Snow;
			}
			if(  translated.ContainsKey( dgrow.Cells["Keys"].Value.ToString() ) ) {
				dgrow.Cells["translatedCounter"].Value = ++t ;
				dgrow.Cells["translatedValues"].Value = translated[ dgrow.Cells["Keys"].Value.ToString() ] ;
			} else {
				dgrow.Cells["translatedValues"].Value = "";
				dgrow.Cells["translatedValues"].Style.BackColor = Color.Wheat;
			}
		}
		dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells; // ColumnHeader, AllCells, AllCellsExceptHeader
		dataGridView.AutoSizeRowsMode    = DataGridViewAutoSizeRowsMode.DisplayedCells; // AllCells
		dataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);*/
    }

	private void SaveTranslated (object sender, EventArgs e) {
		SaveFileDialog saveDialog = new SaveFileDialog();
		saveDialog.Title = "Select A File";
		saveDialog.Filter = "Properties Files (*.properties)|*.properties" + "|" +
							"All Files (*.*)|*.*";
		saveDialog.InitialDirectory = Environment.CurrentDirectory;
		if ( saveDialog.ShowDialog() == DialogResult.OK ) {
			statusLabel.Text = saveDialog.FileName;
		}

		try {
			using (StreamWriter sw = new StreamWriter(saveDialog.FileName)) {

				/*foreach (DataGridViewRow dgrow in dataGridView.Rows) {
					//foreach (var k in keys) {
					//	sw.WriteLine(k.ToString());
					sw.WriteLine(dgrow.Cells["Keys"].Value+" = "+dgrow.Cells[2].Value);
					//}
				}*/

			}
			statusLabel.Text="Kulcsok..." + keys.Count;
		} catch (Exception e3) {
			statusLabel.Text=e3.Message;
		}
	}

    private Dictionary<String,String> openToDirectory(String fileName) {
		Dictionary<String,String> d = new Dictionary<String,String>();
		int i=0;
		try {
			string[] lines = File.ReadAllLines(fileName);
			string row = "";
			string brokenLine = "";
			int firstMark = 0;
			foreach (string line in lines) {
				i++;
				if ( line.TrimStart().StartsWith("#") || line.Length==0 ) continue;
				firstMark = line.IndexOf("=");
				if ( firstMark >= 0 ) {
					row = line.Trim();
					string[] keyAndValue = row.Split('=');
					keyAndValue[0]=keyAndValue[0].Trim();
					keyAndValue[1]=keyAndValue[1].Trim();
					d.Add(keyAndValue[0],keyAndValue[1] + brokenLine);
					//System.Console.WriteLine(i+"\t"+keyAndValue[0].Trim()+"\t"+keyAndValue[1].Trim()+"\n\t"+brokenLine);
					if( !keys.Contains(keyAndValue[0]) ) {
						keys.Add(keyAndValue[0]);
					}
					brokenLine = "";
				} else {
					brokenLine = line.TrimEnd();
					continue;
				}

			}
			//keys = keys.Distinct().ToList();
			System.Console.WriteLine(keys.Count.ToString());
			statusLabel2.Text = d.Count.ToString();
			statusStrip.Refresh();
		} catch (Exception e2) {
			statusLabel.Text=e2.Message + " (row num: " + i + ")";
		}
		return d;
	}


	private Dictionary<String,String> readKeyFile () {

		Dictionary<String,String> keys = new Dictionary<String,String>();

		try {
			using (StreamReader sr = new StreamReader("Win1250-UTF8.csv", Encoding.GetEncoding(1250))) {
				while (sr.Peek() >= 0) {
					String line = sr.ReadLine();
					if ( !line.StartsWith("#") ) {
						String[] cells = line.Split(';');
						keys.Add(cells[1],cells[0]);
					}
				}
			}
			statusLabel.Text="Kulcsok sz�ma:" + keys.Count;
		} catch (Exception e3) {
			statusLabel.Text=e3.Message;
		}

		return keys;

	}

	private void toNational (object sender, EventArgs e) {

		//statusLabel2.Text=e.ToString();

		int i=0;
		/*foreach (DataGridViewRow dgrow in dataGridView.Rows) {
			foreach (var k in readKeyFile()) {
				dgrow.Cells["translatedValues"].Value = dgrow.Cells["translatedValues"].Value.ToString().Replace(k.Key,k.Value);
			}
			statusLabel2.Text = ++i + "\\" + dataGridView.Rows.Count;
			statusStrip.Update();
		}*/
	}

	private void toUTF (object sender, EventArgs e) {

		//statusLabel2.Text=e.ToString();

		int i=0;
		/*foreach (DataGridViewRow dgrow in dataGridView.Rows) {
			foreach (var k in readKeyFile()) {
				dgrow.Cells["translatedValues"].Value = dgrow.Cells["translatedValues"].Value.ToString().Replace(k.Value,k.Key);
			}
			statusLabel2.Text = ++i + "\\" + dataGridView.Rows.Count;
			statusStrip.Update();
		}*/
	}


	
        private void Form1_Load(object sender, EventArgs e)
        {
            statusLabel.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            statusLabel.Text += "×";
        }

        private void Form1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
           statusLabel2.Text = e.X + " " + e.Y;
        }

		

}
