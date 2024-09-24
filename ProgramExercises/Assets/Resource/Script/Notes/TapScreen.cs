using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class TapScreen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{ 
    [SerializeField] TMP_Text displayText;
    [SerializeField] TMP_Text outputText;
    private float pressTime = 0.0f;
    private bool isPressing = false;
    private bool dashDisplayed = false;
    private string MorseSignal = "";
    private string Outputstr = "";

    [SerializeField,Range(0f,1f)]
    float threshold = 0.5f;

    [SerializeField] TextAsset csvMorseFile;
    private Dictionary<string, char> morseCodeDictionary = new();

    private void Start()
    {
        LoadMorseCodeFromCSV();
    }

    void Update()
    {
        if (isPressing)
        {
            pressTime += Time.deltaTime;

            if (pressTime >= threshold && !dashDisplayed)
            {
                MorseSignal += "-";
                displayText.text = MorseSignal;
                dashDisplayed = true;
                pressTime = 0.0f;
            }
        }
    }

    void LoadMorseCodeFromCSV()
    {
        if (csvMorseFile != null)
        {
            var lines = csvMorseFile.text.Split('\n');
            foreach (var line in lines)
            {
                var values = line.Split(',');
                if (values.Length == 2)
                {
                    string morseCode = values[0].Trim();
                    char letter = values[1].Trim()[0];
                    morseCodeDictionary[morseCode] = letter;
                }
            }
            Debug.Log($"CSV�f�|�^ {csvMorseFile.name} ��ǂݍ��݂܂���");
        }
        else Debug.LogError("CSV�f�[�^���A�T�C������Ă��܂���");
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
            MorseSignal += ".";
            displayText.text = MorseSignal;
        }
    }

    //���͂������[���X�M���̔���
    public void ConfirmMorseSignal()
    {
        if (morseCodeDictionary.ContainsKey(MorseSignal))
        {
            char letter = morseCodeDictionary[MorseSignal];
            Outputstr += letter;
            outputText.text = Outputstr;
        }
        else
        {
            Debug.LogError("�����ȃ��[���X�M���ł�");
        }

        ResetMorse();
    }

    void ResetMorse()
    {
        // ���[���X�M�������Z�b�g
        MorseSignal = "";
        displayText.text = MorseSignal;
    }

    public void ClearSignal()
    {
        MorseSignal = "";
        Outputstr = "";
        displayText.text = MorseSignal;
        outputText.text = Outputstr;
    }
}