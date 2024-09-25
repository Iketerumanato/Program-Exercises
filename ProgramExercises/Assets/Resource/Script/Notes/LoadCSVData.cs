using System.Collections.Generic;
using UnityEngine;

public class LoadCSVData
{
    private Dictionary<string, char> morseCodeDictionary = new();

    public LoadCSVData(TextAsset csvData)
    {
        LoadMorseCodeFromCSV(csvData);
    }

    void LoadMorseCodeFromCSV(TextAsset csvData)
    {
        if (csvData != null)
        {
            var lines = csvData.text.Split('\n');
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
}