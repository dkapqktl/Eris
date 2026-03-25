using UnityEngine;
using UnityEngine.UIElements;

public class MouseFollower : MonoBehaviour
{
    void Start()
    {
        InputManager.OnMouseMove += MoveToMouse; // 마우스를 커서를 따라가라
        //  InputManager.OnMouseLeftUp += CreateToMouse; // 마우스 좌클릭시 커서위치를 따라가라
        InputManager.OnMouseLeftDown += CreateToMouse; // 마우스 좌클릭시 커서위치를 따라가라
        InputManager.OnMouseRightUp += DestroyOnMouse;
        // InputManager.OnMouseRightDown += DestroyOnMouse;
    }

    void DestroyOnMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        Debug.Log(GameManager.Instance.Input.GetGameObjectUnderCursor());
    }

    void CreateToMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        // 로딩
        Instantiate(DataManager.LoadDataFile<GameObject>("Square 14"), worldPosition, Quaternion.identity);
    }

    void MoveToMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        transform.position = worldPosition;            
    }
        
}
