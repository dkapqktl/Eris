using Unity.Collections;
using UnityEngine;

public class PotionModule : MonoBehaviour
{
    [SerializeField] private float hpAmount;
    [SerializeField] private float mpAmount;

    [SerializeField] private float coolDown;
    [SerializeField] private float duration;

    [SerializeField] private bool isHoT;


    // float coolDownLeft;
    // 
    // public void UsePotion(HitPointModule potion)
    // {
    //     if (coolDownLeft > 0) return;
    //     if (IsHpPotion) potion.Heal(hpAmount);
    //     if (IsMpPotion) potion.
    //     coolDownLeft = coolDown;
    // }
    // 
    // private void Update()
    // {
    //     coolDownLeft = coolDown - Time.deltaTime;
    // }

}
