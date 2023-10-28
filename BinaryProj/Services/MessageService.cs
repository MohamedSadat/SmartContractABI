using BinaryProj.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryProj.Services
{
    public class MessageService
    {
        public void HashMessage(MessageModel msg)
        {
            msg.MessageHash = HashService.HashAlgoStd($"{msg.Sender}{msg.Reseiver}{msg.Amount}");
        }
    }
}
