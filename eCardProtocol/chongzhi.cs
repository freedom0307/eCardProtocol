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
    public partial class chongzhi : Form
    {
        public chongzhi()
        {
            InitializeComponent();
        }

        private void btnChongzhi_Click(object sender, EventArgs e)
        {
            if (Global.card.SerialportObject.IsOpen())
            {
                Console.WriteLine("串口已打开,串口号：{0}", Global.card.SerialportObject.serialPort1.PortName);;
                double money = Convert.ToDouble (JJe .Text .Trim ());
                byte flag =Convert .ToByte ( stag.Text.Trim());
                Global.CardinformationObject = Global.card.Recharge_card(null, money, flag);
                Console.WriteLine("卡号：{0} ，返回信息： {1} ， 金额：{2} ， 操作码：{3},欠款次数： {4},卡状态： {5} ", Global.CardinformationObject.Card_ID, Global.CardinformationObject.Message, Global.CardinformationObject.Money, Global.CardinformationObject.Opcode, Global.CardinformationObject.Debt_count, Global.CardinformationObject.Card_status);
            }
            else
            {
                MessageBox.Show("请打开串口");
                Console.WriteLine("串口号{0}：是否打开？ {1}", Global.card.SerialportObject.serialPort1.PortName, Global.card.SerialportObject.serialPort1.IsOpen.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chongzhi_Load(object sender, EventArgs e)
        {
            stag.SelectedIndex = 1;
            JJe.Text = "0.00";
        }
    }
}
