using System;
using UnityEngine;

public class MovementModule : CharacterModule, IRunnable
{

    protected Vector3? targetDirection = null;
    protected Vector3? targetDestination = null;
    protected float targetTolerance;
    public sealed override System.Type RegistrationType => typeof(MovementModule);

    public override void OnRegistration(CharacterBase newOwner)
    {
        base.OnRegistration(newOwner);
        GameManager.OnPhysicsCharacter -= MovementUpdate;
        GameManager.OnPhysicsCharacter += MovementUpdate;
    }

    public override void OnUnRegistration(CharacterBase oldOwner)
    {
        base.OnUnRegistration(oldOwner);
        GameManager.OnPhysicsCharacter -= MovementUpdate;
    }

    public void MovementUpdate(float deltaTime)
    {
        Vector3 originPosition = transform.position;
        PhysicsUpdate(deltaTime);
        Vector3 positionDelta = transform.position - originPosition;
        Owner.MovementNotify(positionDelta);
    }

    private void PhysicsUpdate(float deltaTime)
    {
        UpdateToDirection(deltaTime);
        UpdateToDestination(deltaTime);
    }

    public virtual float GetMoveSpeed() => 5.0f;

    public virtual float GetMoveSpeed(float deltaTime) => GetMoveSpeed() * deltaTime;

    public virtual void Translate(Vector3 delta)
    {
        transform.position += delta;
    }

    public void UpdateToDirection(float deltaTime)
    {
        if (targetDirection is null) return;
        float currentMoveSpeed = GetMoveSpeed(deltaTime);
        Translate(currentMoveSpeed * targetDirection.Value);
    }

    public void UpdateToDestination(float deltaTime)
    {
        if (targetDestination is null) return;

        // 해당위치로 조금씩 가는법 => (목적지 - 출발지)
        Vector3 currentMoveDirection = (targetDestination.Value - transform.position); // transform.position => 내위치를 알려주는 코드

        // 얼마나 더 가야하는지
        float distance = currentMoveDirection.magnitude;

        //    거리가         인정범위  밖일때
        if (distance > targetTolerance)
        {
            currentMoveDirection.Normalize(); // 방향 잡기


            float currentMoveSpeed = GetMoveSpeed(deltaTime);

            float resultMoveSpeed = Mathf.Min(currentMoveSpeed, distance);

            //                               방향      *    거리(시간 * 속력)
            transform.position += resultMoveSpeed * currentMoveDirection;
        }
    }

    public void MoveToDestination(Vector3 destination, float tolerance)
    {
        targetDirection = null; // 방향으로는 움직이지 않는다.
        targetDestination = destination;
        targetTolerance = tolerance;
    }

    public void MoveToDirection(Vector3 direction)
    {
        targetDestination = null; // 목적지를 제거한다
        targetDirection = direction.normalized;
    }

    public void StopMovement()
    {
        targetDirection = null;
        targetDestination = null;

    }

}
