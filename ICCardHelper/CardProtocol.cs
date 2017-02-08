using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using eCardInfo;
using SerialportHelper;
using System.Diagnostics;
using AlgorithmHelper;

//using ZRGPS_Common;

namespace ICCardHelper
{
    public class CardProtocol
    {
        private static CardProtocol Instance;
        private static object _lock = new object();
        private static object _lock1 = new object();
        public byte[] new_card = new byte[30] ;//开卡
        public byte[] recharge_card = new byte[23];//充值
        public byte[] deductions_card = new byte[22];//扣款
        public byte[] query_card = new byte[23];//查询余额
        public byte[] initialize_card = new byte[12] ;//销卡      
        byte[] load_Key_card = new byte[20]; //装载秘钥
        byte[] readBlock_card = new byte[13]; //读块
        byte[] writeBlock_card = new byte[12]; //读块
        byte[] deductions_money_temp = new byte[3];//扣款金额
        byte[] money_before_deductions_temp = new byte[4];//扣款前余额
        byte[] money_current_temp = new byte[4];//当前余额
        byte[] money_bill_temp = new byte[7];//账单
        private char  [] frameHead_string = new char [3] { 'M', 'C', 'U' };//注意字符和字符串的区别
        private byte [] frameHead_byte = new byte [3];
        Stopwatch stopwatch = new Stopwatch();
        TimeSpan ts ;
        Queue myQ = new Queue();
        private List<byte> buffer = new List<byte>(4096);
        private List<byte> bufferTemporary = new List<byte>(100);//发送数据数据暂存，主要用于处理转义字符7F
        private List<byte> bufferReceveData = new List<byte>(100);//接收数据暂存，主要用于处理转义字符7F
        public Semaphore semaSerialPort = new Semaphore(1, 1); //串口读写锁
        public Semaphore semaSendRecieve = new Semaphore(1, 1);//协议收发锁
        public delegate void DataHandle();
        //DataHandle interfaceUpdateHandle;
        public MySerialPort SerialportObject = new MySerialPort();
        volatile Int32 Jine;
        public bool Flag = false, NewCard_flag = false, HuiFCard_flag = false;
        ArrayList arraylist = new ArrayList();
        static  CardInfo Card_Info = new CardInfo();
        CardInfo card_info_read_temp = new CardInfo();
        public  ParameterInfo parameterInfoObject = new ParameterInfo();
        private  ConsumptionRecords consumptionRecordsObject = new ConsumptionRecords();
        public List<ConsumptionRecords> consumptionRecordsList = new List<ConsumptionRecords>();
        int BaudTemp=9600;
        private const Int32 moneyBase = 0xffff;
        public static byte[] LastmoneyReport = new byte[4];//卡主动上报的卡内余额；
        Func<string, Int32, byte[], byte[], byte[], byte ,CardInfo> delegateWriteBlock =null ;
        Func<string, Int32, CardInfo> delegateReadBlock = null ;
        Func<string, double, byte , CardInfo> delegateQueryBalance =null ;
        private byte block_num_pram;
        public byte bill_Sum=1;
        enum CardstateEnum { Card_normal=0x50,Card_locked=0x5A,Card_unopend=0x00 };
        bool Card_statebool = false;//0x50正常卡，0x5A锁卡;0x00:未开卡；其他：未知卡
        byte Card_Available_tags;//卡可用状态标记
        bool Bill_clear=true  ;
        bool continue_ornot = false;
        bool new_card_flag = false;
        private byte[] reflection_table = new byte[32]{9,10,12,13,14,16,17,18,20,21,22,24,25,26,28,29,30,32,33,34,36,37,38,40,41,42,44,45,46,48,49,50};

        #region virtual test data

        private static  byte[] newcard = new byte[16] { 0x43, 0x52, 0x44, 0x10, 0x2C, 0x00, 0x9B, 0x69, 0xA0, 0xCE, 0x00, 0x00, 0x07, 0xD0, 0x00, 0x00 };//CRC 0xFA ,0xFA
        #endregion

        public byte[] FrameHead_byte
        {
            get { return frameHead_byte; }
        }
        public CardProtocol()
        {
            SerialportObject.ResceiveMessage += serialport1_ResceiveMessage;
            //System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(Application.StartupPath + @"/pm3.wav");
        }

