using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationScoreText : MonoBehaviour
{
    [SerializeField] private RectTransform[] digitImages;
    [SerializeField] private Texture numberTexture;
    [SerializeField] private float animationTime = 0.5f;
    [SerializeField] private float digitHeight = 32f;
    [SerializeField] private float delayBetweenDigits = 0.1f;
    [SerializeField] private int score;
    private int previousScore;
    private Sequence currentAnimation;
    private Vector2[] initialPositions;

    [SerializeField] DisplayScoreHitory displayScoreHitory;

    private void Start()
    {
        SetupDigitImages();
        StoreInitialPositions();
        previousScore = score;
        UpdateScoreDisplay();
    }

    private void Update()
    {
        // Inspectorでscoreを変更した時のみアニメーションを更新
        if (score != previousScore)
        {
            UpdateScoreDisplay();
            previousScore = score;
        }
    }

    private void SetupDigitImages()
    {
        foreach (var scoreimages in digitImages)
        {
            scoreimages.GetComponent<RawImage>().texture = numberTexture;
        }
    }

    private void StoreInitialPositions()
    {
        initialPositions = new Vector2[digitImages.Length];
        for (int digitsCnt = 0; digitsCnt < digitImages.Length; digitsCnt++)
        {
            initialPositions[digitsCnt] = digitImages[digitsCnt].anchoredPosition;
        }
    }

    private void UpdateScoreDisplay()
    {
        if (currentAnimation != null && currentAnimation.IsActive())
        {
            currentAnimation.Kill();
        }

        int scoreDifference = score - previousScore;
        if (scoreDifference != 0 && displayScoreHitory != null) displayScoreHitory.AddScoreHistory(scoreDifference);

        int[] digits = GetDigits(score);
        currentAnimation = DOTween.Sequence();

        for (int digitsCnt = 0; digitsCnt < digitImages.Length; digitsCnt++)
        {
            RectTransform rectTransform = digitImages[digitImages.Length - 1 - digitsCnt];
            int targetDigit = digits[digitsCnt];

            // 現在の位置を基準に、ターゲットの位置を計算
            Vector2 targetPosition = initialPositions[digitsCnt] + new Vector2(0, targetDigit * digitHeight);

            // 各桁のアニメーションを設定
            float delay = digitsCnt * delayBetweenDigits;
            currentAnimation.Insert(delay, rectTransform.DOAnchorPosY(targetPosition.y, animationTime).SetEase(Ease.OutQuad));
        }

        currentAnimation.Play();
    }

    private int[] GetDigits(int number)
    {
        int[] result = new int[digitImages.Length];
        string numberStr = number.ToString().PadLeft(digitImages.Length, '0');

        for (int digitsCnt = 0; digitsCnt < digitImages.Length; digitsCnt++)
        {
            result[digitsCnt] = int.Parse(numberStr[numberStr.Length - 1 - digitsCnt].ToString());
        }

        return result;
    }
}