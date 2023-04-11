using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace HackLauncher
{
    public partial class Form2 : Form
    {
        string[] game = { "Assault Cube", "Rust", "GTA 5", "COD 4" };
        int counter;
        bool[] isDetected;
        bool[] isDll = { false, false, true, false };


        ImageList imageList = new ImageList();

        public Form2()
        {
            InitializeComponent();

            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    Button b = (Button)c;
                    b.BackColor = Color.FromArgb(31, 31, 31);
                    b.FlatAppearance.BorderSize = 1;
                    b.FlatAppearance.BorderColor = Color.FromArgb(183, 6, 170);
                    b.FlatStyle = FlatStyle.Flat;
                }
            }

            isDetected = new bool[game.Length];
            isDetected[0] = false;
            isDetected[1] = true;
            isDetected[2] = true;
            isDetected[3] = false;

        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern System.IntPtr CreateRoundRectRgn
        (
           int nLeftRect, // x-coordinate of upper-left corner
           int nTopRect, // y-coordinate of upper-left corner
           int nRightRect, // x-coordinate of lower-right corner
           int nBottomRect, // y-coordinate of lower-right corner
           int nWidthEllipse, // height of ellipse
           int nHeightEllipse // width of ellipse
        );

        [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        private static extern bool DeleteObject(System.IntPtr hObject);

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            System.IntPtr ptr = CreateRoundRectRgn(0, 0, this.Width, this.Height, 15, 15); // _BoarderRaduis can be adjusted to your needs, try 15 to start.
            this.Region = System.Drawing.Region.FromHrgn(ptr);
            DeleteObject(ptr);
        }

        private void signIn_Click(object sender, EventArgs e)
        {
            //string[] toRun = new string[game.Length];
            //string[] toRun = string{ game.Length};
            string[] toRun = { "AssaultCubeHackCpp", "", "", "" };


            if (!isDll[counter % toRun.Length])
            {
                try
                {
                    Process.Start(toRun[counter]);
                    MessageBox.Show("launching the program");
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }

            }
            else if (isDll[counter % toRun.Length])
            {
                //Lauch Dll
                //Will need to provide a longer way to open the dll, due to them needing to be injected if only the dll is inlcluded without a seperate launcher
                try
                {

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            counter++;
            gameName_Click(sender, e); // call gameName_Click to update the label's text
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (counter == 0)
            {
                counter = counter + game.Length;
            }
            else
            {
                counter--;
            }
            gameName_Click(sender, e); // call gameName_Click to update the label's text
        }

        private void gameName_Click(object sender, EventArgs e)
        {
            gameName.Text = game[counter % game.Length];

            if (!isDetected[counter % game.Length])
            {
                detectStatus.Text = "Undetected";
                detectStatus.ForeColor = Color.Lime;
            }
            else
            {
                detectStatus.Text = "Detected";
                detectStatus.ForeColor = Color.Red;
            }


            Image[] images = new Image[game.Length];
            images[0] = Image.FromFile("img1.png");
            images[1] = Image.FromFile("img2.png");
            images[2] = Image.FromFile("img3.png");
            images[3] = Image.FromFile("img4.png");

            pictureBox1.Image = images[counter % images.Length];

            if (counter >= images.Length)
            {
                counter = 0;
            }
            pictureBox1.Image = images[counter];

        }

        int mouseX = 0, mouseY = 0, mouseinX = 0, mouseinY = 0;
        bool mouseDown;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            mouseinX = MousePosition.X - Bounds.X;
            mouseinY = MousePosition.Y - Bounds.Y;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                mouseX = MousePosition.X - mouseinX;
                mouseY = MousePosition.Y - mouseinY;

                this.SetDesktopLocation(mouseX, mouseY);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

    }
}
