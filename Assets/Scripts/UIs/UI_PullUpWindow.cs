using UnityEngine;
using UnityEngine.EventSystems;

public class UI_PullUpWindow : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling(); // 클릭시 맨앞으로 나오게 하기(최상단)
    }
}
