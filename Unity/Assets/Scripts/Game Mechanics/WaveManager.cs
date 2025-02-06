using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    private int waveNumber = 1;
    private int totalKills = 0;
    private int nextWaveThreshold = 10;
    private int enemiesRemaining = 0;
    private bool isBossWave = false;

    // Max Wave (Final Level)
    private int finalWave = 10;

    // Predefined enemy counts per wave
     private int[] enemiesPerWave = { 10, 15, 25, 35, 50 };

    // UI Elements
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private TextMeshProUGUI killsText;

    private void Start()
    {
        StartNewWave();
    }
    
   private void StartNewWave()
   {
    isBossWave = (waveNumber == 5 || waveNumber == finalWave);
    
    int enemyCount = GetEnemyCountForWave(waveNumber); // Normal enemies
    enemiesRemaining = enemyCount + (isBossWave ? 1 : 0); // Include the boss if it's a boss wave


    // Update UI
    waveText.text = isBossWave ? $"Wave {waveNumber} - BOSS FIGHT!" : $"Wave: {waveNumber}";
    enemiesText.text = $"Enemies Left: {enemiesRemaining}";
    killsText.text = $"Total Kills: {totalKills}/{nextWaveThreshold}";

    // Spawn normal enemies
    enemySpawner.StartWave(enemyCount, waveNumber);

    // Spawn boss (only for Wave 5 and 10)
    if (isBossWave)
    {
        enemySpawner.SpawnBoss(waveNumber);
    }
}

    private int GetEnemyCountForWave(int wave)
    {
        if (wave - 1 < enemiesPerWave.Length)
        {
            return enemiesPerWave[wave - 1];
        }
        else
        {
            return enemiesPerWave[enemiesPerWave.Length - 1] + (wave - enemiesPerWave.Length) * 10;
        }
    }

    public void EnemyDefeated()
    {
        totalKills++;
        enemiesRemaining--;


        // Update UI
        enemiesText.text = $"Enemies Left: {enemiesRemaining}";
        killsText.text = $"Total Kills: {totalKills}/{nextWaveThreshold}";

        if (totalKills >= nextWaveThreshold && enemiesRemaining <= 0)
        {
            if (waveNumber < finalWave)
            {
                NextWave();
            }
            else
            {
                waveText.text = " Pick Up the timepice";
            }
        }
    }

    private void NextWave()
    {
        waveNumber++;
        int enemyCount = isBossWave ? 1 : GetEnemyCountForWave(waveNumber);
        nextWaveThreshold = totalKills + enemyCount;

        StartNewWave();
    }
}
