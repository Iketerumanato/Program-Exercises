using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationScoreText : MonoBehaviour
{
    [SerializeField] private RectTransform[] digitImages;
    [SerializeField] private Texture numberTexture;
    [SerializeField] private int score;
    [SerializeField] private float animationTime = 0.5f;
    [SerializeField] private float digitHeight = 32f;
    [SerializeField] private float delayBetweenDigits = 0.1f;

    [Header("Auto Increment Settings")]
    [SerializeField] private bool enableAutoIncrement = true;
    [SerializeField] private int incrementAmount = 1;
    [SerializeField] private float updateInterval = 1.0f;

    private Vector2[] initialPositions;
    private int previousScore;
    private Sequence currentAnimation;
    private float updateTimer;

    const float maxScoreImageHeight = 352f;

    private void Start()
    {
        score = 0;
        initialPositions = new Vector2[digitImages.Length];
        for (int digitNum = 0; digitNum < digitImages.Length; digitNum++)
        {
            initialPositions[digitNum] = digitImages[digitNum].anchoredPosition;
        }
        UpdateScoreDisplay(score);

        updateTimer = updateInterval;
    }

    private void Update()
    {
        if (enableAutoIncrement)
        {
            updateTimer -= Time.deltaTime;
            if (updateTimer <= 0)
            {
                UpdateScore(score + incrementAmount);
                updateTimer = updateInterval;
                Debug.Log($"Current Score: {score}");
            }
        }
    }

    public void UpdateScore(int newScore)
    {
        // 現在のアニメーションがあれば停止し、再利用を防ぐ
        if (currentAnimation != null && currentAnimation.IsActive() && currentAnimation.IsPlaying())
        {
            currentAnimation.Kill();
        }

        previousScore = score;
        score = newScore;
        PlaySlotAnimation();
    }

    private void PlaySlotAnimation()
    {
        int[] newDigits = GetDigits(score);
        int[] oldDigits = GetDigits(previousScore);

        currentAnimation = DOTween.Sequence();

        for (int i = 0; i < digitImages.Length; i++)
        {
            int fromDigit = oldDigits[i];
            int toDigit = newDigits[i];

            if (fromDigit != toDigit)
            {
                RectTransform rectTransform = digitImages[i];
                float endY = initialPositions[i].y + toDigit * digitHeight;

                // 行がスムーズに0から9まで動き、9の次は0に戻る
                if (toDigit < fromDigit)
                {
                    // 9から0に戻る場合、一度上へ移動し、次の桁へ
                    currentAnimation.Append(rectTransform.DOAnchorPosY(endY + 10 * digitHeight, animationTime).SetEase(Ease.OutQuad));
                    currentAnimation.Append(rectTransform.DOAnchorPosY(endY, 0f));
                }
                else
                {
                    // 数字が進む場合、上へ移動
                    currentAnimation.Append(rectTransform.DOAnchorPosY(endY, animationTime).SetEase(Ease.OutQuad));
                }

                float delay = i * delayBetweenDigits;
                currentAnimation.Insert(delay, rectTransform.DOAnchorPosY(endY, animationTime).SetEase(Ease.OutQuad));
            }
        }
    }

    private int[] GetDigits(int number)
    {
        int[] result = new int[digitImages.Length];
        string numberStr = number.ToString().PadLeft(digitImages.Length, '0');

        for (int digitNum = 0; digitNum < digitImages.Length; digitNum++)
        {
            result[digitNum] = int.Parse(numberStr[numberStr.Length - 1 - digitNum].ToString());
        }

        return result;
    }

    private void UpdateScoreDisplay(int score)
    {
        int[] digits = GetDigits(score);

        for (int digitNun = 0; digitNun < digitImages.Length; digitNun++)
        {
            RawImage image = digitImages[digitNun].GetComponent<RawImage>();
            if (image != null)
            {
                int digit = digits[digitNun];
                image.uvRect = new Rect(0, 1 - (digit + 1) * (digitHeight / maxScoreImageHeight), 1, digitHeight / maxScoreImageHeight);
            }
        }
    }
}