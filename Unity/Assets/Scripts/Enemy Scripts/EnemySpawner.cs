using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject skeletonPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private WaveManager waveManager;

    private int enemiesAlive = 0; 

    public void StartWave(int enemyCount, int waveNumber)
    {
        enemiesAlive = enemyCount;
        Debug.Log($"[EnemySpawner] Spawning {enemyCount} enemies for Wave {waveNumber}.");
        StartCoroutine(SpawnWave(enemyCount, waveNumber));
    }

    private IEnumerator SpawnWave(int enemyCount, int waveNumber)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(1f);
            SpawnEnemy(waveNumber);
        }
    }

    private void SpawnEnemy(int waveNumber)
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("[EnemySpawner] No spawn points assigned!");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(skeletonPrefab, spawnPoint.position, spawnPoint.rotation);

        EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.ScaleStats(waveNumber);
        }
    }

    public void EnemyDefeated()
    {
        waveManager.EnemyDefeated(); // Notify WaveManager
    }
}
