using UnityEngine;


// 이동할수 있는
public interface IRunnable
{ 
    //                       목적지         도착이라고 인정하는 거리
    public void MoveToDestination(Vector3 destination, float tolerance); // destination 뜻 : 목적지 // tolerance 뜻 : 인정,허용
    public void MoveToDirection(Vector3 direction);
    public void StopMovement();
}
