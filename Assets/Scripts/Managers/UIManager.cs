using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Text _displayedText;
        private string textToBeDisplayed;
        
        private void Start()
        {
            textToBeDisplayed = GameManager.Instance.ComponentManager.TextLines[0].Text;
            _displayedText.text = textToBeDisplayed;
        }
    }
}