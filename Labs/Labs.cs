using System;

namespace Labs
{
    public static class Labs
    {
        public static void Execute(string labName)
        {
            var path = Environment.GetEnvironmentVariable(Resources.LabEnvVarName);
            if (path == null)
            {
                Console.WriteLine("Path is incorrect!");
                return;
            }
            switch (labName.ToLower())
            {
                case "lab1":
                    Lab1.Execute(path);
                    break;
                case "lab2":
                    Lab2.Execute(path);
                    break;
                case "lab3":
                    Lab3.Execute(path);
                    break;
                default:
                    Console.WriteLine("This lab work does not exist yet...");
                    break;
            }
        }
    }
}
