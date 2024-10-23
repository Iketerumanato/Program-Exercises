using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] Button soundButton;
    [SerializeField] SoundPlayer soundPlayer;

    [Header("0to3")]
    [SerializeField] int clipIndex = 0;

    public void PlaySoundInclipIndex()
    {
        soundButton.onClick.AddListener(() => soundPlayer.PlaySound(clipIndex));
    }
}