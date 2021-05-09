using System.Xml.Serialization;
using DefaultNamespace;

public class TextLine {
    [XmlAttribute("ID")]
    public int ID { get; set; }
    [XmlElement("Text")]
    public string Text { get; set; }
}