using UnityEngine;
using TMPro;

public class JudgeNotes : MonoBehaviour
{
    [SerializeField] TMP_Text patternText;
    [SerializeField] TMP_Text resultText;
    [SerializeField] InputMorse _inputMorseIns;

    LoadCSVData _loadmorsePatternIns;
    [SerializeField] TextAsset morsePatternCSVData;

    private string targetMorsePattern = "";
    private string currentInputPattern = "";

    void Start()
    {
        _loadmorsePatternIns = new LoadCSVData(morsePatternCSVData,false);
        targetMorsePattern = _loadmorsePatternIns.GetRandomMorsePattern();
        patternText.text = targetMorsePattern;
        resultText.text = "";
    }

    void Update()
    {
        // TapScreen から現在のモールス入力を取得
        currentInputPattern = _inputMorseIns.CurrentMorseSignal;

        if (currentInputPattern.Length == targetMorsePattern.Length) CheckMorsePattern();
    }

    //成功か失敗かの判定
    void CheckMorsePattern()
    {
        if (currentInputPattern == targetMorsePattern) resultText.text = "Success!";
        else resultText.text = "Failure...";
    }

    //リセットし、もう一度
    public void InitializationPattern()
    {
        _inputMorseIns.ClearSignal();
        targetMorsePattern = _loadmorsePatternIns.GetRandomMorsePattern();
        patternText.text = targetMorsePattern;
        resultText.text = "";
    }
}