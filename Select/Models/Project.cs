using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Select.Rtti;

namespace Select.Models;

/// <summary>
/// The project uses conventions to organize its files and folders.
/// </summary>
public class Project
{
    [XmlIgnore] public string Name { get; private set; } = "";

    [XmlIgnore] public string Filename { get; private set; } = "";
    
    [XmlIgnore] public string Folder { get; private set; } = "";

    [XmlIgnore] public string ResourcesPath { get; private set; } = "";

    [XmlIgnore] public string SchemaPath { get; private set; } = "";

    [XmlIgnore] public string Schema2Path { get; private set; } = "";

    public List<Bookmark> Bookmarks { get; set; } = [];

    #region public methods
    
    public void Save() => XmlUtils.Save(Filename, this);

    public static Project Load(string folder)
    {
        var filename = Path.Combine(folder, "project.xml");

        var project = File.Exists(filename) ? XmlUtils.Load<Project>(filename) : new Project();

        var name = folder.Split('\\').Last();

        project.Name = name;
        project.Folder = folder;
        project.Filename = filename;
        project.ResourcesPath = Path.Combine(folder, "Resources");
        project.SchemaPath = "file://" + Path.Combine(folder, "DbSchema.html").Replace('\\', '/');
        project.Schema2Path = "file://" + Path.Combine(folder, "DbSchema2.html").Replace('\\', '/');
        
        return project;
    }

    #endregion
}