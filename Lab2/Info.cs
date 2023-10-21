using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_37
{
    public class Info
    {
        public bool possible { get; set; } = false;
        public int nOnes { get; set; } = 0;
        public int lastDigit { get; set; } = -1;
        public int prevResult { get; set; } = -1;
    }
}
