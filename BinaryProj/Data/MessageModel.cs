using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryProj.Data
{
    /// <summary>
    /// Size is 81 bytes
    /// </summary>
    public class MessageModel
    {
        public MessageModel() { }
        public string Sender { get; set; } = "";
        public string Reseiver { get; set; } = "";
        public uint Amount { get; set; } = 0;
        /// <summary>
        /// Size is 65 bytes
        /// </summary>
        public string MessageHash { get; set; } = "";
        public int MsgSize { get; set; } = 31;

    }
}
