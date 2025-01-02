using System.Text.RegularExpressions;

namespace DirectoryCleaner;

internal class Program()
{
    internal static string? DirString;
    private static string? _cleanType;
    internal static void Main()
    {
        Console.WriteLine("Welcome to the directory cleaner!");
        Console.WriteLine(@"Provided path must be in correct format (e.g. C:\Users\user\Documents)");
        do
        {
            Console.Write("Please enter the path to the directory you would like to clean:");
            DirString = Console.ReadLine();
            if(!Path.Exists(DirString)) {
                Console.WriteLine("Invalid path format // Path does not exist. Please try again.");
            }
        }while(!Directory.Exists(DirString) || string.IsNullOrEmpty(DirString) || !Path.Exists(DirString));
        Console.WriteLine("Path registered successfully!");
        do
        {
            Console.Write("Would you like to clean files, folders, or both? (F/FF/B): ");
            _cleanType = Console.ReadLine();
            if (string.IsNullOrEmpty(_cleanType) || _cleanType != "F" || _cleanType != "FF" || _cleanType != "B") {
                Console.WriteLine("Invalid input. Please try again.");
            }
        } while (string.IsNullOrEmpty(_cleanType) || _cleanType != "F" || _cleanType != "FF" || _cleanType != "B");
        Console.WriteLine($"CleanType {_cleanType} initialized successfully!");

        switch (CleanType(_cleanType))
        {
            case DirectoryCleaner.CleanType.EFiles:
                CleanFiles();
                break;
            case DirectoryCleaner.CleanType.EFolders:
                // Clean folders method TODO
                break;
            case DirectoryCleaner.CleanType.EAll:
                // Call both when done
                break;
        }
        
        Console.ReadLine();
    }

    internal static void CleanFiles()
    {
        
    }
    
    internal static CleanType CleanType(string cleanType)
    {
        return cleanType.ToUpper() switch
        {
            "F" => DirectoryCleaner.CleanType.EFiles,
            "FF" => DirectoryCleaner.CleanType.EFolders,
            "B" => DirectoryCleaner.CleanType.EAll,
            _ => DirectoryCleaner.CleanType.NONE
        };
    }
}