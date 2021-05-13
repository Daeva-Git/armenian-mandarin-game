using System.Collections;
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
    public bool WaitingForResponse
    {
        get => _waitingForResponse;
        set => _waitingForResponse = value;
    }
    
    private TextLine _currentTextLine;
    private bool _waitingForResponse;
    private bool _flashlightScare;
    private int _currentID;

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

    private bool _gameStart;
    
    private void Update()
    {
        if (!_gameStart) return;
         
        if (ComponentManager.RatController.ScarePlayer && !_flashlightScare)
        {
            _flashlightScare = true;
            ComponentManager.Orange.Blink();
        }

        if (ComponentManager.RatController.ScarePlayer && _flashlightScare)
        {
            _flashlightScare = false;
        }

        if (!_waitingForResponse)
        {
            if (Input.GetKeyDown(KeyCode.Space) && UIManager.TextDisplayed)
            {
                NextTextLine();
            }
        }

        ComponentManager.CameraScript.UpdateCamera();

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }

    public void NextTextLine()
    {
        _currentID++;
        if (_currentID == ComponentManager.TextLines.Capacity)
        {
            UIManager.HideUI();
            UIManager.HowToPlay.text = "Վերջ";
            UIManager.HowToPlay.gameObject.SetActive(true);
            _gameStart = false;
            return;
        }
        _currentTextLine = ComponentManager.TextLines[_currentID];
        UIManager.LoadUI(_currentTextLine);
        SoundManager.Play(_currentTextLine.OST);
        SoundManager.Play(_currentTextLine.Sound);

        if (_currentID == 61)
        {
            ComponentManager.Orange.Blink();
            ComponentManager.Orange.gameObject.SetActive(false);
        }
        
        if (_currentTextLine.WaitFor != 0)
        {
            _waitingForResponse = true;
            if (_currentTextLine.RatCount != 0)
            {
                var ratSpawningRate = _currentTextLine.RatCount;
                ComponentManager.RatController.ShowRats(ratSpawningRate);
            }
            else
            {
                ComponentManager.RatController.HideRats();
            }
        }
    }

    private void Start()
    {
        _currentTextLine = ComponentManager.TextLines[_currentID];
        
        StartCoroutine(HowToPlay());
    }

    private IEnumerator HowToPlay()
    {
        _gameStart = false;
        UIManager.HowToPlay.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        UIManager.HowToPlay.gameObject.SetActive(false);
        _gameStart = true;
        
        UIManager.LoadUI(_currentTextLine);
        SoundManager.Play(_currentTextLine.OST);
    }
}