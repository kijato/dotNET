using System;
using System.Windows.Forms;
using System.Drawing;

/*
set CSC=c:\Windows\Microsoft.NET\Framework64\v4.0.30319\
set CSC=c:\Program Files (x86)\MSBuild\12.0\Bin\amd64\
set CSC=c:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\
set path=%csc%;%path%
csc.exe -debug+ -target:exe -r:System.Windows.Forms.dll,System.Drawing.dll -platform:x64 WindowsFormsSkeletonApplication.cs && WindowsFormsSkeletonApplication.exe
c:\ProgramData\CS-Script\CSScriptNpp\1.7.24.0\Roslyn\csc.exe -debug- -target:exe -r:System.Windows.Forms.dll,System.Drawing.dll -platform:x64 WindowsFormsSkeletonApplication.cs
*/

namespace WindowsFormsSkeletonApplication
{
    class Program : Form
    {

        MainMenu mm = new MainMenu();
		MenuItem mi1;

        Panel p = new Panel();
		
		SplitContainer splitContainer = new SplitContainer();
		
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
            this.Text = Application.StartupPath;
            this.Font = new Font(FontFamily.GenericSansSerif, 10);

            mi1 = new MenuItem(text: "&File");
		mm.MenuItems.Add(mi1);
			MenuItem mi2 = new MenuItem("&Open");
			mi2.Click += new EventHandler(mi2_Click);
			mi1.MenuItems.Add(mi2);
			mi1.MenuItems.Add(new MenuItem(text: "&Save", onClick: mi3_Click));
			mi1.MenuItems.Add("----");
			mi1.MenuItems.Add(new MenuItem(text: "&Exit", onClick: (sender, args) => Application.Exit()));
            this.Menu = mm;

            sb.Panels.Add(sbp1);
            sb.Panels.Add(sbp2);
            sb.ShowPanels = true;
            sb.Font = new Font(FontFamily.GenericSansSerif, this.Font.Size-2);
            this.Controls.Add(sb);

            sbp1.Text = "-";
            sbp1.AutoSize = StatusBarPanelAutoSize.Spring;
            sbp2.Text = System.DateTime.Today.ToLongDateString();
            sbp2.AutoSize = StatusBarPanelAutoSize.Contents;

            /*p.Location = new Point(5, 5);
            p.Size = new Size( this.Width - 18 - 10, this.Height - sb.Height - 50 );
            p.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            p.BackColor = Color.GhostWhite;
            p.MouseMove += new MouseEventHandler(Panel_MouseMove);
            this.Controls.Add(p);*/

		splitContainer.Location = new Point(3, 3);
		splitContainer.Size = new Size( this.Width - 18 - 5, this.Height - sb.Height - 45 );
		splitContainer.BorderStyle = BorderStyle.FixedSingle;
		splitContainer.Anchor = ( AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right );
		splitContainer.Panel1.BackColor = Color.FloralWhite;
		splitContainer.Panel2.BackColor = Color.GhostWhite;
		Controls.Add(splitContainer);
		splitContainer.Panel1.MouseMove += new MouseEventHandler(Panel_MouseMove);

		}

        private void mi2_Click(object sender, EventArgs e)
        {
            MessageBox.Show( sender.ToString() + Environment.NewLine + Environment.NewLine + e.ToString() );
        }

        private void mi3_Click(object sender, EventArgs e)
        {
            MessageBox.Show( this.Font.ToString() +"\n"+ sb.Font.ToString() );
			
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
		f.Show();			
			
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
		
    }
}
