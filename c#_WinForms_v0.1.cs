/*
#!/usr/bin/mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll
set CSC=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\
set CSC=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\Roslyn
set CSC=C:\Windows\Microsoft.NET\Framework64\v4.0.30319
set CSC=C:\Windows\Microsoft.NET\Framework64\v2.0.50727
set path=%CSC%;%path%
csc.exe -debug- -target:winexe -platform:x64 -r:System.Windows.Forms.dll,System.Drawing.dll c#_WinForms_v0.1.cs && c#_WinForms_v0.1.exe
*/

using System;
using System.Windows.Forms;

public class Program : Form
{

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
	}

}
