using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class DisplayScoreHitory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI historyPrefab;
    [SerializeField] private Transform historyContainer;// 履歴の表示場所
    [SerializeField] private int maxHistoryCount = 3;
    [SerializeField] private float historyDuration = 3f;
    [SerializeField] private float verticalSpacing = 30f;

    private List<TextMeshProUGUI> historyList = new List<TextMeshProUGUI>();

    // 履歴を追加するメソッド
    public void AddScoreHistory(int changeAmount)
    {
        // スコア変動のテキストを設定
        string HistoryText = changeAmount > 0 ? $"+{changeAmount}" : changeAmount.ToString();

        TextMeshProUGUI newHistory = Instantiate(historyPrefab, historyContainer);
        newHistory.text = HistoryText;
        newHistory.transform.SetAsFirstSibling(); // 最新の履歴が上に表示されるようにする

        // リストに新しい履歴を追加し、アニメーション処理を実行
        historyList.Insert(0, newHistory);
        RepositionHistoryTexts();

        // 最大履歴数を超えたら一番古い履歴を削除
        if (historyList.Count > maxHistoryCount)
        {
            TextMeshProUGUI oldestHistory = historyList[historyList.Count - 1];
            historyList.RemoveAt(historyList.Count - 1);
            if (oldestHistory != null)
            {
                Destroy(oldestHistory.gameObject);
            }
        }

        // 指定時間後に削除するコルーチンを開始
        StartCoroutine(RemoveHistoryAfterDelay(newHistory, historyDuration));
    }

    // 全ての履歴テキストを再配置するメソッド
    private void RepositionHistoryTexts()
    {
        for (int historyListIndex = 0; historyListIndex < historyList.Count; historyListIndex++)
        {
            if (historyList[historyListIndex] != null) // オブジェクトが存在するか確認
            {
                // DOTweenで座標を下にスライド
                historyList[historyListIndex].rectTransform.DOKill(); // 既存のアニメーションを中断
                historyList[historyListIndex].rectTransform.DOAnchorPosY(-historyListIndex * verticalSpacing, 0.3f);
            }
        }
    }

    // 指定した遅延後に履歴を削除するコルーチン
    private IEnumerator RemoveHistoryAfterDelay(TextMeshProUGUI historyText, float delay)
    {
        yield return new WaitForSeconds(delay);

        // 履歴が存在していれば削除
        if (historyList.Contains(historyText))
        {
            historyList.Remove(historyText);
            if (historyText != null)
            {
                Destroy(historyText.gameObject);
            }

            // 残りの履歴を再配置
            RepositionHistoryTexts();
        }
    }
}