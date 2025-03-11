
using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private AudioSource audioSource; // Audio Source Component
    [SerializeField] private AudioClip finalWaveCompleteSound; // Victory Sound

    private int waveNumber = 1;
    private int totalKills = 0;
    private int enemiesRemaining = 0;
    private bool isBossWave = false;
    private bool bossSpawned = false;  //T his prevents multiple boss spawns

    // Max Wave (Final Level)
    private int finalWave = 5;

    // Manually define the number of enemies per wave
    private int[] enemiesPerWave = { 1, 2, 3, 4, 5 }; // Wave 10 is boss only

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
        // Reset boss spawn flag for each wave
        bossSpawned = false;

        // Determine if it's a boss wave (Wave 5 and the final wave)
        isBossWave = (waveNumber == 5 || waveNumber == finalWave);

        // Get the number of enemies to spawn for the current wave
        int enemyCount = GetEnemyCountForWave(waveNumber);
        enemiesRemaining = enemyCount + (isBossWave ? 1 : 0); // Adds 1 more enemy on boss wave (for the boss..)

        Debug.Log($"[WaveManager] Starting Wave {waveNumber}. Spawning {enemyCount} enemies. Boss: {isBossWave}");

        // Update UI
        waveText.text = isBossWave ? $"Wave {waveNumber} - BOSS FIGHT!" : $"Wave: {waveNumber}";
        enemiesText.text = $"Enemies Left: {enemiesRemaining}";
        killsText.text = $"Total Kills: {totalKills}";

        // Start spawning enemies
        enemySpawner.StartWave(enemyCount, waveNumber);

        // Spawn the boss (only for Wave 5 and 10)
        if (isBossWave && !bossSpawned)
        {
            enemySpawner.SpawnBoss(waveNumber);
            bossSpawned = true; // Mark boss as spawned
            Debug.Log($"[WaveManager] BOSS SPAWNED for Wave {waveNumber}!");
        }
    }

    private int GetEnemyCountForWave(int wave)
    {
        // Return the predefined number of enemies for the given wave, or a default number if out of range
        if (wave - 1 < enemiesPerWave.Length)
        {
            return enemiesPerWave[wave - 1];
        }
        return 50; // Default value if debugging higher waves
    }

    public void EnemyDefeated()
{
    // Make sure we only process if there are enemies remaining
    if (enemiesRemaining > 0) 
    {
        Debug.Log($"[WaveManager] Enemy Defeated! Enemies Remaining: {enemiesRemaining}");

        // Increase total kills only once per valid enemy defeat
        totalKills++; 
        
        // Decrease enemies remaining after a valid kill
        enemiesRemaining--; 

        // Update UI with the new counts
        enemiesText.text = $"Enemies Left: {enemiesRemaining}";
        killsText.text = $"Total Kills: {totalKills}"; // Update kills text immediately after a kill
    }
    else
    {
        Debug.LogWarning("[WaveManager] Attempted to call EnemyDefeated but no enemies are remaining.");
    }

    // Only move to the next wave if all enemies are defeated
    if (enemiesRemaining == 0)
    {
        Debug.Log($"[WaveManager] Wave {waveNumber} complete! Moving to next wave.");
        NextWave();
    }
}


private void NextWave()
{
    // Ensure we only move to the next wave if all enemies are defeated
    if (enemiesRemaining > 0) return;

if (waveNumber >= finalWave)
{
    Debug.Log("[WaveManager] Final wave completed! No more waves.");

    // Play the final wave complete sound
    if (audioSource != null && finalWaveCompleteSound != null)
    {
        audioSource.PlayOneShot(finalWaveCompleteSound);
    }

    return; // Prevent any further waves from being triggered
}


    waveNumber++; // Move to the next wave
    isBossWave = (waveNumber == finalWave); // Check if it's the final boss wave

    int enemyCount = GetEnemyCountForWave(waveNumber);
    enemiesRemaining = enemyCount + (isBossWave ? 1 : 0);

    Debug.Log($"[WaveManager] Advancing to Wave {waveNumber}. Enemies Remaining: {enemiesRemaining}");

    StartCoroutine(WaitForNextWave());
}


    private IEnumerator WaitForNextWave()
    {
        // Wait for 5 seconds before moving to the next wave (adjust this as needed)
        yield return new WaitForSeconds(5f); 

        // After the delay, start the new wave
        StartNewWave();
    }
}
