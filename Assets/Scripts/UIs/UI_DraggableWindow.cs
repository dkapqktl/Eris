using UnityEngine;
using UnityEngine.EventSystems;


public delegate void DragStartEvent(UI_DraggableWindow dragTarget, Vector2 startPosition);

public class UI_DraggableWindow : UIBase // IPointerDownHandler, IPointerMoveHandler
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

    public void OnPointDown(PointerEventData eventData)
    {
        OnDragStart?.Invoke(this, eventData.position);
        rootTransform.SetAsLastSibling();
    }

    public void OnPointUp(PointerEventData eventData)
    {
    }


    public void SetMouseStartPosition(Vector2 screenPosition)
    {
        Debug.Log($"{gameObject} : {screenPosition}");
    }
    public void SetMouseCurrentPosition(Vector2 screenPosition)
    {
        Debug.Log($"{gameObject} : {screenPosition}");
    }
}
