using TMPro;
using UnityEngine;

public class ChangeScaleText : MonoBehaviour
{
    TMP_Text TestText;
    [SerializeField, Range(1f, 3f)] float scaleDuration = 1.5f;
    [SerializeField, Range(0.1f, 1f)] float scaleAmplitude = 0.5f;
    private Vector3 initialScale;
    readonly private float defaultScale = 1f;

    void Start()
    {
        TestText = GetComponent<TMP_Text>();
        initialScale = TestText.transform.localScale;
    }

    //テキストの拡大縮小
    private void Update()
    {
        ScalingText();
    }

    void ScalingText()
    {
        float scaleFactor = defaultScale + Mathf.Sin(Time.time * scaleDuration) * scaleAmplitude;
        TestText.transform.localScale = initialScale * scaleFactor;
    }
}