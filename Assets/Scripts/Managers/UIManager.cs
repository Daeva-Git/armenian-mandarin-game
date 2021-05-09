using System;
using System.Collections;
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
        private string textToDisplay;
        private void Start()
        {
            backgroundPanel.gameObject.SetActive(false);
            textPanel.gameObject.SetActive(false);
            
            LoadText(0);
        }

        public void LoadText(int id)
        {
            textToDisplay = GameManager.Instance.ComponentManager.TextLines[id].Text;
            
            backgroundPanel.gameObject.SetActive(true);
            textPanel.gameObject.SetActive(true);
            
            StartCoroutine(nameof(DisplayText), 1.4 / textToDisplay.Length);
        }
        
        private IEnumerator DisplayText(float waitTime)
        {
            string currentText = "";
            for (int i = 0; i < textToDisplay.Length; i++)
            {
                currentText = currentText + textToDisplay[i];
                displayedText.text = currentText;
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}