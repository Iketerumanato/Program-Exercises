using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] Button soundButton;
    [SerializeField] SoundPlayer soundPlayer;

    [Header("セットしたSoundSettingsのClipと同じ数に")]
    [SerializeField] int clipNum = 0;

    public void PlaySoundInclipIndex()
    {
        soundButton.onClick.AddListener(() => soundPlayer.PlaySound(clipNum));
        Debug.Log($"Play Sound is : " + soundPlayer.GetSoundName(clipNum));
    }
}