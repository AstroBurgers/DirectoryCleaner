namespace DirectoryCleaner;

internal enum CleanType
{
    EFolders,
    EFiles,
    EAll
}

internal class Cleaner(
    string dirString,
    CleanType cleanType,
    List<string> excludedFiles,
    List<string> excludedFolders,
    string maxFileSize = "",
    string minFileSize = "",
    int minFolderDepth = 0,
    int maxFolderDepth = 0)
{
    public Cleaner(string dirString, CleanType cleanType, List<string> excludedFolders, int minFolderDepth = 0, int maxFolderDepth = 0) : this(dirString, cleanType, null, excludedFolders, null, null, minFolderDepth, maxFolderDepth)
    {
    }

    public Cleaner(string dirString, CleanType cleanType, List<string> excludedFiles, string maxFileSize = "", string minFileSize = "") : this(dirString, cleanType, excludedFiles, null, maxFileSize, minFileSize, 0, 0)
    {
    }
    
    internal string DirString { get; set; } = dirString;
    internal CleanType CleanType { get; set; } = cleanType;
    internal List<string> CleanedFiles { get; set; }
    internal List<string> CleanedFolders { get; set; }
    internal List<string> ExcludedFiles { get; set; } = excludedFiles;
    internal List<string> ExcludedFolders { get; set; } = excludedFolders;
    internal string MaxFileSize { get; set; } = maxFileSize;
    internal string MinFileSize { get; set; } = minFileSize;
    internal int MinFolderDepth { get; set; } = minFolderDepth;
    internal int MaxFolderDepth { get; set; } = maxFolderDepth;

    internal void CleanDir()
    {
        if (!Path.Exists(DirString)) {
            Console.WriteLine("Invalid path format // Path does not exist");
            return;
        }

        switch (CleanType)
        {
            case CleanType.EFiles:
                CleanFiles();
                break;
            case CleanType.EFolders:
                CleanFolders();
                break;
            case CleanType.EAll:
                CleanFiles();
                CleanFolders();
                break;
        }
    }

    internal void CleanFiles()
    {
        var files = Directory.GetFiles(DirString);
        foreach (var file in files)
        {
            
        }
    }

    internal void CleanFolders()
    {
        
    }
    
    
    internal static (int, string) ConvertFileSize(string fileSize)
    {
        var size = Convert.ToInt32(fileSize.Substring(0, fileSize.Length - 2));
        var unit = fileSize.Substring(fileSize.Length - 2, 2);
        switch (unit)
        {
            case "KB":
                break;
            case "MB":
                break;
            case "GB":
                break;
            case "TB":
                break;
            default:
                break;
        }
        
        return (size, unit);
    }
}