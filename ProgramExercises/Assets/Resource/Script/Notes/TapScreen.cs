using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using System.IO;

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

    // CSV�t�@�C���̃p�X (Assets�t�H���_�Ȃǂɔz�u)
    public string csvFilePath = "Assets/Resource/Data/MorseCode.csv";

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
        try
        {
            // CSV�t�@�C������s���ǂݍ���
            foreach (var line in File.ReadLines(csvFilePath))
            {
                var values = line.Split(',');
                if (values.Length == 2)
                {
                    string morseCode = values[0].Trim();
                    char letter = values[1].Trim()[0];
                    morseCodeDictionary[morseCode] = letter;
                }
            }
            Debug.Log("CSV���烂�[���X�M����ǂݍ��݂܂���");
        }
        catch (System.Exception e)
        {
            Debug.LogError("CSV�̓ǂݍ��݂Ɏ��s���܂���: " + e.Message);
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
            MorseSignal += ".";
            displayText.text = MorseSignal;
        }
    }

    //���͂������[���X�M���𔻒�
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