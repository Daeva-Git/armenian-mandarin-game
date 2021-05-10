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
            
            //StartCoroutine(FadeAway(0.02f));
        }

        public void LoadText(int id)
        {
            TextLine textLine = GameManager.Instance.ComponentManager.TextLines[id];
            var textToDisplay = textLine.Text;
            speakerName.text = textLine.SpeakerName;
            
            backgroundPanel.gameObject.SetActive(true);
            textPanel.gameObject.SetActive(true);
            speakerNamePanel.gameObject.SetActive(true);
            
            StartCoroutine(DisplayText(0.02f, textToDisplay));
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
        
        private IEnumerator FadeAway(float waitTime)
        {
            Camera.main.orthographic = true;
            for (var i = 1.0f; i < 5; i += 0.04f)
            {
                Camera.main.orthographicSize = i;
                yield return new WaitForSeconds(waitTime);
            }
            
            LoadText(3);
        }
    }
}