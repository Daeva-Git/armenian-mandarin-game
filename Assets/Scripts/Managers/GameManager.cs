using DefaultNamespace.Managers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private ComponentManager componentManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SoundManager soundManager;
    
    public ComponentManager ComponentManager => componentManager;
    public UIManager UIManager => uiManager;
    public SoundManager SoundManager => soundManager;

    private TextLine _currentTextLine;
    private int _currentID;
    private bool flashlightScare = false;
    
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
    }

    void Update()
    {
        if(ComponentManager.RatController.globalBoo && !flashlightScare){
            flashlightScare = true;
            Debug.Log("BOO");
        }
        if(ComponentManager.RatController.globalBoo && flashlightScare){
            flashlightScare = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && UIManager.TextDisplayed)
        {
            _currentTextLine = ComponentManager.TextLines[++_currentID];
            UIManager.LoadUI(_currentTextLine);
            SoundManager.Play(_currentTextLine.OST);
            SoundManager.Play(_currentTextLine.Sound);
            if (_currentTextLine.WaitFor != 0 && _currentTextLine.RatCount != 0)
            {
                var ratSpawningRate = _currentTextLine.RatCount;
                ComponentManager.RatController.ShowRats(ratSpawningRate);
            }
        }
        
        ComponentManager.CameraScript.UpdateCamera();
    }

    private void Start()
    {
        _currentTextLine = ComponentManager.TextLines[_currentID];
        UIManager.LoadUI(_currentTextLine);
        SoundManager.Play(_currentTextLine.OST);
    }
}