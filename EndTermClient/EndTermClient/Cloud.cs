using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EndTermClient
{
    public partial class Cloud : Form
    {
        public Cloud()
        {
            InitializeComponent();
        }
        NetworkStream ns = LoginForm.client.GetStream();
        public static string path = LoginForm.user;
        string prepath = string.Empty;
       

        private void Cloud_Load(object sender, EventArgs e)
        {
            label1.Text = LoginForm.user;
            Thread receive = new Thread(RecvM);
            receive.SetApartmentState(ApartmentState.STA);
            receive.Start();
            byte[] send = System.Text.Encoding.UTF8.GetBytes(path);
            send = LoginForm.EncryptDES(send, LoginForm.keyy);
            ns.Write(send, 0, send.Length);
            Backbutton.Enabled = true;
        }

        void SendFile(string fName)
        {
            try
            {
                string sendpath = "";
                fName = fName.Replace("\\", "/");
                while (fName.IndexOf("/") > -1)
                {
                    sendpath += fName.Substring(0, fName.IndexOf("/") + 1);
                    fName = fName.Substring(fName.IndexOf("/") + 1);
                }
                byte[] x = Encoding.UTF8.GetBytes("Upppp");
                byte[] uppath = Encoding.UTF8.GetBytes(path);
                byte[] pathlength = BitConverter.GetBytes(uppath.Length);
                byte[] fNameByte = Encoding.UTF8.GetBytes(fName);
                byte[] fileData = File.ReadAllBytes(sendpath + fName);
                byte[] clientData = new byte[13 + uppath.Length + fNameByte.Length + fileData.Length];
                byte[] fNameLen = BitConverter.GetBytes(fNameByte.Length);
                x.CopyTo(clientData, 0);
                pathlength.CopyTo(clientData, 5);
                uppath.CopyTo(clientData, 9);
                fNameLen.CopyTo(clientData, 9+uppath.Length);
                fNameByte.CopyTo(clientData, 13+uppath.Length);
                fileData.CopyTo(clientData, 13 + uppath.Length+ fNameByte.Length);
                clientData = LoginForm.EncryptDES(clientData, LoginForm.keyy);
                ns.Write(clientData, 0, clientData.Length);
            }
            catch (Exception ex)
            {

            }
        }

        void RecvM()
        {
            while (true)
            {
                NetworkStream ns= LoginForm.client.GetStream();
                byte[] recv = new byte[5*1024*1024];
                int count = ns.Read(recv, 0, recv.Length);
                byte[] formated = new byte[count];
                Array.Copy(recv, formated, count);
                formated = LoginForm.DecryptDES(formated, LoginForm.keyy);
                count = formated.Length;
                string s = Encoding.UTF8.GetString(formated);
                string s1 = string.Empty;
                if (s.Length > 5) s1 = s.Substring(0, 5);
                if (s1 == "Makes")
                {
                    s = s.Substring(5);
                    if (s == "Success")
                    {
                        MessageBox.Show("Done", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Folder already existed! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                if (s1 == "Downn")
                {
                    int namelength = BitConverter.ToInt32(formated, 5);
                    string name = Encoding.UTF8.GetString(formated, 9, namelength);
                    SaveFileDialog fd = new SaveFileDialog();
                    fd.FileName = name;

                    DialogResult re = fd.ShowDialog();
                    if (re == DialogResult.OK)
                    {
                        BinaryWriter write = new BinaryWriter(File.Open(fd.FileName, FileMode.Append));
                        write.Write(formated, 9 + namelength, count - 9 - namelength);
                        write.Close();
                    }
                }
                else
                {
                    string[] ss = s.Split('>');
                    foreach (string x in ss)
                    {
                        string[] sss = x.Split('*');
                        if (sss.Length == 4)
                        {
                            ListViewItem item = new ListViewItem();
                            item.Text = sss[0];
                            item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = sss[1] });
                            item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = sss[2] });
                            item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = sss[3] });
                            listView1.Items.Add(item);
                        }
                    }
                }
               
            }

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Selected == true)
                {
                    string check = listView1.Items[i].SubItems[2].Text;
                    if (check == "Folder")
                    {
                        path = path + '/' + listView1.Items[i].Text;
                        listView1.Items.Clear();
                        if (path[0] == '/') path = path.Remove(0, 1);
                        byte[] send = System.Text.Encoding.UTF8.GetBytes(path);
                        send = LoginForm.EncryptDES(send, LoginForm.keyy);
                        ns.Write(send, 0, send.Length);
                    }
                }
            }
        }

        private void Backbutton_Click(object sender, EventArgs e)
        {
            int i = path.LastIndexOf('/');
            if (i != -1)
            {
                path = path.Remove(i);
                listView1.Items.Clear();
                byte[] send = System.Text.Encoding.UTF8.GetBytes(path);
                send = LoginForm.EncryptDES(send, LoginForm.keyy);
                ns.Write(send, 0, send.Length);
            }
        }

        private void Delbutton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Selected == true)
                {
                    DialogResult su= MessageBox.Show("Are you sure you want to delete " + listView1.Items[i].Text + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (su == DialogResult.Yes)
                    {
                        string check = listView1.Items[i].SubItems[2].Text;
                        if (check == "Folder")
                        {
                            string path1 = path + '/' + listView1.Items[i].Text;
                            byte[] send = System.Text.Encoding.UTF8.GetBytes("Delfo" + path1);
                            send = LoginForm.EncryptDES(send, LoginForm.keyy);
                            ns.Write(send, 0, send.Length);
                            listView1.Items.Clear();
                            send = System.Text.Encoding.UTF8.GetBytes(path);
                            send = LoginForm.EncryptDES(send, LoginForm.keyy);
                            ns.Write(send, 0, send.Length);
                        }
                        else
                        {
                            string path1 = path + '/' + listView1.Items[i].Text;
                            byte[] send = System.Text.Encoding.UTF8.GetBytes("Delfi" + path1);
                            send = LoginForm.EncryptDES(send, LoginForm.keyy);
                            ns.Write(send, 0, send.Length);
                            listView1.Items.Clear();
                            send = System.Text.Encoding.UTF8.GetBytes(path);
                            send = LoginForm.EncryptDES(send, LoginForm.keyy);
                            ns.Write(send, 0, send.Length);
                        }
                    }
                }
            }
        }

        private void Sharedbutton_Click(object sender, EventArgs e)
        {
            Delbutton.Enabled = false;
            Upbutton.Enabled = false;
            Newbutton.Enabled = false;
            listView1.Items.Clear();
            path = String.Empty;
            byte[] send = System.Text.Encoding.UTF8.GetBytes("Sared"+LoginForm.user);
            send = LoginForm.EncryptDES(send, LoginForm.keyy);
            ns.Write(send, 0, send.Length);
        }

        private void Sbutton_Click(object sender, EventArgs e)
        {
            Share f = new Share();

            f.Show();

        }

        private void Mybutton_Click(object sender, EventArgs e)
        {
            Delbutton.Enabled = true;
            Upbutton.Enabled = true;
            Newbutton.Enabled = true;
            listView1.Items.Clear();
            path = LoginForm.user;
            byte[] send = System.Text.Encoding.UTF8.GetBytes(path);
            send = LoginForm.EncryptDES(send, LoginForm.keyy);
            ns.Write(send, 0, send.Length);
        }

        private void Newbutton_Click(object sender, EventArgs e)
        {
            New f = new New();

            f.ShowDialog();
            f = null;
            listView1.Items.Clear();
            byte[] send = System.Text.Encoding.UTF8.GetBytes(path);
            send = LoginForm.EncryptDES(send, LoginForm.keyy);
            ns.Write(send, 0, send.Length);

        }

        private void Upbutton_Click(object sender, EventArgs e)
        {
            FileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SendFile(fd.FileName);
                listView1.Items.Clear();
                byte[] send = System.Text.Encoding.UTF8.GetBytes(path);
                send = LoginForm.EncryptDES(send, LoginForm.keyy);
                ns.Write(send, 0, send.Length);
            }
        }

        private void Downbutton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
                if (listView1.Items[i].Selected == true)
                {
                    string check = listView1.Items[i].SubItems[2].Text;
                    if (check != "Folder")
                    {
                        string path1 = path + '/' + listView1.Items[i].Text;
                        byte[] send = System.Text.Encoding.UTF8.GetBytes("Downn" + path1);
                        send = LoginForm.EncryptDES(send, LoginForm.keyy);
                        ns.Write(send, 0, send.Length);
                    }
                }    
        }

        private void Cloud_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }
    }
}
