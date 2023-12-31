﻿namespace Lab3_37
{
    public class Program
    {
        public static string InputFilePath = "input.txt";
        public static string OutputFilePath = "output.txt";
        static void Main(string[] args)
        {
            if (!File.Exists(InputFilePath))
            {
                throw new FileNotFoundException("File with data not found!", InputFilePath);
            }
            List<string> inputData = File.ReadLines(InputFilePath).ToList();
            if (inputData.Count == 0)
            {
                throw new Exception("File with data is empty");
            }
            var enginePartsNum = Convert.ToInt32(inputData[0].Trim() ?? "0");
            if (enginePartsNum < 1 || enginePartsNum > 100000)
            {
                throw new Exception($"N is out of range. It must be in range between 1 and 100000.");
            }
            List<int> time = inputData[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(n => Convert.ToInt32(n)).ToList();
            Dictionary<int, List<int>> parts = new Dictionary<int, List<int>>();
            for (int i = 0; i < enginePartsNum; i++)
            {
                parts.Add(i + 1, inputData[i + 2].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(n => Convert.ToInt32(n)).Skip(1).ToList());
            }
            FileInfo outputFileInfo = new FileInfo(OutputFilePath);
            using (StreamWriter streamWriter = outputFileInfo.CreateText())
            {
                var res = GetResult(time, parts);
                streamWriter.WriteLine(res);
            }
        }

        private static string GetResult(List<int> time, Dictionary<int, List<int>> parts)
        {
            var manufacturedDetails = new List<int>();
            SetManufacturedDetails(ref manufacturedDetails, 1, parts);
            var manufactureTime = manufacturedDetails.Sum(det => time[det - 1]);
            return $"{manufactureTime} {manufacturedDetails.Count}\n{string.Join(' ', manufacturedDetails)}";
        }

        private static void SetManufacturedDetails(ref List<int> manufacturedDetails, int detNum, Dictionary<int, List<int>> parts)
        {
            if (parts[detNum].Except(manufacturedDetails).Count() == 0) 
            {
                manufacturedDetails.Add(detNum);
            }
            else 
            {
                foreach (var part in parts[detNum].Except(manufacturedDetails)) { SetManufacturedDetails(ref manufacturedDetails, part, parts); }
                manufacturedDetails.Add(detNum);
            }
        }

    }
}