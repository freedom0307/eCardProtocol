using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ICCardHelper;

namespace SerialportHelper
{
  
    public class SerialRecieveEventArgs : EventArgs
    {
        byte[] RecBuffer;
        public SerialRecieveEventArgs(byte[] buffer)
        {

            RecBuffer = buffer;
        }
        public byte[] receiveData
        {
            get
            {
                return RecBuffer;
            }
        }
    }
    public class MySerialPort
    {
        private volatile static MySerialPort _instance = null;
        private static readonly object lockHelper = new object();
        public static  string PortName;//已打开串口
        public SerialPort serialPort1 = new SerialPort();
        public static MySerialPort instance = null;
        public AutoResetEvent event_1 = new AutoResetEvent(false);
        public delegate void EventHandler(object sender, SerialRecieveEventArgs e);
        public event EventHandler ResceiveMessage;
        public Semaphore semaSendRecieve = new Semaphore(1, 1);//协议收发锁
        public static  int flag = 0x00;//串口是否打开标志位,ox00为未打开，0xFF为打开
        public static  Int32  BaudRate = 0;
        public static  Int32  DataSize = 0;
        public static  string PParity = null;
        public static  string SStopBits = null;


        public MySerialPort()
        {

        }
        public static MySerialPort InitPort(string _PortName = null, Int32 _BaudRate = 9600, Int32 _DataSize = 8, Parity _Parity = Parity.None, StopBits _StopBits = StopBits.One)
        {
            if (instance == null)
            {
                instance = new MySerialPort();
                instance.OpenPort(_PortName, _BaudRate, _DataSize, _Parity, _StopBits);
            }
            return instance;
        }
        public static MySerialPort CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new SerialportHelper.MySerialPort();
                }
            }
            return _instance;
        }
        public int OpenPort(string _PortName, Int32 _BaudRate, Int32 _DataSize, Parity _Parity, StopBits _StopBits)
        {
            BaudRate = _BaudRate;
            DataSize = _DataSize;
            PParity = _Parity.ToString();
            SStopBits = _StopBits.ToString();
            if (serialPort1 == null)
            {
                // 

                return 0x00;
            }
            else
            {
                flag = 0x00;
                serialPort1 = new SerialPort();
                serialPort1.BaudRate = _BaudRate;
                serialPort1.DataBits = _DataSize;
                serialPort1.Parity = _Parity;
                serialPort1.StopBits = _StopBits;
               
                if (_PortName == null || _PortName == "")
                {

                    try
                    {
                        int b;//数组下标；
                        short j = 0;
                        string[] a = System.IO.Ports.SerialPort.GetPortNames();
                        b = a.Length;//获取a数组中元素的个数；
                        if (b < 0)
                        {
                            flag = 0x00;
                        }
                        else
                        {
                        go: for (j = 0; j < b; j++)
                            {

                                try
                                {
                                    if (serialPort1.IsOpen == true)
                                    {

                                        flag = 0xFF;
                                        PortName = serialPort1.PortName;
                                        break;
                                    }
                                    else
                                    {
                                        bool flag_open = false;
                                        int k = 0;
                                        serialPort1.PortName = a[j];
                                        while (!flag_open)
                                        {
                                            serialPort1.Open();
                                            if (serialPort1.IsOpen == true)
                                            {
                                                flag_open = true;
                                                PortName = serialPort1.PortName;
                                                break;
                                            }
                                            k++;
                                            if (k >= 10)
                                            {
                                                break;
                                            }
                                        }
                                        if (serialPort1.IsOpen == true)
                                        {
                                            flag = 0xFF;
                                            PortName = serialPort1.PortName;
                                            break;
                                            //button1.BackgroundImage = Properties.Resources.Go_;
                                        }
                                    }
                                }
                                catch (IOException)//串口不存在异常
                                {
                                    j++;

                                    return 0x01;
                                    //goto go;

                                }
                                catch (UnauthorizedAccessException)//串口资源占用异常
                                {
                                    j++;
                                    return 0x02;
                                    // goto go;
                                }
                                catch (InvalidOperationException)//串口打开后，不能在赋值端口异常
                                {
                                    j++;
                                    return 0x03;
                                    //goto go;
                                }
                            }

                            if (!String.IsNullOrEmpty(PortName))
                            {
                                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                                //serialPort1.DiscardInBuffer();
                                serialPort1.ReceivedBytesThreshold = 1;
                                serialPort1.DtrEnable = true;
                                //serialPort1.Open();
                                if (serialPort1.IsOpen)
                                {
                                    //serialPort1.PortName = PortName;
                                }
                            }


                        }
                        if (serialPort1.IsOpen == false)
                        {
                            flag = 0x00;
                        }

                    }
                    catch
                    {
                    }
                }
                else
                {


                    try
                    {

                        bool flag_open = false;
                        int k = 0;
                        serialPort1.PortName = _PortName;
                        if (serialPort1.IsOpen == false)
                        {
                            while (!flag_open)
                            {
                                serialPort1.Open();
                                if (serialPort1.IsOpen == true)
                                {
                                    flag_open = true;
                                    break;
                                }
                                k++;
                                if (k >= 10)
                                {
                                    break;
                                }
                            }
                        }

                        if (serialPort1.IsOpen == true)
                        {
                            flag = 0xFF;
                            PortName = serialPort1.PortName;
                            //button1.BackgroundImage = Properties.Resources.Go_;
                        }

                    }

                    catch (IOException)//串口不存在异常
                    {

                        return 0x01;
                        //goto go;

                    }
                    catch (UnauthorizedAccessException)//串口资源占用异常
                    {
                        return 0x02;
                        // goto go;
                    }
                    catch (InvalidOperationException)//串口打开后，不能在赋值端口异常
                    {
                        return 0x03;
                        //goto go;
                    }


                    if (!String.IsNullOrEmpty(PortName))
                    {
                        serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                        //serialPort1.DiscardInBuffer();
                        serialPort1.ReceivedBytesThreshold = 1;
                        serialPort1.DtrEnable = true;
                        //serialPort1.Open();
                        if (serialPort1.IsOpen)
                        {
                            //serialPort1.PortName = PortName;
                        }
                    }



                    if (serialPort1.IsOpen == false)
                    {
                        flag = 0x00;
                    }
                }

                return flag;

            }
        }

        public string[] searchPortName()
        {
            string[] a = System.IO.Ports.SerialPort.GetPortNames();
            return a;
        }


        public void serialPort1_DataReceived(object sender, EventArgs e)
        {
            // event_1.WaitOne();

            byte[] RecBuffer;//接收缓冲区

            try
            {
                Thread.Sleep(200);
                if (serialPort1.BytesToRead == 0) return;
                semaSendRecieve.WaitOne();
                RecBuffer = new byte[serialPort1.BytesToRead];//创建接收字节数组
                serialPort1.Read(RecBuffer, 0, RecBuffer.Length);//读取数据
                ResceiveMessage(this, new SerialRecieveEventArgs(RecBuffer));
                
                semaSendRecieve.Release();
            }
            catch
            {

            }
        }
        public void ClosePort()
        {
            try
            {
                if (serialPort1 != null)
                {
                    bool flag_close = true;

                    while (flag_close)
                    {
                        serialPort1.Close();
                        if (serialPort1.IsOpen == false)
                        {
                            flag_close = false;
                        }
                    }

                    //serialPort1 = null;
                }
                else
                    return;
            }
            catch (Exception er)
            {
                ;
            }

        }
        public bool IsOpen()
        {
            bool flag;
            if (serialPort1 != null)
            {
                if (serialPort1.IsOpen == true)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

            }
            else
            {
                flag = false;
            }
            return flag;

        }
        public int WriteByteToSerialPort(byte[] Temb, int len, int delay = 50)
        {
            //serialPort1.PortName  = PortName;
            if (serialPort1 == null)
                return -2;
            //int len = Temb.Length;
            if (!serialPort1.IsOpen)
                return -1;
            else
            {
                try
                {
                    //semaSendRecieve.WaitOne();
                    serialPort1.Write(Temb, 0, len);
                    //semaSendRecieve.Release();
                    //event_1.Set();
                    Thread.Sleep(delay);
                }
                catch
                {
                    return -3;
                }
            }       
            return 0;
        }
       public static  string [] portnameArray ()
        {
            string[] temp = new string[20];
           for (int i=0;i<20;i++)
           {
               temp[i] = "COM" + (i+1).ToString();
           }
           return temp;
        }
       public static string[] baudrateArray()
       {
           string []temp = new string[] { "2400", "4800","9600", "14400", "19200", "28800", "38400", "57600", "115200" };
           return temp;
       }
       public static string[] dataArray()
        {
            string[] temp= new string[] { "6", "7", "8" };
            return temp;
        }
       public static string[] checkArray()
        {
            string[] temp = new string[] { "None", "Odd", "Even" };
            return temp;
        }
       public static string[] stopArray()
        {
            string[] temp = new string[] { "1", "2" };
            return temp;
        }
        public  static  string [] SerialPortUIparam()
        {
            string[] ParaArray = new string[8];
            ParaArray[0] = PortName;
            ParaArray[1] = BaudRate.ToString();
            ParaArray[2] = DataSize.ToString();
            ParaArray[3] = PParity;
            ParaArray[4] = SStopBits;
            if (flag ==0xFF)
            {             
                ParaArray[5] = "串口已打开";
                ParaArray[6] = "关闭串口";
                ParaArray[7] = "true";
            }
            else
            {
                ParaArray[0] = "COM1";          
                ParaArray[5] = "串口已关闭";
                ParaArray[6] = "打开串口";
                ParaArray[7] = "fasle";
            }
            return ParaArray;

        }
    }
}
