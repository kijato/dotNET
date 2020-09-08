/*
set CSC=c:\Windows\Microsoft.NET\Framework64\v4.0.30319\
set CSC=c:\Program Files (x86)\MSBuild\12.0\Bin\amd64\
set CSC=c:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\
set path=%csc%;%path%
csc.exe -debug+ -target:exe -r:System.Windows.Forms.dll,System.Drawing.dll -platform:x64 WindowsFormsSkeletonApplication.cs && WindowsFormsSkeletonApplication.exe
c:\ProgramData\CS-Script\CSScriptNpp\1.7.24.0\Roslyn\csc.exe -debug- -target:exe -r:System.Windows.Forms.dll,System.Drawing.dll -platform:x64 WindowsFormsSkeletonApplication.cs
*/

using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
//using System.Collections.Generic;
using System.Xml.Linq;
using UglyToad.PdfPig;
using UglyToad.PdfPig.AcroForms;
using UglyToad.PdfPig.AcroForms.Fields;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Outline;
using UglyToad.PdfPig.Tokens;

namespace WindowsFormsSkeletonApplication
{
    class Program : Form
    {

		SplitContainer splitContainer;
		
		TreeView treeView;
		ListView listView;
		
        StatusBar sb;
        StatusBarPanel sbp1;
        StatusBarPanel sbp2;

		Form f;

	[STAThread]
        public static void Main(string[] args)
        {
            try
            {
                //Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.Run(new Program());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public Program()
        {
            f = new Form();
			this.Size = new Size(700, 400);
            this.Text = Application.StartupPath;
            this.Font = new Font(FontFamily.GenericSansSerif, 8);

			MainMenu mm = new MainMenu();
			MenuItem mi1 = new MenuItem(text: "&File");
			mi1.MenuItems.Add(new MenuItem("&Open directory", new EventHandler(mi2_Click)));
			mi1.MenuItems.Add(new MenuItem("&Save", new EventHandler(mi3_Click)));
			mi1.MenuItems.Add(new MenuItem("-"));
			mi1.MenuItems.Add(new MenuItem(text: "&Exit", onClick: (sender, args) => Application.Exit()));
			mm.MenuItems.Add(mi1);
			this.Menu = mm;

			StatusBar sb = new StatusBar();
			StatusBarPanel sbp1 = new StatusBarPanel();
			StatusBarPanel sbp2 = new StatusBarPanel();
            sb.Panels.Add(sbp1);
            sb.Panels.Add(sbp2);
            sb.ShowPanels = true;
            this.Controls.Add(sb);

            sbp1.Text = "-";
            sbp1.AutoSize = StatusBarPanelAutoSize.Spring;
            sbp2.Text = System.DateTime.Today.ToLongDateString();
            sbp2.AutoSize = StatusBarPanelAutoSize.Contents;

			splitContainer = new SplitContainer();
			splitContainer.Location = new Point(3, 3);
			splitContainer.Size = new Size( this.Width - 23, this.Height - sb.Height - 45 );
			splitContainer.BorderStyle = BorderStyle.FixedSingle;
			splitContainer.Anchor = ( AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right );
			splitContainer.Panel1.BackColor = Color.FloralWhite;
			splitContainer.Panel2.BackColor = Color.GhostWhite;
			//splitContainer.Panel1.MouseMove += new MouseEventHandler(Panel_MouseMove);
			Controls.Add(splitContainer);

			treeView = new TreeView();
			treeView.Dock = DockStyle.Fill;
			treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeView_NodeMouseClick);
			treeView.MouseDoubleClick += new MouseEventHandler(treeView_MouseDoubleClick);
			//treeView.MouseMove += new MouseEventHandler(treeView_MouseMove);
			splitContainer.Panel1.Controls.Add(treeView);
			
			listView = new ListView();
			listView.Dock = DockStyle.Fill;
			listView.View = View.List;
			listView.GridLines = true;
			splitContainer.Panel2.Controls.Add(listView);

			FillTreeView(Environment.CurrentDirectory,"*.pdf");
		}

        private void mi2_Click(object sender, EventArgs e)
        {
            using(var fbd = new FolderBrowserDialog())
			{
				fbd.SelectedPath = Environment.CurrentDirectory;//Directory.GetCurrentDirectory()
				fbd.ShowNewFolderButton = false;
				DialogResult result = fbd.ShowDialog();
				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
				{
					treeView.Nodes.Clear(); 
					//LoadDirectory(fbd.SelectedPath);
					FillTreeView(fbd.SelectedPath,"*.pdf");
				}
			}
        }

        private void mi3_Click(object sender, EventArgs e)
        {
        }

		private void treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
			string[] row = { treeView.SelectedNode.Text };
			var listViewItem = new ListViewItem(row); 
			listView.Items.Add(listViewItem);
        }
		
