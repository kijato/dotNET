/*
#!/usr/bin/mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll
*/

using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public class HelloWorld : Form
{
    static MenuStrip menuStrip = new MenuStrip();
	
	static TextBox t;

    static public void Main()
    {
        try
        {
            Application.Run(new HelloWorld());
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public HelloWorld()
    {
        Button b = new Button { Location = new Point(20, 20) };
        b.Text = "Click Me!";
        b.Click += new EventHandler(Button_Click);
        Controls.Add(b);
		t = new TextBox { Location = new Point(20, 40), Size = new Size(250,200), Multiline = true, Anchor = ( AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right) };
		Controls.Add(t);

        ToolStripMenuItem fileItem = new ToolStripMenuItem("&File");
        var openOriginalFileMenuItem = fileItem.DropDownItems.Add("Open &original...");
        openOriginalFileMenuItem.Click += new EventHandler(FileMenuItem_Click);
        var saveFileMenuItem = fileItem.DropDownItems.Add("&Save");
        saveFileMenuItem.Enabled = false;
        var quitMenuItem = fileItem.DropDownItems.Add("&Quit");
        //	quitMenuItem.Click += new EventHandler (Quit);
        menuStrip.Items.Add(fileItem);
        Controls.Add(menuStrip);

    }

    private void FileMenuItem_Click(object sender, EventArgs e)
    {
        MessageBox.Show("FileMenuItem Clicked!");
    }

    private void Button_Click(object sender, EventArgs e)
    {
		// string[] files = Directory.GetFiles(@"c:\", "c*");
		// string[] dirs  = Directory.GetDirectories(@"c:\", "p*", SearchOption.TopDirectoryOnly);
		ProcessDirectory(Directory.GetCurrentDirectory());
		foreach(string file in fileList)
		t.AppendText(file+Environment.NewLine);
    }
	
	static List<String> fileList = new List<String>();
    /* https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getdirectories?view=netframework-4.8 */
	// Process all files in the directory passed in, recurse on any directories that are found, and process the files they contain.
	public static void ProcessDirectory(string targetDirectory)
    {
        // Process the list of files found in the directory.
        string [] fileEntries = Directory.GetFiles(targetDirectory);
        foreach(string fileName in fileEntries)
            ProcessFile(fileName);
        // Recurse into subdirectories of this directory.
        string [] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
        foreach(string subdirectory in subdirectoryEntries)
            ProcessDirectory(subdirectory);
    }
	// Insert logic for processing found files here.
    public static void ProcessFile(string fileName)
    {
        fileList.Add(Path.GetFullPath(fileName));
    }
	
}

/*

If you save this code as hello.cs, you would compile it like this:

csc hello.cs -r:System.Windows.Forms.dll

The compiler will create â€śhello.exeâ€ť, which you can run using:

mono hello.exe

*/
