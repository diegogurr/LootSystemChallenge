using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private Transform spawnPointA;
    [SerializeField]
    private Transform spawnPointB;
    [SerializeField]
    private int numberOfEnemies = 5;
    [SerializeField]
    private float spawnInterval = 2f;
    [SerializeField]
    private int poolSize = 10;

    private List<GameObject> enemyPool;
    private int spawnedEnemies = 0;

    void Start()
    {
        InitializePool();
        StartCoroutine(SpawnEnemies());
    }

    private void InitializePool()
    {
        GameObject gameObject = new GameObject("Enemy Pool");
        enemyPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.SetParent(gameObject.transform);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (spawnedEnemies < numberOfEnemies)
        {
            GameObject enemy = GetPooledEnemy();
            if (enemy != null)
            {
                Vector3 spawnPosition = GetRandomPositionOnLine(spawnPointA.position, spawnPointB.position);
                enemy.transform.position = spawnPosition;
                enemy.SetActive(true);
                spawnedEnemies++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject GetPooledEnemy()
    {
        foreach (var enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
        return null;
    }

    private Vector3 GetRandomPositionOnLine(Vector3 pointA, Vector3 pointB)
    {
        float t = Random.Range(0f, 1f);
        return Vector3.Lerp(pointA, pointB, t);
    }
}
