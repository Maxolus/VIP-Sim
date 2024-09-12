using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform parentRectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;

    private void Awake()
    {
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (parentRectTransform != null)
        {
            originalPosition = parentRectTransform.anchoredPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0.6f; // Make the handle semi-transparent
            canvasGroup.blocksRaycasts = false; // Allow the handle to be dragged
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (parentRectTransform != null)
        {
            parentRectTransform.anchoredPosition += eventData.delta; // Move the parent taskbar
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1.0f; // Reset transparency
            canvasGroup.blocksRaycasts = true; // Make the handle interactable again
        }
    }

    public void ResetPosition()
    {
        if (parentRectTransform != null)
        {
            parentRectTransform.anchoredPosition = originalPosition; // Reset to original position if needed
        }
    }
}
