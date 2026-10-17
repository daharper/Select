namespace Select.Messaging;

/// <summary>
/// Base class for all application messages.
/// </summary>
public class AppMessage
{
}

public class StartupMessage : AppMessage
{
}

public class ShutdownMessage : AppMessage
{
}

public class OpenProjectMessage : AppMessage
{
}

public class CloseProjectMessage : AppMessage
{
}

public class NewQueryMessage : AppMessage
{
}

public class NewWebMessage : AppMessage
{
}

public class ResizePageMessage : AppMessage
{
}

public class ShowUrlMessage(string url) : AppMessage
{
    public string Url { get; set; } = url;
}
