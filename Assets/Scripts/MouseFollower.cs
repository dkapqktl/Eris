using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseFollower : MonoBehaviour, IFunctionable
{
    void Start() // 오브잭트(네모) 만들기
    {
        RegistrationFunctions();
    }

    void OnDestroy()  // 오브잭트(네모) 없애기
    {
        UnRegistrationFunctions();
    }

    public void RegistrationFunctions() // CreateToMouse => 생성하는데 // RegistrationFunctions => 생성을 했으면 등록도 해주자
    {
        InputManager.OnMouseMove += MoveToMouse; // 마우스를 커서를 따라가라
        // InputManager.OnMouseLeftUp += CreateToMouse; // 마우스 좌클릭시 커서위치를 따라가라
        // InputManager.OnMouseLeftButton += CreateToMouse; // 마우스 좌클릭시 커서위치를 따라가라
        // InputManager.OnMouseRightButton += DestroyOnMouse;
        // InputManager.OnMouseRightDown += DestroyOnMouse;
        InputManager.OnMouseLeftButton += CreateToMouse;
    }

    private void CreateToMouse(bool value, Vector2 screenPosition, Vector3 worldPosition)
    {
        GameObject inst = ObjectManager.CreateObject("NemoMan", worldPosition);
    }

    public void UnRegistrationFunctions()
    {
        // InputManager.OnMouseLeftButton += CreateToMouse;
        // InputManager.OnMouseRightButton += DestroyOnMouse;
    }

    // void DestroyOnMouse(Vector2 screenPosition, Vector3 worldPosition)
    // {
    //     ObjectManager.DestroyObject(GameManager.Instance.Input.GetGameObjectUnderCursor());
    // }

    void CreateToMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        // 로딩
        GameObject inst = ObjectManager.CreateObject("NemoMan", worldPosition);
    }

    /*
    void CreateToMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        // 로딩
        GameObject inst = ObjectManager.CreateObject(DataManager.LoadDataFile<GameObject>("Square 14"));
        inst.transform.position = worldPosition;
    }
    */

    void MoveToMouse(Vector2 screenPosition, Vector3 worldPosition)
    {
        transform.position = worldPosition;            
    }
        
}