using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationScoreText : MonoBehaviour
{
    [SerializeField] private bool autoIncrement = false;
    [SerializeField] private int incrementAmount = 1;
    [SerializeField] private Image[] digitImages;
    [SerializeField] private int score;
    [SerializeField] private float animationTime = 0.5f;
    [SerializeField] private float digitHeight = 50f; // 1つの数字の高さ
    [SerializeField] private Sprite numberSprite; // 数字のスプライトシート

    private int previousValue;
    private bool isAnimating;
    private int[] digits;

    private void Start()
    {
        DOTween.SetTweensCapacity(500, 50);
        SetupDigitImages();
        UpdateScoreDisplay(true);
    }

    private void Update()
    {
        if (autoIncrement)
        {
            AddScore(incrementAmount);
        }
    }

    private void SetupDigitImages()
    {
        foreach (var image in digitImages)
        {
            image.sprite = numberSprite;
            image.type = Image.Type.Tiled;
            image.rectTransform.sizeDelta = new Vector2(digitHeight, digitHeight * 10);
        }
    }

    public void AddScore(int additionalValue)
    {
        if (additionalValue == 0) return;

        if (isAnimating)
        {
            DOTween.Kill(this);
        }

        previousValue = score;
        score += additionalValue;
        PlayDigitsAnim();
    }

    private void PlayDigitsAnim()
    {
        isAnimating = true;
        digits = GetAllDigits(score);

        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < digitImages.Length; i++)
        {
            int fromDigit = GetAllDigits(previousValue)[i];
            int toDigit = digits[i];
            int difference = (toDigit - fromDigit + 10) % 10;

            sequence.Join(digitImages[i].rectTransform
                .DOAnchorPosY(-difference * digitHeight, animationTime)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => ResetDigitPosition(digitImages[i])));
        }

        sequence.OnComplete(() =>
        {
            isAnimating = false;
            UpdateScoreDisplay(false);
        });
    }

    private void ResetDigitPosition(Image digitImage)
    {
        Vector2 currentPos = digitImage.rectTransform.anchoredPosition;
        digitImage.rectTransform.anchoredPosition = new Vector2(currentPos.x, 0);
    }

    private void UpdateScoreDisplay(bool immediate)
    {
        digits = GetAllDigits(score);
        for (int i = 0; i < digitImages.Length; i++)
        {
            int digit = (i < digits.Length) ? digits[digitImages.Length - 1 - i] : 0;
            if (immediate)
            {
                digitImages[i].rectTransform.anchoredPosition = new Vector2(0, 0);
            }
            digitImages[i].rectTransform.localPosition = new Vector3(digitImages[i].rectTransform.localPosition.x, -digit * digitHeight, 0);
        }
    }

    private int[] GetAllDigits(int number)
    {
        int[] result = new int[6];
        for (int i = 0; i < 6; i++)
        {
            result[i] = number % 10;
            number /= 10;
        }
        return result;
    }
}