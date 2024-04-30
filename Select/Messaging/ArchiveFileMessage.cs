namespace Select.Messaging;

public class ArchiveFileMessage(string path, string name, string url) : AppMessage
{
    public string Path => path;

    public string Url => url;
    
    public string Name => name;

    public static ArchiveFileMessage Create(string path)
    {
        var name = System.IO.Path.GetFileName(path);
        var url = $"file://{path.Replace('\\', '/')}";
        
        return new ArchiveFileMessage(path, name, url);
    }
}
