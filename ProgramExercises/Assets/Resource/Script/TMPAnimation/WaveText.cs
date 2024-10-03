using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class WaveText : MonoBehaviour
{
    TMP_Text TestText;

    [SerializeField, Range(1f, 10f)]
    private float waveHeight = 5f;

    [SerializeField, Range(1f, 5f)]
    private float waveSpeed = 2f;

    readonly int maxCharacterIndex = 4;

    private CancellationTokenSource cts;

    private async void Start()
    {
        TestText = GetComponent<TMP_Text>();
        cts = new CancellationTokenSource();

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
        while(!cancellationToken.IsCancellationRequested)
        {
            UpdateMeshVertices_Wave();
            await Task.Yield();
        }

    }

    void UpdateMeshVertices_Wave()
    {
        TestText.ForceMeshUpdate();
        var textInfo = TestText.textInfo;

        for (int characterNum = 0; characterNum < textInfo.characterCount; characterNum++)
        {
            var charInfo = textInfo.characterInfo[characterNum];

            if (!charInfo.isVisible)
                continue;

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int charactorIndexNum = 0; charactorIndexNum < maxCharacterIndex; charactorIndexNum++)
            {
                var orig = verts[charInfo.vertexIndex + charactorIndexNum];
                verts[charInfo.vertexIndex + charactorIndexNum] = orig + new Vector3(0f, Mathf.Sin(Time.time * waveSpeed + orig.x * 0.01f) * waveHeight, 0f);
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