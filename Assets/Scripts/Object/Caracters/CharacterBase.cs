using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    // 
    ControllerBase _controller;
    public ControllerBase Controller => _controller;

    public virtual string DisplayName => "character";


    //확장은 가능한데 수정은 불가능한 원칙
    public virtual void OnPossessed(ControllerBase newController) { }
    //                 빙의되다, 소유되다
    public ControllerBase Possessed(ControllerBase form) // Possessed 이 함수는 form을 입력 받는다
    {
        // if(_controller) 컨트롤러가 이미 있었다면
        // Unpossessed(); 해지한다.
        if (Controller) Unpossessed();
        _controller = form; // 컨트롤러는 그 입력받은 프롬이다
        OnPossessed(Controller);
        return Controller; // 그리고 컨트롤러를 반환해라

    }




    //확장은 가능한데 수정은 불가능한 원칙
    public virtual void OnUnpossessed(ControllerBase oldController) { }
    //           혼이 나가다
    public void Unpossessed()
    {
        if (Controller) OnUnpossessed(Controller);
        _controller = null; // 컨트롤러는 없다
    }

    public bool Unpossessed(ControllerBase oldController)
    {
        if (Controller != oldController) return false; // 컨트롤러가 oldController가 아니라면 (즉 newController) 라면 거짓 반환
        Unpossessed(); // 그게 아니라면 언포제시드 함수 실행하고

        return true; //  참을 반환
    }

}