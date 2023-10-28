using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapLib.Data
{
    public class MapModel
    {
        public string FileName { get; set; } = "";
        public string MutexName { get; set; } = "";
        public int Length { get; set; } = 0;
        public long Size { get; set; } = 0;
    }
}
