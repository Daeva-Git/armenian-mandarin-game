using System.Collections.Generic;
using System.Xml;
using DefaultNamespace;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    private const string TextDataPath = "XML/TextData";
    
    public List<TextLine> TextLines = new List<TextLine>();

    private void Awake()
    {
        var textData = TextContainer.Load(TextDataPath);
        TextLines = textData.TextLines;
    }
}
