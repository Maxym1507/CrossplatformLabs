using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsLibrary
{
    public class L1Manager
    {
        private readonly List<(double DollarRate, double EuroRate)> _rates;

        public L1Manager(List<(double, double)> rates)
        {
            _rates = rates;
        }

        public string Run()
        {
            if (_rates == null || !_rates.Any())
            {
                throw new Exception("No exchange rates provided.");
            }

            double result = GetMaxEarnings(_rates);
            return string.Format("{0:0.00}", result).Replace(',', '.');
        }

        private double GetMaxEarnings(List<(double DollarRate, double EuroRate)> rates)
        {
            double res = 100;
            for (int i = 0; i < rates.Count - 1;)
            {
                var bestSellDays = GetBestSellDays(i, rates);

                if (bestSellDays.e == i && bestSellDays.d == i)
                {
                    i++;
                }
                else if (bestSellDays.e == bestSellDays.d)
                {
                    res = Math.Max(GetEarningsAfterExchange(res, rates[i].DollarRate, rates[bestSellDays.d].DollarRate), GetEarningsAfterExchange(res, rates[i].EuroRate, rates[bestSellDays.e].EuroRate));
                    i = bestSellDays.e + 1;
                }
                else if (bestSellDays.d != i && (bestSellDays.e == i || bestSellDays.d < bestSellDays.e))
                {
                    res = GetEarningsAfterExchange(res, rates[i].DollarRate, rates[bestSellDays.d].DollarRate);
                    i = bestSellDays.d + 1;
                }
                else if (bestSellDays.e != i && (bestSellDays.d == i || bestSellDays.e < bestSellDays.d))
                {
                    res = GetEarningsAfterExchange(res, rates[i].EuroRate, rates[bestSellDays.e].EuroRate);
                    i = bestSellDays.e + 1;
                }
            }
            return res;
        }

        public static (int d, int e) GetBestSellDays(int current, List<(double d, double e)> pricesPerDayList)
        {
            (int d, int e) days = (current, current);
            for (int i = current + 1; i < pricesPerDayList.Count && (days.d == i - 1 || days.e == i - 1); i++)
            {
                if (days.d == i - 1 && pricesPerDayList[i].d > pricesPerDayList[i - 1].d) { days.d++; }
                if (days.e == i - 1 && pricesPerDayList[i].e > pricesPerDayList[i - 1].e) { days.e++; }
            }
            return days;
        }
        public static double GetEarningsAfterExchange(double money, double todayPrice, double nextDayPrice) => money / todayPrice * nextDayPrice;
    }
}