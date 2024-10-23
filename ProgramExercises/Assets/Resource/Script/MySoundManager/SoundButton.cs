using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] Button soundButton;
    [SerializeField] SoundPlayer soundPlayer;

    [Header("セットしたSoundSettingsのClipと同じ数に")]
    [SerializeField] int clipIndex = 0;

    public void PlaySoundInclipIndex()
    {
        soundButton.onClick.AddListener(() => soundPlayer.PlaySound(clipIndex));
        Debug.Log($"Play Sound is : " + soundPlayer.GetSoundName(clipIndex));
    }
}