using ICCardHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlgorithmHelper;
using SerialportHelper;
using eCardInfo;
using JsonHelper;
//using ZRGPS_Common;

namespace eCardProtocol
{
    public partial class frm_Test : Form
    {
        public static ICCardHelper.CardProtocol card2 = new CardProtocol();
     
        public frm_Test()
        {
            InitializeComponent();
        }

        private void frm_Test_Load(object sender, EventArgs e)
        {
            Console.WriteLine("C:{0},R:{1},D:{2}", Convert.ToByte('C'), Convert.ToByte('R'), Convert.ToByte('D'));
        }

        private void btnSetPort_Click(object sender, EventArgs e)
        {
            Frm_portSet frmtemp = new Frm_portSet();
            frmtemp.Show();
        }

        private void btnNew_card_Click(object sender, EventArgs e)
        {
            if (Global.card.SerialportObject.IsOpen())
            {

                Console.WriteLine("串口已打开,串口号：{0}", Global.card.SerialportObject.serialPort1.PortName);
                Console.WriteLine("准备开卡");
                Console.WriteLine("请输入充值金额");
                float money = Convert.ToSingle(Console.ReadLine().Trim());
                //Console.WriteLine("请输入读秘钥A");
                //string Akey = Console.ReadLine().Trim();
                //Console.WriteLine("请输入写秘钥B");
                //string Bkey = Console.ReadLine().Trim();
               // Global .card.CardInitialization (ref  Global .CardinformationObject );
                string Akey = "FFFFFFFFFFFF";
                string Bkey = "FFFFFFFFFFFF";
                Global .CardinformationObject = Global.card.New_card(null , money , null, Akey , Bkey );
                Console.WriteLine("卡号：{0} ，返回信息： {1} ， 金额：{2} ， 操作码：{3} ", Global.CardinformationObject.Card_ID, Global.CardinformationObject.Message, Global.CardinformationObject.Money, Global.CardinformationObject.Opcode);
            }
            else
            {
                MessageBox.Show("请打开串口");
                Console.WriteLine("串口号{0}：是否打开？ {1}", Global.card.SerialportObject.serialPort1.PortName, Global.card.SerialportObject.serialPort1.IsOpen.ToString());
            }
        }

        private void btnRecharge_card_Click(object sender, EventArgs e)
        {
            if (Global.card.SerialportObject.IsOpen())
            {
                Console.WriteLine("串口已打开,串口号：{0}", Global.card.SerialportObject.serialPort1.PortName);
                //Global.card.Recharge_card ("123", 40000,0x02);
                //Global.card.CardInitialization(ref  Global.CardinformationObject);
                Console.WriteLine("准备充值");
                Console.WriteLine("请输入充值金额");
                float money = Convert.ToSingle(Console.ReadLine().Trim());
                Global.CardinformationObject = Global.card.Recharge_card(null, money, 0x01);
                Console.WriteLine("卡号：{0} ，返回信息： {1} ， 金额：{2} ， 操作码：{3} ", Global.CardinformationObject.Card_ID, Global.CardinformationObject.Message, Global.CardinformationObject.Money, Global.CardinformationObject.Opcode);
            }
            else
            {
                MessageBox.Show("请打开串口");
                Console.WriteLine("串口号{0}：是否打开？ {1}", Global.card.SerialportObject.serialPort1.PortName, Global.card.SerialportObject.serialPort1.IsOpen.ToString());
            }
        }

        private void btnDeductions_card_Click(object sender, EventArgs e)
        {
            if (Global.card.SerialportObject.IsOpen())
            {
                Console.WriteLine("串口已打开,串口号：{0}", Global.card.SerialportObject.serialPort1.PortName);
                //Global.card.Deductions_card ("123", 20000, 0x01);
                Console.WriteLine("准备扣款");
                Console.WriteLine("请输入扣款金额");
                float money = Convert.ToSingle(Console.ReadLine().Trim());
                Global.CardinformationObject = Global.card.Deductions_card(null, money, 0x01);
                Console.WriteLine("卡号：{0} ，返回信息： {1} ， 金额：{2} ， 操作码：{3} ", Global.CardinformationObject.Card_ID, Global.CardinformationObject.Message, Global.CardinformationObject.Money, Global.CardinformationObject.Opcode);
            }
            else
            {
                MessageBox.Show("请打开串口");
                Console.WriteLine("串口号{0}：是否打开？ {1}", Global.card.SerialportObject.serialPort1.PortName, Global.card.SerialportObject.serialPort1.IsOpen.ToString());
            }
        }

