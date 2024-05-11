using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.1f; // Delay between each character
    public int targetFontSize = 20; // Target font size to start the effect
    public float startTimeDelay = 2.0f; // Delay before starting the effect
    private TMP_Text textComponent;
    private string fullText;
    private string currentText = "";
    private bool isTyping = false;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        fullText = textComponent.text;
        textComponent.text = "";

        StartCoroutine(StartTypewriter());
    }

    IEnumerator StartTypewriter()
    {
        yield return new WaitForSeconds(startTimeDelay);
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
