using SmartContractLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryPSmartContractLibroj.Data
{
    internal class TransactionModel
    {
        public MessageModel Message { get; set; } = new MessageModel();
        public string Signature { get; set; } = "";
    }
}
