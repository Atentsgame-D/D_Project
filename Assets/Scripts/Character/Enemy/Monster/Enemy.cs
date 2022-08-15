using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEditor;

public class Enemy : MonoBehaviour
{
    EnemyState enemyState = EnemyState.Idle;

    public Transform patrolRoute = null;
    public enum Type { A, B};
    public Type enemyType;

    //스탯 관련
    public Slider hpSlider;
    public int maxHealth;
    public int curHealth;
    //public Transform meleeArea;

    private float timeCountDown = 3.0f;
    private float waitTime = 3.0f;

    private bool isChase;
    //private bool isAttack;
    private bool isDead = false;

    private int wayPointCount = 0;
    private int index = 0;
    Vector3 targetPosition = new();
    IEnumerator repeatChase;
    WaitForSeconds oneSecond = new(0.5f);

    // 시야 범위
    float closeSightRange = 2.5f;
    float sightAngle = 120.0f;
    float sightRange = 10.0f;
    //IBattle attackTarget;

    // 공격 관련
    float attackCoolTime = 1.5f;
    float rushCoolTime = 2.0f;
    float attackSpeed = 1.5f;
    public float attackPower = 10.0f;
    public float defensePower = 10.0f;
    float smashRate = 0.4f;
    float rushRate = 0.9f;

    public float AttackPower { get => attackPower; }

    public float DefensePower { get => defensePower; }

    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    NavMeshAgent nav;
    Animator anim;
    Player player;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Start()
    {
        if (patrolRoute)
        {
            wayPointCount = patrolRoute.childCount;
        }
    }

    private void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                ChaseStart();
                break;

            case EnemyState.Attack:
                Targeting();

                break;

