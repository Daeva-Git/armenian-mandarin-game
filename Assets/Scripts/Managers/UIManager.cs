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
        
        private int _id = 0;
        public int CurrentID => _id;
        private bool _innerView;
        
        private void Start()
        {
            backgroundPanel.gameObject.SetActive(false);
            textPanel.gameObject.SetActive(false);
            speakerNamePanel.gameObject.SetActive(false);

            _innerView = true;
            //StartCoroutine(FadeAway(0.02f));

            LoadText(_id);
        }

        
        public void LoadText(int id)
        {
            var textLine = GameManager.Instance.ComponentManager.TextLines[id];
            var textToDisplay = textLine.Text;
            speakerName.text = textLine.SpeakerName;
            
            backgroundPanel.gameObject.SetActive(true);
            textPanel.gameObject.SetActive(true);
            
            if (speakerName.text.Equals("") && _innerView)
            {
                speakerNamePanel.gameObject.SetActive(false);
                _innerView = !_innerView;
                StartCoroutine(FadeOut(0.02f));
            }
            else if (!_innerView)
            {
                speakerNamePanel.gameObject.SetActive(true);
                _innerView = !_innerView;
                StartCoroutine(FadeIn(0.02f));
            }
            
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
            while (!Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
            }
            
            LoadText(++_id);
        }
        
        private IEnumerator FadeOut(float waitTime)
        {
            Camera.main.orthographic = true;
            for (var i = 1.0f; i < 5; i += 0.03f)
            {
                Camera.main.orthographicSize = i;
                yield return new WaitForSeconds(waitTime);
            }
        }
        
        private IEnumerator FadeIn(float waitTime)
        {
            for (var i = Camera.main.orthographicSize; i > 3.5f; i -= 0.01f)
            {
                Camera.main.orthographicSize = i;
                yield return new WaitForSeconds(waitTime);
            }
            Camera.main.orthographic = false;
        }
    }
}