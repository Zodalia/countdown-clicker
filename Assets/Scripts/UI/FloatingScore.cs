using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class FloatingScore : MonoBehaviour
{
    [SerializeField] private float scale;
    [SerializeField] private float animTime;
    [SerializeField] private float yOffset;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var score = root.Q<Label>("Score");
        var time = root.Q<Label>("Time");

        var sequence = DOTween.Sequence();
        sequence.Append(score.DOMoveY(100, animTime, false));
        sequence.Join(time.DOScale(scale, animTime));

        sequence.OnComplete(() => {Destroy(gameObject);});
        sequence.Play();
    }
}