            case EnemyState.Dead:
                break;
        }

        hpSlider.value = (float)curHealth / (float)maxHealth;
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    void ChangeState(EnemyState newState)
    {
        if (isDead)
        {
            return;
        }

        // 이전 상태를 나가면서 해야할 행동
        switch (enemyState)
        {
            case EnemyState.Idle:
                nav.isStopped = true;

                break;

            case EnemyState.Patrol:
                nav.isStopped = true;
                break;

            case EnemyState.Chase:
                nav.isStopped = true; 
                StopCoroutine(repeatChase);

                break;

            case EnemyState.Attack:
                nav.isStopped = true;

                break;

            case EnemyState.Dead:
                isDead = false;

                break;
        }

        // 새 상태로 들어가면서 해야할 행동
        switch (newState)
        {
            case EnemyState.Idle:
                nav.isStopped = true;
                timeCountDown = waitTime;
                break;

            case EnemyState.Patrol:
                nav.isStopped = false;
                nav.SetDestination(patrolRoute.GetChild(index).position);
                break;

            case EnemyState.Chase:
                nav.isStopped = false;
                nav.SetDestination(targetPosition);
                repeatChase = RepeatChase();
                StartCoroutine(repeatChase);

                break;

            case EnemyState.Attack:
                nav.isStopped = true;
                attackCoolTime = attackSpeed;

                break;

            case EnemyState.Dead:

                break;

            default:
                break;

        }

        enemyState = newState;
        anim.SetInteger("EnemyState", (int)enemyState);
    }

    private void Idle()
    {
        if (SearchPlayer())
        {
            ChangeState(EnemyState.Chase);

            return;
        }

        timeCountDown -= Time.deltaTime;

        if (timeCountDown < 0)
        {
            ChangeState(EnemyState.Patrol);

            return;
        }
    }

    private void Patrol()
    {
        if (SearchPlayer())
        {
            ChangeState(EnemyState.Chase);

            return;
        }

        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            index++;
            index %= wayPointCount;

            ChangeState(EnemyState.Idle);

            return;
        }
    }

    bool SearchPlayer()
    {
        bool result = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange, LayerMask.GetMask("Player"));
        if (colliders.Length > 0)
        {
            Vector3 pos = colliders[0].transform.position;

            // 플레이어가 시야내에 있으면
            if (InSightAngle(pos))
            {
                // 블록에 막혀있지 않으면
                if (!BlockByWall(pos)) // 타겟 = 플레이어 설정
                {
                    targetPosition = pos;
                    result = true;
                }
            }

            // 플레이어가 전방 시야에 없고 근접한 일정 범위 내에 있을 때
            if (!result && (pos - transform.position).sqrMagnitude < closeSightRange * closeSightRange)
            {
                // 블록에 막혀있지 않으면
                if (!BlockByWall(pos)) // 타겟 = 플레이어 설정
                {
                    targetPosition = pos;
                    result = true;
                }
            }
        }

        return result;
    }

    IEnumerator RepeatChase()
    {
        while (true)
        {
            yield return oneSecond;
            nav.SetDestination(targetPosition);
        }
    }

    // 플레이어가 시야 내에 있는가
    bool InSightAngle(Vector3 targetPosition)
    {
        float angle = Vector3.Angle(transform.forward, targetPosition - transform.position);

        return (sightAngle * 0.5f) > angle;
    }

    // 벽에 막혀 있는가
    bool BlockByWall(Vector3 targetPosition)
    {
        bool result = true;
        Ray ray = new(transform.position, targetPosition - transform.position);
        ray.origin = ray.origin + Vector3.up * 0.5f;

        if (Physics.Raycast(ray, out RaycastHit hit, sightRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                result = false;
            }
        }

        return result;
    }

    void Targeting()
    {
        attackCoolTime -= Time.deltaTime;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), 0.1f);

        if (attackCoolTime < 0)
        {
            anim.SetTrigger("Attack");
            Attack();
            attackCoolTime = attackSpeed;
        }
    }

    public void Attack()
    {
        switch (enemyType)
        {
            case Type.A:

                if (player != null)
                {
                    float damage = AttackPower;

                    anim.SetTrigger("Attack");

                    if (Random.Range(0.0f, 1.0f) < smashRate)
                    {
                        anim.SetTrigger("SmashAttack");
                        damage *= 2.0f;
                    }

                    attackCoolTime = attackSpeed;
                    player.TakeDamage(damage);
                }

                break;

            case Type.B:

                if (player != null)
                {
                    float damage = AttackPower;

                    anim.SetTrigger("Attack");

                    if (Random.Range(0.0f, 1.0f) < smashRate)
                    {
                        anim.SetTrigger("SmashAttack");
                        damage *= 2.0f;
                    }

                    if (Random.Range(0.0f, 1.0f) < rushRate)
                    {
                        rigid.AddForce(transform.forward * 25, ForceMode.Impulse);

                        rushCoolTime -= Time.deltaTime;
                    }

                    attackCoolTime = attackSpeed;
                    player.TakeDamage(damage);
                }

                break;
        }
    }

    private void FreezeVelocity()    // 물리력이 NavAgent 이동을 방해하지 않도록 하기
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.Inst.MainPlayer.gameObject)
        {
            //attackTarget = other.GetComponent<IBattle>();
            ChangeState(EnemyState.Attack);

            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.Inst.MainPlayer.gameObject)
        {
            ChangeState(EnemyState.Chase);
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon")) // 무기에 쳐맞을때
        {
            curHealth -= (int)player.AttackPower; // 체력감소. 임시로 강제 형변환 하였음 이후에 단위 통일할 것 
            StartCoroutine(OnDamage());

            Debug.Log("Enemy : " + Mathf.Max(0, curHealth)); // 체력을 0밑으로 떨어지지 않게 함
        }
    }

    IEnumerator OnDamage()
    {
        mat.color = Color.yellow;
        anim.SetTrigger("Take Damage");

        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            // 임시 수정           
            isChase = false;
            Destroy(gameObject, 2);
            ChangeState(EnemyState.Dead);
            anim.SetTrigger("Die");
            anim.SetBool("IsDead", true); // 피격후 다른 모션으로 넘어가는것 방지
            
            // 공격에 의한 피격, 충돌 판정 삭제를 위해 콜라이더 비활성화
            gameObject.GetComponent<Collider>().enabled = false;     
            gameObject.GetComponent<SphereCollider>().enabled = false;
            mat.color = Color.black;

            // 원본 코드
            //isChase = false;        // 추적 중지
            //mat.color = Color.black;
            //gameObject.layer = 8;       // 죽었을때 시체끼리만 부딪히게 레이어 설정
            //nav.enabled = false;        // 사망 리액션을 위해 NavAgent 비활성
            //ChangeState(EnemyState.Dead);
            //anim.SetTrigger("Die");

            //Destroy(gameObject, 3);
        }
    }

    // 추격 시작
    void ChaseStart()
    {
        isChase = true;
        if(!SearchPlayer())
        {
            ChangeState(EnemyState.Patrol);
            return;
        }

    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, transform.up, sightRange);

        Handles.color = Color.green;
        if (enemyState == EnemyState.Chase || enemyState == EnemyState.Attack)
        {
            Handles.color = Color.red;
        }

        Handles.DrawWireDisc(transform.position, transform.up, closeSightRange);

        Vector3 forward = transform.forward * sightRange;

        Quaternion q1 = Quaternion.Euler(0.5f * sightAngle * transform.up);
        Quaternion q2 = Quaternion.Euler(-0.5f * sightAngle * transform.up);

        Handles.DrawLine(transform.position, transform.position + q1 * forward);
        Handles.DrawLine(transform.position, transform.position + q2 * forward);

        Handles.DrawWireArc(transform.position, transform.up, q2 * transform.forward, sightAngle, sightRange, 3.0f);

    }
}
