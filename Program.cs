using System.Text.RegularExpressions;

namespace DirectoryCleaner;

internal class Program()
{
    internal static string? dirString;
    internal static void Main()
    {
        Console.WriteLine("Welcome to the directory cleaner!");
        Console.WriteLine(@"Provided path must be in format: C:\Users\User\...\Directory");
        do
        {
            Console.Write("Please enter the path to the directory you would like to clean:");
            dirString = Console.ReadLine();
            if(!Path.Exists(dirString)) {
                Console.WriteLine("Invalid path format // Path does not exist. Please try again.");
            }
        }while(!Directory.Exists(dirString) || string.IsNullOrEmpty(dirString) || !Path.Exists(dirString));

        Console.ReadLine();
    }
}