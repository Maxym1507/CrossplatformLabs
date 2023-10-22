using Labs;

namespace Lab4
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a command.");
                return;
            }
            string command = args[0].ToLower();
            switch (command)
            {
                case "version":
                    Console.WriteLine($"Author: Maxym Smikh\nVersion: 0.1");
                    break;
                case "run":
                    Labs.Labs.Execute(args[1]);
                    break;
                case "set-path":
                    Environment.SetEnvironmentVariable("LAB_PATH", args[2]);
                    break;
                default:
                    break;
            }
        }
    }
}