using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Security.Cryptography;
using System.IO;

namespace EndTermClient
{
    public partial class LoginForm : Form
    {
        public static TcpClient client = new TcpClient();
        public static string user = string.Empty;
        public IPEndPoint IP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        public NetworkStream ns;
        public static byte[] keyy = Encoding.UTF8.GetBytes("ducvahuy");
        public LoginForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        public static byte[] EncryptDES(byte[] clearData, byte[] key)
        {
            DES desEncrypt = new DESCryptoServiceProvider();
            desEncrypt.Mode = CipherMode.ECB;
            desEncrypt.Key = key;
            desEncrypt.Padding = PaddingMode.PKCS7;
            ICryptoTransform transForm = desEncrypt.CreateEncryptor();
            using (MemoryStream encryptedStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(encryptedStream, transForm, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(clearData, 0, clearData.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] encryptedData = encryptedStream.ToArray();
                    return encryptedData;
                }
            }
        }

        public static byte[] DecryptDES(byte[] clearData, byte[] key)
        {
            DES desDecrypt = new DESCryptoServiceProvider();
            desDecrypt.Mode = CipherMode.ECB;
            desDecrypt.Key = key;
            ICryptoTransform transForm = desDecrypt.CreateDecryptor();
            using (MemoryStream decryptedStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(decryptedStream, transForm, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(clearData, 0, clearData.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] encryptedData = decryptedStream.ToArray();
                    return encryptedData;
                }
            }
        }

        void ConnectS()
        {
            try
            {
                client.Connect(IP);
                ns = client.GetStream();
            }
            catch
            {
                MessageBox.Show("Can't connect to server!");
                return;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ConnectS();
            //  UsrtextBox.Select();
            checkBox1.Select();
        }

        

       
       
        private void Loginbutton_Click(object sender, EventArgs e)
        {
            if ((UsrtextBox.ForeColor== Color.Black) && (PasstextBox.ForeColor == Color.Black))
            {
                string send = UsrtextBox.Text + "/" + PasstextBox.Text;
                send = "Login" + send;
                byte[] sendbytes = System.Text.Encoding.UTF8.GetBytes(send);
                sendbytes = EncryptDES(sendbytes, keyy);
                ns.Write(sendbytes, 0, sendbytes.Length);

                
                byte[] recv = new byte[1024];
                int count = ns.Read(recv, 0, recv.Length);
                byte[] formated = new byte[count];
                Array.Copy(recv, formated, count);
                formated = DecryptDES(formated, keyy);
                string s = Encoding.UTF8.GetString(formated);
                string x = s.Substring(5);

                if (x == "Right")
                {
                    user = UsrtextBox.Text;
                    checkBox1.Checked = false;
                    this.Hide();
                    Cloud formMain = new Cloud();
                    formMain.Show();
                   // formMain.ShowDialog();
                   // formMain = null;
                   // this.Show();
                
                }
                else
                {
                    MessageBox.Show("Username or Password incorrect. Try again.",
                        "Login", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    // UsrtextBox.Focus();
                    UsrtextBox.SelectAll();
                }
            }
            else
            {
                MessageBox.Show("Please enter username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                UsrtextBox.Select();
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                PasstextBox.UseSystemPasswordChar = false;
            }
            else
            {
                PasstextBox.UseSystemPasswordChar = true;
            }
        }

        private void Exitbutton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReglinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.Show();
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
    }
}
