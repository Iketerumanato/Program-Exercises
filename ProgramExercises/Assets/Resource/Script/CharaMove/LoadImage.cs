using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadImage : MonoBehaviour
{
    [SerializeField] AssetReference assetReference;
    SpriteRenderer PlayerRenderer;

    private async void Start()
    {
        PlayerRenderer = GetComponent<SpriteRenderer>();

        // アセット読み込み
        var spriteHandle = Addressables.LoadAssetAsync<Sprite>(assetReference);
        var sprite = await spriteHandle.Task;
        PlayerRenderer.sprite = sprite;
    }

    private void OnDestroy()
    {
        //アセットの解放
        Addressables.Release(PlayerRenderer.sprite);
    }
}