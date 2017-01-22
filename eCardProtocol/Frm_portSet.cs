using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SerialportHelper;
using eCardInfo;
using System.IO.Ports;
using ICCardHelper;

namespace eCardProtocol
{
    public partial class Frm_portSet : Form
    {
        public ICCardHelper .CardProtocol card1=new CardProtocol();
        string []portUIParam=new string[8];

        
        public Frm_portSet()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WindowPortSetInit();
            OpenPort(null, Convert.ToInt32(cmbBaudrate.SelectedItem), Convert.ToInt32(cmbDatabit.SelectedItem), (string)cmbCheckbit.SelectedItem == "None" ? Parity.None : ((string)cmbCheckbit.SelectedItem == "Odd" ? Parity.Odd : Parity.Even), (string)cmbStopbit.SelectedItem == "1" ? StopBits.One : StopBits.Two);
            if (Global .card.SerialportObject .serialPort1 .IsOpen )
            {
                Console.WriteLine("串口已打开！");
            }
            
        }
        public void WindowPortSetInit()
        {
            string[] portnameItems = SerialportHelper.MySerialPort.portnameArray();
            string[] baudratenameItems = SerialportHelper.MySerialPort.baudrateArray();
            string[] databitItems = SerialportHelper.MySerialPort.dataArray();
            string[] checkbitItems = SerialportHelper.MySerialPort.checkArray();
            string[] stopbitItems = SerialportHelper.MySerialPort.stopArray();
            foreach (var a in  portnameItems )
            {
                cmbPortname.Items.Add(a);
            }
            foreach (var a in baudratenameItems )
            {
                cmbBaudrate .Items.Add(a);
            }
            foreach (var a in databitItems)
            {
                cmbDatabit.Items.Add(a);
            }
            foreach (var a in checkbitItems)
            {
                cmbCheckbit.Items.Add(a);
            }
            foreach (var a in stopbitItems)
            {
                cmbStopbit.Items.Add(a);
            }      
            cmbBaudrate.SelectedIndex = 2;
            cmbDatabit.SelectedIndex = 2;
            cmbCheckbit.SelectedIndex = 0;
            cmbStopbit.SelectedIndex = 0; 
            cmbPortname.SelectedIndex = 0;
            
        }


        public void OpenPort(string _PortName, Int32 _BaudRate, Int32 _DataSize, Parity _Parity, StopBits _StopBits)
        {
            Parity parity_=Parity .None ;
            bool is_open_port;
            Global.card.SerialportObject.OpenPort( _PortName, _BaudRate,  _DataSize,  _Parity,  _StopBits);
            //serialPortObject.OpenPort(null , Convert.ToInt32(cmbBaudrate.SelectedItem), Convert.ToInt32(cmbDatabit.SelectedItem), Parity.Even,(string)cmbStopbit.SelectedItem == "1" ? StopBits.One : StopBits.Two);
            portUIParam = SerialportHelper.MySerialPort.SerialPortUIparam();
            cmbPortname.SelectedItem = portUIParam[0];
            cmbBaudrate.SelectedItem = portUIParam[1];
            cmbDatabit.SelectedItem = portUIParam[2];
            cmbCheckbit.SelectedItem = portUIParam[3];
            cmbStopbit.SelectedItem = portUIParam[4];
            portstate.Text = portUIParam[5];
            portControl.Text = portUIParam[6];
            is_open_port = portUIParam[7] == "true" ? true : false;
            if (is_open_port )
            {
                cmbPortname.BackColor = Color.LightGreen;
            }
            else
            {
                cmbPortname.BackColor = Color.Red;
            }
        }
        /// <summary>
        /// 打开或者关闭串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void portControl_Click(object sender, EventArgs e)
        {
            if (portControl .Text =="打开串口")
            {
                if (Global.card.SerialportObject.serialPort1.IsOpen)
                {
                    portstate.Text = "串口已打开";
                    portControl.Text = "关闭串口";
                    cmbPortname.BackColor = Color.LightGreen;
                    Console.WriteLine("串口已打开！");
                }
                else
                {
                    Global.card.SerialportObject.OpenPort(cmbPortname .Text.Trim () , Convert.ToInt32(cmbBaudrate.SelectedItem), Convert.ToInt32(cmbDatabit.SelectedItem), (string)cmbCheckbit.SelectedItem == "None" ? Parity.None : ((string)cmbCheckbit.SelectedItem == "Odd" ? Parity.Odd : Parity.Even), (string)cmbStopbit.SelectedItem == "1" ? StopBits.One : StopBits.Two);
                    if (Global.card.SerialportObject.serialPort1.IsOpen)
                    {
                        portstate.Text = "串口已打开";
                        portControl.Text = "关闭串口";
                        cmbPortname.BackColor = Color.LightGreen;
                        Console.WriteLine("串口已打开！");
                    }
                     else
                    {
                        portstate.Text = "串口已关闭";
                        portControl.Text = "打开串口";
                        cmbPortname.BackColor = Color.Red;
                        Console.WriteLine("串口已关闭！");
                    }
                }
            }
            else
            {
                Global.card.SerialportObject.ClosePort();
                if (!Global.card.SerialportObject.serialPort1.IsOpen)
                {
                    portstate.Text = "串口已关闭";
                    portControl.Text = "打开串口";
                    cmbPortname.BackColor = Color.Red ;
                    Console.WriteLine("串口已关闭！");
                }
                    
            }
        }

       

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPortname_SelectedIndexChanged(object sender, EventArgs e)
        {
            //OpenPort(cmbPortname.Text , Convert.ToInt32(cmbBaudrate.SelectedItem), Convert.ToInt32(cmbDatabit.SelectedItem), (string)cmbCheckbit.SelectedItem == "None" ? Parity.None : ((string)cmbCheckbit.SelectedItem == "Odd" ? Parity.Odd : Parity.Even), (string)cmbStopbit.SelectedItem == "1" ? StopBits.One : StopBits.Two);
        }

      
    }
}
