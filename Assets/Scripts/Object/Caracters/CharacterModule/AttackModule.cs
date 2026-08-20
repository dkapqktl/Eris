using TMPro;
using UnityEngine;

public class AttackModule : CharacterModule
{
    public sealed override System.Type RegistrationType => typeof(AttackModule);
    
    StatusModule isStatus;

    [SerializeField] LayerMask targetLayer;
    [SerializeField] float attackRange = 1.5f;


    [SerializeField] private float baseAD = 1f;
    private float AttackDamage => isStatus ? baseAD + (isStatus.Strength * 2) + (isStatus.Dexterity) : baseAD;
    public float ViewAttackDamage => AttackDamage;


    [SerializeField] private float baseAP = 1f;
    private float AvilityPower => isStatus ? baseAP + (isStatus.Intelligence * 2) : baseAP;
    public float ViewAvilityPower => AvilityPower;


    [SerializeField] private float baseAttackSpeed = 3f;
    private float AttackSpeed => isStatus ? baseAttackSpeed + (isStatus.Dexterity * 0.5f) : baseAttackSpeed;
    public float ViewAttackSpeed => AttackSpeed;

    public float finalAttackTime = 0f;


    [SerializeField] private float baseCriticalMultiple = 1.25f;
    private float CriticalMultiple => isStatus ? baseCriticalMultiple + (isStatus.Dexterity * 0.005f) : baseCriticalMultiple;
    public float ViewCriticalMultiple => CriticalMultiple;


    [SerializeField] private float baseCriticalChance = 0f;

    private float CriticalChance => isStatus ? baseCriticalChance + (isStatus.Dexterity * 0.5f) : baseCriticalChance;
    public float ViewCriticalChance => CriticalChance;


    [SerializeField] private float _adPenetration = 0f; // 관통력
    public float ADPenetration => _adPenetration;


    [SerializeField] private float _apPenetration = 0f;
    public float APPenetration => _apPenetration;

    [SerializeField] private float _buff = 0f;
    public float Buff => _buff;

    public float attackAngle = 90f; // 부채꼴 공격
    public override void OnRegistration(CharacterBase newOwner)
    {
        base.OnRegistration(newOwner);
        if (newOwner)
        {
            isStatus = newOwner?.GetComponent<StatusModule>();
        }
    }

    public override void OnUnRegistration(CharacterBase oldOwner)
    {
        base.OnUnRegistration(oldOwner);
    }

    public float AD(float adAddBuff, float adMultiplierBuff)
    {
        float adDamage;

        adDamage = AttackDamage + adAddBuff * adMultiplierBuff;

        return adDamage;
    }

    // public float APDeal(buff)
    // {
    // 
    // }

    public bool CriticalRandom()
    {
        if (CriticalChance == 0) return false;
        int criticalvalue = Random.Range(1, 100);

        if (CriticalChance >= criticalvalue) return true;
        else return false;
    }

    public float CriticalDamage()
    {
        float criticalDeal;
        if (CriticalChance == 0f) return 1f;

        if (CriticalRandom()) criticalDeal = (AttackDamage + AvilityPower) * CriticalMultiple;
        else criticalDeal = (AttackDamage + AvilityPower);

        return criticalDeal;
    }

    public float FinalDamage()
    {
        float finalDamage;
        float Criticalsuccess;



        if (CriticalRandom()) Criticalsuccess = (AttackDamage + AvilityPower) * CriticalMultiple;
        else Criticalsuccess = (AttackDamage + AvilityPower);

        finalDamage = Criticalsuccess;

        return finalDamage;
    }

    public void OnDrawGizmos()
    {
        if (Owner is null) return;
        Vector2 attackDirection = (InputManager.CursorWorldPosition - Owner.transform.position).normalized; // 플레이어 위치에서 마우스 방향 계산
        Vector2 attackPosition = (Vector2)Owner.transform.position + attackDirection * attackRange; // 앞쪽 공격 지점 계산

        Gizmos.DrawWireSphere(Owner.transform.position, attackRange);
    }

    public void AngleAttack(Vector3 targetPosition)
    {
        Debug.Log("Attack 호출됨");

        // if (finalAttackTime + AttackSpeed > Time.time) return;

        Vector2 ownerPosition = Owner.transform.position; // 플레이어 위치

        Vector2 attackDirection = ((Vector2)targetPosition - ownerPosition).normalized; // 플레이어 위치에서 마우스 방향 계산

        Vector2 attackPosition = (Vector2)Owner.transform.position + attackDirection * attackRange; // 앞쪽 공격 지점 계산
    
        Collider2D[] targets = Physics2D.OverlapCircleAll(ownerPosition, attackRange, targetLayer); // 공격범위 안 적 탐색

        

        Debug.Log($"TargetLayer 값: {targetLayer.value}");

        Debug.Log($"공격 위치: {attackPosition}");
        Debug.Log($"찾은 타겟 수: {targets.Length}");

        // finalAttackTime = Time.time;

        foreach (Collider2D hit in targets)
        {
            Vector2 targetDirection =
                ((Vector2)hit.transform.position - ownerPosition).normalized;

            float angle =   Vector2.Angle(attackDirection, targetDirection);

            if (angle <= attackAngle / 2f)
            {
                if (hit.TryGetComponent(out HitPointModule hp))
                {
                    hp.TakeDamage(
                        Owner.gameObject,
                        Owner.Controller,
                        FinalDamage()
                    );

                    Debug.Log("부채꼴 공격!");
                }
            }
        }
    }

    // public float Defence()
    // {
    //     float def = (150f / (150f + Defence));
    //     return def;
    // }

}
