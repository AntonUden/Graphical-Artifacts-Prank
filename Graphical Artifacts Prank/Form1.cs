using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphical_Artifacts_Prank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics g;
        Bitmap backBuffer;
        Graphics BB;

        BackgroundWorker bw;
        bool isRunning;

        Random seed;

        int LinesAmount = 10;

        private void Form1_Load(object sender, EventArgs e)
        {
            bw = new BackgroundWorker();
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width + 100, Screen.PrimaryScreen.Bounds.Height);
            this.Location = new Point(0, 0);
            this.BackColor = Color.Fuchsia;
            this.TransparencyKey = this.BackColor;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;


            g = this.CreateGraphics();
            backBuffer = new Bitmap(this.Width, this.Height);
            BB = Graphics.FromImage(backBuffer);


            seed = new Random();
            isRunning = true;
            bw.DoWork += startLoop;
            bw.RunWorkerAsync();
        }

        private void startLoop(object sender, EventArgs e)
        {
            while(isRunning)
            {
                try
                {
                    g.DrawImage(Render(),new Point(0,0));
                    this.Focus();
                    Thread.Sleep(3);
                }
                catch(Exception err)
                {

                }
            }
        }

        private Bitmap Render()
        {
            try
            {
                BB.Clear(this.TransparencyKey);

                int choice = seed.Next(0, 16);

                if (choice < 3)
                {
                    for (int i = 0; i < seed.Next(1, 30); i++)
                    {
                        BB.FillRectangle(new SolidBrush(randomColor()), new Rectangle(seed.Next(0, this.Width), seed.Next(0, this.Height), seed.Next(2, 50), seed.Next(2, 50)));
                    }
                }
                if (choice > 6 && choice < 10)
                {
                    List<Point> starts = new List<Point>();
                    List<Point> ends = new List<Point>();

                    for (int n = 0; n < LinesAmount; n++)
                    {
                        int y = seed.Next(0, this.Height);

                        starts.Add(new Point(0, y));
                        ends.Add(new Point(this.Width, y));
                    }

                    foreach (Point start in starts)
                    {
                        BB.DrawLine(new Pen(new SolidBrush(randomColor())), start, ends[starts.IndexOf(start)]);
                    }
                }
                if (choice == 10 || choice == 1 || choice == 2 || choice == 9)
                {
                    Thread.Sleep(seed.Next(10, 100));
                }
            }
            catch(Exception e)
            {

            }

            return backBuffer;
        }

        private Color randomColor()
        {
            return Color.FromArgb(seed.Next(0, 255), seed.Next(0, 255), seed.Next(0, 255));
        }
    }
}
