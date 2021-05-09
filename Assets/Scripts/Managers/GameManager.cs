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

    public ComponentManager ComponentManager => componentManager;
    public UIManager UIManager => uiManager;

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
}