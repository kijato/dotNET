/*
#!/usr/bin/mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll
set CSC=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\
set CSC=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\Roslyn
set CSC=C:\Windows\Microsoft.NET\Framework64\v4.0.30319
set CSC=C:\Windows\Microsoft.NET\Framework64\v2.0.50727
set path=%CSC%;%path%
csc.exe -debug- -target:winexe -platform:x64 -r:System.Windows.Forms.dll,System.Drawing.dll c#_WinForms_v0.02.cs && c#_WinForms_v0.02.exe
*/

using System;
using System.Windows.Forms;
using System.Drawing; // Size(), Point()

public class Program
{
	static Form f;
	static Label myLabel;
	static Button myButton;

	public static void Main()
	{
		try {

			f = new Form();
			f.Size = new Size(500, 400);
			f.MouseMove += new MouseEventHandler(MouseMove);

			myLabel = new Label[] { Location = new Point(10, 10), Text = "...", Width = f.Width-120, Anchor = ( AnchorStyles.Bottom | AnchorStyles.Left ) };
			f.Controls.Add(myLabel);

			myButton = new Button[] { Location = new Point(f.Width-100, f.Height-70), Text = "Kilépés" , Anchor = ( AnchorStyles.Bottom | AnchorStyles.Right ) };
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

/*

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe  -debug- -target:winexe -platform:x64 -r:System.Windows.Forms.dll,System.Drawing.dll c#_WinForms_v0.02.cs && c#_WinForms_v0.02.exe
Microsoft (R) Visual C# Compiler version 4.8.3761.0
for C# 5
Copyright (C) Microsoft Corporation. All rights reserved.

This compiler is provided as part of the Microsoft (R) .NET Framework, but only supports language versions up to C# 5, which is no longer the latest version. For compilers that support newer versions of the C# programming language, see http://go.microsoft.com/fwlink/?LinkID=533240

c#_WinForms_v0.02.cs(29,28): error CS0103: A(z) "Location" név nem szerepel ebben a környezetben.
c#_WinForms_v0.02.cs(29,58): error CS0103: A(z) "Text" név nem szerepel ebben a környezetben.
c#_WinForms_v0.02.cs(29,72): error CS0103: A(z) "Width" név nem szerepel ebben a környezetben.
c#_WinForms_v0.02.cs(29,93): error CS0103: A(z) "Anchor" név nem szerepel ebben a környezetben.
c#_WinForms_v0.02.cs(32,30): error CS0103: A(z) "Location" név nem szerepel ebben a környezetben.
c#_WinForms_v0.02.cs(32,78): error CS0103: A(z) "Text" név nem szerepel ebben a környezetben.
c#_WinForms_v0.02.cs(32,97): error CS0103: A(z) "Anchor" név nem szerepel ebben a környezetben.

*/