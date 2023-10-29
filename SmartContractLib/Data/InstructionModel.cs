using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartContractLib.Data
{
    public class InstructionModel
    {
        public string ProgramId { get; set; } = "";
        public List<AccountModel> Accounts { get; set; } = new List<AccountModel>();
        public byte[]? Data { get; set; } 
    }
}
