using UnityEngine;

public class MovableCharacter : CharacterBase, IRunnable, IFunctionable
{
    protected Vector3 targetDestination;
    protected float targetTolerance;


    void Start()
    {
        RegistrationFunctions();
    }

    public void RegistrationFunctions()
    {
        GameManager.OnPhysicsCharacter -= PhysicsUpdate;
        GameManager.OnPhysicsCharacter += PhysicsUpdate;
    }


    public void UnRegistrationFunctions()
    {
        GameManager.OnPhysicsCharacter -= PhysicsUpdate;

    }
    private void PhysicsUpdate(float deltaTime)
    {
        // 해당위치로 조금씩 가는법 => (목적지 - 출발지)
        Vector3 currentMoveDirection = (targetDestination - transform.position); // transform.position => 내위치를 알려주는 코드
        
        // 얼마나 더 가야하는지
        float distance = currentMoveDirection.magnitude;
        
        //    거리가         인정범위  밖일때
        if (distance > targetTolerance)
        {
            currentMoveDirection.Normalize(); // 방향 잡기

            //                               방향      *    거리(시간 * 속력)
            transform.position += deltaTime * 5.0f * currentMoveDirection;
        }
    }


    public void MoveToDestination(Vector3 destination, float tolerance)
    {
       targetDestination = destination;
       targetTolerance = tolerance;
    }

    public void MoveToDirection(Vector3 direction)
    {
       
    }
    public void StopMovement()
    {
      
    }

}
