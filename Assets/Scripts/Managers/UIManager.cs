using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Text displayedText;
        [SerializeField] private Image backgroundPanel;  
        [SerializeField] private Image textPanel;  
        
        private void Start()
        {
            backgroundPanel.gameObject.SetActive(false);
            textPanel.gameObject.SetActive(false);
            
            LoadText(0);
        }

        public void LoadText(int id)
        {
            displayedText.text = GameManager.Instance.ComponentManager.TextLines[id].Text;
            
            backgroundPanel.gameObject.SetActive(true);
            textPanel.gameObject.SetActive(true);
        }
    }
}