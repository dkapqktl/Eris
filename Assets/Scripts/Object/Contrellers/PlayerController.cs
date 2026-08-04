using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : ControllerBase
{
    protected override void OnPossess(CharacterBase newCharacter)
    {
        base.OnPossess(newCharacter);
        InputManager.OnMouseRightButton -= AttackToMouse;
        InputManager.OnMouseRightButton += AttackToMouse;
        InputManager.OnMove -= MoveToDirection;
        InputManager.OnMove += MoveToDirection;

    }

    protected override void OnUnpossess(CharacterBase oldCharacter)
    {
        base.OnUnpossess(oldCharacter);
        InputManager.OnMouseRightButton -= AttackToMouse;
        InputManager.OnMove -= MoveToDirection;
    }

    private void AttackToMouse(bool value, Vector2 screenPosition, Vector3 worldPosition)
    {
        if (value) CommandAttackToDestination(worldPosition);
    }



    public void MoveToMousePosition(bool value, Vector2 screenPosition, Vector3 worldPosition)
    {
        if (value) CommandMoveToDestination(worldPosition, 0.0f);
    }


    public void MoveToDirection(Vector2 value)
    {
        CommandMoveToDirection(value);
    }

}
