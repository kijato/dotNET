/*
#!/usr/bin/mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll
set CSC=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\
set CSC=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\Roslyn
set CSC=C:\Windows\Microsoft.NET\Framework64\v4.0.30319
set CSC=C:\Windows\Microsoft.NET\Framework64\v2.0.50727
set path=%CSC%;%path%
csc.exe -debug- -target:winexe -platform:x64 -r:System.Windows.Forms.dll,System.Drawing.dll c#_WinForms_v0.03.cs && c#_WinForms_v0.03.exe
*/

using System;
using System.Windows.Forms;
using System.Drawing;

using System.Reflection; // https://codecharm.com/blog/archive/2017-11-19-c-assembly-automatic-versioning/
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.1.123")]

public class Program
{
	static Form f;
	static Label myLabel;
	static Button myButton;

	//[STAThread]
	public static void Main()
	{
		try {

			f = new Form();
			f.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			f.Size = new Size(500, 400);
			f.MouseMove += new System.Windows.Forms.MouseEventHandler(MouseMove);
            

			myLabel = new Label { Location = new Point(10, 10), Text = "...", Width = f.Width-120, Anchor = ( AnchorStyles.Bottom | AnchorStyles.Left ) };
			f.Controls.Add(myLabel);

			myButton = new Button { Location = new Point(f.Width-100, f.Height-70), Text = "Kilépés" , Anchor = ( AnchorStyles.Bottom | AnchorStyles.Right ) };
			f.Controls.Add(myButton);
			myButton.Click += new EventHandler (ButtonClick);

			Application.Run(f);

		} catch (Exception e) {
			myLabel.Text = e.Message;
		}
	}

	private static void ButtonClick (object sender, EventArgs e)
	{
		Application.Exit();
	}
	
	private static void MouseMove(object sender, MouseEventArgs e)
	{
		myLabel.Text = e.X + " " + e.Y;
	}	

}
