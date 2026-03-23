using System.Collections;
using UnityEngine;

// class     : 변수 o / 함수 내용 o / 객체생성 o // 자동차로 따지면 : 벤츠(분류), S클래스(분류)
// abstract  : 변수 o / 함수 내용 △ / 객체생성 x // 자동차로 따지면 : 승용차
// interface : 변수 x / 함수 내용 x / 객체생성 x // 탑승 할 수 있는 기능 등
// instance  : 자동차로 따지면 : 123고1234 // class에서 S클래스를 줘 => 객체(instance)

public abstract class ManagerBase : MonoBehaviour
{
    GameManager _connetedManager;

    // 프로퍼티에도 virtual을 쓸 수 있다.
    public virtual int LoadCount => 1;
   

    // virtual 을 쓰려고 할땐 OCP(개방폐쇄원칙) 을 생각해야함 (확장은 가능, 수정은 불가)
    public IEnumerator Connect(GameManager newManager) // 연결
    {
        if (_connetedManager != null) Disconnect();

        _connetedManager = newManager;
        yield return OnConnected(newManager);
    }

    // virtual 대신 abstract : 부모에서 정의하지 않겠다, 자식이 알아서 만들어라
    protected abstract IEnumerator OnConnected(GameManager newManager); // 연결 했을때


    public void Disconnect() // 연결 해제
    {
        _connetedManager = null;
        OnDisconnected(); 
    }
    
    protected abstract void OnDisconnected(); // 연결 해제 되었을때


}
