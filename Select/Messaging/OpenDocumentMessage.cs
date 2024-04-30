namespace Select.Messaging;

public class OpenDocumentMessage(string name, string path) : AppMessage
{
    public string Name => name;
    
    public string Path => path;
}
