using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EndTerm.Connection;
using System.IO;
using System.Security.Cryptography;

namespace EndTerm
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        TcpListener listener = new TcpListener(IPAddress.Any,8080);
        List<TcpClient> clientlist;
        string path = "D:/EntermData";
        byte[] keyy = Encoding.UTF8.GetBytes("ducvahuy");

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

        string login(string s)
        {
            string[] arr = s.Split('/');
            string mySQL = string.Empty;
            mySQL += "SELECT * FROM LoginTbl ";
            mySQL += "WHERE Username = '" + arr[0] + "' ";
            mySQL += "AND Password = '" + arr[1] + "' ";

            DataTable userData = ServerConnection.executeSQL(mySQL);
            if (userData.Rows.Count > 0)
            {
                return ("Right");
            }
            return "Wrong";
        }

        string regis(string s)
        {
            string[] arr = s.Split('/');
            string mySQL = "SELECT Username FROM LoginTbl WHERE Username = '" + arr[0] + "'";
            DataTable checkDup = ServerConnection.executeSQL(mySQL);
            if (checkDup.Rows.Count>0)
            {
                return "Duplicate";
            }
            mySQL = string.Empty;
            mySQL += "INSERT INTO LoginTbl (Username, Password) ";
            mySQL += "VALUES ('" + arr[0] + "','" + arr[1] + "')";
            EndTerm.Connection.ServerConnection.executeSQL(mySQL);
            System.IO.Directory.CreateDirectory(path + "/" + arr[0]);
            return "Success";
        }

        void shares(string s)
        {
            string[] arr = s.Split('/');
            string mySQL = "SELECT shared FROM LoginTbl Where Username = '" + arr[0] + "'";
            DataTable check = ServerConnection.executeSQL(mySQL);
            string st=check.Rows[0][0].ToString();
            if ((!st.Contains(arr[1]))||(st==""))
            {
                st = st + '/' + arr[1];
                if (st[0] == '/') st = st.Remove(0, 1);
                mySQL = "UPDATE LoginTbl SET shared='" + st + "' Where Username = '" + arr[0] + "'";
                ServerConnection.executeSQL(mySQL);
            }
        }

        string shared(string s)
        {
            string mySQL = "SELECT shared FROM LoginTbl Where Username = '" + s + "'";
            DataTable check = ServerConnection.executeSQL(mySQL);
            string st = check.Rows[0][0].ToString();
            string[] ss = st.Split('/');
            st = "";
            foreach (string x in ss)
            {
                string xx = x.Trim();
                st = st + xx + "* *Folder* >";
            }    
            return st;
        }

        private void clearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                clearFolder(di.FullName);
                di.Delete();
            }
        }


        private void LoginForm_Load(object sender, EventArgs e)
        {

            clientlist = new List<TcpClient>();
            listener.Start();
            Thread Listen = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        clientlist.Add(client);

                        Thread receive = new Thread(RecvM);
                        receive.IsBackground = true;
                        receive.Start(client);
                    }
                }
                catch
                {
                    listener = new TcpListener(IPAddress.Any, 8080);

                }
            });
            Listen.IsBackground = true;
            Listen.Start();
        }

        void RecvM(object o)
        {
            TcpClient client = (TcpClient)o;
            string usr;
            NetworkStream ns = client.GetStream();
            try
            {
                while (true)
                {  
                    byte[] recv = new byte[5*1024*1024];
                    int count = ns.Read(recv, 0, recv.Length);
                    if (count == 0)
                    {
                        break;
                    }
                    byte[] formated = new byte[count];
                    Array.Copy(recv, formated, count);
                    formated = DecryptDES(formated, keyy);
                    count = formated.Length;
                    string s = Encoding.UTF8.GetString(formated);
                    string ss = "";
                    if (formated.Length>4) ss=Encoding.UTF8.GetString(formated,0,5);
                    if (ss == "Login")
                    {
                        s = s.Substring(5);
                        string[] check = s.Split('/');
                        usr = check[0];
                        string res = login(s);
                        res = "Login" + res;
                        byte[] lg = System.Text.Encoding.UTF8.GetBytes(res);
                        lg = EncryptDES(lg, keyy);
                        ns.Write(lg, 0, lg.Length);
                    }
                    else
                    if (ss == "Regis")
                    {
                        s = s.Substring(5);
                        string res = regis(s);
                        res = "Regis" + res;
                        byte[] lg = System.Text.Encoding.UTF8.GetBytes(res);
                        lg = EncryptDES(lg, keyy);
                        NetworkStream ns2 = client.GetStream();
                        ns2.Write(lg, 0, lg.Length);
                    }
                    else
                    if (ss == "Makes")
                    {
                        bool kq = true;
                        string name;
                        s = s.Substring(5);
                        string[] sp = s.Split('*');
                        byte[] lg = System.Text.Encoding.UTF8.GetBytes("MakesSuccess");
                        NetworkStream ns2 = client.GetStream();
                        foreach (string dir in Directory.GetDirectories(path+'/'+sp[0]))
                        {
                            name = new DirectoryInfo(dir).Name;
                            if (name==sp[1])
                            {
                                lg = System.Text.Encoding.UTF8.GetBytes("MakesFailed");
                                lg = EncryptDES(lg, keyy);
                                ns2.Write(lg, 0, lg.Length);
                                kq = false;
                                break;
                            }         
                        }
                        if (kq)
                        {
                            System.IO.Directory.CreateDirectory(path + '/' + sp[0] + '/' + sp[1]);
                            lg = EncryptDES(lg, keyy);
                            ns2.Write(lg, 0, lg.Length);
                        }
                    }
                    else
                    if (ss == "Delfo")
                    {
                        s = s.Substring(5);
                        clearFolder(path + '/' + s);
                        Directory.Delete(path + '/' + s);
                    }
                    else
                    if (ss=="Delfi")
                    {
                        s = s.Substring(5);
                        File.Delete(path + '/' + s);
                    }
                    else
                    if (ss == "Sared")
                    {
                        s = s.Substring(5);
                        byte[] lg = System.Text.Encoding.UTF8.GetBytes(shared(s));
                        lg = EncryptDES(lg, keyy);
                        NetworkStream ns2 = client.GetStream();
                        ns2.Write(lg, 0, lg.Length);
                    }
                    else
                    if (ss == "Sares")
                    {
                        s = s.Substring(5);
                        shares(s);
                    }
                    else
                    if (ss=="Upppp")
                    {
                        int pathlength = BitConverter.ToInt32(formated, 5);
                        string savepath = Encoding.UTF8.GetString(formated, 9, pathlength);
                        int namelength = BitConverter.ToInt32(formated, 9 + pathlength);
                        string name = Encoding.UTF8.GetString(formated, 13 + pathlength, namelength);
                        BinaryWriter write = new BinaryWriter(File.Open(path + "/" + savepath + "/" + name, FileMode.Append));
                        write.Write(formated, 13 + pathlength + namelength, count - 13 - pathlength - namelength);
                        write.Close();
                    }
                    else
                    if (ss=="Downn")
                    {
                        s = s.Substring(5);
                        string fName = path + "/" + s;
                        try
                        {
                            string sendpath = "";
                            fName = fName.Replace("\\", "/");
                            while (fName.IndexOf("/") > -1)
                            {
                                sendpath += fName.Substring(0, fName.IndexOf("/") + 1);
                                fName = fName.Substring(fName.IndexOf("/") + 1);
                            }
                            byte[] x = Encoding.UTF8.GetBytes("Downn");
                            byte[] fNameByte = Encoding.UTF8.GetBytes(fName);
                            byte[] fileData = File.ReadAllBytes(sendpath + fName);
                            byte[] clientData = new byte[9 + fNameByte.Length + fileData.Length];
                            byte[] fNameLen = BitConverter.GetBytes(fNameByte.Length);
                            x.CopyTo(clientData, 0);
                            fNameLen.CopyTo(clientData, 5);
                            fNameByte.CopyTo(clientData, 9);
                            fileData.CopyTo(clientData, 9 + fNameByte.Length);
                            clientData = EncryptDES(clientData, keyy);
                            ns.Write(clientData, 0, clientData.Length);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        string name;
                        string size;
                        string time;
                        string ex;
                        string res = String.Empty;
                        foreach (string dir in Directory.GetDirectories(path + '/' + s))
                        {               
                            name = new DirectoryInfo(dir).Name;
                            time = new DirectoryInfo(dir).CreationTime.ToString();
                            size = " ";
                            ex = "Folder";
                            string r = name + "*" + size + "*" + ex + "*" + time;
                            res = res + r + ">";
                        }
                        foreach (string f in Directory.GetFiles(path + '/' +s))
                        {                       
                            name = new DirectoryInfo(f).Name;
                            FileInfo fi = new FileInfo(f);
                            size = fi.Length.ToString();
                            time = new DirectoryInfo(f).CreationTime.ToString();
                            ex = new DirectoryInfo(f).Extension;
                            string r = name + "*" + size + "*" + ex + "*" + time;
                            res = res + r + ">";
                        }
                        byte[] send = System.Text.Encoding.UTF8.GetBytes(res);
                        send = EncryptDES(send, keyy);
                        ns.Write(send, 0, send.Length);
                    }
                }
                client.GetStream().Close();
                client.Close();
                clientlist.Remove(client); 
            }
            catch
            {

                //client.GetStream().Close();
                client.Close();
                clientlist.Remove(client);
                //client=listener.EndAcceptTcpClient;
            }
        }

        private void Loginbutton_Click(object sender, EventArgs e)
        {
           
        }
    }
}
