using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimerObject: MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float pulseScale;
    private Action onClick;
    private SpriteRenderer spriteRenderer;

    Sequence sequence;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOPunchScale(new Vector3(pulseScale, pulseScale, 0f), 0.1f, 10, 0)).SetAutoKill(false);
    }

    public void SetCallback(Action onClick)
    {
        this.onClick = onClick;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }

    void OnDestroy()
    {
        sequence.Kill();
    }

    public void Pulse()
    {
        sequence.Restart();
    }
}