using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

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
// https://docs.microsoft.com/en-us/dotnet/api/control.mousehover?view=netframework-4.7.2

public class Form1 : Form  
{  

    MainMenu mainMenu = new MainMenu();

    StatusBar mainStatusBar = new StatusBar();
    
    MenuStrip menuStrip = new MenuStrip();
    StatusStrip statusStrip = new StatusStrip();  
    ToolStripStatusLabel statusLabel = new ToolStripStatusLabel();

    SplitContainer splitContainer = new SplitContainer();
    
    Panel panel1 = new Panel();
    TextBox textBox = new TextBox();
    
    GraphicsPath mousePath = new GraphicsPath();

    [STAThread]
    public static void Main()
    {
        //Application.SetCompatibleTextRenderingDefault(false);
        Application.EnableVisualStyles();
        Application.Run(new Form1());
    }   

    /*protected override void OnPaint(PaintEventArgs pae) 
    { 
      Graphics myGraphics = pae.Graphics; 
      Pen myPen = new Pen(Color.Red, 3); 
      myGraphics.DrawLine(myPen, 30, 20, 300, 100); 
    }  */ 

   public Form1()  
   {
        Size = new Size(800, 600);
        
		MenuItem mi1 = new MenuItem(text: "&File");
		mi1.MenuItems.Add(new MenuItem("&Open", new EventHandler(ButtonClick)));
		mi1.MenuItems.Add(new MenuItem("&Save", new EventHandler(ButtonClick)));
		mi1.MenuItems.Add(new MenuItem("-"));
		mi1.MenuItems.Add(new MenuItem(text: "&Exit", onClick: (sender, args) => Application.Exit()));
		mainMenu.MenuItems.Add(mi1);
		this.Menu = mainMenu;

        var fileItem = new ToolStripMenuItem("&File");
        menuStrip.Items.Add(fileItem);
        Controls.Add(menuStrip);

        var openFileMenuItem = fileItem.DropDownItems.Add("&Megnyitás");
        var saveFileMenuItem = fileItem.DropDownItems.Add("M&entés");
                               fileItem.DropDownItems.Add("-");
        var quitFileMenuItem = fileItem.DropDownItems.Add("&Kilépés");
		openFileMenuItem.Click += new EventHandler (ButtonClick);
		saveFileMenuItem.Enabled = false;
		quitFileMenuItem.Click += new EventHandler (ButtonClick);
        

        
        Controls.Add(mainStatusBar);
        
        statusStrip.Name = "statusStrip";  
        statusStrip.BackColor = Color.Beige;  
        Controls.Add(statusStrip);
        
        statusStrip.Items.AddRange(new ToolStripItem[] {statusLabel});
        statusLabel.Text = "fgdfgdfgd";

        Controls.Add(splitContainer);
        splitContainer.Dock = DockStyle.Fill;
        splitContainer.Panel1.Controls.Add(panel1);
        splitContainer.Panel2.Controls.Add(textBox);
        panel1.Dock = DockStyle.Fill;
        panel1.BackColor = Color.White;
        textBox.Dock = DockStyle.Fill;
        textBox.Multiline = true;
        
        panel1.MouseClick += new MouseEventHandler(MouseClick);
        //panel1.MouseLeave += new EventHandler(MouseLeave);
        //panel1.MouseEnter += new EventHandler(MouseEnter);
        panel1.MouseHover += new EventHandler(MouseHover);

        panel1.Paint +=      new PaintEventHandler(Paint);

        //panel1.MouseUp +=    new MouseEventHandler(MouseUp);
        panel1.MouseMove +=  new MouseEventHandler(MouseMove);
        panel1.MouseDown +=  new MouseEventHandler(MouseDown);
        //panel1.MouseWheel += new MouseEventHandler(MouseWheel);

   }
   
    private void ButtonClick (object sender, EventArgs e) {
      
      statusLabel.Text += "x";
      
    }

    private void MouseHover(object sender, EventArgs e) 
    {
        // Update the mouse event label to indicate the MouseHover event occurred.
        mainStatusBar.Text = sender.GetType().ToString() + ": MouseHover";

    }   
   
   private void MouseDown(object sender, MouseEventArgs e)  
   {
  
        if (e.Button == MouseButtons.Left ) {
      Point mouseDownLocation = new Point(e.X, e.Y);
      mousePath.AddLine(mouseDownLocation,mouseDownLocation);
      panel1.Focus();
      panel1.Invalidate();
    }
        if (e.Button == MouseButtons.Right ) {
      mousePath.CloseAllFigures ();
      //panel1.Focus();
      panel1.Invalidate();
    }
    
  
  } 
   
   private void MouseMove(object sender, MouseEventArgs e)  
   {  
        mainStatusBar.Text = e.X + " " + e.Y;
         Point mouseDownLocation = new Point(e.X, e.Y);
        //mousePath.AddLine(mouseDownLocation,mouseDownLocation);
        //panel1.Focus();
        //panel1.Invalidate();
           
   } 
   
   private void Paint(object sender, PaintEventArgs e)  
   {  
        //mainStatusBar.Text = "..";
        Graphics myGraphics = e.Graphics; 
        Pen myPen = new Pen(Color.Red, 3); 
        myGraphics.DrawLine(myPen, 30, 20, 300, 100);
         e.Graphics.DrawPath(System.Drawing.Pens.DarkRed, mousePath);
   }

    private void MouseClick(object sender, MouseEventArgs e)  
    {  
        /*Point p = new Point(e.X, e.Y);  
        x = p.X;  
        y = p.Y;  
        panel1.Invalidate();  
        mainStatusBar.Text = "click";*/
  
        switch (e.Button) {
            case MouseButtons.Left:
                mainStatusBar.Text += "L";
                break;
            case MouseButtons.Middle:
                mainStatusBar.Text += "M";
                break;
            case MouseButtons.Right:
                mainStatusBar.Text += "R";
                break;
            default:
            break;
        }
    }
 
}  
