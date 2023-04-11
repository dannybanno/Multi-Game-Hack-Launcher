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

            foreach (Control d in Controls)
            {
                if (d is TextBox)
                {
                    TextBox tb = (TextBox)d;
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    //tb.BorderColor = Color.FromArgb(183, 6, 170);
                }
            }
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            System.IntPtr ptr = CreateRoundRectRgn(0, 0, this.Width, this.Height, 15, 15); // _BoarderRaduis can be adjusted to your needs, try 15 to start.
            this.Region = System.Drawing.Region.FromHrgn(ptr);
            DeleteObject(ptr);
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

        int mouseX = 0, mouseY = 0, mouseinX = 0, mouseinY = 0;
        bool mouseDown;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            mouseinX = MousePosition.X - Bounds.X;
            mouseinY = MousePosition.Y - Bounds.Y;
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
