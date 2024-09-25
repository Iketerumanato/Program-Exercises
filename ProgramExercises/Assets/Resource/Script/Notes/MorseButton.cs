using UnityEngine;
using UnityEngine.EventSystems;

public class MorseButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    public TapScreen _tapScreen;

    // ボタンが押されたとき
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_tapScreen != null) _tapScreen.OnPointerDown();
        
    }

    // ボタンが離されたとき
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_tapScreen != null) _tapScreen.OnPointerUp();
    }
}