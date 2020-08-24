/*
#!/usr/bin/mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll

set path=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\;%path%
°
csc.exe -debug+ -target:winexe -r:System.Windows.Forms.dll -r:System.Drawing.dll C#_Leica_frt_to_csv.cs

*/

using System;
using System.Windows.Forms;
using System.Drawing;

using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class Program
{
	static TextBox myTextBox;
	static Label myLabel;
	static Button myButton;

	static List<int> pontok = new List<int>();
	//myArrayList.Add(new 56);
	//Hashtable pont = new Hashtable();
	//openWith.Add("txt", "notepad.exe");

	[STAThread]
	public static void Main() {
		try {

		var f = new Form();
			f.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			f.Size = new Size(500, 400);

			myTextBox = new TextBox { Location = new Point(10, 15), Text = "", Width = f.Width-35, Height = f.Height-100, Multiline = true, WordWrap = false, ScrollBars = ScrollBars.Both, Anchor = ( AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right ) };

			f.Controls.Add(myTextBox);
			myTextBox.TextChanged += new EventHandler (TextChanged);

			myLabel = new Label { Location = new Point(10, f.Height-70), Text = "...", Width = f.Width-120, Anchor = ( AnchorStyles.Bottom | AnchorStyles.Left ) };
			f.Controls.Add(myLabel);

			myButton = new Button { Location = new Point(f.Width-100, f.Height-70), Text = "Megnyit..." , Anchor = ( AnchorStyles.Bottom | AnchorStyles.Right ) };
			f.Controls.Add(myButton);
			myButton.Click += new EventHandler (ButtonClick);

			Application.Run(f);

		} catch (Exception e) {
			myLabel.Text = e.Message;
		}
	}

	static private void TextChanged (object sender, EventArgs e) {
		//myLabel.Text = myTextBox.Text.Length.ToString();
	}

	static private void ButtonClick (object sender, EventArgs e) {
		OpenFileDialog openDialog = new OpenFileDialog();
		openDialog.Title = "Select A File";
		openDialog.Filter = "Text Files (*.txt)|*.txt" + "|" + 
							"All Files (*.*)|*.*";
		openDialog.InitialDirectory = Environment.CurrentDirectory;
		if ( openDialog.ShowDialog() == DialogResult.OK )
		{
			myLabel.Text = openDialog.FileName;
			try {
				//String file = File.ReadAllText(myLabel.Text);
				string[] lines = File.ReadAllLines(myLabel.Text);
				string row = "";

				foreach (string line in lines) {
					if ( line.Contains("Meghat") || line.Contains("Y=") || line.Contains("X=") || line.Contains("h=") || line.Contains("hibák") ) {
		// let stw be "John Smith $100,000.00 M"
		// sb_trim = Regex.Replace(stw, @"\s+\$|\s+(?=\w+$)", ","); 				//sb_trim becomes "John Smith,100,000.00,M"
		// sb_trim = Regex.Replace(sb_trim, @"(?<=\d),(?=\d)|[.]0+(?=,)", "");		//sb_trim becomes "John Smith,100000,M"
						row+=line;
						//System.Console.WriteLine(row);
						//System.Diagnostics.Debug.WriteLine(l);
						/*kvs  = l.Split(';');
						foreach ( string kv in kvs ) {
							myTextBox.AppendText("¤¤¤"+kv+"¤¤¤"+Environment.NewLine);
						}*/
					//} else if ( line.Contains("Pont meghatározás") ) {
					} else if ( (Regex.Match(line,@"-{20,}")).Success ) {
						row = Regex.Replace(row, @"(\s{2,}|\t+)", ";");
						row = Regex.Replace(row, @"=", ":");
						row+=Environment.NewLine;
						myTextBox.AppendText(row);
						row="";
					}
				}

				foreach ( string line in myTextBox.Text.Split('\n') ) {
					foreach ( string kv in line.Split(';') ) {
						string[] k = kv.Split(':');
						myTextBox.AppendText("¤¤¤"+k[0]+" -> "+k[1]+"¤¤¤"+Environment.NewLine);
					}
				}
				

			} catch (Exception e2) {
				System.Diagnostics.Debug.WriteLine(e2.Message);
			}
		}

		
    }

}