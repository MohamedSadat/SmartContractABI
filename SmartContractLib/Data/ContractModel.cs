
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartContractLib.Data
{
    public class ContractModel
    {
        public string Name { get; set; } = "";
        public string Owner { get; set; } = "";
        public uint Balance { get; set; } = 0;
        public string FromAccount { get; set; } = "";
        public string ToAccount { get; set; } = "";
        public DateTime TransDate { get; set; } = DateTime.Now;
        public string ErrorMessage { get; set; } = "";
        public bool IsEmpty { get; set; } = false;
    }
}
