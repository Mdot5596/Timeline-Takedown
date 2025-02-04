using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    private int waveNumber = 1;
    private int totalKills = 0;
    private int nextWaveThreshold = 10; // First wave requires 10 kills
    private int enemiesRemaining = 0;

    // Predefined enemy counts per wave (Wave 1 = 10, Wave 2 = 15, Wave 3 = 25, etc.)
    private int[] enemiesPerWave = { 10, 15, 25, 35, 50 };

    // UI Elements
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private TextMeshProUGUI killsText;

    private void Start()
    {
        StartNewWave(); // Start the first wave
    }

    private void StartNewWave()
    {
    int enemyCount;
    
       // If wave number is within our predefined list, use it
    if (waveNumber - 1 < enemiesPerWave.Length)
    {
        enemyCount = enemiesPerWave[waveNumber - 1]; 
    }
    else
    {
        // If wave number is higher than predefined list, continue increasing by 10 each wave
        enemyCount = enemiesPerWave[enemiesPerWave.Length - 1] + (waveNumber - enemiesPerWave.Length) * 10;
    }

    if (enemyCount <= 0)
    {
        return; // Prevent the game from continuing with no enemies
    }

    enemiesRemaining = enemyCount;

    // Update UI
    waveText.text = $"Wave: {waveNumber}";
    enemiesText.text = $"Enemies Left: {enemiesRemaining}";
    killsText.text = $"Total Kills: {totalKills}/{nextWaveThreshold}";

    // Actually start the wave
    enemySpawner.StartWave(enemyCount, waveNumber);
    }

   // Function to correctly get the number of enemies per wave
   private int GetEnemyCountForWave(int wave)
   {
    if (wave - 1 < enemiesPerWave.Length)
    {
        return enemiesPerWave[wave - 1]; // Use predefined values for early waves
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

        // Check if it's time for the next wave
        if (totalKills >= nextWaveThreshold && enemiesRemaining <= 0)
        {
            NextWave();
        }
    }

  private void NextWave()
  {
    waveNumber++;

    // Set kill threshold to the total number of enemies spawned in this wave
    int enemyCount = GetEnemyCountForWave(waveNumber);
    nextWaveThreshold = totalKills + enemyCount; // Make sure threshold matches enemies spawned

    StartNewWave();
 }

}
