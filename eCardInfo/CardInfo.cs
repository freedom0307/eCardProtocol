using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCardInfo
{
    public class CardInfo
    {
        /// <summary>
        /// 卡类型
        /// </summary>
        private string card_type;
        public  string Card_type
        {
            get {return card_type ;}
            set {card_type =value ;}
        }
        /// <summary>
        /// 卡号
        /// </summary>
        private string  card_ID;
        public  string Card_ID
        {
            get { return card_ID; }
            set { card_ID = value; }
        }

        /// <summary>
        /// 金额
        /// </summary>
        private float money;
        public  float Money
        {
            get { return money; }
            set { money = value; }
        }
        /// <summary>
        /// 操作码
        /// </summary>
        private int opcode;
        public  int Opcode
        {
            get { return opcode; }
            set { opcode = value; }
        }
        /// <summary>
        /// 卡状态
        /// 0x50:正常卡；0x5a:锁卡；0x5e:卡异常；0x00：未开卡；其他：未知卡
        /// </summary>
        private int card_status;
        public  int Card_status
        {
            get { return card_status; }
            set { card_status = value; }
        }

        /// <summary>
        /// 操作返回信息
        /// </summary>
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        private string user_name;

        public string User_name
        {
            get { return user_name; }
            set { user_name = value; }
        }

        /// <summary>
        /// 手机号
        /// </summary>
        private string mobile;

        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        /// <summary>
        /// 欠款次数
        /// </summary>
        private int  debt_count;
        public int  Debt_count
        {
            get { return debt_count; }
            set { debt_count = value; }
        }
        /// <summary>
        /// 最大欠款次数
        /// </summary>
        private int debt_count_max;
        public int Debt_count_max
        {
            get { return debt_count_max; }
            set { debt_count_max = value; }
        }
        /// <summary>
        /// 最后一个记录序号
        /// </summary>
        private int last_record_serial_number;
        public int Last_record_serial_number
        {
            get { return last_record_serial_number; }
            set { last_record_serial_number = value; }
        }

    }

    [Serializable]
    public class ConsumptionRecords
    {
        /// <summary>
        /// 卡号
        /// </summary>
        private string card_ID;
        public string Card_ID
        {
            get { return card_ID; }
            set { card_ID = value; }
        }
        /// <summary>
        /// 块号
        /// </summary>
        private byte  block_NUM;
        public byte  Block_NUM
        {
            get { return block_NUM; }
            set { block_NUM = value; }
        }
        /// <summary>
        /// 读块，写块操作码
        /// 读：READ 写：WRITE
        /// </summary>
        private string  opcodeType;
        public string OpcodeType
        {
            get { return opcodeType; }
            set { opcodeType = value; }
        }
        /// <summary>
        /// 成功还是失败
        /// </summary>
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        /// <summary>
        /// 账单号
        /// </summary>
        private string bill;
        public string Bill
        {
            get { return bill; }
            set { bill = value; }
        }
        /// <summary>
        /// 扣款金额
        /// </summary>
        private string deductionsAmount;
        public string DeductionsAmount
        {
            get { return  deductionsAmount; }
            set { deductionsAmount = value; }
        }
        /// <summary>
        /// 扣款前余额
        /// </summary>
        private string balanceBeforedeductions;
        public string BalanceBeforedeductions
        {
            get { return balanceBeforedeductions; }
            set { balanceBeforedeductions = value; }
        }

        private string balance;
        public string Balance
        {
            get { return balance; }
            set { balance = value; }
        }

    }
}
