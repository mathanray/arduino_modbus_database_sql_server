using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO.Ports;

namespace Base_integration
{
    public partial class Form1 : Form
    {
        private Byte[] buffer = new byte[8];
        private SerialPort port = new SerialPort("COM9", 115200, Parity.None, 8, StopBits.One);
        private string format = "yyyy-MM-dd HH:mm:ss";

        public Form1()
        {
            port.Handshake = Handshake.None;
            port.RtsEnable = true;
            port.DtrEnable = true;
            InitializeComponent();
            Create_button1_Click();
            SerialPortProgram();
        }

        private void Create_button1_Click()
        {
            // create button1 object
            Button button1 = new Button();

            // Set Button properties
            button1.Height = 40;
            button1.Width = 300;
            button1.BackColor = Color.Red;
            button1.ForeColor = Color.Blue;
            button1.Location = new Point(20, 20);
            button1.Text = "Sensor Database";
            button1.Name = "Sensor";
            button1.Font = new Font("Georgia", 16);

            //Add a button Click Event handler
            button1.Click += new EventHandler(button1_Click);

            //Add button to form1
            Controls.Add(button1);
        }

        //button connection
        private void button1_Click(object sender, EventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=LAPTOP-name\SQLEXPRESS;Initial Catalog=ExportDB_express;User ID=xxxxxxxx;Password=xxxxxxx";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";
            sql = "SELECT [SensorId], [Data] FROM dbo.[Sensor];";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                Output += "Sensor" + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + "\ndata value: " + "\n";
            }
            MessageBox.Show(Output);
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }

        private void SerialPortProgram()
        {
            Console.WriteLine("Incoming Data:");
            // Attach a method to be called when there
            // is data waiting in the port's buffer 
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            // Begin communications 
            port.Open();
            // Enter an application loop to keep this thread alive 
            Console.ReadLine();
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            buffer = new byte[8];
            port.Read(buffer, 0, 8);
            //Console.WriteLine("[{0}]", string.Join(", ", buffer));
            if((buffer[0] == 1) && (buffer[1] == 6))
            {
                string connetionString;
                SqlConnection cnn;
                connetionString = @"Data Source=Laptop-name\SQLEXPRESS;Initial Catalog=ExportDB_express;User ID=xxxxxxx;Password=xxxxxxxx";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                SqlCommand command;
                SqlDataAdapter adapter = new SqlDataAdapter();
                string sql = "";
                if((buffer[2] == 0) && (buffer[3] == 0))
                {
                    DateTime dt = DateTime.Now;
                    sql = "Update dbo.[Sensor] set Data=" + buffer[5] + ", timestamp=('" + dt.ToString(format) + "') where SensorId=1";    
                }
                if((buffer[2] == 0) && (buffer[3] == 1))
                {
                    DateTime dt = DateTime.Now;
                    sql = "Update dbo.[Sensor] set Data=" + buffer[5] + ", timestamp=('" + dt.ToString(format) + "') where SensorId=2";
                }
                if ((buffer[2] == 0) && (buffer[3] == 2))
                {
                    DateTime dt = DateTime.Now;
                    sql = "Update dbo.[Sensor] set Data=" + buffer[5] + ", timestamp=('" + dt.ToString(format) + "') where SensorId=3";
                }
                if ((buffer[2] == 0) && (buffer[3] == 3))
                {
                    DateTime dt = DateTime.Now;
                    sql = "Update dbo.[Sensor] set Data=" + buffer[5] + ", timestamp=('" + dt.ToString(format) + "') where SensorId=4";
                }
                if ((buffer[2] == 0) && (buffer[3] == 4))
                {
                    DateTime dt = DateTime.Now;
                    sql = "Update dbo.[Sensor] set Data=" + buffer[5] + ", timestamp=('" + dt.ToString(format) + "') where SensorId=5";
                }
                if ((buffer[2] == 0) && (buffer[3] == 5))
                {
                    DateTime dt = DateTime.Now;
                    sql = "Update dbo.[Sensor] set Data=" + buffer[5] + ", timestamp=('" + dt.ToString(format) + "') where SensorId=6";
                }
                if ((buffer[2] == 0) && (buffer[3] == 6))
                {
                    DateTime dt = DateTime.Now;
                    sql = "Update dbo.[Sensor] set Data=" + buffer[5] + ", timestamp=('" + dt.ToString(format) + "') where SensorId=7";
                }
                if ((buffer[2] == 0) && (buffer[3] == 7))
                {
                    DateTime dt = DateTime.Now;
                    sql = "Update dbo.[Sensor] set Data=" + buffer[5] + ", timestamp=('" + dt.ToString(format) + "') where SensorId=8";
                }
                if ((buffer[2] == 0) && (buffer[3] == 8))
                {
                    DateTime dt = DateTime.Now;
                    sql = "Update dbo.[Sensor] set Data=" + buffer[5] + ", timestamp=('" + dt.ToString(format) + "') where SensorId=9";
                }
                if ((buffer[2] == 0) && (buffer[3] == 9))
                {
                    DateTime dt = DateTime.Now;
                    sql = "Update dbo.[Sensor] set Data=" + buffer[5] + ", timestamp=('" + dt.ToString(format) + "') where SensorId=10";
                }
                if (sql != "")
                {
                    command = new SqlCommand(sql, cnn);

                    adapter.UpdateCommand = new SqlCommand(sql, cnn);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    command.Dispose();
                }
                
                cnn.Close();
            }
        }

    }
}
