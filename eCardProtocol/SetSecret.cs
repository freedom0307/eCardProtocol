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
    public partial class SetSecret : Form
    {
        string Akey, Bkey;
        public SetSecret()
        {
            InitializeComponent();
        }

        private void SetSecret_Load(object sender, EventArgs e)
        {
            txtAkey.Text = "112233445566";
            txtBkey.Text = "775544332288";
            //txtAkey.Text = "112233445566";
            //txtBkey.Text = "775544332288";

        }

        private void btnSetKey_Click(object sender, EventArgs e)
        {
            if (Global.card.SerialportObject.IsOpen())
            {
                Console.WriteLine("串口已打开,串口号：{0}", Global.card.SerialportObject.serialPort1.PortName);
                Console.WriteLine("请装载秘钥");
                //Console.WriteLine("请输入读秘钥A");
                //string Akey = Console.ReadLine().Trim();
                //Console.WriteLine("请输入写秘钥B");
                //string Bkey = Console.ReadLine().Trim();
                // Akey = "FFFFFFFFFFFF";
                // Bkey = "FFFFFFFFFFFF";
                // Akey = "112233445566";
                // Bkey = "665544332211";
                //Akey = "112233445566";
                //Bkey = "775544332288";
                Akey = txtAkey.Text.Trim();
                Bkey = txtBkey.Text.Trim();
                Global.CardinformationObject = Global.card.Load_Key_card(Akey, Bkey);
                Console.WriteLine("卡号：{0} ，返回信息： {1} ， 金额：{2} ， 操作码：{3} ", Global.CardinformationObject.Card_ID, Global.CardinformationObject.Message, Global.CardinformationObject.Money, Global.CardinformationObject.Opcode);
            }
            else
            {
                MessageBox.Show("请打开串口");
                Console.WriteLine("串口号{0}：是否打开？ {1}", Global.card.SerialportObject.serialPort1.PortName, Global.card.SerialportObject.serialPort1.IsOpen.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
