using DG.Tweening;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public bool isCameraShake= false;
    public bool isScreenShake = false;

    [Header("Camera Shake Setting")]
    [SerializeField] Transform mainCameraTransform;
    [SerializeField] Vector3 cameraPositionStrength;
    [SerializeField] Vector3 cameraRotationStrength;
    private float shakecameraDuration = 0.3f;

    [Header("Screen Shake Setting")]
    [SerializeField] Material screenShakeMaterial;
    private float shakeIntensity = 0f;
    readonly string ShakeSetFloatStr = "_ShakeIntensity";
    public float shakeScreenDuration = 0.6f;
    public float shakeScreenIntensity = 0.6f;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        screenShakeMaterial.SetFloat(ShakeSetFloatStr, shakeIntensity > 0f ? shakeIntensity : 0f);
        Graphics.Blit(src, dest, screenShakeMaterial);
    }

    public void ScreenShaker(float intensity, float duration)
    {
        shakeIntensity = intensity;
        Invoke(nameof(StopShake), duration);
    }

    private void StopShake()
    {
        shakeIntensity = 0f;
    }

    public void CameraShaker()
    {
        mainCameraTransform.DOComplete();
        mainCameraTransform.DOShakePosition(shakecameraDuration, cameraPositionStrength);
        mainCameraTransform.DOShakeRotation(shakecameraDuration, cameraRotationStrength);
    }
}