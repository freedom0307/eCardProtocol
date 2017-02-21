using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eCardProtocol
{
    public partial class Koukuan : Form
    {
        public Koukuan()
        {
            InitializeComponent();
        }

        private void btnkoukuan_Click(object sender, EventArgs e)
        {
            if (Global.card.SerialportObject.IsOpen())
            {
                Console.WriteLine("串口已打开,串口号：{0}", Global.card.SerialportObject.serialPort1.PortName);
                //Global.card.Deductions_card ("123", 20000, 0x01);
               // Console.WriteLine("准备扣款");
                //Console.WriteLine("请输入扣款金额");
                //double money = Convert.ToDouble(Console.ReadLine().Trim());
               // Console.WriteLine("请输入扣款标志,预扣输入 1 ，扣费输入 2：");
               // string str1 = Console.ReadLine().Trim();
                //byte flag = 0x00;
                
                //Console.WriteLine("请输入扣款次数：");
                //string count = Console.ReadLine().Trim();
                //for (int k = 0; k < Convert.ToByte(count); k++)
                byte flag = Convert.ToByte(comboBox1.Text .ToString());
                double money = Convert.ToDouble(comboBox2.Text.Trim());
                {
                    Global.CardinformationObject = Global.card.Deductions_card(null, money, flag);
                    Console.WriteLine("卡号：{0} ，返回信息： {1} ， 金额：{2} ， 操作码：{3},欠款次数： {4}，卡状态：{5} ", Global.CardinformationObject.Card_ID, Global.CardinformationObject.Message, Global.CardinformationObject.Money, Global.CardinformationObject.Opcode, Global.CardinformationObject.Debt_count, Global.CardinformationObject.Card_status);
                }

            }
            else
            {
                MessageBox.Show("请打开串口");
                Console.WriteLine("串口号{0}：是否打开？ {1}", Global.card.SerialportObject.serialPort1.PortName, Global.card.SerialportObject.serialPort1.IsOpen.ToString());
            }
        }

        private void Koukuan_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.Text = "00";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
