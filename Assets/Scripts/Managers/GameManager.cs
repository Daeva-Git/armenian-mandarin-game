using DefaultNamespace.Managers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private ComponentManager componentManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private CameraScript cameraScript;
    [SerializeField] private RatController ratController;
    
    public ComponentManager ComponentManager => componentManager;
    public UIManager UIManager => uiManager;
    public SoundManager SoundManager => soundManager;
    public CameraScript CameraScript => cameraScript;
    public RatController RatController => ratController;

    private TextLine _currentTextLine;
    private int _currentID;
    private int stage;
    
    public static GameManager Instance
    {
        get => _instance;
        private set => _instance = value;
    }
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(gameObject);
        
        stage = 0;
    }

    void Update()
    {
        // while (UIManager.TextDisplayed && !Input.GetKeyDown(KeyCode.Space)) ;
        // _currentTextLine = ComponentManager.TextLines[++_currentID];
        // UIManager.LoadText(_currentTextLine);
        // SoundManager.PlaySound(_currentTextLine.OST);
    }

    private void Start()
    {
        _currentTextLine = ComponentManager.TextLines[_currentID];
        UIManager.LoadText(_currentTextLine);
        SoundManager.PlaySound(_currentTextLine.OST);
    }
}