﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmHelper
{
    
    public class Algorithmhelper
    {
        private static string LogPath = AppDomain.CurrentDomain.BaseDirectory;
        private static  string keyfilename = "key.txt";
        private static string coutfilename = "count.txt";
        

        #region CRC Check algorithm given by jiangwei
        static ushort [] CRC_Table=       //  CCITT     X16+X12+X5+1 //为什么加上const会报错？？？
        {   
           0x0000, 0x1021, 0x2042, 0x3063, 0x4084, 0x50A5, 0x60C6, 0x70E7, 
           0x8108, 0x9129, 0xA14A, 0xB16B, 0xC18C, 0xD1AD, 0xE1CE, 0xF1EF, 
           0x1231, 0x0210, 0x3273, 0x2252, 0x52B5, 0x4294, 0x72F7, 0x62D6, 
           0x9339, 0x8318, 0xB37B, 0xA35A, 0xD3BD, 0xC39C, 0xF3FF, 0xE3DE, 
           0x2462, 0x3443, 0x0420, 0x1401, 0x64E6, 0x74C7, 0x44A4, 0x5485, 
           0xA56A, 0xB54B, 0x8528, 0x9509, 0xE5EE, 0xF5CF, 0xC5AC, 0xD58D, 
           0x3653, 0x2672, 0x1611, 0x0630, 0x76D7, 0x66F6, 0x5695, 0x46B4, 
           0xB75B, 0xA77A, 0x9719, 0x8738, 0xF7DF, 0xE7FE, 0xD79D, 0xC7BC, 
           0x48C4, 0x58E5, 0x6886, 0x78A7, 0x0840, 0x1861, 0x2802, 0x3823, 
           0xC9CC, 0xD9ED, 0xE98E, 0xF9AF, 0x8948, 0x9969, 0xA90A, 0xB92B, 
           0x5AF5, 0x4AD4, 0x7AB7, 0x6A96, 0x1A71, 0x0A50, 0x3A33, 0x2A12, 
           0xDBFD, 0xCBDC, 0xFBBF, 0xEB9E, 0x9B79, 0x8B58, 0xBB3B, 0xAB1A, 
           0x6CA6, 0x7C87, 0x4CE4, 0x5CC5, 0x2C22, 0x3C03, 0x0C60, 0x1C41, 
           0xEDAE, 0xFD8F, 0xCDEC, 0xDDCD, 0xAD2A, 0xBD0B, 0x8D68, 0x9D49, 
           0x7E97, 0x6EB6, 0x5ED5, 0x4EF4, 0x3E13, 0x2E32, 0x1E51, 0x0E70, 
           0xFF9F, 0xEFBE, 0xDFDD, 0xCFFC, 0xBF1B, 0xAF3A, 0x9F59, 0x8F78, 
           0x9188, 0x81A9, 0xB1CA, 0xA1EB, 0xD10C, 0xC12D, 0xF14E, 0xE16F, 
           0x1080, 0x00A1, 0x30C2, 0x20E3, 0x5004, 0x4025, 0x7046, 0x6067, 
           0x83B9, 0x9398, 0xA3FB, 0xB3DA, 0xC33D, 0xD31C, 0xE37F, 0xF35E, 
           0x02B1, 0x1290, 0x22F3, 0x32D2, 0x4235, 0x5214, 0x6277, 0x7256, 
           0xB5EA, 0xA5CB, 0x95A8, 0x8589, 0xF56E, 0xE54F, 0xD52C, 0xC50D, 
           0x34E2, 0x24C3, 0x14A0, 0x0481, 0x7466, 0x6447, 0x5424, 0x4405, 
           0xA7DB, 0xB7FA, 0x8799, 0x97B8, 0xE75F, 0xF77E, 0xC71D, 0xD73C, 
           0x26D3, 0x36F2, 0x0691, 0x16B0, 0x6657, 0x7676, 0x4615, 0x5634, 
           0xD94C, 0xC96D, 0xF90E, 0xE92F, 0x99C8, 0x89E9, 0xB98A, 0xA9AB, 
           0x5844, 0x4865, 0x7806, 0x6827, 0x18C0, 0x08E1, 0x3882, 0x28A3, 
           0xCB7D, 0xDB5C, 0xEB3F, 0xFB1E, 0x8BF9, 0x9BD8, 0xABBB, 0xBB9A, 
           0x4A75, 0x5A54, 0x6A37, 0x7A16, 0x0AF1, 0x1AD0, 0x2AB3, 0x3A92, 
           0xFD2E, 0xED0F, 0xDD6C, 0xCD4D, 0xBDAA, 0xAD8B, 0x9DE8, 0x8DC9, 
           0x7C26, 0x6C07, 0x5C64, 0x4C45, 0x3CA2, 0x2C83, 0x1CE0, 0x0CC1, 
           0xEF1F, 0xFF3E, 0xCF5D, 0xDF7C, 0xAF9B, 0xBFBA, 0x8FD9, 0x9FF8, 
           0x6E17, 0x7E36, 0x4E55, 0x5E74, 0x2E93, 0x3EB2, 0x0ED1, 0x1EF0       
        };
         public static ushort CrcCheck(byte [] ptr,uint len)      
        {
            int i = 0;
              byte  crcdata;
              ushort CRC_data = 0;                        
              while(len!=0) 
              { 
                crcdata=(byte )(CRC_data/256);     
                CRC_data<<=8;                         
                CRC_data^=CRC_Table[crcdata^ptr[i++]];   
                len--; 
              } 
              return(CRC_data); 
        }
        #endregion
        #region CRC Check algorithm by myself
         static byte[] ArrayCRCHi = 
        {  
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,  
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,  
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,  
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,  
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81,  
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,  
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,  
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,  
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,  
            0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,  
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,  
            0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,  
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,  
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,  
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,  
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,  
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,  
            0x40  
        };

         static byte[] ArrayCRCLo =  
        {  
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4,  
            0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,  
            0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD,  
            0x1D, 0x1C, 0xDC, 0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,  
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7,  
            0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,  
            0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE,  
            0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,  
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2,  
            0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,  
            0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB,  
            0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,  
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91,  
            0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,  
            0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88,  
            0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,  
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80,  
            0x40  
        };
         public static Int16 CRC16(byte[] data, int DataLen)
         {
             byte CRCHi = 0xFF;    // 高CRC字节初始化 
             byte CRCLo = 0xFF;    // 低CRC 字节初始化
             byte index;                  // CRC循环中的索引 
             int i = 0;
             while (DataLen-- > 0)                // 传输消息缓冲区  
             {
                 index = (System.Byte)(CRCHi ^ data[i++]); // 计算CRC      BYTE   
                 CRCHi = (System.Byte)(CRCLo ^ ArrayCRCHi[index]);
                 CRCLo = ArrayCRCLo[index];
             }
             return (Int16)(CRCHi << 8 | CRCLo);
         }
        #endregion
         public static byte[] Int32_Bytes4(int n)
         {
             byte[] b = new byte[4];

             for (int i = 0; i < 4; i++)
             {
                 b[i] = (byte)(n >> (24 - i * 8));

             }
             return b;
         }
         public static  Int64 Byte4_Int64(byte[] array, int StartIndex,int len)//注意数据溢出
         {
             string str = null;
             for (int t = 0; t < len ; t++)
             {
                 string str1;
                 str1 = Convert.ToString(array[t + StartIndex], 16).ToUpper();
                 str1 = ((str1.Length == 1 ? "0" + str1 : str1));
                 str += str1;
                 //Console.WriteLine("十六进制字符串{0}", t);
                 // Console.WriteLine(str1);
             }
             Int32 result = Int32.Parse(str, System.Globalization.NumberStyles.HexNumber);
             Int64 result1 = Int64.Parse(str, System.Globalization.NumberStyles.HexNumber);
             return result1;
         }
         public static void  StringArray_ByteArray(char[] str, out byte[] bytegoal)
         {
             bytegoal = new byte[str.Length];
             int i = 0;
             foreach (var t in str )
             {
                 bytegoal[i++] = Convert .ToByte (t);
             }
                
         }
        public static void ByteArray_StringArray(byte []bytp, out char [] stringgoal)
        {
            stringgoal = new char [bytp.Length];
            int i = 0;
            foreach (var t in bytp)
            {
                stringgoal[i++] = Convert .ToChar (t);
            }
                
        }
        public static T[]MemCopy<T>(ref T [] ArrayGoal,int indexGoal,T[]ArraySource,int indexSource,int count)
        {
            int i = indexGoal;
            int j=indexSource ;
            for (; j < indexSource + count; )
            {
                ArrayGoal[i++] = ArraySource[j++];
            }
            return ArrayGoal;
        }
        public static bool FrameHeadVerdict(byte  [] framehead)
        {
            bool flag = false;
            if (framehead[0]==Convert .ToByte ('C') &&framehead[1]==Convert .ToByte ('R')&&framehead[2]==Convert .ToByte ('D'))
            {
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// 接收数据保存及打印
        /// </summary>
        /// <param name="CD"></param>
        /// 
        public static void WriteLOG_Console(byte[] array, string operation)
        {
            string filename = "Log.txt";
            string totalpath = Path.Combine(LogPath, filename);
            StreamWriter sw1 = null, sw2 = null;
            try
            {
                if (Directory.Exists(LogPath))
                {
                    if (File.Exists(totalpath))
                    {
                        sw1 = File.AppendText(totalpath);
                    }
                    else
                    {
                        sw1 = File.CreateText(totalpath);
                    }
                }
                else
                {
                    Directory.CreateDirectory(filename);
                    sw1 = File.CreateText(totalpath);
                }
                /****************************************************写日志****************************************************/
                sw1.Write("\r\n");
                sw1.WriteLine("************************************************************************");
                sw1.WriteLine(string.Format("{0}:{1}", DateTime.Now.ToString ("u", DateTimeFormatInfo.InvariantInfo), operation));
                //\t是转义字符，表示制表符，相当于键盘上的Tab键按一次的效果
                for (int t = 0; t < array.Length; t++)
                {
                    string str1 = Convert.ToString(array[t], 16).ToUpper();
                    if (str1.Length == 1)
                        str1 = "0" + str1;
                    sw1.Write(str1);
                    sw1.Write(' ');
                }
                sw1.Write(' ');
                //sw.Write("数组长度："+array.Length.ToString ());
                //sw.WriteLine("************************************************************************");
                /****************************************************写日志****************************************************/

                /****************************************************打印记录**************************************************/
                //Console .WriteLine (dt.ToString()); //  26/11/2009 AM 11:21:30
                Console.WriteLine("************************************************************************");
                Console.WriteLine(DateTime.Now.ToString("u", DateTimeFormatInfo.InvariantInfo) + ":" + operation);
                //\t是转义字符，表示制表符，相当于键盘上的Tab键按一次的效果
                for (int t = 0; t < array.Length; t++)
                {
                    string str1 = Convert.ToString(array[t], 16).ToUpper ();
                    if (str1.Length == 1)
                        str1 = "0" + str1;
                    Console.Write(str1 + " ");
                }
                sw2.Write(" ");
                sw2.Write("数组长度：" + array.Length.ToString());
                Console.WriteLine("\r\n");
                /****************************************************打印记录****************************************************/
            }
            catch
            {
                Console.WriteLine("Input data error!");
            }
            finally
            {
                if (sw1 != null)
                {
                    sw1.Flush();
                    sw1.Close();
                    sw1.Dispose();
                }
                if (sw2 != null)
                {
                    sw2.Flush();
                    sw2.Close();
                    sw2.Dispose();
                }
            }
        }
        public static void SaveKeyValue(string  keyRead,string keyWrite)
        {
            string totalpath = Path.Combine(LogPath, keyfilename);
            StreamWriter sw=null ;
            try
            {
                if (Directory.Exists(LogPath))
                {
                    if (File.Exists(totalpath))
                    {
                        sw = File.AppendText(totalpath);
                    }
                    else
                    {
                        sw = File.CreateText(totalpath);
                    }
                }
                else
                {
                    Directory.CreateDirectory(keyfilename);
                    sw = File.CreateText(totalpath);
                }
                sw.Write(keyRead);
                sw.Write(keyWrite);
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            finally
            {
                if (sw !=null )
                {
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }

        }
       
        public static bool  ReadKeyValue(ref string keyRead,ref string keyWrite)
        {
            bool flag = false;
            string totalpathkey = Path.Combine(LogPath, keyfilename);
            string totalpathcount = Path.Combine(LogPath,coutfilename);
            StreamReader rw = null;
            string keytemp = string.Empty;
            try
            {
                if (File.Exists(totalpathkey))
                {
                    rw = File.OpenText(totalpathkey);
                    keytemp=rw.ReadToEnd().Trim ();
                    keyRead = keytemp.Substring(0, 6);
                    keyWrite = keytemp.Substring(6, 6);
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
            }
            finally
            {
                if (rw !=null )
                {
                    rw.Close();
                    rw.Dispose();
                }
            }
            return flag;
        }
        /// <summary>
        /// 将装载的秘钥字符串形式转换为字节格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] Key_string_byte(string str)
        {
            byte[] bt = new byte[6] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            for (int i = 0; i < 6; i++)
            {
                string str1 = str.Substring(2 * i, 2);
                bt[i] = (byte)(Convert.ToInt32(str1, 16));
            }
            return bt;
        }

    }
        
   
}
