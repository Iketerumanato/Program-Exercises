using UnityEngine;
using TMPro;

public class InputMorse : MonoBehaviour
{ 
    [SerializeField] TMP_Text displayText;
    [SerializeField] TMP_Text outputText;
    private float pressTime = 0f;
    const float InitialPressTime = 0f;
    private bool isPressing = false;
    private bool dashDisplayed = false;
    private string MorseSignal = "";
    private string Outputstr = "";

    [SerializeField,Range(0f,1f)]
    float threshold = 0.5f;

    [SerializeField] TextAsset MorseCSVData;
    private LoadCSVData _loadCsvDataIns;

    [SerializeField] bool IsLoadData = false;

    public string CurrentMorseSignal
    {
        get { return MorseSignal; }
    }

    private void Start()
    {
        if(IsLoadData) _loadCsvDataIns = new(MorseCSVData,true);
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
                pressTime = InitialPressTime;
            }
        }
    }

    #region ボタンの処理群

    public void OnPointerDown()
    {
        isPressing = true;
        pressTime = InitialPressTime;
        dashDisplayed = false;
    }

    public void OnPointerUp()
    {
        isPressing = false;

        if (!dashDisplayed)
        {
            MorseSignal += ".";
            displayText.text = MorseSignal;
        }
    }

    //入力したモールス信号の判定
    public void ConfirmMorseSignal()
    {
        var letter = _loadCsvDataIns.GetLetterFromMorseCode(MorseSignal);

        if (letter.HasValue)
        {
            Outputstr += letter;
            outputText.text = Outputstr;
        }
        else Debug.LogError("無効なモールス信号です");

        ResetMorse();
    }

    void ResetMorse()
    {
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
    #endregion
}