        public static CardProtocol GetInstance()
        {
            if (Instance == null)
            {
                lock (_lock)
                {
                    if (Instance == null)
                    {
                        Instance = new CardProtocol();
                    }
                }
            }
            return Instance;
        }
        /// <summary>
        /// 读卡，此命令用的是扣款指令，扣零
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="SUM"></param>
        /// <returns></returns>
        public CardInfo ReadCard(string CardID, Int32 SUM = 0)//读卡
        {
            stopwatch.Start();
            int k = 0;
            try
            {
               

            }
            catch (Exception error)
            {
                
            }
            while (true)
            {
                Thread.Sleep(100);
                if (k >= 10 || Card_Info.Opcode  != 0xFF)
                    break;
                k++;
            }
            return Card_Info;

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
        }
        /// <summary>
        /// 办卡
        /// </summary>
        /// 权限模式说明
        /// <param name="CardID"></param>
        /// <param name="SUM"></param>
        /// <returns></returns>
        public CardInfo New_card(string CardID,double Money,string mode,string key_read,string key_write)//开卡
        {
           
            stopwatch.Start();
            int k = 0;
            new_card_flag = true;
            const byte length=30;
            const byte command_flag=0x2C;
            byte[] key_temp = new byte[6];
            Int64 moneytemp;
            CardInitialization(ref Card_Info, ref consumptionRecordsObject);
           
            ushort CRC;
            try
            {
                delegateReadBlock = ReadBlock_card;
                delegateReadBlock(null, 8);
                if (continue_ornot )
                {
                    if (Card_Available_tags == 0x50)
                    {
                        Card_Info.Message = "已开卡，请勿重复开卡！";
                        Card_Info .Card_status = Card_Available_tags;
                        Card_Info.Opcode = -3;
                        return Card_Info;
                    }
                    if (Card_Available_tags == 0x5A)
                    {
                        Card_Info.Message = "卡已锁住，不能开卡！";
                        Card_Info.Opcode = -3;
                        return Card_Info;
                    }

                    Algorithmhelper.StringArray_ByteArray(frameHead_string, out frameHead_byte);//
                    Algorithmhelper.MemCopy<byte>(ref  new_card, 0, frameHead_byte, 0, 3);
                    new_card[3] = length;
                    new_card[4] = command_flag;
                    new_card[5] = Convert.ToByte(mode);
                    if (CardID != null)
                    {
                        Int32 cardid = Convert.ToInt32(CardID);
                        byte[] btid = Algorithmhelper.Int64_Bytes4(cardid);
                        Algorithmhelper.MemCopy<byte>(ref  new_card, 6, btid, 0, 4);
                    }
                    string ttt = (100 * Money).ToString("#");//不要用科学计数法
                    if (ttt == "")
                        moneytemp = 0;
                    else
                        moneytemp = Convert.ToInt64(ttt);
                    byte[] btmoney = Algorithmhelper.Int64_Bytes4(moneytemp + moneyBase);
                    Algorithmhelper.MemCopy<byte>(ref  new_card, 10, btmoney, 0, 4);
                    if (StringVerdict(key_read) && StringVerdict(key_write))
                    {
                        key_temp = Algorithmhelper.Key_string_byte(key_read);
                        Algorithmhelper.MemCopy<byte>(ref  new_card, 14, key_temp, 0, 6);
                        key_temp = Algorithmhelper.Key_string_byte(key_write);
                        Algorithmhelper.MemCopy<byte>(ref  new_card, 20, key_temp, 0, 6);
                    }
                    else
                    {
                        Card_Info.Message = "密码格式错误！";
                        Console.WriteLine("密码格式错误");
                    }
                    CRC = Algorithmhelper.CrcCheck(new_card, length - 2);
                    new_card[length - 2] = (byte)(CRC >> 8);
                    new_card[length - 1] = (byte)(CRC & 0x00ff);
                    SerialportObject.WriteByteToSerialPort(new_card, length);
                    Algorithmhelper.WriteLOG_Console(new_card, "开卡(发送)");
                }
               
            }
            catch (Exception error)
            {
                Card_Info.Message = error.Message;
                Card_Info.Opcode = 0xFF;
                Algorithmhelper.WriteLOG_Console(new_card, "开卡——异常："+error .Message+"(发送)");
            }
            while (true)
            {
                Thread.Sleep(500);
                if (k >= 10 || Card_Info.Opcode != 0xFF)
                    break;
                k++;

            }
            new_card_flag = false;
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
        }
        /// <summary>
        /// 充值，SUM为实际金额的100倍，充值金额存储在金额所在字节的前三个字节发送数据。
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="SUM"></param>
        /// <returns></returns>
        public CardInfo Recharge_card(string CardID, double  Money,byte recharge_flag=0x01)//充值
        {
           
            stopwatch.Start();
            int k = 0;
            const byte length = 23;
            const byte command_flag = 0x51;
            Int64 moneytemp;
            ushort CRC;
            
            try
            {
                delegateReadBlock = ReadBlock_card;
                delegateReadBlock.Invoke (null, 8);
                Thread.Sleep(50);
                CardInitialization(ref Card_Info, ref consumptionRecordsObject);
                if (continue_ornot )
                {
                    if (Card_Available_tags == 0x50)
                    {
                        
                        Algorithmhelper.StringArray_ByteArray(frameHead_string, out frameHead_byte);//
                        Algorithmhelper.MemCopy<byte>(ref  recharge_card, 0, frameHead_byte, 0, 3);
                        recharge_card[3] = length;
                        recharge_card[4] = command_flag;
                        recharge_card[5] = recharge_flag;
                        if (CardID != null)
                        {
                            Int32 cardid = Convert.ToInt32(CardID);
                            byte[] btid = Algorithmhelper.Int64_Bytes4(cardid);
                            Algorithmhelper.MemCopy<byte>(ref recharge_card, 6, btid, 0, 4);
                        }
                        DateTime currentTime = DateTime.Now;
                        int year = currentTime.Year, month = currentTime.Month, day = currentTime.Day, hour = currentTime.Hour, minute = currentTime.Minute, second = currentTime.Second;
                        recharge_card[10] = Convert.ToByte(year.ToString().Substring(0, 2), 10);
                        recharge_card[11] = Convert.ToByte(year.ToString().Substring(2, 2), 10);
                        recharge_card[12] = (byte)currentTime.Month;
                        recharge_card[13] = (byte)currentTime.Day;
                        recharge_card[14] = (byte)currentTime.Hour;
                        recharge_card[15] = (byte)currentTime.Minute;
                        recharge_card[16] = (byte)currentTime.Second;
                        string ttt = (100 * Money).ToString("#");//不要用科学计数法
                        if (ttt == "")
                            moneytemp = 0;
                        else
                            moneytemp = Convert.ToInt32(ttt);
                        byte[] momey_temp = Algorithmhelper.Int64_Bytes4(moneytemp);
                        Algorithmhelper.MemCopy<byte>(ref recharge_card, 17, momey_temp, 0, 4);
                        CRC = Algorithmhelper.CrcCheck(recharge_card, length - 2);
                        recharge_card[length - 2] = (byte)(CRC >> 8);
                        recharge_card[length - 1] = (byte)(CRC & 0x00ff);
                        SerialportObject.WriteByteToSerialPort(recharge_card, length);
                        Algorithmhelper.WriteLOG_Console(recharge_card, "充值(发送)");
                    }
                    if (Card_Available_tags == 0x5a)
                    {
                        Card_Info.Message = "锁卡，请联系管理员！";
                        Card_Info.Opcode = -6;
                        return Card_Info;
                    }
                    if (Card_Available_tags == 0x5e)
                    {
                        Card_Info.Message = "卡异常，请联系管理员！";
                        Card_Info.Opcode = -7;
                        return Card_Info;
                    }
                    if (Card_Available_tags ==0x00)
                    {
                        Card_Info.Message = "未开卡，请先开卡！";
                        Card_Info.Opcode = -5;
                        return Card_Info;
                    }
                    
                }
                
            }
            catch (Exception error)
            {
                Card_Info.Message = error.Message;
                Card_Info.Opcode = 0xFF;
                Algorithmhelper.WriteLOG_Console(recharge_card, "充值——异常：" + error.Message + "(发送)");
            }
            while (true)
            {
                Thread.Sleep(200);
                if (k >= 10 || Card_Info.Opcode != 0xFF)
                    break;
                k++;

            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

        }
        /// <summary>
        /// 刷卡，即扣款，使用从卡内扣除输入的金额。
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="SUM"></param>
        /// <returns></returns>
        public CardInfo Deductions_card(string CardID, double  Money, byte deductions_flag=0xFF)//扣款
        {
            # region 测试用

            //try
            //{
            //    block_num_pram = 0;
            //    delegateReadBlock = ReadBlock_card;
            //    if (card_info_read_temp !=null )
            //    {
            //        card_info_read_temp.Card_ID = string.Empty;
            //        card_info_read_temp.Card_status = 0x5a;
            //        card_info_read_temp.Card_type = string.Empty;
            //        card_info_read_temp.Debt_count = 0;
            //        card_info_read_temp.Debt_count_max = 0;
            //        card_info_read_temp.Last_record_serial_number = 0;
            //        card_info_read_temp.Opcode = 0xFF;
            //    }
            //    else
            //    {
            //        card_info_read_temp = new CardInfo();
            //    }
            //     card_info_read_temp=delegateReadBlock .Invoke(null,8); //暂时注释掉
            //    if (card_info_read_temp.Opcode == 0)
            //    {
            //        //Int32 balance_tempINT = Convert.ToInt32(consumptionRecordsObject.Balance);//如果后面扣款成功，则为扣款前余额，如果扣款失败，则为当前余额
            //        //money_before_deductions_temp = Algorithmhelper.Int32_Bytes4(balance_tempINT);//扣款前余额
            //        Algorithmhelper.MemCopy<byte>(ref money_before_deductions_temp, 0, money_before_deductions_temp, 0, 4);
            //    }
            //}
            //catch (Exception err)
            //{

            //}
            #endregion
            stopwatch.Start();
            int k = 0;
            const byte length = 22;
            const byte command_flag = 0x3C;
            ushort CRC;
            Int64 moneytemp;
            CardInitialization(ref Card_Info, ref consumptionRecordsObject);
            try
            {
                delegateReadBlock = ReadBlock_card;
                delegateReadBlock.Invoke(null, 8);
                Thread.Sleep(50);
                CardInitialization(ref Card_Info, ref consumptionRecordsObject);
                if(continue_ornot )
                {
                    if (Card_Available_tags == 0x50)
                    {
                        Algorithmhelper.StringArray_ByteArray(frameHead_string, out frameHead_byte);//
                        Algorithmhelper.MemCopy<byte>(ref deductions_card, 0, frameHead_byte, 0, 3);
                        deductions_card[3] = length;
                        deductions_card[4] = command_flag;
                        deductions_card[5] = deductions_flag;
                        if (CardID != null)
                        {
                            Int32 cardid = Convert.ToInt32(CardID);
                            byte[] btid = Algorithmhelper.Int64_Bytes4(cardid);
                            Algorithmhelper.MemCopy<byte>(ref deductions_card, 6, btid, 0, 4);
                        }
                        DateTime currentTime = DateTime.Now;
                        int year = currentTime.Year, month = currentTime.Month, day = currentTime.Day, hour = currentTime.Hour, minute = currentTime.Minute, second = currentTime.Second;
                        deductions_card[10] = Convert.ToByte(year.ToString().Substring(0, 2), 10);
                        deductions_card[11] = Convert.ToByte(year.ToString().Substring(2, 2), 10);
                        deductions_card[12] = (byte)currentTime.Month;
                        deductions_card[13] = (byte)currentTime.Day;
                        deductions_card[14] = (byte)currentTime.Hour;
                        deductions_card[15] = (byte)currentTime.Minute;
                        deductions_card[16] = (byte)currentTime.Second;
                        string ttt = (100 * Money).ToString("#");//不要用科学计数法
                        if (ttt == "")
                            moneytemp = 0;
                        else
                            moneytemp = Convert.ToInt32(ttt);
                        byte[] momey_temp = Algorithmhelper.Int64_Bytes4(moneytemp);
                        Algorithmhelper.MemCopy<byte>(ref deductions_card, 17, momey_temp, 1, 3);
                        CRC = Algorithmhelper.CrcCheck(deductions_card, length - 2);
                        deductions_card[length - 2] = (byte)(CRC >> 8);
                        deductions_card[length - 1] = (byte)(CRC & 0x00ff);
                        SerialportObject.WriteByteToSerialPort(deductions_card, length);
                        Algorithmhelper.WriteLOG_Console(deductions_card, "扣款(发送)");
                    }
                    if (Card_Available_tags == 0x5a)
                    {
                        Card_Info.Message = "锁卡，请联系管理员！";
                        Card_Info.Opcode = -6;
                        return Card_Info;
                    }
                    if (Card_Available_tags == 0x5e)
                    {
                        Card_Info.Message = "卡异常，请联系管理员！";
                        Card_Info.Opcode = -7;
                        return Card_Info;
                    }
                    if (Card_Available_tags == 0x00)
                    {
                        Card_Info.Message = "未开卡，请先开卡！";
                        Card_Info.Opcode = -5;
                        return Card_Info;
                    }
                }

                
            }
            catch (Exception error)
            {
                Card_Info.Message = error.Message;
                Card_Info.Opcode = 0xFF;
                Algorithmhelper.WriteLOG_Console(deductions_card, "扣款——异常：" + error.Message + "(发送)");
            }
            while (true)
            {
                Thread.Sleep(200);
                if (k >= 10 || Card_Info.Opcode != 0xFF)
                    break;
                k++;

            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;

        }
        public CardInfo Query_card(string CardID, double Money=0, byte recharge_flag=0x00)//查询卡信息
        {
            stopwatch.Start();
            int k = 0;
            const byte length = 23;
            const byte command_flag = 0x51;
            ushort CRC;
            CardInitialization(ref Card_Info, ref consumptionRecordsObject);
            try
            {
                delegateReadBlock = ReadBlock_card;
                delegateReadBlock.Invoke(null, 8);
                Thread.Sleep(50);
                CardInitialization(ref Card_Info, ref consumptionRecordsObject);
                if (continue_ornot )
                {
                    if (Card_Available_tags == 0x50)
                    {
                        Algorithmhelper.StringArray_ByteArray(frameHead_string, out frameHead_byte);//
                        Algorithmhelper.MemCopy<byte>(ref query_card, 0, frameHead_byte, 0, 3);
                        query_card[3] = length;
                        query_card[4] = command_flag;
                        query_card[5] = recharge_flag;
                        if (CardID != null)
                        {
                            Int32 cardid = Convert.ToInt32(CardID);
                            byte[] btid = Algorithmhelper.Int64_Bytes4(cardid);
                            Algorithmhelper.MemCopy<byte>(ref query_card, 6, btid, 0, 4);
                        }
                        DateTime currentTime = DateTime.Now;
                        int year = currentTime.Year, month = currentTime.Month, day = currentTime.Day, hour = currentTime.Hour, minute = currentTime.Minute, second = currentTime.Second;
                        query_card[10] = Convert.ToByte(year.ToString().Substring(0, 2), 10);
                        query_card[11] = Convert.ToByte(year.ToString().Substring(2, 2), 10);
                        query_card[12] = (byte)currentTime.Month;
                        query_card[13] = (byte)currentTime.Day;
                        query_card[14] = (byte)currentTime.Hour;
                        query_card[15] = (byte)currentTime.Minute;
                        query_card[16] = (byte)currentTime.Second;
                        string strtem = (100 * Money).ToString("");
                        Int32 moneytemp = Convert.ToInt32(strtem);
                        byte[] momey_temp = Algorithmhelper.Int64_Bytes4(moneytemp);
                        Algorithmhelper.MemCopy<byte>(ref query_card, 17, momey_temp, 0, 4);
                        CRC = Algorithmhelper.CrcCheck(query_card, length - 2);
                        query_card[length - 2] = (byte)(CRC >> 8);
                        query_card[length - 1] = (byte)(CRC & 0x00ff);
                        SerialportObject.WriteByteToSerialPort(query_card, length);
                        Algorithmhelper.WriteLOG_Console(query_card, "查询(发送)");
                    }
                    if (Card_Available_tags == 0x5a)
                    {
                        Card_Info.Message = "锁卡，请联系管理员！";
                        Card_Info.Opcode = -6;
                        return Card_Info;
                    }
                    if (Card_Available_tags == 0x5e)
                    {
                        Card_Info.Message = "卡异常，请联系管理员！";
                        Card_Info.Opcode = -7;
                        return Card_Info;
                    }
                    if (Card_Available_tags == 0x00)
                    {
                        Card_Info.Message = "未开卡，请先开卡！";
                        Card_Info.Opcode = -5;
                        return Card_Info;
                    }
                }
                
            }
            catch (Exception error)
            {
                Card_Info.Message = error.Message;
                Card_Info.Opcode = 0xFF;
                Algorithmhelper.WriteLOG_Console(recharge_card, "查询——异常：" + error.Message + "(发送)");
            }
            while (true)
            {
                Thread.Sleep(800);
                if (k >= 10 || Card_Info.Opcode != 0xFF)
                    break;
                k++;

            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
        }

        /// <summary>
        /// 销卡，将卡内密码初始化为FFFFFFFFFFFF
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="SUM"></param>
        /// <returns></returns>
        public CardInfo Initialize_card(string CardID)//销卡
        {
          
          
            stopwatch.Start();
           
            int k = 0;
            const byte length = 12;
            const byte command_flag = 0x38;
            ushort CRC;
            
            try
            {
                delegateReadBlock = ReadBlock_card;
                delegateReadBlock.Invoke (null, 8);
                Thread.Sleep(50);
                CardInitialization(ref Card_Info, ref consumptionRecordsObject);
                if (continue_ornot )
                {
                    if (Card_Available_tags == 0x50)
                    {
                        Algorithmhelper.StringArray_ByteArray(frameHead_string, out frameHead_byte);//
                        Algorithmhelper.MemCopy<byte>(ref initialize_card, 0, frameHead_byte, 0, 3);
                        initialize_card[3] = length;
                        initialize_card[4] = command_flag;
                        initialize_card[5] = 0xFF;
                        if (CardID != null)
                        {
                            Int32 cardid = Convert.ToInt32(CardID);
                            byte[] btid = Algorithmhelper.Int64_Bytes4(cardid);
                            Algorithmhelper.MemCopy<byte>(ref initialize_card, 6, btid, 0, 4);
                        }
                        CRC = Algorithmhelper.CrcCheck(initialize_card, length - 2);
                        initialize_card[length - 2] = (byte)(CRC >> 8);
                        initialize_card[length - 1] = (byte)(CRC & 0x00ff);
                        SerialportObject.WriteByteToSerialPort(initialize_card, length);
                        Algorithmhelper.WriteLOG_Console(initialize_card, "销卡(发送)");
                    }
                    if (Card_Available_tags == 0x5a)
                    {
                        Card_Info.Message = "锁卡，请联系管理员！";
                        Card_Info.Opcode = -6;
                        return Card_Info;
                    }
                    if (Card_Available_tags == 0x5e)
                    {
                        Card_Info.Message = "卡异常，请联系管理员！";
                        Card_Info.Opcode = -7;
                        return Card_Info;
                    }
                    if (Card_Available_tags == 0x00)
                    {
                        Card_Info.Message = "未开卡，不用销卡！";
                        Card_Info.Opcode = -5;
                        return Card_Info;
                    }
                    if (!Bill_clear)
                    {
                        Card_Info.Message = "还有未支付订单，不能销卡！";
                        Card_Info.Opcode = -8;
                        return Card_Info;
                    }
                }
                
            }
            catch (Exception error)
            {
                Card_Info.Message = error.Message;
                Card_Info.Opcode = 0xFF;
                Algorithmhelper.WriteLOG_Console(initialize_card, "销卡——异常：" + error.Message + "(发送)");
            }
            while (true)
            {
                Thread.Sleep(200);
                if (k >= 10 || Card_Info.Opcode != 0xFF)
                    break;
                k++;

            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
        }     
        /// <summary>
        /// 将秘钥A和秘钥B写入读卡器内，具体含义见Y13R用户手册
        /// </summary>
        /// <param name="_KeyA"></param>
        /// <param name="_KeyB"></param>
        /// <param name="Mode"></param>
        /// <returns></returns>
        public CardInfo Load_Key_card(string key_read, string key_write)//装载密码
        {
            stopwatch.Start();
            int k = 0;
            const byte length = 20;
            const byte command_flag = 0x47;
            byte[] key_temp = new byte[6];
            ushort CRC;
            try
            {
                //delegateReadBlock = ReadBlock_card;
                //delegateReadBlock.Invoke(null, 8);
                //Thread.Sleep(50);
                CardInitialization(ref Card_Info, ref consumptionRecordsObject);
                //if (continue_ornot )
                {
                    Algorithmhelper.StringArray_ByteArray(frameHead_string, out frameHead_byte);//
                    Algorithmhelper.MemCopy<byte>(ref load_Key_card, 0, frameHead_byte, 0, 3);
                    load_Key_card[3] = length;
                    load_Key_card[4] = command_flag;
                    load_Key_card[5] = 0xFF;
                    if (StringVerdict(key_read) && StringVerdict(key_write))
                    {
                        key_temp = Algorithmhelper.Key_string_byte(key_read);
                        Algorithmhelper.MemCopy<byte>(ref load_Key_card, 6, key_temp, 0, 6);
                        key_temp = Algorithmhelper.Key_string_byte(key_write);
                        Algorithmhelper.MemCopy<byte>(ref load_Key_card, 12, key_temp, 0, 6);
                    }
                    else
                    {
                        Card_Info.Message = "密码格式错误！";
                        Console.WriteLine("密码格式错误");
                    }
                    CRC = Algorithmhelper.CrcCheck(load_Key_card, length - 2);
                    load_Key_card[length - 2] = (byte)(CRC >> 8);
                    load_Key_card[length - 1] = (byte)(CRC & 0x00ff);
                    SerialportObject.WriteByteToSerialPort(load_Key_card, length);
                    Algorithmhelper.WriteLOG_Console(load_Key_card, "装载密码(发送)");
                }
                //else
                //{
                //    Card_Info.Message = "卡异常，请联系管理员！";
                //    Card_Info.Opcode = -7;
                //    return Card_Info;
                //}
               
            }
            catch (Exception error)
            {
                Card_Info.Message = error.Message;
                Card_Info.Opcode = 0xFF;
                Algorithmhelper.WriteLOG_Console(load_Key_card, "装载密码——异常：" + error.Message + "(发送)");
            }
            while (true)
            {
                Thread.Sleep(100);
                if (k >= 10 || Card_Info.Opcode != 0xFF)
                    break;
                k++;

            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
        }
        /// <summary>
        /// 读块
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="block_num"></param>
        /// <returns></returns>
        public CardInfo ReadBlock_card (string CardID, Int32 block_num)
        {
            stopwatch.Start();
            int k = 0;
            const byte length = 13;
            const byte command_flag = 0x5C;
            ushort CRC;
            CardInitialization(ref Card_Info, ref consumptionRecordsObject);
            try
            {
                //delegateReadBlock = ReadBlock_card;
                //delegateReadBlock(null, 8);
               // if (continue_ornot )
                {
                  //  if (Card_Available_tags == 0x50)
                    {
                        Algorithmhelper.StringArray_ByteArray(frameHead_string, out frameHead_byte);//
                        Algorithmhelper.MemCopy<byte>(ref readBlock_card, 0, frameHead_byte, 0, 3);
                        readBlock_card[3] = length;
                        readBlock_card[4] = command_flag;
                        readBlock_card[5] = 0xFF;
                        if (CardID != null)
                        {
                            Int32 cardid = Convert.ToInt32(CardID);
                            byte[] btid = Algorithmhelper.Int64_Bytes4(cardid);
                            Algorithmhelper.MemCopy<byte>(ref readBlock_card, 6, btid, 0, 4);
                        }
                        readBlock_card[10] = (byte)block_num;
                        CRC = Algorithmhelper.CrcCheck(readBlock_card, length - 2);
                        readBlock_card[length - 2] = (byte)(CRC >> 8);
                        readBlock_card[length - 1] = (byte)(CRC & 0x00ff);
                        SerialportObject.WriteByteToSerialPort(readBlock_card, length);
                        Algorithmhelper.WriteLOG_Console(readBlock_card, "读块(发送)");
                    }
                    if (Card_Available_tags == 0x5a)
                    {
                        Card_Info.Message = "锁卡，请联系管理员！";
                        Card_Info.Opcode = -6;
                        return Card_Info;
                    }
                    if (Card_Available_tags == 0x5e)
                    {
                        Card_Info.Message = "卡异常，请联系管理员！";
                        Card_Info.Opcode = -7;
                        return Card_Info;
                    }
                    if (!new_card_flag)
                    {
                        if (Card_Available_tags == 0x00)
                        {
                            Card_Info.Message = "未开卡，请先开卡！";
                            Card_Info.Opcode = -5;
                            return Card_Info;
                        }
                    }
                   
                }
                
            }
            catch (Exception error)
            {
                Card_Info.Message = error.Message;
                Card_Info.Opcode = 0xFF;
                Algorithmhelper.WriteLOG_Console(readBlock_card, "读块——异常：" + error.Message + "(发送)");
            }
            while (true)
            {
                Thread.Sleep(100);
                if (k >= 10 || Card_Info.Opcode != 0xFF)
                    break;
                k++;

            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
        }

        /// <summary>
        /// 写块
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="block_num"></param>
        /// <returns></returns>
        public CardInfo WriteBlock_card(string CardID, Int32 block_num,byte []bill,byte []moneyop,byte []lastmoney,byte flag=0xFF)
        {
            stopwatch.Start();
            int k = 0;
            const byte length = 29;
            const byte command_flag = 0x3D;
            ushort CRC;
            CardInitialization(ref Card_Info, ref consumptionRecordsObject);
            try
            {
                delegateReadBlock = ReadBlock_card;
                delegateReadBlock.Invoke(null, 8);
                Thread.Sleep(50);
                CardInitialization(ref Card_Info, ref consumptionRecordsObject);
                if (continue_ornot )
                {
                    if (Card_Available_tags == 0x50)
                    {
                        Algorithmhelper.StringArray_ByteArray(frameHead_string, out frameHead_byte);//
                        Algorithmhelper.MemCopy<byte>(ref writeBlock_card, 0, frameHead_byte, 0, 3);
                        writeBlock_card[3] = length;
                        writeBlock_card[4] = command_flag;
                        writeBlock_card[5] = flag;
                        if (CardID != null)
                        {
                            Int32 cardid = Convert.ToInt32(CardID);
                            byte[] btid = Algorithmhelper.Int64_Bytes4(cardid);
                            Algorithmhelper.MemCopy<byte>(ref writeBlock_card, 6, btid, 0, 4);
                        }
                        writeBlock_card[10] = (byte)block_num;
                        Algorithmhelper.MemCopy<byte>(ref writeBlock_card, 11, bill, 0, 6);
                        Algorithmhelper.MemCopy<byte>(ref writeBlock_card, 17, moneyop, 0, 3);
                        Algorithmhelper.MemCopy<byte>(ref writeBlock_card, 20, LastmoneyReport, 0, 4);
                        CRC = Algorithmhelper.CrcCheck(writeBlock_card, length - 2);
                        writeBlock_card[length - 2] = (byte)(CRC >> 8);
                        writeBlock_card[length - 1] = (byte)(CRC & 0x00ff);
                        SerialportObject.WriteByteToSerialPort(writeBlock_card, length);
                        Algorithmhelper.WriteLOG_Console(writeBlock_card, "写块(发送)");
                    }
                    if (Card_Available_tags == 0x5a)
                    {
                        Card_Info.Message = "锁卡，请联系管理员！";
                        Card_Info.Opcode = -6;
                        return Card_Info;
                    }
                    if (Card_Available_tags == 0x5e)
                    {
                        Card_Info.Message = "卡异常，请联系管理员！";
                        Card_Info.Opcode = -7;
                        return Card_Info;
                    }
                    if (Card_Available_tags == 0x00)
                    {
                        Card_Info.Message = "未开卡，请先开卡！";
                        Card_Info.Opcode = -5;
                        return Card_Info;
                    }
                }
               
            }
            catch (Exception error)
            {
                Card_Info.Message = error.Message;
                Card_Info.Opcode = 0xFF;
                Algorithmhelper.WriteLOG_Console(writeBlock_card, "写块——异常：" + error.Message + "(发送)");
            }
            while (true)
            {
                Thread.Sleep(100);
                if (k >= 10 || Card_Info.Opcode != 0xFF)
                    break;
                k++;

            }
            return Card_Info;
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
        }
        /// <summary>
        /// 查询记录，共可以查询32条记录
        /// </summary>
        /// <param name="CardID"></param>
        /// <param name="start_block_num"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public string QueryConsumptionRecords(string CardID)
        {
            int k = 0;
            try
            {
                if (continue_ornot )
                {
                    delegateReadBlock = ReadBlock_card;
                    delegateReadBlock.Invoke(null, 8);
                    Thread.Sleep(50);
                    CardInitialization(ref Card_Info, ref consumptionRecordsObject);
                    if (Card_Available_tags == 0x50)
                    {
                        int i = 0;
                        consumptionRecordsList.RemoveRange(0, consumptionRecordsList.Count);
                        delegateReadBlock = ReadBlock_card;

                        #region 读参数信息，获取订单记录
                        {
                            //card_info_read_temp = new CardInfo();
                            //card_info_read_temp = delegateReadBlock.Invoke(null, 8); //暂时注释掉
                            if (card_info_read_temp.Opcode == 0)
                            {
                                //bill_Sum =(byte) card_info_read_temp.Debt_count;
                                //Algorithmhelper.MemCopy<byte>(ref money_before_deductions_temp, 0, money_before_deductions_temp, 0, 4);
                            }
                        }

                        #endregion

                        for (; i < bill_Sum + 1; i++)
                        {
                            CardInfo temp = ReadBlock_card(null, reflection_table[i]);
                            Console.WriteLine("卡号：{0} ，返回信息： {1} ， 金额：{2} ， 操作码：{3} ", temp.Card_ID, temp.Message, temp.Money, temp.Opcode);

                        }
                        //var RecordsList = consumptionRecordsList.Where<ConsumptionRecords>(s => s.Block_NUM != 8);
                        //return JsonHelper.Jsonhelper.ObjectToJson<List<ConsumptionRecords>>(RecordsList);
                        consumptionRecordsList.RemoveRange(0, 1);
                    }
                    if (Card_Available_tags == 0x5a)
                    {
                        Card_Info.Message = "锁卡，请联系管理员！";
                        Card_Info.Opcode = -6;
                        return null ;
                    }
                    if (Card_Available_tags == 0x5e)
                    {
                        Card_Info.Message = "卡异常，请联系管理员！";
                        Card_Info.Opcode = -7;
                        return null ;
                    }
                    if (Card_Available_tags == 0x00)
                    {
                        Card_Info.Message = "未开卡，无订单可查！";
                        Card_Info.Opcode = -5;
                        return null ;
                    }
                }
                
            }
            catch (Exception error)
            {
                Card_Info.Message = error.Message;
                Card_Info.Opcode = 0xFF;
                Algorithmhelper.WriteLOG_Console(writeBlock_card, "写块——异常：" + error.Message + "(发送)");
            }
            while (true)
            {
                Thread.Sleep(100);
                if (k >= 10 || Card_Info.Opcode != 0xFF)
                    break;
                k++;

            }
            return JsonHelper.Jsonhelper.ObjectToJson<List<ConsumptionRecords>>(consumptionRecordsList);
        }
        void serialport1_ResceiveMessage(object sender, SerialRecieveEventArgs e)
        {
            //throw new NotImplementedException() ;
            stopwatch.Start();
           
            lock (_lock1)
            {
                try
                {
                    //Thread.Sleep(500);
                    buffer.AddRange(e.receiveData);
                    //semaSerialPort.WaitOne();
                    //Algorithmhelper.WriteLOG_Console(buffer.ToArray(), "原始接收数据");
                    dataverdict(buffer);
                    ComRec();
                    //this.Invoke(interfaceUpdateHandle);
                    // semaSerialPort.Release();

                }
                catch
                {
                    buffer.RemoveRange(0, buffer.Count);
                    bufferReceveData.RemoveRange(0, bufferReceveData.Count);
                }
            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
           // Algorithmhelper.WriteLOG_Console (e.receiveData  , "接收的数据");
            
            
        }
        private void dataverdict(List<byte> buffer)//对接收到的数据做判断
        {
            try
            {
                while (buffer.Count >= 4)
                {
                    bool flag = false;
                    byte [] head =new byte[3];
                    byte length = buffer[3];
                    flag = Algorithmhelper.FrameHeadVerdict(buffer.ToArray () ) ;
                   
                    if (flag && length  == buffer.Count)
                    {
                        myQ.Enqueue(buffer);
                        //buffer.RemoveRange(0, buffer.Count);
                        break;
                    }
                    else if (flag && length !=buffer.Count) 
                    {
                        buffer.RemoveRange(0, buffer.Count);
                        break;
                    }
                    else
                    {
                        buffer.RemoveRange(0, buffer.Count);
                        break;
                    }                       
                }
            }
            catch(Exception error)
            {
                buffer.RemoveRange(0, buffer.Count);
                Console.WriteLine("异常信息：{0}",error.Message);


            }
        }
        void ComRec()//协议解析
        {

            if (myQ.Count > 0)
            {
                byte[] recBuffer = (myQ.Dequeue() as List<byte>).ToArray();
                string[] str1 = new string[recBuffer.Length];
                int   len = recBuffer.Length;
                ushort  CRC,CRCH,CRCL;
                byte FunCode = recBuffer[4];//功能码，区别不同命令 
                byte state = recBuffer[5];//应答标记
                CRC = Algorithmhelper.CrcCheck (recBuffer ,(uint)len-2 );
                buffer.RemoveRange(0, buffer.Count);   //清缓存
                CRCH=(ushort )( CRC>>8);
                CRCL=(ushort )( CRC&0x00ff);
                if (CRCH == recBuffer[len - 2] && CRCL == recBuffer[len - 1])
                {
                    if (FunCode == 0x1A)//读卡-主动上报
                    {                       
                        switch (state)
                        {
                            case 0x00:
                                Card_Info.Opcode = 0x00;
                                Card_Info .Card_ID =Algorithmhelper.Byte4_Int64(recBuffer,6,4 ).ToString () ;
                                consumptionRecordsObject.Card_ID = Card_Info.Card_ID;
                                Card_Info .Money =(((double )Algorithmhelper.Byte4_Int64 (recBuffer ,12,4))/100).ToString ("#0.00");
                               // Card_Info .Card_type =几种类型？？？？
                                Card_Info.Debt_count = recBuffer[16];
                                Card_Info.Debt_count_max = recBuffer[17];
                                Card_Info.Last_record_serial_number = recBuffer[18];
                                Card_Info.Message = "卡正常";
                                bill_Sum = recBuffer[18];
                                Card_Available_tags = recBuffer[19];
                                if (recBuffer [5]==0x00)
                                    continue_ornot =true ;
                               // Card_Info.Card_status = recBuffer[19];//卡状态
                                if (recBuffer[19] == 0x50) 
                                    Card_statebool = true; 
                                if ((recBuffer[20] & 0xff) != 0 || (recBuffer[21] & 0xff) != 0 || (recBuffer[22] & 0xff) != 0 || (recBuffer[23] & 0xff) != 0)
                                    Bill_clear = false;
          
                                Algorithmhelper.MemCopy<byte>(ref LastmoneyReport, 0, recBuffer, 12, 4); 
                                break;
                            case 0x0f:
                                Card_Info.Message = "卡异常";
                                Card_Info.Opcode = -3;
                                break;
                            default :
                                break;
                        }
                        Algorithmhelper.WriteLOG_Console(recBuffer, "读卡（接收数据）");

                    }
                    if (FunCode == 0x2C)//开卡
                    {
                        Algorithmhelper .WriteLOG_Console(recBuffer, "开卡（接收数据）");
                        switch (state)
                        {
                            case 0x00:            //正确                                                       
                                Card_Info.Card_ID  = Algorithmhelper .Byte4_Int64 (recBuffer ,6,4).ToString ();
                                Card_Info.Money =(((double ) (Algorithmhelper.Byte4_Int64(recBuffer, 10,4)-moneyBase))/100).ToString ("#0.00");//此处将10误写成了15导致结果不正确(进入异常，故无法获得正确结果)，切记，20170119
                 
                                Card_Info.Message = "操作成功";
                                Card_Info.Opcode = 0x00;
                                //Console.WriteLine(Card_Info.Message);
                                NewCard_flag = true;
                                Card_Info.Card_status = 0x50;//修改卡的状态
                                Console.WriteLine("开卡成功！");
                                break;
                            case 0x01:            //部分成功
                                Jine = 0xFF;
                                Card_Info.Message = "部分成功";
                                Card_Info.Opcode  = -1;
                              //  Console.WriteLine(Card_Info.Message);
                                break;
                            case 0x0F:             //操作失败
                                Jine = 0xFE;
                                Card_Info.Opcode  = -3;
                                Card_Info.Message = "操作失败";
                               // Console.WriteLine(Card_Info.Message);
                                break;
                            case 0x0E:             //校验错误
                                Jine = 0xFB;
                                Card_Info.Opcode  = -4;
                                Card_Info.Message = "校验错误";
                               // Console.WriteLine(Card_Info.Message);
                                break;
                        }

                    }
                    if (FunCode == 0x51)//充值
                    {
                        Algorithmhelper .WriteLOG_Console(recBuffer, "充值（接收数据）");
                        switch (state)
                        {
                            case 0x00:
                                Card_Info.Opcode  = 0;
                                Card_Info.Message = "操作成功";
                                Console.WriteLine(Card_Info.Message);
                                Card_Info.Card_ID = Algorithmhelper.Byte4_Int64(recBuffer, 6,4).ToString ();
                                Card_Info.Money =(((double)(Algorithmhelper.Byte4_Int64(recBuffer, 10,4) - moneyBase)) / 100).ToString ("#0.00");
                                Card_Info.Card_status = Card_Available_tags;
                                break;
                            case 0x0F:              //操作失败    
                                Card_Info.Opcode = -3;
                                Card_Info.Message = "操作失败";
                                Console.WriteLine(Card_Info.Message);
                                break;
                            case 0x0E:              //校验错误
                                Card_Info.Opcode  = -4;
                                Card_Info.Message = "校验错误";
                                Console.WriteLine(Card_Info.Message);
                                break;
                        }

                    }
                    if (FunCode == 0x3C)//扣款
                    {
                        Algorithmhelper .WriteLOG_Console(recBuffer, "扣款（接收数据）");
                        int ID3 = 0;
                        //CardInfo card_info_read_temp,card_info_write_temp;
                        delegateWriteBlock = WriteBlock_card;
                        byte []tempbill=new byte[6];
                        
                        switch (state)
                        {
                            case 0x00://正常扣款
                                Card_Info.Opcode = 0x00;                                
                                Card_Info.Card_ID  = Algorithmhelper .Byte4_Int64 (recBuffer ,6,4).ToString ();
                                Card_Info.Money = (((double)(Algorithmhelper.Byte4_Int64(recBuffer, 10,4) - moneyBase)) / 100).ToString ("#0.00");
                                Card_Info.Message = "操作成功";
                                Card_Info.Card_status = Card_Available_tags;
                                #region 仅做测试用

                                //Algorithmhelper .MemCopy <byte>(ref tempbill ,0,deductions_card,11,6);//账单，写块时使用
                                //Algorithmhelper .MemCopy <byte>(ref deductions_money_temp ,0,deductions_card ,17,3);//扣款金额
                                //Algorithmhelper .MemCopy <byte>(ref money_bill_temp,0,deductions_card ,10,7)  ;//账单，返回结果时使用
                                //Algorithmhelper.MemCopy<byte>(ref money_current_temp, 0, recBuffer, 10, 4);//当前金额，如果扣款成功，返回当前金额
                               
                                //card_info_read_temp=delegateReadBlock .Invoke(null,8);//暂时注释掉
                                //if (card_info_read_temp.Opcode ==0)
                                //{
                                //    Int32 balance_tempINT=Convert .ToInt32 ( consumptionRecordsObject.Balance);//
                                //    money_before_deductions_temp=Algorithmhelper .Int32_Bytes4 (balance_tempINT ) ;//扣款前余额
                                //    Algorithmhelper .MemCopy <byte>(ref money_before_deductions_temp ,0,money_before_deductions_temp ,0,4 );
                                //    delegateWriteBlock.BeginInvoke(null, block_num_pram+1, tempbill, deductions_money_temp, money_before_deductions_temp, 0xFF, null, null);//写块
                                //}
                                
                                #endregion
                                Console.WriteLine(Card_Info.Message);
                                break;
                            case 0x01://已扣款
                                Card_Info.Opcode = -1;
                                Card_Info.Message = "已扣款";
                                Console.WriteLine(Card_Info.Message);
                                break;
                            case 0x02: //余额不足
                                Card_Info.Opcode = -2;
                                Card_Info.Message = "余额不足";
                                Console.WriteLine(Card_Info.Message);
                                break;
                            case 0x0f://操作失败       
                                Card_Info.Opcode = -3;
                                Card_Info.Message = "操作失败";
                                Console.WriteLine(Card_Info.Message);
                                break;
                            case 0x0E://校验错误
                                Card_Info.Opcode = -4;
                                Card_Info.Message = "校验错误";
                                Console.WriteLine(Card_Info.Message);
                                break;
                        }
                    }
                    if (FunCode == 0x38)//恢复卡
                    {
                        Algorithmhelper .WriteLOG_Console(recBuffer, "恢复卡（接收数据）");
                        switch (state)
                        {
                            case 0x00:
                                Card_Info.Opcode = 0x00;
                                Card_Info.Card_ID  = Algorithmhelper .Byte4_Int64 (recBuffer ,6,4).ToString ();
                                Card_Info.Message = "操作成功";
                                Console.WriteLine(Card_Info.Message);
                                break;
                            case 0x01:                  //部分成功
                                Card_Info.Opcode  = -1;
                                Card_Info.Message = "部分成功";
                                Console.WriteLine(Card_Info.Message);
                                break;
                            case 0x0F:                   //操作失败
                                Jine = 0xFE;
                                Card_Info.Opcode = -3;
                                Card_Info.Message = "操作失败";
                                Console.WriteLine(Card_Info.Message);
                                break;
                            case 0x0E:                  //校验错误
                                Jine = 0xFB;
                                Card_Info.Opcode = -4;
                                Card_Info.Message = "校验错误";
                                Console.WriteLine(Card_Info.Message);
                                break;
                        }
                    }

                    if (FunCode == 0x47)//装载密码
                    {
                        Algorithmhelper .WriteLOG_Console(recBuffer, "装载秘钥（接收数据）");
                        switch (state)
                        {
                            case 0x00:
                                Card_Info.Opcode  = 0;
                                Card_Info.Message = "操作成功";
                                Console.WriteLine(Card_Info.Message);
                                Card_Info.Card_status =Card_Available_tags;
                                break;
                            case 0x0F:
                                Card_Info.Opcode = -3;
                                Card_Info.Message = "操作失败";
                                Console.WriteLine(Card_Info.Message);
                                break;
                            case 0x0E:
                                Card_Info.Opcode = -4;
                                Card_Info.Message = "CRC错误";
                                Console.WriteLine(Card_Info.Message);
                                break;
                        }
                    }
                    if (FunCode == 0x5C)//读块
                    {
                        Console.WriteLine("块号：{0}", recBuffer[10]);
                        Algorithmhelper.WriteLOG_Console(recBuffer, "读块（接收数据）");
                        try
                        {
                            ConsumptionRecords consumptionRecords_read_block_tempObject = new ConsumptionRecords();
                            consumptionRecords_read_block_tempObject.Card_ID = Algorithmhelper .Byte4_Int64 (recBuffer,6,4).ToString ("#");//卡号
                            consumptionRecords_read_block_tempObject.Block_NUM = recBuffer[10];//块号
                            consumptionRecords_read_block_tempObject.OpcodeType = "READ BLOCK";//操作类型
                            //delegateQueryBalance = Recharge_card ;
                            switch (state)
                            {
                                case 0x00:
                                    Card_Info.Opcode = 0;
                                    const byte length=6;
                                    Card_Info.Card_ID = Algorithmhelper.Byte4_Int64(recBuffer, 6,4).ToString();
                                    Card_Info.Message = "操作成功";
                                    Card_Info.Card_status = Card_Available_tags;
                                    //consumptionRecordsObject.Block_NUM = recBuffer[8];
                                    //consumptionRecordsObject.Balance =Algorithmhelper .Byte4_Int32(recBuffer ,2).ToString ();
                                    if (recBuffer[10]==8)//参数信息
                                    {
                                        //Algorithmhelper.MemCopy<byte>(ref  money_before_deductions_temp , 0, recBuffer, 13, 4);//存储扣款前余额,写记录时用
                                        block_num_pram = recBuffer[19];
                                        bill_Sum = recBuffer[19];//账单总数
                                        Card_Available_tags = recBuffer[20];
                                        Card_Info.Money = (((double)(Algorithmhelper.Byte4_Int64(recBuffer, 13, 4) - moneyBase)) / 100).ToString("#0.00");//存储当前余额
                                        if ((recBuffer[21] & 0xff) != 0 || (recBuffer[22] & 0xff) != 0 || (recBuffer[23] & 0xff) != 0 || (recBuffer[24] & 0xff) != 0)
                                            Bill_clear = false;
                                        if (recBuffer[20] == 0x50)
                                            continue_ornot = true;
                                        
                                    }
                                    else
                                    {
                                        StringBuilder strtemp = new StringBuilder();//存储账单信息
                                        for (byte p = 0; p < length; p++)
                                        {
                                            string str = recBuffer[p + 11].ToString();
                                            if (str.Length == 1)
                                                str = "0" + str;
                                            strtemp.Append(str);
                                        }
                                        consumptionRecords_read_block_tempObject.Bill = "20" + strtemp;//账单
                                        consumptionRecords_read_block_tempObject.DeductionsAmount =((double ) Algorithmhelper.Byte4_Int64(recBuffer, 17, 3)/100).ToString("#0.00");//扣款金额
                                        consumptionRecords_read_block_tempObject.BalanceBeforedeductions =( ((double )(Algorithmhelper.Byte4_Int64(recBuffer, 20, 4) - moneyBase)) / 100).ToString("#0.00");//扣款前金额
                                        Card_Info.Money = (Convert.ToSingle(consumptionRecords_read_block_tempObject.BalanceBeforedeductions) - Convert.ToSingle(consumptionRecords_read_block_tempObject.DeductionsAmount)).ToString("#0.00");
                                    }
                                    
                                    #region 查询当前余额
                                    //CardInfo  card_info_temp= delegateQueryBalance.Invoke (null ,0,0x00);
                                    //if (card_info_temp .Opcode ==0)
                                    //{
                                    //     consumptionRecordsObject.Balance =card_info_temp .Money.ToString () ;//当前余额
                                    //}
                                    #endregion
                                    consumptionRecords_read_block_tempObject.Message ="成功";
                                    Console.WriteLine(Card_Info.Message);
                                    break;
                                case 0x0F:
                                    Card_Info.Opcode = -3;
                                    Card_Info.Message = "操作失败";
                                    consumptionRecords_read_block_tempObject.Message ="失败";
                                    Console.WriteLine(Card_Info.Message);
                                    break;
                                case 0x0E:
                                    Card_Info.Opcode = -4;
                                    Card_Info.Message = "CRC错误";
                                    consumptionRecords_read_block_tempObject.Message ="失败";
                                    Console.WriteLine(Card_Info.Message);
                                    break;
                            }
                            if (consumptionRecords_read_block_tempObject.Block_NUM != null && consumptionRecords_read_block_tempObject.Block_NUM != 8)
                                consumptionRecordsList.Add(consumptionRecords_read_block_tempObject);
                        }
                        catch (Exception err)
                        {
                            Console.WriteLine();
                        }
                        finally
                        {
                           
                        }
                        
                    }
                    if (FunCode == 0x3D)//写块
                    {
                        Console.WriteLine("块号：{0}", recBuffer[10]);
                        Algorithmhelper.WriteLOG_Console(recBuffer, "写块（接收数据）");
                        try
                        {
                            consumptionRecordsObject.Card_ID = Card_Info.Card_ID;//卡号
                            consumptionRecordsObject.Block_NUM = recBuffer[8];//块号
                            consumptionRecordsObject.OpcodeType = "WRITE BLOCK";//操作类型
                            switch (state)
                            {
                                case 0x00:
                                    Card_Info.Opcode = 0;
                                    Card_Info.Card_ID = Algorithmhelper.Byte4_Int64(recBuffer, 6,4).ToString();
                                    Card_Info.Message = "操作成功";
                                    consumptionRecordsObject.Message = "成功";//操作信息
                                    Card_Info.Card_status = Card_Available_tags;
                                    //consumptionRecordsObject.Balance = Algorithmhelper.Byte4_Int32(money_current_temp,0,4).ToString ();//当前金额
                                    consumptionRecordsObject.BalanceBeforedeductions =(((double ) Algorithmhelper.Byte4_Int64(money_before_deductions_temp, 0,4)/100)).ToString("#0.00");//扣款前金额
                                    consumptionRecordsObject .DeductionsAmount =Algorithmhelper .Byte4_Int64 (deductions_money_temp ,0,3).ToString ("#0.00");//扣款金额
                                    StringBuilder strtemp=new StringBuilder();
                                    for (int p=0;p<money_bill_temp .Length ;p++)
                                    {
                                        strtemp.Append( money_bill_temp [p].ToString ("#"));
                                    }
                                    consumptionRecordsObject.Bill = strtemp.ToString ();
                                    Console.WriteLine(Card_Info.Message);
                                    break;
                                case 0x0F:
                                    Card_Info.Opcode = -3;
                                    Card_Info.Message = "操作失败";
                                    consumptionRecordsObject.Message = "失败";//操作信息
                                    Console.WriteLine(Card_Info.Message);
                                    break;
                                case 0x0E:
                                    Card_Info.Opcode = -4;
                                    Card_Info.Message = "CRC错误";
                                    consumptionRecordsObject.Message = "失败";//操作信息
                                    Console.WriteLine(Card_Info.Message);
                                    break;
                                default:
                                    break;

                            }
                            if (consumptionRecordsObject != null)
                                consumptionRecordsList.Add(consumptionRecordsObject);
                        }
                        catch (Exception er)
                        {

                        }
                        finally
                        {
                            
                        }
                    }
                                  
                }

            }
        }

        /// <summary>
        /// 数据类型转换，字节数组转为十进制数
        /// </summary>
        /// <param name="CD"></param>
        /// 
        bool StringVerdict(string str)
        {
            bool flag = false;
            if (str.Length != 12)
            {
                return false;
            }
            else
            {
                //int k = 0;
                //foreach (char _str in str)
                //{
                //    if (_str < '0' || _str > '9')
                //        flag = false;
                //    k++;
                //}
                //if (k >= 12)
                    flag = true;
            }
            return flag;
        }
        byte[] Key(string str)
        {
            byte[] bt = new byte[6] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            for (int i = 0; i < 6; i++)
            {
                string str1 = str.Substring(2 * i, 2);
                bt[i] = (byte)(Convert.ToInt32(str1, 16));
            }
            return bt;
        }

        #region Virtual Test Function
        public void NEWCARD()
        {
            short CRC;
            uint length =(uint ) newcard.Length;
            Console.WriteLine("开卡：");
            CRC = (short)Algorithmhelper.CrcCheck(newcard , length-2);
            newcard[length - 2] = (byte)(CRC >> 8);
            newcard[length - 1] = (byte)(CRC & 0x00ff);
            Console.WriteLine("CRC jiang :{0}", CRC);
            SerialportObject.WriteByteToSerialPort(newcard, (int )length);
            Algorithmhelper.WriteLOG_Console(newcard, "测试*****开卡(发送)");

            //CRC = Algorithmhelper.CRC16(newcard, 14);
            //Console.WriteLine("CRC me :{0}", CRC);
            //newcard[14] = (byte)(CRC >> 8);
            //newcard[15] = (byte)CRC;
            //SerialportObject.WriteByteToSerialPort(newcard, (int)length);
            //Algorithmhelper.WriteLOG_Console(newcard, "测试*****开卡(发送)");
        }
         
        #endregion

        #region 卡初始化
        public void CardInitialization( ref  CardInfo  cardinfo ,ref ConsumptionRecords consumm)
        {
            if (cardinfo != null)
            {
                cardinfo.Card_ID = string.Empty;
                cardinfo.Card_status = 0x0f;
                cardinfo.Card_type = string.Empty;
                cardinfo.Debt_count = 0;
                cardinfo.Debt_count_max = 0;
                cardinfo.Last_record_serial_number = 0;
                cardinfo.Message ="失败";
                //cardinfo.Mobile = string.Empty;
                cardinfo.Money = "0.00";
                cardinfo.Opcode = 0xFF;
                //cardinfo.User_name = string.Empty;
            }
            else
            {
                return;
            }
            if (consumm != null)
            {
                //consumm.Balance = string.Empty;
                consumm.BalanceBeforedeductions = string.Empty;
                consumm.Bill = string.Empty;
                consumm.Block_NUM = 0xFF;
                consumm.Card_ID = string.Empty;
                consumm.DeductionsAmount = string.Empty;
                consumm.Message = "失败";
                consumm.OpcodeType = "未知";
            }
            else
            {
                return;
            }
        }
        #endregion

    }
}
