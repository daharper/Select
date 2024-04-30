using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Select.Rtti;

public static class XmlUtils
{
    public static string ToString(object obj, string? defaultNamespace = null)
    {
        var sb = new StringBuilder();

        using (var stream = new StringWriter(sb))
        {
            var type = obj.GetType();
         
            var serializer = string.IsNullOrWhiteSpace(defaultNamespace)
                ? new XmlSerializer(type)
                : new XmlSerializer(type, defaultNamespace);

            serializer.Serialize(stream, obj);
        }

        return sb.ToString();
    }
    
    public static T ToObject<T>(string xml, string? defaultNamespace = null) where T : class, new()
    {
        using var stream = new StringReader(xml);

        var serializer = string.IsNullOrEmpty(defaultNamespace)
            ? new XmlSerializer(typeof(T))
            : new XmlSerializer(typeof(T), defaultNamespace);

        var obj = serializer.Deserialize(stream) as T;
        return obj ?? new T();
    }
    
    public static void Save(string filepath, object obj, string? defaultNamespace = null)
    {
        var xml = ToString(obj, defaultNamespace);
        File.WriteAllText(filepath, xml);
    }
    
    public static T Load<T>(string path) where T : class, new()
    {
        var xml = File.ReadAllText(path);
        return ToObject<T>(xml);
    }

    public static string PrettyPrintXml(string xml)
    {
        if (string.IsNullOrEmpty(xml)) return string.Empty;

        var doc = new XmlDocument();
        doc.LoadXml(xml);

        using var stringWriter = new StringWriter();

        var xmlReader = new XmlNodeReader(doc);
        var xmlWriter = new XmlTextWriter(stringWriter)
        {
            Formatting = Formatting.Indented,
            Indentation = 1,
            IndentChar = '\t'
        };

        xmlWriter.WriteNode(xmlReader, true);
        return stringWriter.ToString();
    }
}
