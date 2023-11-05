using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartContractLib.Data
{
    public class AppSettingModel
    {
        public string ContractPath { get; set; } = "Contracts";
        public string TransactionPath { get; set; } = "Transactions";
        public string MessagePath { get; set; } = "Messages";
        public string AccountPath { get; set; } = "Accounts";
    }
}
