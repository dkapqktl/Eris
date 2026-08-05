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

    public void Attack(Vector3 targetPosition)
    {
        if (finalAttackTime + AttackSpeed > Time.time) return;

        Vector2 attackDirection = (targetPosition - Owner.transform.position).normalized; // 플레이어 위치에서 마우스 방향 계산

        Vector2 attackPosition = (Vector2)Owner.transform.position + attackDirection * attackRange; // 앞쪽 공격 지점 계산

        Collider2D[] targets = Physics2D.OverlapCircleAll(attackPosition, 0.7f, targetLayer); // 공격범위 안 적 탐색

        finalAttackTime = Time.time;



        foreach (Collider2D hit in targets) // 찾은 적에게 대미지 적용
        {
            if (!hit.CompareTag("Monster")) continue;

            if (hit.TryGetComponent(out HitPointModule hp))
            {
                hp.TakeDamage(Owner.gameObject, Owner.Controller, FinalDamage());
            }
        }
    }

    // public float Defence()
    // {
    //     float def = (150f / (150f + Defence));
    //     return def;
    // }

}
