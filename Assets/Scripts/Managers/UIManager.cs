using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image fadePanel;
        [SerializeField] private Text displayedText;
        [SerializeField] private Image textPanel;
        [SerializeField] private Text speakerName;
        [SerializeField] private Image speakerNamePanel;
        [SerializeField] private Button responseButton;
        
        private View _currentView;
        private bool _textDisplayed;

        public bool TextDisplayed => _textDisplayed;
        public enum View
        {
            Default,
            Inner,
            Outer
        }
        
        private void Start()
        {
            _currentView = View.Outer;
            responseButton.GetComponentInChildren<Text>().text = "Արձագանքել";
            StartCoroutine(FadeOut(0.02f));
            HideUI();
            
            responseButton.onClick.AddListener(OnResponseButtonPress);
        }

        private void HideUI()
        {
            textPanel.gameObject.SetActive(false);
            speakerNamePanel.gameObject.SetActive(false);
            responseButton.gameObject.SetActive(false);
        }

        public void LoadUI(TextLine textLine)
        {
            StartCoroutine(LoadUIEnumerator(textLine));
        }

        private IEnumerator LoadUIEnumerator(TextLine textLine)
        {
            HideUI();
            _textDisplayed = false;

            speakerName.text = textLine.SpeakerName;
            var textToDisplay = textLine.Text;
            var narrator = textLine.SpeakerName == null;
            var view = textLine.View;
            var waitFor = textLine.WaitFor;

            yield return StartCoroutine(LoadTextEnumerator(textToDisplay, narrator, view));
            if (textLine.SandDrop)
            {
                GameManager.Instance.ComponentManager.SandParticle.Play();
            }
            LoadTimer(waitFor);
        }

        private IEnumerator LoadTextEnumerator(string textToDisplay, bool narrator, View view)
        {
            if (view != View.Default)
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
            }
            StartCoroutine(DisplayText(0.02f, textToDisplay, narrator));
        }

        private void LoadTimer(float waitFor)
        {
            if (waitFor == 0) return;
            responseButton.gameObject.SetActive(true);
            StartCoroutine(LoadTimerEnumerator(waitFor));
        }

        private IEnumerator LoadTimerEnumerator(float duration)
        {
            float normalizedTime = 0;
            while(normalizedTime <= 1f)
            {
                if (!GameManager.Instance.WaitingForResponse) yield break;
                normalizedTime += Time.deltaTime / duration;
                yield return null;
            }

            GameManager.Instance.ComponentManager.Orange.Blink();
        }
        
        private IEnumerator DisplayText(float waitTime, string text, bool narrator)
        {
            textPanel.gameObject.SetActive(true);
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
            var mainCamera = GameManager.Instance.ComponentManager.MainCamera;
            mainCamera.orthographic = true;
            
            for (var i = 1.0f; i < 5; i += 0.03f)
            {
                mainCamera.orthographicSize = i;
                yield return new WaitForSeconds(waitTime);
            }
        }

        public void OnResponseButtonPress()
        {
            GameManager.Instance.WaitingForResponse = false;
            GameManager.Instance.NextTextLine();
        }
        
        private IEnumerator FadeIn(float waitTime)
        {
            var mainCamera = GameManager.Instance.ComponentManager.MainCamera;
            
            for (var i = mainCamera.orthographicSize; i > 3f; i -= 0.01f)
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