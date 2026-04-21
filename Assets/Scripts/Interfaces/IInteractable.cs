using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;

public enum InteractType
{
    None,
    Talk, Take, Trade, Move, DoorOpen, DoorClose,
    Lenth
}

public interface IInteractable // 상호작용가능한
{
    public bool IsInteractable(GameObject from); // 상호작용 가능한지
    public string GetInteractText(GameObject from); // 상호작용의 문구
    public InteractType GetInteractType(); // 상호작용의 타입은(종류) 무엇인지
    public void Interact(GameObject from); // Interact 뜻 : 상호작용 // GameObject form => 상호작용 가능한 대상
    public void StopInteract(GameObject from);
}
