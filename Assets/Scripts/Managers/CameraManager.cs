using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : ManagerBase
{
    /* 인풋메니저에 있던거임
    Camera mainCamera = Camera.main;
    Physics2DRaycaster raycaster2D = mainCamera.GetComponent<Physics2DRaycaster>();
    PhysicsRaycaster raycaster3D = mainCamera.GetComponent<PhysicsRaycaster>();
        
    if (is2D)
    {
        PointerEventData data = new(EventSystem.current);
        List<RaycastResult> result = new();
        raycaster2D?.Raycast(data, result);
    } 아래 방법으로 만듦
    */

    public Camera MainCamera { get; private set; } // 남들이 볼수 있지만 나만 수정 가능
    public Physics2DRaycaster Raycaster2D { get; private set; }
    public PhysicsRaycaster   Raycaster3D { get; private set; }

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        yield return null;
    }

    protected override void OnDisconnected()
    {

    }

    public void SetMainCamera(Camera wantCamera)
    {
        MainCamera = wantCamera;
        if (MainCamera)
        {
            Raycaster2D = wantCamera.GetComponent<Physics2DRaycaster>();
            Raycaster3D = wantCamera.GetComponent<PhysicsRaycaster>();
        }
    }

    public void GetRaycastResult2D(Vector2 screenPosition, List<RaycastResult> outResult)
    {
        PointerEventData eventData = new(EventSystem.current);
        eventData.position = screenPosition;
        if(Raycaster2D) Raycaster2D.Raycast(eventData, outResult);
    }

    public void GetRaycastResult3D(Vector2 screenPosition, List<RaycastResult> outResult)
    {
        PointerEventData eventData = new(EventSystem.current);
        eventData.position = screenPosition;
        if (Raycaster3D) Raycaster3D.Raycast(eventData, outResult);
    }
}
