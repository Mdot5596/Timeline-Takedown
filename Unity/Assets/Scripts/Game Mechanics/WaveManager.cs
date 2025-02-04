using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private int startEnemies = 5;
    [SerializeField] private float difficultyMultiplier = 1.2f;

    private int waveNumber = 1;
    private int totalKills = 0;
    private int nextWaveThreshold = 10;
    private int enemiesRemaining = 0;

    // UI Elements
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private TextMeshProUGUI killsText;

    private void Start()
    {
        Debug.Log($"[WaveManager] Game Started - Beginning Wave {waveNumber}");
        StartNewWave();
    }

    private void StartNewWave()
    {
        int enemyCount = Mathf.RoundToInt(startEnemies * Mathf.Pow(difficultyMultiplier, waveNumber - 1));
        enemiesRemaining = enemyCount;

        Debug.Log($"[WaveManager] Starting Wave {waveNumber}. Enemies to spawn: {enemyCount}");
        
        // Update UI
        waveText.text = $"Wave: {waveNumber}";
        enemiesText.text = $"Enemies Left: {enemiesRemaining}";
        killsText.text = $"Total Kills: {totalKills}/{nextWaveThreshold}";

        // Spawn new wave
        enemySpawner.StartWave(enemyCount, waveNumber);
    }

    public void EnemyDefeated()
    {
        totalKills++;
        enemiesRemaining--;

        Debug.Log($"[WaveManager] Enemy Killed! Total Kills: {totalKills}, Enemies Remaining: {enemiesRemaining}");

        // Update UI
        enemiesText.text = $"Enemies Left: {enemiesRemaining}";
        killsText.text = $"Total Kills: {totalKills}/{nextWaveThreshold}";

        if (totalKills >= nextWaveThreshold)
        {
            Debug.Log($"[WaveManager] Kill threshold reached! Moving to next wave.");
            NextWave();
        }
    }

    private void NextWave()
    {
        waveNumber++;
        nextWaveThreshold += waveNumber * 10; // 10 → 30 → 60 → 100

        Debug.Log($"[WaveManager] Advancing to Wave {waveNumber}. New threshold: {nextWaveThreshold}");

        StartNewWave();
    }
}