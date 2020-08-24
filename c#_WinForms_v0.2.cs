/*
#!/usr/bin/mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll
set CSC=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\
set CSC=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\Roslyn
set CSC=C:\Windows\Microsoft.NET\Framework64\v4.0.30319
set CSC=C:\Windows\Microsoft.NET\Framework64\v2.0.50727
set path=%CSC%;%path%
csc.exe -debug- -target:exe -platform:x64 -r:System.Windows.Forms.dll,System.Drawing.dll c#_WinForms_v0.2.cs && c#_WinForms_v0.2.exe
*/

using System;
using System.Windows.Forms;
using System.Drawing;

public class Program : Form
{

	static Button myButton;

	[STAThread]
	public static void Main()
	{
		try {
			//Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.Run(new Program());
		} catch (Exception e) {
			Console.WriteLine(e.Message);
		}
	}

	public Program()
	{
		Initcomponents();
	}

	private void Initcomponents()
	{
		this.Text = "myForm";
		this.Size = new Size(500, 400);
		this.MouseMove += new MouseEventHandler(MyMouseMove);
		this.Controls.Add()
		
		myButton = new Button { Location = new Point(Width-100, Height-70),	Text = "Kilépés", Anchor = ( AnchorStyles.Bottom | AnchorStyles.Right ) };
		myButton.Click += new EventHandler (ButtonClick);
		this.Controls.Add(myButton);

		}
	private static void ButtonClick (object sender, EventArgs e)
	{
		Application.Exit();
	}
	public void MyMouseMove(object sender, MouseEventArgs e)
	{
		Console.WriteLine( e.X + " " + e.Y ); // -target:exe !!!
	}

}
