using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onButtonDown = new UnityEvent();
    public UnityEvent onButonUp = new UnityEvent();

    //Eventos para o botões virtuais
    public void OnPointerDown(PointerEventData eventData)
    {
        onButtonDown.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onButonUp.Invoke();
    }
}
