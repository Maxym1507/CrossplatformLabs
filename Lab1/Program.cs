namespace Lab1_37
{
    public class Program
    {
        public static string InputFilePath = @"..\..\input.txt";
        public static string OutputFilePath = @"..\..\output.txt";

        static void Main(string[] args)
        {
            FileInfo outputFileInfo = new FileInfo(OutputFilePath);
            List<string> inputData = File.ReadLines(InputFilePath).ToList();
            int numberOfDays = Convert.ToInt32(inputData?[0].Trim() ?? "0");
            List<(double, double)> prices = inputData?.Skip(1).Select(p => {
                var vals = p.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return (Convert.ToDouble(vals[0].Replace('.', ',')), Convert.ToDouble(vals[1].Replace('.', ',')));
            }).ToList() ?? new List<(double, double)>();
            using (StreamWriter streamWriter = outputFileInfo.CreateText())
            {
                if (numberOfDays <= 0 || numberOfDays > 5000)
                {
                    streamWriter.WriteLine("Out of range exception!");
                }
                else
                {
                    var res = string.Format("{0:0.00}", GetMaxEarnings(prices)).Replace(',', '.');
                    streamWriter.WriteLine(res);
                    Console.WriteLine(res);
                }
            }
        }

        public static double GetMaxEarnings(List<(double, double)> pricesPerDayList)
        {
            double res = 100;
            for (int i = 0; i < pricesPerDayList.Count - 1;)
            {
                var d = res / pricesPerDayList[i].Item1 * pricesPerDayList[i + 1].Item1;
                var e = res / pricesPerDayList[i].Item2 * pricesPerDayList[i + 1].Item2;
                var buf = Math.Max(d, e);
                if (res < buf)
                {
                    res = Math.Max(d, e);
                    i += 2;
                }
                else
                {
                    i++;
                }
            }
            return res;
        }
    }
}