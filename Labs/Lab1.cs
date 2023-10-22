using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Labs
{
    internal static class Lab1
    {
        public static void Execute(string path)
        {
            var inputPath = Path.Combine(path, Resources.InputFileName);
            var outputPath = Path.Combine(path, Resources.OutputFileName);
            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException("File with data not found!", inputPath);
            }
            List<string> inputData = File.ReadLines(inputPath).ToList();
            if (inputData.Count == 0)
            {
                throw new Exception("File with data is empty");
            }

            int numberOfDays = Convert.ToInt32(inputData?[0].Trim() ?? "0");
            if (numberOfDays <= 0 || numberOfDays > 5000)
            {
                throw new Exception($"Number of days (N) is out of range. It must be in range between 1 and 5000.");
            }
            if (inputData.Count != numberOfDays + 1)
            {
                throw new Exception($"The number of lines with the exchange rate should be {numberOfDays}. You entered {inputData.Count - 1} lines.");
            }
            List<(double, double)> prices = inputData.Skip(1).Select(p => {
                var vals = p.Split(' ');
                return (Convert.ToDouble(vals[0].Replace('.', ',')), Convert.ToDouble(vals[1].Replace('.', ',')));
            }).ToList() ?? new List<(double, double)>();
            FileInfo outputFileInfo = new FileInfo(outputPath);
            using (StreamWriter streamWriter = outputFileInfo.CreateText())
            {
                var res = string.Format("{0:0.00}", GetMaxEarnings(prices)).Replace(',', '.');
                streamWriter.WriteLine(res);
                Console.WriteLine();
            }
        }

        private static double GetMaxEarnings(List<(double d, double e)> pricesPerDayList)
        {
            double res = 100;
            for (int i = 0; i < pricesPerDayList.Count - 1;)
            {
                var bestSellDays = GetBestSellDays(i, pricesPerDayList);

                if (bestSellDays.e == i && bestSellDays.d == i)
                {
                    i++;
                }
                else if (bestSellDays.e == bestSellDays.d)
                {
                    res = Math.Max(GetEarningsAfterExchange(res, pricesPerDayList[i].d, pricesPerDayList[bestSellDays.d].d), GetEarningsAfterExchange(res, pricesPerDayList[i].e, pricesPerDayList[bestSellDays.e].e));
                    i = bestSellDays.e + 1;
                }
                else if (bestSellDays.d != i && (bestSellDays.e == i || bestSellDays.d < bestSellDays.e))
                {
                    res = GetEarningsAfterExchange(res, pricesPerDayList[i].d, pricesPerDayList[bestSellDays.d].d);
                    i = bestSellDays.d + 1;
                }
                else if (bestSellDays.e != i && (bestSellDays.d == i || bestSellDays.e < bestSellDays.d))
                {
                    res = GetEarningsAfterExchange(res, pricesPerDayList[i].e, pricesPerDayList[bestSellDays.e].e);
                    i = bestSellDays.e + 1;
                }
            }
            return res;
        }

        private static (int d, int e) GetBestSellDays(int current, List<(double d, double e)> pricesPerDayList)
        {
            (int d, int e) days = (current, current);
            for (int i = current + 1; i < pricesPerDayList.Count && (days.d == i - 1 || days.e == i - 1); i++)
            {
                if (days.d == i - 1 && pricesPerDayList[i].d > pricesPerDayList[i - 1].d) { days.d++; }
                if (days.e == i - 1 && pricesPerDayList[i].e > pricesPerDayList[i - 1].e) { days.e++; }
            }
            return days;
        }

        private static double GetEarningsAfterExchange(double money, double todayPrice, double nextDayPrice) => money / todayPrice * nextDayPrice;
    }
}
