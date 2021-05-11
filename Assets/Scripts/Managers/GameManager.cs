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
    private bool _playerResponse = true;
    public bool PlayerResponse
    {
        get => _playerResponse;
        set => _playerResponse = value;
    }
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

    private void Update()
    {
        if(ComponentManager.RatController.globalBoo && !flashlightScare){
            flashlightScare = true;
        }
        if(ComponentManager.RatController.globalBoo && flashlightScare){
            flashlightScare = false;
        }
        // if (_playerResponse)
        // {
            if (Input.GetKeyDown(KeyCode.Space) && UIManager.TextDisplayed)
            {
                NextTextLine();
            }
        // }

        ComponentManager.CameraScript.UpdateCamera();

        if(Input.GetKeyDown("escape"))
            Application.Quit();
            
    }

    public void NextTextLine()
    {
        _currentTextLine = ComponentManager.TextLines[++_currentID];
        UIManager.LoadUI(_currentTextLine);
        SoundManager.Play(_currentTextLine.OST);
        SoundManager.Play(_currentTextLine.Sound);
        if (_currentTextLine.WaitFor != 0 && _currentTextLine.RatCount != 0)
        {
            if(_currentTextLine.RatCount == -1)
                ComponentManager.RatController.HideRats();
            else{
                // _playerResponse = false;
                var ratSpawningRate = _currentTextLine.RatCount;
                ComponentManager.RatController.ShowRats(ratSpawningRate);
            }
            // _playerResponse = false;
        }
    }
    
    private void Start()
    {
        _currentTextLine = ComponentManager.TextLines[_currentID];
        UIManager.LoadUI(_currentTextLine);
        SoundManager.Play(_currentTextLine.OST);
    }
}