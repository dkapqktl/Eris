using UnityEngine;

public interface IOpenable
{
    // ISP => 인터페이스 분리원칙 // 그래서 UIs 폴더 만들때 인터페이스 폴더도 만듦
    // interface 에서만 아래 항목들이 오류가 안남
    public bool IsOpen { get; }

    public void Open();

    public void Close();

    public void Toggle();
}
