/*
#!/usr/bin/mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll

/usr/bin/mcs -debug+ -target:winexe -r:System.Windows.Forms.dll -r:System.Drawing.dll c#_winforms_DEGREE_SIGN_FAIL.cs.cs
/usr/bin/mcs -debug+ -target:winexe -r:System.Windows.Forms.dll -r:System.Drawing.dll -codepage:65001 -langversion:Experimental -out:c#_winforms_DEGREE_SIGN_FAIL.cs.exe c#_winforms_DEGREE_SIGN_FAIL.cs
	Char 	Dec 	Hex 	Entity 	Name			URL	
	°		176		00B0	&deg;	DEGREE SIGN		https://www.w3schools.com/charsets/ref_utf_latin1_supplement.asp	
	℃		8451	2103	-		DEGREE CELSIUS	https://www.w3schools.com/charsets/ref_utf_letterlike.asp

*/

using System;
using System.Windows.Forms;
using System.Drawing;

public class Program : Form
{
	static TextBox myTextBox;
	static Label myLabel;
	
	[STAThread]
    public static void Main()
    {
        try {

			var f = new Form();
			f.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			f.Size = new Size(400, 300);
			
			myTextBox = new TextBox { Location = new Point(10, 20), Text = "-", Width = 365, Height = 200, Multiline = true, WordWrap = true, ScrollBars = ScrollBars.Both, Anchor = ( AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right ) };
			
			f.Controls.Add(myTextBox);
			myTextBox.TextChanged += new EventHandler (OnChange);
			myTextBox.Click += new EventHandler (OnClick);

			myLabel = new Label { Location = new Point(10, 230), Text = "..." , Anchor = ( AnchorStyles.Bottom | AnchorStyles.Left ) };
			f.Controls.Add(myLabel);
			
			Application.Run(f);

		} catch (Exception e) {
			myLabel.Text = e.ToString();
		}
    }
	
	static private void OnChange (object sender, EventArgs e)
	{
        myLabel.Text = myTextBox.Text.Length.ToString();
    }
	
	static private void OnClick (object sender, EventArgs e)
	{
        //myLabel.Text = "";
        myLabel.ForeColor = System.Drawing.Color.Red;
    }

}
