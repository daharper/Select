namespace Select.Messaging;

public class ShowUrlMessage(string url) : AppMessage
{
    public string Url { get; set; } = url;
}