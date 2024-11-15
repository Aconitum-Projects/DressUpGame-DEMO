using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private RectTransform rectTransform;

    public float hoverScale = 1.1f;
    public float clickScale = 1.2f;
    public float animationDuration = 0.2f;

    private Vector3 originalScale;

    void Start()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;

        button.onClick.AddListener(OnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        rectTransform.DOScale(hoverScale, animationDuration).SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.DOScale(originalScale, animationDuration).SetEase(Ease.OutQuad);
    }

    private void OnClick()
    {
        rectTransform.DOScale(clickScale, animationDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            rectTransform.DOScale(originalScale, animationDuration).SetEase(Ease.OutQuad);
        });
    }
}