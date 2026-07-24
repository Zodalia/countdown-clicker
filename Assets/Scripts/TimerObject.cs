using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimerObject: MonoBehaviour, IPointerClickHandler
{
    private Action onClick;

    public void SetCallback(Action onClick)
    {
        this.onClick = onClick;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }
}