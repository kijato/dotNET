using System;
using System.Windows.Forms;
using System.Drawing;

using System.IO;
using System.Text.RegularExpressions;
using System.Collections; // for Hashtable()
using System.Collections.Generic; // for List()
using System.Text; // Encoding
using System.Data; // DataTable

using System.ComponentModel;
using System.Threading;

using System.Reflection; // https://codecharm.com/blog/archive/2017-11-19-c-assembly-automatic-versioning/
//[assembly: AssemblyVersion("1.0.*")]
//[assembly: AssemblyFileVersion("1.0.1.124")]

// https://www.c-sharpcorner.com/article/how-to-draw-shapes-inside-panel-control-in-winforms-using-visual-studio-2017/
// https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.mousehover?view=netframework-4.7.2

public class Form1 : Form  
{  

    StatusBar mainStatusBar = new StatusBar();

    [STAThread]
    public static void Main()
    {
        //Application.SetCompatibleTextRenderingDefault(false);
        Application.EnableVisualStyles();
        Application.Run(new Form1());
    }   

    protected override void OnPaint(PaintEventArgs pae) 
    { 
      Graphics myGraphics = pae.Graphics; 
      Pen myPen = new Pen(Color.Red, 3); 
      myGraphics.DrawLine(myPen, 30, 20, 300, 100); 
    }   

   public Form1()  
   {
        Size = new Size(800, 600);
        Controls.Add(mainStatusBar);
        MouseMove += new MouseEventHandler(Form1_MouseMove);
        Paint += new System.Windows.Forms.PaintEventHandler(Form1_Paint);
   }  

   private void Form1_MouseMove(object sender, MouseEventArgs e)  
   {  
        mainStatusBar.Text = e.X + " " + e.Y;
   }  
   private void Form1_Paint(object sender, EventArgs e)  
   {  
        mainStatusBar.Text = "..";
   }  
 
}  
