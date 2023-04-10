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

namespace HackLauncher
{
    public partial class Form2 : Form
    {
        string[] game = { "Assault Cube", "Rust", "GTA 5"};
        int counter;
        bool[] isDetected;
        bool[] isDll = {false, false, true };

        public Form2()
        {
            InitializeComponent();
            button1.BackColor = Color.FromArgb(30, 30, 40);
            button2.BackColor = Color.FromArgb(30, 30, 40);
            button2.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.BorderSize = 0;

            isDetected = new bool[game.Length];
            isDetected[0] = false;
            isDetected[1] = true;
            isDetected[2] = false;
        }

        string[] toRun = new string[3];

        private void signIn_Click(object sender, EventArgs e)
        {
            toRun[0] = "AssaultCubeHackCpp.exe";
            toRun[1] = "";
            toRun[2] = "";

            
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
                
            } else if(isDll[counter % toRun.Length])
            {
                //Lauch Dll
                //Will need to provide a longer way to open the dll, due to them needing to be injected if only the dll is inlcluded excluding a seperate launcher
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
            if(counter == 0)
            {
                counter = counter + game.Length;
            }else
            {
                counter--;
            }
            gameName_Click(sender, e); // call gameName_Click to update the label's text
        }

        Image[] images = new Image[3];

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



            images[0] = Image.FromFile("img1.png");
            images[1] = Image.FromFile("img2.png");
            images[2] = Image.FromFile("img3.png");

            pictureBox1.Image = images[counter % images.Length];

            if (counter >= images.Length)
            {
                counter = 0;
            }
            pictureBox1.Image = images[counter];

        }

        int mouseX = 0, mouseY = 0;
        bool mouseDown;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                mouseX = MousePosition.X - 200;
                mouseY = MousePosition.Y - 40;

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
