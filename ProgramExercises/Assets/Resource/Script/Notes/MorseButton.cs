using UnityEngine;
using UnityEngine.EventSystems;

public class MorseButton : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] InputMorse _inputMorseIns;

    // ボタンが押されたとき
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_inputMorseIns != null) _inputMorseIns.OnPointerDown();    
    }

    // ボタンが離されたとき
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_inputMorseIns != null) _inputMorseIns.OnPointerUp();
    }
}