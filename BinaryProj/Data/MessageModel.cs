using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryProj.Data
{
    public class MessageModel
    {
        public MessageModel() { }
        public string Sender { get; set; } = "";
        public uint Amount { get; set; } = 0;

    }
}
