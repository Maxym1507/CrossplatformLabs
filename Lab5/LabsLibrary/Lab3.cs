using System;
using System.Collections.Generic;
using System.Linq;

namespace LabsLibrary
{
    public class L3Manager
    {
        private readonly List<int> _time;
        private readonly Dictionary<int, List<int>> _parts;

        public L3Manager(List<int> time, Dictionary<int, List<int>> parts)
        {
            _time = time;
            _parts = parts;
        }

        public string Run()
        {
            var manufacturedDetails = new List<int>();
            SetManufacturedDetails(ref manufacturedDetails, 1, _parts);
            var manufactureTime = manufacturedDetails.Sum(det => _time[det - 1]);
            return $"{manufactureTime} {manufacturedDetails.Count}\n{string.Join(" ", manufacturedDetails)}";
        }

        private void SetManufacturedDetails(ref List<int> manufacturedDetails, int detNum, Dictionary<int, List<int>> parts)
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
