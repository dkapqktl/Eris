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

    Vector2 shiftedPosition;
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDragStart?.Invoke(this, eventData.position);
        shiftedPosition = Vector2.zero;
    }


    public void SetMouseStartPosition(Vector2 screenPosition)
    {
        currentScreenPosition = screenPosition;
    }
    public void SetMouseCurrentPosition(Vector2 screenPosition)
    {
        Vector2 screenDelta = screenPosition - currentScreenPosition;
        currentScreenPosition = screenPosition;


        if (shiftedPosition.x * screenDelta.x > 0.0f)
        {
            float counter = Mathf.Min(Mathf.Abs(screenDelta.x), Mathf.Abs(shiftedPosition.x));
            counter *= Mathf.Sign(shiftedPosition.x);
            shiftedPosition.x = counter;
        }
        if (shiftedPosition.y * screenDelta.y > 0.0f)
        {
            float counter = Mathf.Min(Mathf.Abs(screenDelta.y), Mathf.Abs(shiftedPosition.y));
            counter *= Mathf.Sign(shiftedPosition.y);
            shiftedPosition.y = counter;
        }

        if (screenDelta.sqrMagnitude == 0.0f) return;


        Rect rootRect = rootTransform.rect;

        //                                  원래위치               +  이동량(스크린위치)
        rootRect.position += (Vector2)(rootTransform.localPosition / UIManager.UIScale) + screenDelta;

        Vector2 overScreen = rootRect.InversedAABB(UIManager.UIBoundary);
        shiftedPosition += overScreen;
        screenDelta += rootRect.InversedAABB(UIManager.UIBoundary);
        // 일정영역을 나간만큼 다시 돌아와라

        Vector3 positionDelta = (Vector3)screenDelta;

        if (UIManager.UIScale > 0.0f) positionDelta /= UIManager.UIScale;

        rootTransform.localPosition += positionDelta;
    }
}
