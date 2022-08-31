using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoint;
    public float MinSpawnTime = 1.0f;       // 적 생성에 걸리는 최소 시간
    public float MaxSpawnTime = 7.0f;       // 적 생성에 걸리는 최대 시간
    public int maxSpawn = 5;

    private float spawnRange = 5.0f;
    private float spawnTime;
    private float timeAfterSpawn;           // 게임 진행 시간
    int spawnCount = 0;

    GameObject enemy;

    private void Start()
    {
        timeAfterSpawn = 0;
        spawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);
    }

    private void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        timeAfterSpawn += Time.deltaTime;
        spawnCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (timeAfterSpawn >= spawnTime)
        {
            if (spawnCount < maxSpawn)
            {
                int spawnPos = Random.Range(0, spawnPoint.Length);
                Vector2 randomSpawnRange = Random.insideUnitCircle * spawnRange;
                enemy = Instantiate(enemyPrefab, spawnPoint[spawnPos].position, spawnPoint[spawnPos].rotation);

                enemy.GetComponent<Enemy>().patrolRoute = GameObject.Find("PatrolRoute").GetComponent<Transform>();
                timeAfterSpawn = 0f;
            }
        }

        spawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);
    }
}
