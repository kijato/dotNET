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
using System.Collections.Generic;

namespace WindowsFormsSkeletonApplication
{
    class Program : Form
    {

        MainMenu mm = new MainMenu();

		SplitContainer splitContainer = new SplitContainer();
		
		TreeView treeView;
		ListView listView;
		
        StatusBar sb = new StatusBar();
        StatusBarPanel sbp1 = new StatusBarPanel();
        StatusBarPanel sbp2 = new StatusBarPanel();

		Form f = new Form();
		ListBox listBox1 = new ListBox();
		Label l = new Label();

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
            this.Size = new Size(500, 300);
            this.Text = Application.StartupPath;
            this.Font = new Font(FontFamily.GenericSansSerif, 8);

			MenuItem mi1 = new MenuItem(text: "&File");
			mi1.MenuItems.Add(new MenuItem("&Open directory", new EventHandler(mi2_Click)));
			mi1.MenuItems.Add(new MenuItem(text: "&Save", onClick: mi3_Click));
			mi1.MenuItems.Add(new MenuItem("-"));
			mi1.MenuItems.Add(new MenuItem(text: "&Exit", onClick: (sender, args) => Application.Exit()));
			mm.MenuItems.Add(mi1);
			this.Menu = mm;

            sb.Panels.Add(sbp1);
            sb.Panels.Add(sbp2);
            sb.ShowPanels = true;
            this.Controls.Add(sb);

            sbp1.Text = "-";
            sbp1.AutoSize = StatusBarPanelAutoSize.Spring;
            sbp2.Text = System.DateTime.Today.ToLongDateString();
            sbp2.AutoSize = StatusBarPanelAutoSize.Contents;


			splitContainer.Location = new Point(3, 3);
			splitContainer.Size = new Size( this.Width - 18 - 5, this.Height - sb.Height - 45 );
			splitContainer.BorderStyle = BorderStyle.FixedSingle;
			splitContainer.Anchor = ( AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right );
			splitContainer.Panel1.BackColor = Color.FloralWhite;
			splitContainer.Panel2.BackColor = Color.GhostWhite;
			splitContainer.Panel1.MouseMove += new MouseEventHandler(Panel_MouseMove);
			Controls.Add(splitContainer);

			treeView = new TreeView();
			treeView.Dock = DockStyle.Fill;
			/*TreeNode treeNode = new TreeNode("Windows");
			treeView.Nodes.Add(treeNode);
			treeNode = new TreeNode("Linux");
			treeView.Nodes.Add(treeNode);
			TreeNode node2 = new TreeNode("C#");
			TreeNode node3 = new TreeNode("VB.NET");
			TreeNode[] array = new TreeNode[] { node2, node3 };
			treeNode = new TreeNode("Dot Net Perls", array);
			treeView.Nodes.Add(treeNode);*/
			treeView.MouseDoubleClick += new MouseEventHandler(treeView_MouseDoubleClick);
			treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeView_NodeMouseClick);
			splitContainer.Panel1.Controls.Add(treeView);
			
			listView = new ListView();
			listView.Dock = DockStyle.Fill;
			listView.View = View.List;
			listView.GridLines = true;
			splitContainer.Panel2.Controls.Add(listView);

		}

		private void treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
			string[] row = { treeView.SelectedNode.Text };
			var listViewItem = new ListViewItem(row); 
			listView.Items.Add(listViewItem);
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
					listView.Clear();
					foreach (var row in getFileList(fbd.SelectedPath, "*.pdf"))
					{
						ListViewItem item = new ListViewItem(row.ToString());
						//for (int i = 1; i < data.Columns.Count; i++)
						//{
						//	item.SubItems.Add(row[i].ToString());
						//}
						listView.Items.Add(item);
					}
				}
			}
			
        }

        private void mi3_Click(object sender, EventArgs e)
        {
            /*MessageBox.Show( this.Font.ToString() +"\n"+ sb.Font.ToString() );
			
			listBox1.Width = f.Width - 25;
			listBox1.Location = new Point(5,5);
			foreach ( FontFamily oneFontFamily in FontFamily.Families )
			{
				listBox1.Items.Add(oneFontFamily.Name);
			}
			listBox1.DoubleClick += new EventHandler(ListBox1_DoubleClick);
			f.Controls.Add(listBox1);
			l.Location = new Point(5,listBox1.Top + listBox1.Height +5);
			f.Controls.Add(l);
			f.Show();*/


			PopulateTreeView(Directory.GetCurrentDirectory());
			
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            sbp1.Text = e.X + " " + e.Y;
        }

        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                 l.Text = listBox1.SelectedItem.ToString();
            }
        }
		
		

		private List<String> getFileList(String path, String filter)
		{
			List<String> fileList = new List<String>();
			ProcessDirectory(path, filter);
			return fileList;
			
			/* https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getdirectories?view=netframework-4.8 */
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

		}			

		
		
		// https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/creating-an-explorer-style-interface-with-the-listview-and-treeview
		private void PopulateTreeView(String path)
		{
			TreeNode rootNode;
			
			DirectoryInfo info = new DirectoryInfo(path);
			if (info.Exists)
			{
				rootNode = new TreeNode(info.Name);
				rootNode.Tag = info;
				GetDirectories(info.GetDirectories(), rootNode);
				treeView.Nodes.Add(rootNode);
			}
		}

		private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
		{
			TreeNode aNode;
			DirectoryInfo[] subSubDirs;
			foreach (DirectoryInfo subDir in subDirs)
			{
				aNode = new TreeNode(subDir.Name, 0, 0);
				aNode.Tag = subDir;
				aNode.ImageKey = "folder";
				subSubDirs = subDir.GetDirectories();
				if (subSubDirs.Length != 0)
				{
					GetDirectories(subSubDirs, aNode);
				}
				nodeToAddTo.Nodes.Add(aNode);
			}
		}

		void treeView_NodeMouseClick(object sender,
		TreeNodeMouseClickEventArgs e)
		{
			TreeNode newSelected = e.Node;
			listView.Items.Clear();
			DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
			ListViewItem.ListViewSubItem[] subItems;
			ListViewItem item = null;

			foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
			{
				item = new ListViewItem(dir.Name, 0);
				subItems = new ListViewItem.ListViewSubItem[]
					{new ListViewItem.ListViewSubItem(item, "Directory"),
					 new ListViewItem.ListViewSubItem(item,
						dir.LastAccessTime.ToShortDateString())};
				item.SubItems.AddRange(subItems);
				listView.Items.Add(item);
			}
			foreach (FileInfo file in nodeDirInfo.GetFiles())
			{
				item = new ListViewItem(file.Name, 1);
				subItems = new ListViewItem.ListViewSubItem[]
					{ new ListViewItem.ListViewSubItem(item, "File"),
					 new ListViewItem.ListViewSubItem(item,
						file.LastAccessTime.ToShortDateString())};

				item.SubItems.AddRange(subItems);
				listView.Items.Add(item);
			}

			listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}		

		
    }
}
