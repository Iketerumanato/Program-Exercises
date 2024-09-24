using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TapScreen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{ 
    [SerializeField] TMP_Text displayText;
    private float pressTime = 0.0f;
    private bool isPressing = false;
    private bool dashDisplayed = false;
    private string morseSignal = "";

    [SerializeField,Range(0f,1f)]
    float threshold = 0.5f;

    void Update()
    {
        if (isPressing)
        {
            pressTime += Time.deltaTime;

            if (pressTime >= threshold && !dashDisplayed)
            {
                morseSignal += "-";
                displayText.text = morseSignal;
                dashDisplayed = true;
                pressTime = 0.0f;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
        pressTime = 0.0f;
        dashDisplayed = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;

        if (!dashDisplayed)
        {
            morseSignal += ".";
            displayText.text = morseSignal;
        }
    }

    public void ClearSignal()
    {
        morseSignal = "";
        displayText.text = morseSignal;
    }
}