using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoint;
    public float MinSpawnTime = 1.0f;       // �� ������ �ɸ��� �ּ� �ð�
    public float MaxSpawnTime = 7.0f;       // �� ������ �ɸ��� �ִ� �ð�

    private float spawnTime;
    private float timeAfterSpawn;           // ���� ���� �ð�

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

        if (timeAfterSpawn >= spawnTime)
        {
            int spawnPos = Random.Range(0, spawnPoint.Length);
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint[spawnPos].position, spawnPoint[spawnPos].rotation);
            timeAfterSpawn = 0f;

            enemy.GetComponent<Enemy>().patrolRoute = transform.GetChild(0);
        }

        spawnTime = Random.Range(MinSpawnTime, MaxSpawnTime);
    }
}
