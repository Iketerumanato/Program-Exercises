using UnityEngine;
using UnityEngine.EventSystems;

public class MorseButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public TapScreen _tapScreen;

    // �{�^���������ꂽ�Ƃ�
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_tapScreen != null) _tapScreen.OnPointerDown();
        
    }

    // �{�^���������ꂽ�Ƃ�
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_tapScreen != null) _tapScreen.OnPointerUp();
    }
}