using System;
using System.Runtime.CompilerServices;
using DefaultNamespace.Managers;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    [SerializeField] private ComponentManager componentManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CameraScript cameraScript;
    [SerializeField] private RatController ratController;

    public ComponentManager ComponentManager => componentManager;
    public UIManager UIManager => uiManager;
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

    void Update(){
        if(stage == 0){
            ratController.showRats(4);
        }
        
    }
}