using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine;

public class RatateCharaText : MonoBehaviour
{
    TMP_Text TestText;

    [SerializeField, Range(45f, 180f)]
    private float rotateSpeed = 180f;

    [SerializeField, Range(0.5f, 3f)]
    private float pauseDuration = 1f;

    readonly int maxCharacterIndex = 4;
    readonly float Circumference = 360f;
    readonly private List<CharacterRotationState> charStates = new();
    private CancellationTokenSource cts;

    private class CharacterRotationState
    {
        public float currentAngle = 0f;
        public bool isRotating = true;
    }

    private async void Start()
    {
        TestText = GetComponent<TMP_Text>();

        cts = new CancellationTokenSource();

        for (int charaNum = 0; charaNum < TestText.text.Length; charaNum++)
        {
            charStates.Add(new CharacterRotationState());
        }

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
            await RotateCharactersAsync(cancellationToken);
            await Task.Delay(Mathf.RoundToInt(pauseDuration * 1000), cancellationToken);
        }
    }

    private async Task RotateCharactersAsync(CancellationToken cancellationToken)
    {
        float elapsedTime = 0f;
        float rotationDuration = Circumference / rotateSpeed;

        while (elapsedTime < rotationDuration && !cancellationToken.IsCancellationRequested)
        {
            float deltaTime = Time.deltaTime;
            elapsedTime += deltaTime;

            for (int charastateNum = 0; charastateNum < charStates.Count; charastateNum++)
            {
                charStates[charastateNum].currentAngle += rotateSpeed * deltaTime;
                if (charStates[charastateNum].currentAngle > Circumference)
                {
                    charStates[charastateNum].currentAngle -= Circumference;
                }
            }

            UpdateMeshVertices_RotateChara();
            await Task.Yield();
        }
    }

    private void UpdateMeshVertices_RotateChara()
    {
        TestText.ForceMeshUpdate();
        var textInfo = TestText.textInfo;

        for (int charaNum = 0; charaNum < textInfo.characterCount; charaNum++)
        {
            var charInfo = textInfo.characterInfo[charaNum];

            if (!charInfo.isVisible)
                continue;

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            Vector3 center = (verts[charInfo.vertexIndex] + verts[charInfo.vertexIndex + 2]) / 2;

            Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, charStates[charaNum].currentAngle), Vector3.one);

            for (int charaindexNum = 0; charaindexNum < maxCharacterIndex; charaindexNum++)
            {
                var orig = verts[charInfo.vertexIndex + charaindexNum];
                var dir = orig - center;
                verts[charInfo.vertexIndex + charaindexNum] = center + rotationMatrix.MultiplyVector(dir);
            }
        }

        for (int meshNum = 0; meshNum < textInfo.meshInfo.Length; meshNum++)
        {
            var meshInfo = textInfo.meshInfo[meshNum];
            meshInfo.mesh.vertices = meshInfo.vertices;
            TestText.UpdateGeometry(meshInfo.mesh, meshNum);
        }
    }

    private void OnDisable()
    {
        cts?.Cancel();
        cts?.Dispose();
        cts = null;
    }
}