using UnityEngine;
using TMPro;

public class JudgeNotes : MonoBehaviour
{
    [SerializeField] TMP_Text patternText;
    [SerializeField] TMP_Text resultText;
    [SerializeField] TapScreen _tapScreenIns;

    LoadMorsePattaernData _loadmorsePatternIns;
    [SerializeField] TextAsset morsePatternCSVData;

    private string targetMorsePattern = "";
    private string currentInputPattern = "";

    void Start()
    {
        // ランダムにモールスパターンを選出
        _loadmorsePatternIns = new LoadMorsePattaernData(morsePatternCSVData);
        targetMorsePattern = _loadmorsePatternIns.GetRandomMorsePattern();
        patternText.text = targetMorsePattern;
        resultText.text = "";
    }

    void Update()
    {
        // TapScreen から現在のモールス入力を取得
        currentInputPattern = _tapScreenIns.CurrentMorseSignal;

        if (currentInputPattern.Length == targetMorsePattern.Length)
        {
            CheckMorsePattern();
        }
    }

    void CheckMorsePattern()
    {
        if (currentInputPattern == targetMorsePattern) resultText.text = "Success!";

        else resultText.text = "Failure...";

        // 次のパターンを選出してリセット
        currentInputPattern = "";
        targetMorsePattern = "random pattern";  // ここにランダムパターンの取得処理
        patternText.text = targetMorsePattern;
    }

    //リセットし、もう一度
    public void InitializationPattern()
    {
        _tapScreenIns.ClearSignal();
        targetMorsePattern = _loadmorsePatternIns.GetRandomMorsePattern();
        patternText.text = targetMorsePattern;
        resultText.text = "";
    }
}