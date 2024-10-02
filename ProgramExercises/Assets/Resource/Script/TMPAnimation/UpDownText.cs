using UnityEngine;
using TMPro;

public class UpDownText : MonoBehaviour
{
    TextMeshProUGUI TestText;

    [SerializeField,Range(5f,15f)]
    private float amplitude = 10f;

    [SerializeField, Range(1f, 5f)]
    private float frequency = 2f;

    readonly int maxCharacterIndex = 4;
    readonly int characterSpeed = 1;

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

            float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
            if (characterNum % 2 == characterSpeed) yOffset *= -characterSpeed;//奇数番目は上下、偶数番目は下上の動き

            for (int charactorIndexNum = 0; charactorIndexNum < maxCharacterIndex; charactorIndexNum++)
            {
                var orig = verts[charInfo.vertexIndex + charactorIndexNum];
                verts[charInfo.vertexIndex + charactorIndexNum] = orig + new Vector3(0f, yOffset, 0f);
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