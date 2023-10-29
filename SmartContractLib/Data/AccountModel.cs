using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartContractLib.Data
{
    public class AccountModel
    {
        public string Address { get; set; } = "";
        public string PubKey { get; set; } = "";

        public uint Balance { get; set; } = 0;
        public int IsSigner { get; set; } = 0;
        //for smart contract = 1
        public int IsExcutable { get; set; } = 0;

    }
}
