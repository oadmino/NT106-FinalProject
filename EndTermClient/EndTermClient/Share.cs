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
    public partial class Share : Form
    {
        public Share()
        {
            InitializeComponent();
        }

        private void Okbutton_Click(object sender, EventArgs e)
        {
            NetworkStream ns = LoginForm.client.GetStream();
            byte[] data = System.Text.Encoding.UTF8.GetBytes("Sares" + textBox1.Text+"/"+LoginForm.user);
            data = LoginForm.EncryptDES(data, LoginForm.keyy);
            ns.Write(data, 0, data.Length);
            this.Close();
        }

        private void Canbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
