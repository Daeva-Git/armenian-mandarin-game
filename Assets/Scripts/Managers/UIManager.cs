using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image backgroundPanel;  
        
        [SerializeField] private Text displayedText;
        [SerializeField] private Image textPanel;
        
        
        [SerializeField] private Text speakerName;
        [SerializeField] private Image speakerNamePanel;
        
        private void Start()
        {
            backgroundPanel.gameObject.SetActive(false);
            textPanel.gameObject.SetActive(false);
            speakerNamePanel.gameObject.SetActive(false);
            
            LoadText(3);
        }

        public void LoadText(int id)
        {
            TextLine textLine = GameManager.Instance.ComponentManager.TextLines[id];
            var textToDisplay = textLine.Text;
            speakerName.text = textLine.SpeakerName;
            
            backgroundPanel.gameObject.SetActive(true);
            textPanel.gameObject.SetActive(true);
            speakerNamePanel.gameObject.SetActive(true);
            
            StartCoroutine(DisplayText(0.3f, textToDisplay));
        }
        
        private IEnumerator DisplayText(float waitTime, string text)
        {
            var currentText = "";
            foreach (var nextChar in text)
            {
                currentText += nextChar;
                displayedText.text = currentText;
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}