using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    [SerializeField] private Orange orange;
    [SerializeField] private CameraScript cameraScript;
    [SerializeField] private RatController ratController;
    [SerializeField] private GameObject playerFlashLight;
    [SerializeField] private ParticleSystem sandParticle;
    public Orange Orange => orange;
    public CameraScript CameraScript => cameraScript;
    public RatController RatController => ratController;
    public GameObject PlayerFlashLight => playerFlashLight;
    public ParticleSystem SandParticle => sandParticle;

    
    private const string TextDataPath = "XML/TextData";
    
    public List<TextLine> TextLines = new List<TextLine>();

    private void Awake()
    {
        var textData = TextContainer.Load(TextDataPath);
        TextLines = textData.TextLines;
    }
}
