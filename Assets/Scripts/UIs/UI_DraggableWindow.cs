using UnityEngine;
using UnityEngine.EventSystems;


public delegate void DragStartEvent(UI_DraggableWindow dragTarget, Vector2 startPosition);

public class UI_DraggableWindow : UIBase, IPointerDownHandler
{
    // bool isDragging = false;
    // 
    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     isDragging = true;
    // }
    // 
    // public void OnPointerMove(PointerEventData eventData)
    // {
    //     if (isDragging) transform.position += (Vector3)eventData.delta;
    // }
    public event DragStartEvent OnDragStart;

    [SerializeField] RectTransform rootTransform;

    Vector2 currentScreenPosition;
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDragStart?.Invoke(this, eventData.position);
        
    }


    public void SetMouseStartPosition(Vector2 screenPosition)
    {
        currentScreenPosition = screenPosition;
    }
    public void SetMouseCurrentPosition(Vector2 screenPosition)
    {
        Vector2 screenDelta = screenPosition - currentScreenPosition;
        
        Rect rootRect = rootTransform.rect;

        rootRect.position += screenDelta;

        screenDelta += rootRect.InversedAABB(UIManager.UIBoundary);
        // 일정영역을 나간만큼 다시 돌아와라

        Vector3 positionDelta = (Vector3)screenDelta;

        if (UIManager.UIScale > 0.0f) positionDelta /= UIManager.UIScale;

        rootTransform.localPosition += positionDelta;
        currentScreenPosition = screenPosition;
    }
}
