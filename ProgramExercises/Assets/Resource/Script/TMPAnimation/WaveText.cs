using TMPro;
using UnityEngine;

public class WaveText : MonoBehaviour
{
    TextMeshProUGUI TestText;

    [SerializeField, Range(1f, 10f)]
    private float waveHeight = 5f;

    [SerializeField, Range(1f, 5f)]
    private float waveSpeed = 2f;

    readonly int maxCharacterIndex = 4;

    private void Start()
    {
        TestText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
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
}