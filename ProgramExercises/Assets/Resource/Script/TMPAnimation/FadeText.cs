using TMPro;
using UnityEngine;
using System.Threading.Tasks;

public class FadeText : MonoBehaviour
{
    TMP_Text TestText;

    [SerializeField,Range(3f,0.1f)]
    float fadeDuration = 2.0f;

    [SerializeField] bool IsFade = true;
    private bool IsFading = false;

    private Color textcolor;
    private float currentAlpha = 1f;

    readonly float maxTextAlpha = 1f;
    readonly float minTextAlpha = 0f;

    void Start()
    {
        TestText = GetComponent<TMP_Text>();
        textcolor = TestText.color;
        currentAlpha = IsFade ? minTextAlpha : maxTextAlpha;
        textcolor.a = currentAlpha;
        TestText.color = textcolor;
    }

    private void Update()
    {
        if (!IsFading)
        {
            if (IsFade && currentAlpha < maxTextAlpha) _ = FadeInAsync();          
            else if (!IsFade && currentAlpha > minTextAlpha) _ = FadeOutAsync();
        }
    }

    private async Task FadeInAsync()
    {
        IsFading = true;
        float fadeTimer = 0f;

        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            currentAlpha = Mathf.Lerp(minTextAlpha, maxTextAlpha, fadeTimer / fadeDuration);
            textcolor.a = currentAlpha;
            TestText.color = textcolor;

            await Task.Yield();
        }

        currentAlpha = maxTextAlpha;
        textcolor.a = currentAlpha;
        TestText.color = textcolor;
        IsFading = false;
    }

    private async Task FadeOutAsync()
    {
        IsFading = true;
        float fadeTimer = 0f;

        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            currentAlpha = Mathf.Lerp(maxTextAlpha, minTextAlpha, fadeTimer / fadeDuration);
            textcolor.a = currentAlpha;
            TestText.color = textcolor;

            await Task.Yield(); 
        }

        currentAlpha = minTextAlpha;
        textcolor.a = currentAlpha;
        TestText.color = textcolor;
        IsFading = false;
    }
}