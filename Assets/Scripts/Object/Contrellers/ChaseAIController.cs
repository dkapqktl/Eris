using UnityEngine;

public class ChaseAIController : AIController
{
    protected override void OnPossess(CharacterBase newCharacter)
    {
        GameManager.OnUpdateController -= Think;
        GameManager.OnUpdateController += Think;
    }

    protected override void OnUnpossess(CharacterBase oldCharacter)
    {
        GameManager.OnUpdateController -= Think;
    }
    protected override void Think(float deltaTime)
    {
        if (!FocusTarget) return; // 대상없으면 안함
        CommandMoveToDestination(FocusTarget.transform.position, 1.0f); // 대상 위치로 이동
    }

}
