
using SmartContractLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartContractLib.Services
{
    public class MessageService
    {
        public void HashMessage(MessageModel msg)
        {
            msg.MessageHash = HashService.HashAlgoStd($"{msg.Sender}{msg.Receiver}{msg.Amount}");
        }
    }
}
