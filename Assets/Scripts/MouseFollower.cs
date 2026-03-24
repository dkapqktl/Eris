using UnityEngine;
using UnityEngine.UIElements;

public class MouseFollower : MonoBehaviour
{

    float zPosition = 0f;
    void Start()
    {
        InputManager.OnMouseMove += MoveToMouse; // 마우스를 따라가라
    }

    public void OnKeyZ()
    {
        zPosition -= 10f;
    }

    public void OnKeyC()
    {
        zPosition += 10f;
    }
    void MoveToMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        worldPosition.z += zPosition;
        transform.position = worldPosition;            
      
    }
        
}