        private void btnReadblock_Click(object sender, EventArgs e)
        {
            if (Global.card.SerialportObject.IsOpen())
            {
                Console.WriteLine("串口已打开,串口号：{0}", Global.card.SerialportObject.serialPort1.PortName);
                Console.WriteLine("请读块");
                Console.WriteLine("循环读取 VS 单次读取？循环读取请输入：Y,否则输入：N！");
                string str = Console.ReadLine().Trim();
                if (str =="Y")
                {
                    Console.WriteLine("***********************循环读取***********************");
                    int i = 0;
                    {
                        //for (; i < 64; i++)
                        //{
                        //    Global.CardinformationObject = Global.card.ReadBlock_card(null, i);
                        //    Console.WriteLine("卡号：{0} ，返回信息： {1} ， 金额：{2} ， 操作码：{3} ", Global.CardinformationObject.Card_ID, Global.CardinformationObject.Message, Global.CardinformationObject.Money, Global.CardinformationObject.Opcode);

                        //}
                        //if (i == 64)
                        //    Console.WriteLine(Jsonhelper.ObjectToJson<List<ConsumptionRecords>>(Global.card.consumptionRecordsList));
                    }
                    {
                        Console.WriteLine("***********************调用方法***********************");
                        Console.WriteLine(Global.card.QueryConsumptionRecords());
                    }
                   
                    


                }
                
                else
                {
                    Console.WriteLine("请输入块号：");
                    string blocknum = Console.ReadLine();
                    Global.CardinformationObject = Global.card.ReadBlock_card(null, Convert.ToInt16(blocknum));
                    Console.WriteLine("卡号：{0} ，返回信息： {1} ， 金额：{2} ， 操作码：{3} ", Global.CardinformationObject.Card_ID, Global.CardinformationObject.Message, Global.CardinformationObject.Money, Global.CardinformationObject.Opcode);
                    Console.WriteLine(Jsonhelper.ObjectToJson<List<ConsumptionRecords>>(Global.card.consumptionRecordsList));
                }
               

                
               
            }
            else
            {
                MessageBox.Show("请打开串口");
                Console.WriteLine("串口号{0}：是否打开？ {1}", Global.card.SerialportObject.serialPort1.PortName, Global.card.SerialportObject.serialPort1.IsOpen.ToString());
            }
        }

        private void btnInitialize_card_Click(object sender, EventArgs e)
        {
            if (Global.card.SerialportObject.IsOpen())
            {
                Console.WriteLine("串口已打开,串口号：{0}",Global.card.SerialportObject.serialPort1.PortName);
                Console.WriteLine("请销卡");
                Global.CardinformationObject = Global.card.Initialize_card(null,0);
                Console.WriteLine("卡号：{0} ，返回信息： {1} ， 金额：{2} ， 操作码：{3} ", Global.CardinformationObject.Card_ID, Global.CardinformationObject.Message, Global.CardinformationObject.Money, Global.CardinformationObject.Opcode);
            }
            else
            {
                MessageBox.Show("请打开串口");
                Console.WriteLine("串口号{0}：是否打开？ {1}", Global.card.SerialportObject.serialPort1.PortName, Global.card.SerialportObject.serialPort1.IsOpen.ToString());
            }
        }

        private void btnVirtualTest_Click(object sender, EventArgs e)
        {
            Console.WriteLine("虚拟测试开始！");
            Global.card.NEWCARD();
            
        }

        private void btnLoadkey_Click(object sender, EventArgs e)
        {
            if (Global.card.SerialportObject.IsOpen())
            {
                Console.WriteLine("串口已打开,串口号：{0}", Global.card.SerialportObject.serialPort1.PortName);
                Console.WriteLine("请装载秘钥");
                //Console.WriteLine("请输入读秘钥A");
                //string Akey = Console.ReadLine().Trim();
                //Console.WriteLine("请输入写秘钥B");
                //string Bkey = Console.ReadLine().Trim();
                string Akey = "FFFFFFFFFFFF";
                string Bkey = "FFFFFFFFFFFF";
                Global.CardinformationObject = Global.card.Load_Key_card(Akey, Bkey);
                Console.WriteLine("卡号：{0} ，返回信息： {1} ， 金额：{2} ， 操作码：{3} ", Global.CardinformationObject.Card_ID, Global.CardinformationObject.Message, Global.CardinformationObject.Money, Global.CardinformationObject.Opcode);
            }
            else
            {
                MessageBox.Show("请打开串口");
                Console.WriteLine("串口号{0}：是否打开？ {1}", Global.card.SerialportObject.serialPort1.PortName, Global.card.SerialportObject.serialPort1.IsOpen.ToString());
            }
        }

        private void btnQuery_card_Click(object sender, EventArgs e)
        {
            if (Global.card.SerialportObject.IsOpen())
            {
                Console.WriteLine("串口已打开,串口号：{0}", Global.card.SerialportObject.serialPort1.PortName);
                //Global.card.Recharge_card ("123", 40000,0x02);
                //Global.card.CardInitialization(ref  Global.CardinformationObject);
                Console.WriteLine("准备查询余额");
                Global.CardinformationObject = Global.card.Query_card (null,0, 0x00);
                Console.WriteLine("卡号：{0} ，返回信息： {1} ， 金额：{2} ， 操作码：{3} ", Global.CardinformationObject.Card_ID, Global.CardinformationObject.Message, Global.CardinformationObject.Money, Global.CardinformationObject.Opcode);
            }
            else
            {
                MessageBox.Show("请打开串口");
                Console.WriteLine("串口号{0}：是否打开？ {1}", Global.card.SerialportObject.serialPort1.PortName, Global.card.SerialportObject.serialPort1.IsOpen.ToString());
            }
        }
    }
}
