using System.Xml.Serialization;
using DefaultNamespace.Managers;

public class TextLine {
    [XmlAttribute("ID")]
    public int ID { get; set; }
    [XmlElement("SpeakerName")]
    public string SpeakerName { get; set; }
    [XmlElement("Text")]
    public string Text { get; set; }
    [XmlElement("RatCount")]
    public int RatCount { get; set; }
    [XmlElement("OST")]
    public SoundManager.OST OST { get; set; }
    [XmlElement("View")]
    public UIManager.View View { get; set; }
    [XmlElement("Sound")]
    public SoundManager.Sound Sound { get; set; }
    [XmlElement("WaitFor")]
    public float WaitFor { get; set; }
}