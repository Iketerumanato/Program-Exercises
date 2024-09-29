using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UniRx;
using System;

public class InputMorse : MonoBehaviour
{ 
    [SerializeField] TMP_Text displayText;
    [SerializeField] TMP_Text outputText;
    private float pressTime = 0f;
    const float InitialPressTime = 0f;
    private string MorseSignal = "";
    public string CurrentMorseSignal
    {
        get { return MorseSignal; }
    }

    private string Outputstr = "";

    [SerializeField,Range(0f,1f)]
    float thresholdTime = 0.5f;

    [SerializeField, Range(0f, 0.05f)]
    float pressSpeed = 0.01f;

    [SerializeField] TextAsset MorseCSVData;
    private LoadCSVData _loadCsvDataIns;

    [SerializeField] bool IsLoadData = false;

    //private CancellationTokenSource _cts;
    //public string CurrentMorseSignal => MorseSignal;

    private Subject<Unit> pointerDownSubject = new();
    private Subject<Unit> pointerUpSubject = new();

    private bool isDashInputted = false;

    private void Start()
    {
        if(IsLoadData) _loadCsvDataIns = new(MorseCSVData,true);

        pointerDownSubject
           .SelectMany(_ =>
               Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(10))
                   .Select(time => time * pressSpeed)
                   .TakeUntil(pointerUpSubject)
           )
           .Repeat()
           .Subscribe(time =>
           {
               pressTime = (float)time;

               if (pressTime >= thresholdTime && !isDashInputted)
               {
                   MorseSignal += "-";
                   displayText.text = MorseSignal;
                   isDashInputted = true;
               }
           });

        pointerUpSubject
            .Subscribe(_ =>
            {
                if (pressTime < thresholdTime)
                {
                    MorseSignal += ".";
                    displayText.text = MorseSignal;
                }

                pressTime = InitialPressTime;
            });
    }

    #region ボタンの処理群

    #region async/await版
    //public async Task OnPointerDownAsync()
    //{
    //    _cts = new CancellationTokenSource();
    //    await HandlePressDuration(_cts.Token);
    //}

    //public void OnPointerUp()
    //{
    //    _cts?.Cancel();
    //    if (pressTime < thresholdTime)
    //    {
    //        MorseSignal += ".";
    //        displayText.text = MorseSignal;
    //    }
    //    pressTime = InitialPressTime;
    //}

    //private async Task HandlePressDuration(CancellationToken token)
    //{
    //    pressTime = InitialPressTime;
    //    while (pressTime < thresholdTime)
    //    {
    //        await Task.Delay(10);

    //        if (token.IsCancellationRequested) return;

    //        pressTime += pressSpeed;
    //    }
    //    MorseSignal += "-";
    //    displayText.text = MorseSignal;
    //}
    #endregion

    public void OnPointerDown()
    {
        isDashInputted = false;
        pointerDownSubject.OnNext(Unit.Default);
    }

    // ボタンが離された時に呼ばれる
    public void OnPointerUp()
    {
        pointerUpSubject.OnNext(Unit.Default);  
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
        else Debug.LogWarning("無効なモールス信号です");

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