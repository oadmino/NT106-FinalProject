using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndTermClient
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        int check()
        {
            if (UsrtextBox.ForeColor==Color.Silver)
            {
                MessageBox.Show("Please enter username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                UsrtextBox.Select();
                return 1;
            }
            if (PasstextBox.ForeColor==Color.Silver)
            {
                MessageBox.Show("Please enter password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                PasstextBox.Select();
                return 2;
            }
            if (ConfirmtextBox.ForeColor==Color.Silver)
            {
                MessageBox.Show("Please enter confirm password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ConfirmtextBox.Select();
                return 3;
            }
            if (PasstextBox.Text!=ConfirmtextBox.Text)
            {
                MessageBox.Show("Password and Confirm Password does not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                PasstextBox.Select();
                return 4;
            }
            return 0;
        }

        private void UsrtextBox_Enter(object sender, EventArgs e)
        {
            if ((UsrtextBox.Text == "Username") && (UsrtextBox.ForeColor == Color.Silver))
            {
                UsrtextBox.Text = "";
                UsrtextBox.ForeColor = Color.Black;
            }
        }

        private void UsrtextBox_Leave(object sender, EventArgs e)
        {
            if (UsrtextBox.Text == "")
            {
                UsrtextBox.Text = "Username";
                UsrtextBox.ForeColor = Color.Silver;
            }
        }

        private void PasstextBox_Enter(object sender, EventArgs e)
        {
            if ((PasstextBox.Text == "Password") && (PasstextBox.ForeColor == Color.Silver))
            {
                PasstextBox.Text = "";
                PasstextBox.ForeColor = Color.Black;
                PasstextBox.UseSystemPasswordChar = true;
            }
        }

        private void PasstextBox_Leave(object sender, EventArgs e)
        {
            if (PasstextBox.Text == "")
            {
                PasstextBox.Text = "Password";
                PasstextBox.ForeColor = Color.Silver;
                PasstextBox.UseSystemPasswordChar = false;
            }
        }

        private void ConfirmtextBox_Enter(object sender, EventArgs e)
        {
            if ((ConfirmtextBox.Text == "Password Confirm") && (ConfirmtextBox.ForeColor == Color.Silver))
            {
                ConfirmtextBox.Text = "";
                ConfirmtextBox.ForeColor = Color.Black;
                ConfirmtextBox.UseSystemPasswordChar = true;
            }
        }

        private void ConfirmtextBox_Leave(object sender, EventArgs e)
        {
            if (ConfirmtextBox.Text == "")
            {
                ConfirmtextBox.Text = "Password Confirm";
                ConfirmtextBox.ForeColor = Color.Silver;
                ConfirmtextBox.UseSystemPasswordChar = false;
            }
        }

        private void Signbutton_Click(object sender, EventArgs e)
        {
            if (check()==0)
            {
                NetworkStream ns = LoginForm.client.GetStream();
                string send = UsrtextBox.Text + "/" + PasstextBox.Text+"/"+ConfirmtextBox.Text;
                send = "Regis" + send;
                byte[] sendbytes = System.Text.Encoding.UTF8.GetBytes(send);
                sendbytes = LoginForm.EncryptDES(sendbytes, LoginForm.keyy);
                ns.Write(sendbytes, 0, sendbytes.Length);

                byte[] recv = new byte[1024];
                int count = ns.Read(recv, 0, recv.Length);
                byte[] formated = new byte[count];
                Array.Copy(recv, formated, count);
                formated = LoginForm.DecryptDES(formated, LoginForm.keyy);
                string s = Encoding.UTF8.GetString(formated);
                string x = s.Substring(5);

                if (x == "Success")
                {
                    MessageBox.Show("Your account has been create.",
                        "SignUp", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    MessageBox.Show("Username has been taken. Try another username.",
                        "SignUp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    UsrtextBox.Select();
                }
            }
        }
    }
}
