using System;
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

    private void Start()
    {
        _currentTextLine = ComponentManager.TextLines[_currentID];
        UIManager.LoadText(_currentTextLine);
        SoundManager.PlaySound(_currentTextLine.OST);
    }

    private void Update()
    {
        // while (UIManager.TextDisplayed && !Input.GetKeyDown(KeyCode.Space)) ;
        // _currentTextLine = ComponentManager.TextLines[++_currentID];
        // UIManager.LoadText(_currentTextLine);
        // SoundManager.PlaySound(_currentTextLine.OST);
    }
}