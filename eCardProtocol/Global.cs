using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICCardHelper;
using eCardInfo;

namespace eCardProtocol
{
    public  class Global
    {
        public static ICCardHelper.CardProtocol card = new CardProtocol();
        public static CardInfo CardinformationObject = null;
    }
}
