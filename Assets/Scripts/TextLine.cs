using System.Xml.Serialization;

public class TextLine {
    [XmlAttribute("ID")]
    public int ID { get; set; }
    [XmlElement("SpeakerName")]
    public string SpeakerName { get; set; }
    [XmlElement("Text")]
    public string Text { get; set; }
}