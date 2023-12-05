using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LabsLibrary
{
    public static class Lab2
    {
        public static string Run(string pathInpFile = "INPUT.TXT")
        {
            if (!File.Exists(pathInpFile))
            {
                throw new FileNotFoundException("File with data not found!", pathInpFile);
            }
            List<string> inputData = File.ReadLines(pathInpFile).ToList();
            if (inputData.Count == 0)
            {
                throw new Exception("File with data is empty");
            }
            int numberOfResDigits = Convert.ToInt32(inputData?[0].Trim() ?? "0");
            if (numberOfResDigits < 2 || numberOfResDigits > 100000)
            {
                throw new Exception($"N is out of range. It must be in range between 2 and 100000.");
            }
            var rowBoolFunk = inputData[1].Select(x => Convert.ToInt32(x.ToString())).ToList();
            int[,] boolFunk = { { rowBoolFunk[0], rowBoolFunk[1] }, { rowBoolFunk[2], rowBoolFunk[3] } };
            return GetResult(numberOfResDigits, boolFunk);
        }

        private static string GetResult(int n, int[,] f)
        {
            var info = new List<List<Info>>();
            for (int i = 0; i < 2; i++)
            {
                info.Add(new List<Info>());
                for (int j = 0; j <= n; j++)
                {
                    info[i].Add(new Info());
                }
            }

            info[0][1] = new Info() { possible = true, nOnes = 0, lastDigit = 0, prevResult = -1 };
            info[1][1] = new Info() { possible = true, nOnes = 1, lastDigit = 1, prevResult = -1 };
            for (int len = 2; len <= n; len++)
            {
                for (int prevResult = 0; prevResult <= 1; prevResult++)
                {
                    if (!info[prevResult][len - 1].possible) { continue; }
                    for (int lastDigit = 0; lastDigit <= 1; lastDigit++)
                    {
                        int result = f[prevResult, lastDigit];
                        int nOnes = info[prevResult][len - 1].nOnes + lastDigit;
                        if (!info[result][len].possible || nOnes > info[result][len].nOnes)
                        {
                            info[result][len] = new Info() { possible = true, nOnes = nOnes, lastDigit = lastDigit, prevResult = prevResult };
                        }
                    }
                }
            }

            if (!info[1][n].possible) { return "No solution"; }
            else
            {
                StringBuilder sb = new StringBuilder();
                int result = 1;
                int len = n;
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
