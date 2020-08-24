/*
#!/usr/bin/mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll
set CSC=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\Roslyn\
set CSC=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\Roslyn
set CSC=C:\Windows\Microsoft.NET\Framework64\v4.0.30319
set CSC=C:\Windows\Microsoft.NET\Framework64\v2.0.50727
set path=%CSC%;%path%
csc.exe -debug- -target:exe -r:System.Windows.Forms.dll,System.Drawing.dll -platform:x64 c#_WinForms_v0.3.cs && c#_WinForms_v0.3.exe
*/

using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;


using System.Reflection; // https://codecharm.com/blog/archive/2017-11-19-c-assembly-automatic-versioning/
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.1.123")]

[assembly: AssemblyTitle("WindowsFormsApplication1")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("WindowsFormsApplication1")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]


namespace myProgram
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public partial class Form1 : Form
    {

        private System.Windows.Forms.Button button1;
        private Label labe11;

        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);

            this.labe11 = new Label() { Size = new System.Drawing.Size(this.Width-10, 20), Location = new System.Drawing.Point(20, 20) };
            this.Controls.Add(labe11);

            this.button1 = new System.Windows.Forms.Button() { Size = new System.Drawing.Size(80, 30) };
            this.button1.Location = new System.Drawing.Point(this.Width-100, this.Height-80);
            this.button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.button1.Anchor = ( AnchorStyles.Right | AnchorStyles.Bottom );
            this.button1.Text = "button1";
            this.Controls.Add(this.button1);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "o";
            labe11.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            this.Text += "o";
        }

        private void Form1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
           this.button1.Text = e.X + " " + e.Y;
        }

        private void Proba()
        {
            try
            {
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }


}
