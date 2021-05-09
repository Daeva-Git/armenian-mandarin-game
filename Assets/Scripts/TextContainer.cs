using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace DefaultNamespace
{
    [XmlRoot("TextCollection")]
    public class TextContainer
    {
        [XmlArray("TextData")] [XmlArrayItem("TextLine")]
        public List<TextLine> TextLines = new List<TextLine>();

        public static TextContainer Load(string path)
        {
            var xml = Resources.Load<TextAsset>(path);
            var serializer = new XmlSerializer(typeof(TextContainer));
            var reader = new StringReader(xml.text);
            var items = serializer.Deserialize(reader) as TextContainer;
            
            reader.Close();
            
            return items;
        }

        public void Add(TextLine textLine)
        {
            TextLines.Add(textLine);
        }
    }
}