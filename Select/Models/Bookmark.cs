namespace Select.Models;

/// <summary>
/// The BrowserView maintains a list of bookmarks.
/// These are persisted in the Project.
/// </summary>
public class Bookmark
{
    public string Name { get; set; } = "";

    public string Url { get; set; } = "";
}
