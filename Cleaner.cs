namespace DirectoryCleaner;

internal enum CleanType
{
    EFolders,
    EFiles,
    EAll
}

internal class Cleaner(
    CleanType cleanType,
    List<string> excludedFiles,
    List<string> excludedFolders,
    string maxFileSize = "",
    string minFileSize = "",
    int minFolderDepth = 0,
    int maxFolderDepth = 0)
{
    public Cleaner(CleanType cleanType, List<string> excludedFolders, int minFolderDepth = 0, int maxFolderDepth = 0) : this(cleanType, null, excludedFolders, null, null, minFolderDepth, maxFolderDepth)
    {
    }

    public Cleaner(CleanType cleanType, List<string> excludedFiles, string maxFileSize = "", string minFileSize = "") : this(cleanType, excludedFiles, null, maxFileSize, minFileSize, 0, 0)
    {
    }
    
    internal CleanType CleanType { get; set; } = cleanType;
    internal List<string> CleanedFiles { get; set; }
    internal List<string> CleanedFolders { get; set; }
    internal List<string> ExcludedFiles { get; set; } = excludedFiles;
    internal List<string> ExcludedFolders { get; set; } = excludedFolders;
    internal string MaxFileSize { get; set; } = maxFileSize;
    internal string MinFileSize { get; set; } = minFileSize;
    internal int MinFolderDepth { get; set; } = minFolderDepth;
    internal int MaxFolderDepth { get; set; } = maxFolderDepth;
}