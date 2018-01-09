using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress = Dns.Resolve("localhost").AddressList[0];
            int port = 5000;
            //string localsddress = "127.0.0.1";

            DataSet DS = new DataSet();
            XmlSerializer xs = new XmlSerializer(typeof(DataSet));

            TcpListener listener = new TcpListener(port);
            listener.Start();
           

            TcpClient client = listener.AcceptTcpClient();
            Stream stream = client.GetStream();
            DS = (DataSet)xs.Deserialize(stream);
            stream.Close();
            client.Close();
            //listener.Close();

            dataGridView1.DataSource = DS.Tables[0].DefaultView;
            dataGridView1.Refresh();
            listener.Stop();
        }
    }
}
