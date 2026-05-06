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

    protected override IEnumerator OnConnected(GameManager newManager)
    {
        SetMainCamera(Camera.main);
        yield return null;
    }

    protected override void OnDisconnected()
    {

    }

    public void SetMainCamera(Camera wantCamera)
    {
        MainCamera = wantCamera;
    }


    public void GetRaycastResult(Vector2 screenPosition, List<RaycastResult> outResult)
    {
        EventSystem currentEvent = EventSystem.current;
        if (!currentEvent) return;

        PointerEventData eventData = new(currentEvent);
        eventData.position = screenPosition;

        currentEvent.RaycastAll(eventData, outResult);
    }
}
