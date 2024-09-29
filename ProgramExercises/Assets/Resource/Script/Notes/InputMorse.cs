using UnityEngine;
using System;
using UniRx;
using System.Linq;
using TMPro;

public class InputMorse : MonoBehaviour
{ 
    [SerializeField] TMP_Text displayText;
    [SerializeField] TMP_Text outputText;
    private float pressTime = 0f;
    const float InitialPressTime = 0f;
    private string MorseSignal = "";
    private string Outputstr = "";

    [SerializeField,Range(0f,1f)]
    float thresholdTime = 0.5f;

    [SerializeField] TextAsset MorseCSVData;
    private LoadCSVData _loadCsvDataIns;

    [SerializeField] bool IsLoadData = false;

    public string CurrentMorseSignal
    {
        get { return MorseSignal; }
    }

    private Subject<Unit> onPointerDownSubject = new();
    private Subject<Unit> onPointerUpSubject = new();

    private void Start()
    {
        if(IsLoadData) _loadCsvDataIns = new(MorseCSVData,true);

        var pressDurationObservable = onPointerDownSubject
            .SelectMany(_ =>
                Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(0.01f))
                    .Select(time => time * 0.01f)
                    .TakeUntil(onPointerUpSubject)
            )
            .Subscribe(time =>
            {
                pressTime = (float)time;

                if (pressTime >= thresholdTime && MorseSignal.LastOrDefault() != '-')
                {
                    MorseSignal += "-";
                    displayText.text = MorseSignal;
                    pressTime = InitialPressTime;
                }
            });

        onPointerUpSubject
            .Where(_ => pressTime < thresholdTime)
            .Subscribe(_ =>
            {
                MorseSignal += ".";
                displayText.text = MorseSignal;
            });
    }

    #region ボタンの処理群

    public void OnPointerDown()
    {
        onPointerDownSubject.OnNext(Unit.Default);
    }

    public void OnPointerUp()
    {
        onPointerUpSubject.OnNext(Unit.Default);
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