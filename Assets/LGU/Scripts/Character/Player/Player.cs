using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class Player : MonoBehaviour, IHealth, IBattle, IMana
{
    GameObject weapon;
    GameObject shield;

    ParticleSystem ps;
    Animator anim;

    //IHealth--------------------------------------------------------------------------------------------------------------------------------------------
    public float hp = 100.0f;
    float maxHP = 100.0f;

    public float HP
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                onHealthChange?.Invoke();
            }
        }
    }

    public float MaxHP
    {
        get => maxHP;
    }

    public System.Action onHealthChange { get; set; }
    //IMana --------------------------------------------------------------------------------------------------------------------------------------------
    public float mp = 100.0f;
    float maxMP = 100.0f;

    public float MP
    {
        get => mp;
        set
        {
            if (mp != value)
            {
                mp = value;
                onManaChange?.Invoke();
            }
        }
    }

    public float MaxMP => maxMP;

    public System.Action onManaChange { get; set; }
    //IBattle--------------------------------------------------------------------------------------------------------------------------------------------
    float attackPower = 30.0f;
    float defencePower = 0.0f;
    float criticalRate = 0.3f;

    public float AttackPower { get => attackPower;}

    public float DefencePower { get => defencePower; }

    public void Attack(IBattle target)
    {
        if (target != null)
        {
            float damage = AttackPower;
            if (Random.Range(0.0f, 1.0f) < criticalRate)
            {
                damage *= 2.0f;
            }
            target.TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage - defencePower;
        if (finalDamage < 1.0f)
        {
            finalDamage = 1.0f;
        }
        HP -= finalDamage;

        if (HP > 0.0f)
        {
            //살아있다.
            anim.SetTrigger("Hit");
        }
        else
        {
            //죽었다.
            //if (isdead == false)
            //{
            //    die();
            //}
        }
    }

    // LockOn------------------------------------------------------------------------------------------------------------------------------------------
    public GameObject lockOnEffect;
    Transform lockOnTarget;
    float lockOnRange = 5.0f;
    float firstDir = 0.0f;

    public Transform LockOnTarget { get => lockOnTarget; }

    // ItemPickup----------------------------------------------------------------------------------------------------------------------------------------
    int money = 0;
    public int Money
    {
        get => money;
        set
        {
            if(money != value)
            {
                money = value;
                OnMoneyChange?.Invoke(money);
            }
            
        }
    }

    float itemPickupRange = 2.0f;
    float dropRange = 2.0f;
    public System.Action<int> OnMoneyChange;

    // 인벤토리용 -----------------------------------------------------------------------------------------------------------------------------------------
    Inventory inven;

    // --------------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        weapon = GetComponentInChildren<FindWeapon>().gameObject;
        shield = GetComponentInChildren<FindShield>().gameObject;
        anim = GetComponent<Animator>();
        ps = weapon.GetComponentInChildren<ParticleSystem>();
        inven = new Inventory();
    }

    private void Start()
    {
        if(lockOnEffect == null)
        {
            lockOnEffect = GameObject.Find("LockOnEffect");
        }
        GameManager.Inst.InvenUI.InitializeInventory(inven);
    }

    public void ShowWeapons(bool isShow)
    {
        weapon.SetActive(isShow);
        shield.SetActive(isShow);
    }

    public void TurnOnAura(bool turnOn)
    {
        if (turnOn)
        {
            ps.Play();
        }
        else
        {
            ps.Stop();
        }
    }

    public void LockOnToggle()
    {
        if(lockOnTarget == null)
        {
            LockOn();
        }
        else
        {
            if (!LockOn())
            {
                LockOff();
            }
        }
    }

    bool LockOn()
    {
        bool result = false;

        Collider[] cols = Physics.OverlapSphere(transform.position, lockOnRange, LayerMask.GetMask("Enemy"));
        if(cols.Length > 0)
        {
            Collider nearest = null;
            nearest = cols[0];
            firstDir = (cols[0].transform.position - transform.position).sqrMagnitude;
            foreach (var col in cols)
            {
                float Dir = (col.transform.position - transform.position).sqrMagnitude;
                if (Dir < firstDir)
                {
                    firstDir = Dir;
                    nearest = col;
                }
            }
            lockOnTarget = nearest.transform;
            lockOnEffect.transform.position = lockOnTarget.position;
            lockOnEffect.transform.parent = lockOnTarget;
            result = true;
        }
        return result;
    }

    void LockOff()
    {
        lockOnEffect.transform.position = transform.position;
        lockOnEffect.transform.parent = transform;
        lockOnTarget = null;
    }
    private void Update()
    {
        if(lockOnTarget != null)
        {
            if (LockOnTarget.gameObject.layer == LayerMask.NameToLayer("Die") || (lockOnTarget.transform.position - transform.position).magnitude > lockOnRange)
            {
                LockOff();
            }
        }
    }

    public void ItemPickup()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, itemPickupRange, LayerMask.GetMask("Item"));
        foreach(var col in cols)
        {
            Item item = col.GetComponent<Item>();

            // as : as 앞의 변수가 as 뒤의 타입으로 캐스팅이 되면 캐스팅 된 결과를 주고 안되면 null을 준다.
            // is : is 앞의 변수가 is 뒤의 타입으로 캐스팅이 되면 true, 아니면 false.
            IConsumable consumable = item.data as IConsumable;  

            if( consumable != null)
            {
                consumable.Consume(this);   // 먹자마자 소비하는 형태의 아이템은 각자의 효과에 맞게 사용됨
                Destroy(col.gameObject);
            }
            else
            {
                if(inven.AddItem(item.data))
                {
                    Destroy(col.gameObject);
                }
            }
        }
    }

    public Vector3 ItemDropPosition(Vector3 inputPos)
    {
        Vector3 result = Vector3.zero;
        Vector3 toInputPos = inputPos - transform.position;
        if( toInputPos.sqrMagnitude > dropRange * dropRange)
        {
            result = transform.position + toInputPos.normalized* dropRange;
        }
        else
        {
            result = inputPos;
        }
        return result;
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.up, lockOnRange);
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.up, itemPickupRange);
    }
}