		void treeView_NodeMouseClick(object sender,	TreeNodeMouseClickEventArgs e)
		{
			using (PdfDocument document = PdfDocument.Open(fileName))
			{
			}
		}		

		void treeView_MouseMove(object sender, MouseEventArgs e)  
		{
			ToolTip toolTip = new ToolTip();
			// Get the node at the current mouse pointer location.  
			TreeNode theNode = treeView.GetNodeAt(e.X, e.Y);  
			// Set a ToolTip only if the mouse pointer is actually paused on a node.  
			if (theNode != null && theNode.Tag != null)  
			{  
			// Change the ToolTip only if the pointer moved to a new node.  
			if (theNode.Tag.ToString() != toolTip.GetToolTip(treeView))  
				toolTip.SetToolTip(treeView, theNode.Tag.ToString());  
			}  
			else     // Pointer is not over a node so clear the ToolTip.  
			{  
				toolTip.SetToolTip(treeView, "");  
			}
		}  

		public void FillTreeView(string path, string pattern)
		{
			LoadDirectory(path);
			
			void LoadDirectory(string Dir)
			{  
				DirectoryInfo di = new DirectoryInfo(Dir);  
				//Setting ProgressBar Maximum Value  
				//progressBar1.Maximum = Directory.GetFiles(Dir, "*.*", SearchOption.AllDirectories).Length + Directory.GetDirectories(Dir, "**", SearchOption.AllDirectories).Length;  
				TreeNode tds = treeView.Nodes.Add(di.Name);  
				tds.Tag = di.FullName;  
				tds.StateImageIndex = 0;  
				LoadFiles(Dir, tds);  
				LoadSubDirectories(Dir, tds);  
				}  			
			void LoadSubDirectories(string dir, TreeNode td)  
			{  
			   // Get all subdirectories  
			   string[] subdirectoryEntries = Directory.GetDirectories(dir);  
			   // Loop through them to see if they have any other subdirectories  
			   foreach (string subdirectory in subdirectoryEntries)  
			   {  
				   DirectoryInfo di = new DirectoryInfo(subdirectory);  
				   TreeNode tds = td.Nodes.Add(di.Name);  
				   tds.StateImageIndex = 0;  
				   tds.Tag = di.FullName;  
				   LoadFiles(subdirectory, tds);  
				   LoadSubDirectories(subdirectory, tds);  
				   //UpdateProgress();  
			   }  
			}			
			void LoadFiles(string dir, TreeNode td)  
			{  
				string[] Files = Directory.GetFiles(dir,pattern);  
				// Loop through them to see files  
				foreach (string file in Files)  
				{  
					if( !file.EndsWith(".pdf") )
						continue;
					FileInfo fi = new FileInfo(file);  
					TreeNode tds = td.Nodes.Add(fi.Name);  
					tds.Tag = fi.FullName;  
					tds.StateImageIndex = 1;  
					//UpdateProgress();  
				}  
			} 			
		}
		
		/*private List<String> getFileList(string path, string filter)
		{
			List<String> fileList = new List<String>();
			ProcessDirectory(path, filter);
			return fileList;
			
			// https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getdirectories?view=netframework-4.8
			// Process all files in the directory passed in, recurse on any directories that are found, and process the files they contain.
			void ProcessDirectory(string targetDirectory, string pattern)
			{
				// Process the list of files found in the directory.
				string [] fileEntries = Directory.GetFiles(targetDirectory);
				foreach(string fileName in fileEntries)
					ProcessFile(fileName);
				// Recurse into subdirectories of this directory.
				//string [] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
				string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory, pattern, SearchOption.TopDirectoryOnly);
				foreach(string subdirectory in subdirectoryEntries)
					ProcessDirectory(subdirectory, pattern);
			}
			// Insert logic for processing found files here.
			void ProcessFile(string fileName)
			{
				fileList.Add(Path.GetFullPath(fileName));
			}	
		}*/

	}
}
