using UnityEngine;

public class UIBase : MonoBehaviour
{
    // ISP => 인터페이스 분리원칙 // 그래서 UIs 폴더 만들때 인터페이스 폴더도 만듦

    // public 확장에는 열려있고
    // protected virtual 수정에는 닫혀있다

    public GameObject SetChild(GameObject newChild)
    {
        if (!newChild) return null;

        //새아가             너의부모는    나다.
        newChild.transform.SetParent(transform);

        return OnSetChild(newChild);
    }

    protected virtual GameObject OnSetChild(GameObject newChild)
    {
        return newChild;
    }

    public void UnSetChild(GameObject oldChild)
    {
        if (!oldChild) return;

        if (oldChild.transform.parent == transform)
        {
            oldChild.transform.SetParent(null);
        }


        OnSetChild(oldChild);
    }

    protected virtual void OnUnsetChild(GameObject oldChild)
    {
        
    }


}
