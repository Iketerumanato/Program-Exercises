using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class ChangeScaleText : MonoBehaviour
{
    TMP_Text TestText;
    [SerializeField, Range(1f, 3f)] float scaleDuration = 1.5f;
    [SerializeField, Range(0.1f, 1f)] float scaleAmplitude = 0.5f;
    private Vector3 initialScale;
    readonly private float defaultScale = 1f;

    private CancellationTokenSource cts;

    private async void Start()
    {
        TestText = GetComponent<TMP_Text>();
        cts = new CancellationTokenSource();
        initialScale = TestText.transform.localScale;

        try
        {
            await AnimateCharactersAsync(cts.Token);
        }
        catch (TaskCanceledException)
        {
            Debug.LogWarning("Animation task was canceled.");
        }
    }

    private async Task AnimateCharactersAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            ScalingText();
            await Task.Yield();
        }
    }

    void ScalingText()
    {
        float scaleFactor = defaultScale + Mathf.Sin(Time.time * scaleDuration) * scaleAmplitude;
        TestText.transform.localScale = initialScale * scaleFactor;
    }

    private void OnDisable()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = null;
    }
}