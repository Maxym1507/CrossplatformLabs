using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsLibrary
{
    public class L2Manager
    {
        private readonly int _numberOfResDigits;
        private readonly int[,] _boolFunk;

        public L2Manager(int numberOfResDigits, int[,] boolFunk)
        {
            _numberOfResDigits = numberOfResDigits;
            _boolFunk = boolFunk;
        }

        public string Run() 
        {
            var info = new List<List<Info>>();
            for (int i = 0; i < 2; i++)
            {
                info.Add(new List<Info>());
                for (int j = 0; j <= _numberOfResDigits; j++)
                {
                    info[i].Add(new Info());
                }
            }

            info[0][1] = new Info() { possible = true, nOnes = 0, lastDigit = 0, prevResult = -1 };
            info[1][1] = new Info() { possible = true, nOnes = 1, lastDigit = 1, prevResult = -1 };
            for (int len = 2; len <= _numberOfResDigits; len++)
            {
                for (int prevResult = 0; prevResult <= 1; prevResult++)
                {
                    if (!info[prevResult][len - 1].possible) { continue; }
                    for (int lastDigit = 0; lastDigit <= 1; lastDigit++)
                    {
                        int result = _boolFunk[prevResult, lastDigit];
                        int nOnes = info[prevResult][len - 1].nOnes + lastDigit;
                        if (!info[result][len].possible || nOnes > info[result][len].nOnes)
                        {
                            info[result][len] = new Info() { possible = true, nOnes = nOnes, lastDigit = lastDigit, prevResult = prevResult };
                        }
                    }
                }
            }

            if (!info[1][_numberOfResDigits].possible) { return "No solution"; }
            else
            {
                StringBuilder sb = new StringBuilder();
                int result = 1;
                int len = _numberOfResDigits;
                while (len > 0)
                {
                    sb.Append(info[result][len].lastDigit);
                    result = info[result][len].prevResult;
                    len--;
                }
                return sb.ToString();
            }
        }
    }

    public class Info
    {
        public bool possible { get; set; } = false;
        public int nOnes { get; set; } = 0;
        public int lastDigit { get; set; } = -1;
        public int prevResult { get; set; } = -1;
    }
}
