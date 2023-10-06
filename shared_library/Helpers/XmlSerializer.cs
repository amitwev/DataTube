namespace shared_library.Helpers;

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class XmlSerializer<T> : ISerialize<T>
{
    public string Serialize(T obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }
        
        var serializer = new XmlSerializer(typeof(T));

        using (var stringWriter = new StringWriter())
        {
            using (var xmlWriter = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(xmlWriter, obj);
                return stringWriter.ToString();
            }
        }
    }

    public T Deserialize(string xml)
    {
        if (string.IsNullOrEmpty(xml))
        {
            throw new ArgumentNullException(nameof(xml));
        }

        var serializer = new XmlSerializer(typeof(T));

        using (var stringReader = new StringReader(xml))
        {
            using (var xmlReader = XmlReader.Create(stringReader))
            {
                return (T)serializer.Deserialize(xmlReader);
            }
        }
    }
}
