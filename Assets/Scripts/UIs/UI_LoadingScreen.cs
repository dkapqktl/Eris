using UnityEngine;

public class UI_LoadingScreen : UIBase, IOpenable
{
    // 프로퍼티를 만들때 원본이 되는 변수를 만들어줬었으나 get; set; 만 있는 경우 그냥 변수처럼 사용가능 // set만 protected 인 변수처럼 활용
    public bool IsOpen => gameObject.activeSelf;

    public void Open()   => gameObject.SetActive(true);
    

    public void Close()  => gameObject.SetActive(false);
    

    public void Toggle() => gameObject.SetActive(!IsOpen);
    

}
