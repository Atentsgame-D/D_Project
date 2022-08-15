using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class Enemy_Boss : MonoBehaviour,IHealth
{
    NavMeshAgent agent;
    Animator anim;

    Boss_EnemyState state = Boss_EnemyState.Idle;

    //Idle 용 --------------------------------------------------------------------------------------
    float waitTime = 3.0f;
    float timeCountDown = 3.0f;
    //추적용------------------------------------------------------------------------------------------
    float sightRange = 10.0f;
    float closeSightRange = 2.5f;
    Vector3 targetPosition = new();
    WaitForSeconds oneSecond = new WaitForSeconds(1.0f);
    IEnumerator repeatChase = null;
    float sightAngle = 150.0f;   //-45 ~ +45 범위
    //공격용 -----------------------------------------------------------------------------------------
    float attackCoolTime = 2.0f;
    float attackSpeed = 1.0f;
    Player player;
    //IBattle attackTarget;

    //사망용 -----------------------------------------------------------------------------------------
    bool isDead = false;

    //IHealth -------------------------------------------------------------------------------------
    public float hp = 100.0f;
    float maxHP = 100.0f;
    public float HP
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0.0f, maxHP);
            onHealthChange?.Invoke();
        }
    }

    public float MaxHP { get => maxHP; }

    public System.Action onHealthChange { get; set; }


    //IBattle -------------------------------------------------------------------------------------
    public float attackPower = 10.0f;
    public float defencePower = 10.0f;
    float criticalRate = 0.1f;

    public float AttackPower { get => attackPower; }

    public float DefencePower { get => defencePower; }

    //---------------------------------------------------------------------------------------------

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void Update()
    {
        Debug.Log($"공격 쿨타임: {attackCoolTime}");
        //if(patrolRoute!=null)
        //{
        //    agent.SetDestination(patrolRoute.position);  // 길찾기는 연산량이 많은 작업. SetDestination을 자주하면 안된다.
        //}        

        switch (state)
        {
            case Boss_EnemyState.Idle:
                IdleUpdate();
                break;
           case Boss_EnemyState.Chase:
                ChaseUpdate();
                break;
            case Boss_EnemyState.Attack:
                Targeting();
                break;
            case Boss_EnemyState.Dead:
            default:
                break;
        }
    }
    void IdleUpdate()
    {
        if (SearchPlayer())
        {
            ChangeState(Boss_EnemyState.Chase);
            return;
        }

       /* timeCountDown -= Time.deltaTime;
        if (timeCountDown < 0)
        {*/
           // ChangeState(Boss_EnemyState.Idle);
           // return;
        //}
    }

    bool SearchPlayer()
    {
        bool result = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange, LayerMask.GetMask("Player"));
        if (colliders.Length > 0)    // 시야 범위 안에 있는가?
        {
            Vector3 pos = colliders[0].transform.position;
            if (InSightAngle(pos))  // 시야 각도 안에 있는가?
            {
                if (!BlockByWall(pos))  // 벽에 가렸는가?
                {
                    targetPosition = pos;
                    result = true;
                }
            }
            if (!result && (pos - transform.position).sqrMagnitude < closeSightRange * closeSightRange)
            {
                targetPosition = pos;
                result = true;
            }
        }
        return result;
    }

    void ChaseUpdate()
    {
        if (!SearchPlayer())
        {
            ChangeState(Boss_EnemyState.Idle);
            return;
        }
    }
    IEnumerator RepeatChase()
    {
        while (true)
        {
            yield return oneSecond;
            agent.SetDestination(targetPosition);
        }
    }
    
    
    void Targeting()
    {
        attackCoolTime -= Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 0.1f);
        if (attackCoolTime < 0.0f)
        {
            anim.SetTrigger("Attack");
            Attack(player);
            attackCoolTime = attackSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.Inst.MainPlayer.gameObject)
        {
            //attackTarget = other.GetComponent<IBattle>();
            ChangeState(Boss_EnemyState.Attack);
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.Inst.MainPlayer.gameObject)
        {
            ChangeState(Boss_EnemyState.Idle);
            return;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon")) // 무기에 쳐맞을때
        {
            //curHealth -= (int)player.AttackPower; // 체력감소. 임시로 강제 형변환 하였음 이후에 단위 통일할 것 
            //StartCoroutine(OnDamage());
            Debug.Log("공격받음");
            //Debug.Log("Enemy : " + Mathf.Max(0, curHealth)); // 체력을 0밑으로 떨어지지 않게 함
        }
    }

    void ChangeState(Boss_EnemyState newState)
    {
        if (isDead)
        {
            return;
        }

        // 이전 상태를 나가면서 해야할 일들
        switch (state)
        {
            case Boss_EnemyState.Idle:
                agent.isStopped = true;
                break;
             case Boss_EnemyState.Chase:
                agent.isStopped = true;
                StopCoroutine(repeatChase);
                break;
            case Boss_EnemyState.Attack:
                agent.isStopped = true;
                //attackTarget = null;
                break;
            case Boss_EnemyState.Dead:
                agent.isStopped = true;
                isDead = false;
                break;
            default:
                break;
        }

        // 새 상태로 들어가면서 해야할 일들
        switch (newState)
        {
            case Boss_EnemyState.Idle:
                agent.isStopped = true;
                timeCountDown = waitTime;
                break;
           case Boss_EnemyState.Chase:
                agent.isStopped = false;
                agent.SetDestination(targetPosition);
                repeatChase = RepeatChase();
                StartCoroutine(repeatChase);
                break;
            case Boss_EnemyState.Attack:
                agent.isStopped = true;
                attackCoolTime = attackSpeed;
                break;
            case Boss_EnemyState.Dead:
                DiePresent();
                break;
            default:
                break;
        }

        state = newState;
        anim.SetInteger("EnemyState", (int)state);
    }
    void DiePresent()
    {
        
        anim.SetBool("Dead", true);
        anim.SetTrigger("Die");
        isDead = true;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        HP = 0;
    }

    IEnumerator DeadEffect()
    {

        Boss_HP_Bar hpBar = GetComponentInChildren<Boss_HP_Bar>();
        hpBar.gameObject.SetActive(false);

        yield return new WaitForSeconds(3.0f);
        Collider[] colliders = GetComponents<Collider>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }
        agent.enabled = false;
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.isKinematic = false;
        rigid.drag = 20.0f;
        Destroy(this.gameObject, 5.0f);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(transform.position, sightRange);
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.up, sightRange);


        Handles.color = Color.green;
        if (state == Boss_EnemyState.Chase || state == Boss_EnemyState.Attack)
        {
            Handles.color = Color.red;  // 추적이나 공격 중일 때만 빨간색
        }
        Handles.DrawWireDisc(transform.position, transform.up, closeSightRange); // 근접 시야 범위

        Vector3 forward = transform.forward * sightRange;
        Quaternion q1 = Quaternion.Euler(0.5f * sightAngle * transform.up);
        Quaternion q2 = Quaternion.Euler(-0.5f * sightAngle * transform.up);
        Handles.DrawLine(transform.position, transform.position + q1 * forward);    // 시야각 오른쪽 끝
        Handles.DrawLine(transform.position, transform.position + q2 * forward);    // 시야각 왼쪽 끝

        Handles.DrawWireArc(transform.position, transform.up, q2 * transform.forward, sightAngle, sightRange, 5.0f);// 전체 시야범위
    }
    bool InSightAngle(Vector3 targetPosition)
    {
        // 두 백터의 사이각
        float angle = Vector3.Angle(transform.forward, targetPosition - transform.position);
        // 몬스터의 시야범위 각도사이에 있는지 없는지
        return (sightAngle * 0.5f) > angle;
    }
    bool BlockByWall(Vector3 targetPosition)
    {
        bool result = true;
        Ray ray = new(transform.position, targetPosition - transform.position); 
        ray.origin += Vector3.up * 0.5f;    
        if (Physics.Raycast(ray, out RaycastHit hit, sightRange))
        {
            if (hit.collider.CompareTag("Player"))     
            {
                result = false; 
            }
        }

        return result;  
    }
    public void Attack(Player target)
    {
        if (target != null)
        {
            float damage = AttackPower;
            if (Random.Range(0.0f, 1.0f) < criticalRate)
            {
                damage *= 2.0f;
            }
            target.TakeDamage(damage);
            Debug.Log("적 공격");
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
            anim.SetTrigger("Hit");
            attackCoolTime = attackSpeed;
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead == false)
        {
            ChangeState(Boss_EnemyState.Dead);
        }
    }
}
