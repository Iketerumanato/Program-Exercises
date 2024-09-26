using System.Collections.Generic;
using UnityEngine;

public class LoadCSVData
{
    private Dictionary<string, char> morseCodeDictionary = new();
    private List<string> morsePatterns = new();

    readonly int maxDataLength = 2;

    public LoadCSVData(TextAsset csvData, bool IsCSVDataType)
    {       
        if(IsCSVDataType) LoadMorseCodeFromCSV(csvData);
        else LoadMorsePatternFromCSV(csvData);
    }

    void LoadMorseCodeFromCSV(TextAsset csvData)
    {
        if (csvData != null)
        {
            var lines = csvData.text.Split('\n');
            foreach (var line in lines)
            {
                var values = line.Split(',');
                if (values.Length == maxDataLength)
                {
                    string morseCode = values[0].Trim();
                    char letter = values[1].Trim()[0];
                    morseCodeDictionary[morseCode] = letter;
                }
            }
            Debug.Log($"CSVデ－タ {csvData.name} を読み込みました");
        }
        else Debug.LogError("CSVデータがアサインされていません");
    }

    // モールス信号に対応する文字を返す
    public char? GetLetterFromMorseCode(string morseCode)
    {
        if (morseCodeDictionary.ContainsKey(morseCode)) return morseCodeDictionary[morseCode];
        else return null;
    }

    void LoadMorsePatternFromCSV(TextAsset csvData)
    {
        if (csvData != null)
        {
            var lines = csvData.text.Split('\n');
            foreach (var line in lines)
            {
                // 空行をスキップ
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // モールス信号のパターンをリストに追加
                morsePatterns.Add(line.Trim());
            }
            Debug.Log($"CSVデータ {csvData.name} を読み込みました");
        }
        else
        {
            Debug.LogError("CSVデータがアサインされていません");
        }
    }

    // ランダムにモールス信号パターンを取得
    public string GetRandomMorsePattern()
    {
        if (morsePatterns.Count == 0)
        {
            Debug.LogError("モールスパターンが読み込まれていません。");
            return null;
        }
        int randomIndex = Random.Range(0, morsePatterns.Count);
        return morsePatterns[randomIndex];
    }
}