using System.Collections.Generic;
using UnityEngine;

public class LoadMorsePattaernData
{
    private List<string> morsePatterns = new();

    // コンストラクタで TextAsset を受け取る
    public LoadMorsePattaernData(TextAsset csvData)
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