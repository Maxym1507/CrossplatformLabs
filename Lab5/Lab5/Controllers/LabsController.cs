using Lab5.Models;
using LabsLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab5.Controllers
{
    public class LabsController : Controller
    {
        public IActionResult Lab1()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult Lab1(Lab1DataModel model)
        {
            var labRunner = new L1Manager(ParseRates(model.RatesInput));

            try
            {
                model.Calculated = labRunner.Run();
            }
            catch (ArgumentException e)
            {
                model.ErrorValue = e.Message;
            }
            catch (Exception e)
            {
                model.ErrorValue = e.Message;
            }

            return View(model);
        }

        public IActionResult Lab2()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult Lab2(Lab2DataModel model)
        {
            var labRunner = new L2Manager(model.NumberOfResDigits, GetBoolFunk(model.BoolFunk));

            try
            {
                model.Calculated = labRunner.Run();
            }
            catch (ArgumentException e)
            {
                model.ErrorValue = e.Message;
            }
            catch (Exception e)
            {
                model.ErrorValue = e.Message;
            }

            return View(model);
        }

        public IActionResult Lab3()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public IActionResult Lab3(Lab3DataModel model)
        {
            var labRunner = new L3Manager(model.Time.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(t => Convert.ToInt32(t)).ToList(), ParsePartes(model.Parts));

            try
            {
                model.Calculated = labRunner.Run();
            }
            catch (ArgumentException e)
            {
                model.ErrorValue = e.Message;
            }
            catch (Exception e)
            {
                model.ErrorValue = e.Message;
            }

            return View(model);
        }

        private Dictionary<int, List<int>> ParsePartes(string parts)
        {
            return parts.Split("\r\n", StringSplitOptions.TrimEntries).Select((line, index) => new
            {
                Key = index + 1,
                Value = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            }).ToDictionary(key => key.Key, value => value.Value);
        }

        private int[,] GetBoolFunk(string boolFunk)
        {
            var rowBoolFunk = boolFunk.Select(x => Convert.ToInt32(x.ToString())).ToList();
            return new int[,] { { rowBoolFunk[0], rowBoolFunk[1] }, { rowBoolFunk[2], rowBoolFunk[3] } };
        }

        private List<(double, double)> ParseRates(string ratesInput)
        {
            var rates = new List<(double, double)>();
            rates = ratesInput.Trim().Replace('.', ',').Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Select(row =>
            {
                var vals = row.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                return (Convert.ToDouble(vals[0]), Convert.ToDouble(vals[1]));
            }).ToList();
            return rates;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
