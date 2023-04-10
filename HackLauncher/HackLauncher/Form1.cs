using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HackLauncher
{

    public partial class frmLogin : Form
    {

        string[,] users = { { "1", "1" }, { "danny", "danny" } };   // would need to use a better login system with the users and passwords being stored somewhere else encrypted 
        public bool isLoginSuccessful = false;


        public frmLogin()
        {
            InitializeComponent();
            signIn.BackColor = Color.FromArgb(31, 31, 31);
            showPass.BackColor = Color.FromArgb(31, 31, 31);
        }

        private void signIn_Click(object sender, EventArgs e)
        {
            if (username.Text == "" || password.Text == "")
            {
                isLoginSuccessful = false;
                return;
            }

            for (int i = 0; i < users.GetLength(0); i++)
            {
                if (username.Text == users[i, 0] && password.Text == users[i, 1])
                {
                    isLoginSuccessful = true;
                    break;
                }
            }

            if (isLoginSuccessful)
            {
                MessageBox.Show("successfully logged in");
                this.Close();
            }
            else
            {
                MessageBox.Show("this user does not exist");
            }

        }


        bool passShow = false;
        private void showPass_Click(object sender, EventArgs e)
        {
            if (passShow)
            {
                password.PasswordChar = '*';
                passShow = false;
            }
            else
            {
                password.PasswordChar = '\0';
                passShow = true;
            }
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
