using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image fadePanel;
        [SerializeField] private Text displayedText;
        [SerializeField] private Image textPanel;
        [SerializeField] private Text speakerName;
        [SerializeField] private Image speakerNamePanel;
        
        private View _currentView;
        private bool _textDisplayed;

        public bool TextDisplayed => _textDisplayed;
        public enum View
        {
            Inner,
            Outer
        }
        
        private void Start()
        {
            _currentView = View.Outer;
            StartCoroutine(FadeOut(0.02f));
            HideUI();
        }

        private void ShowText()
        {
            textPanel.gameObject.SetActive(true);
        }

        private void HideUI()
        {
            textPanel.gameObject.SetActive(false);
            speakerNamePanel.gameObject.SetActive(false);
        }
        
        public void LoadText(TextLine textLine)
        {
            HideUI();
            
            _textDisplayed = false;
            
            var textToDisplay = textLine.Text;
            var narrator = textLine.SpeakerName == null;
            var view = textLine.View;
            speakerName.text = textLine.SpeakerName;

            StartCoroutine(LoadTextEnumerator(textToDisplay, narrator, view));
        }

        private IEnumerator LoadTextEnumerator(string textToDisplay, bool narrator, View view)
        {
            if (_currentView != view)
            {
                if (view == View.Inner)
                {
                    yield return StartCoroutine(FadeIn(0.02f)); 
                }
                else if (view == View.Outer)
                {
                    yield return StartCoroutine(FadeOut(0.02f));
                }
                _currentView = view;
            }
            StartCoroutine(DisplayText(0.02f, textToDisplay, narrator));
        }

        private IEnumerator DisplayText(float waitTime, string text, bool narrator)
        {
            ShowText();
            speakerNamePanel.gameObject.SetActive(!narrator);
            
            var currentText = "";
            foreach (var nextChar in text)
            {
                currentText += nextChar;
                displayedText.text = currentText;
                yield return new WaitForSeconds(waitTime);
            }

            _textDisplayed = true;
        }
        
        private IEnumerator FadeOut(float waitTime)
        {
            var mainCamera = Camera.main;
            mainCamera.orthographic = true;
            for (var i = 1.0f; i < 5; i += 0.03f)
            {
                mainCamera.orthographicSize = i;
                yield return new WaitForSeconds(waitTime);
            }
        }
        
        private IEnumerator FadeIn(float waitTime)
        {
            var mainCamera = Camera.main;
            
            for (var i = mainCamera.orthographicSize; i > 2f; i -= 0.01f)
            {
                mainCamera.orthographicSize = i;
                yield return new WaitForSeconds(waitTime);
            }

            var fadePanelColor = fadePanel.color;
            fadePanelColor.a = 1f;
            mainCamera.orthographic = false;
            for (var i = 1f; i >= 0; i -= 0.01f)
            {
                fadePanelColor.a = i;
                fadePanel.color = fadePanelColor;
                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}