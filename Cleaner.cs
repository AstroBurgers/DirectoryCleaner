using Microsoft.VisualBasic.FileIO;

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
    bool deleteItems,
    string maxFileSize = "",
    string minFileSize = "",
    int minFolderDepth = 0,
    int maxFolderDepth = 0)
{
    public Cleaner(string dirString, CleanType cleanType, List<string> excludedFolders, bool deleteItems, int minFolderDepth = 0, int maxFolderDepth = 0) : this(dirString, cleanType, null, excludedFolders, deleteItems, null, null, minFolderDepth, maxFolderDepth)
    {
    }

    public Cleaner(string dirString, CleanType cleanType, List<string> excludedFiles, bool deleteItems, string maxFileSize = "", string minFileSize = "") : this(dirString, cleanType, excludedFiles, null, deleteItems, maxFileSize, minFileSize, 0, 0)
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
    internal bool DeleteItems { get; set; } = deleteItems;

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
            default:
                Console.WriteLine("Invalid clean type.");
                break;
        }
    }

    internal void CleanFiles()
    {
        var files = Directory.GetFiles(DirString);
        foreach (var file in files)
        {
            var (minSize, minUnit) = ConvertFileSize(MinFileSize);
            var (maxSize, maxUnit) = ConvertFileSize(MaxFileSize);
            var fileInfo = new FileInfo(file);
            if (ExcludedFiles.Contains(fileInfo.Name)) {
                Console.WriteLine($"File {fileInfo.Name} is excluded from cleaning.");
                continue;
            }
            if (ConvertSize(fileInfo.Length, minUnit) < minSize || ConvertSize(fileInfo.Length, maxUnit) > maxSize) {
                Console.WriteLine($"File {fileInfo.Name} is not within the size range.");
                continue;
            }
            
            if (DeleteItems)
            {
                FileSystem.DeleteFile(file, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            }

            CleanedFiles.Add(file);
        }
    }

    internal void CleanFolders()
    {
        var folders = Directory.GetDirectories(DirString);
        foreach (var folder in folders)
        {
            var folderInfo = new DirectoryInfo(folder);
            if (ExcludedFolders.Contains(folderInfo.Name)) {
                Console.WriteLine($"Folder {folderInfo.Name} is excluded from cleaning.");
                continue;
            }
            if (folderInfo.GetDirectories().Length < MinFolderDepth || folderInfo.GetDirectories().Length > MaxFolderDepth) {
                Console.WriteLine($"Folder {folderInfo.Name} is not within the depth range.");
                continue;
            }
            
            if (DeleteItems)
            {
                FileSystem.DeleteDirectory(folder, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            }

            CleanedFolders.Add(folder);
        }
    }

    internal static long ConvertSize(long sizeInBytes, string unit)
    {
        var finalNum = unit switch
        {
            "KB" => sizeInBytes / 1024,
            "MB" => sizeInBytes / 1024 / 1024,
            "GB" => sizeInBytes / 1024 / 1024 / 1024,
            _ => sizeInBytes
        };
        return finalNum;
    }
    
    internal static (int, string) ConvertFileSize(string fileSize)
    {
        var size = Convert.ToInt32(fileSize.Substring(0, fileSize.Length - 2));
        var unit = fileSize.Substring(fileSize.Length - 2, 2);
        return (size, unit);
    }
}