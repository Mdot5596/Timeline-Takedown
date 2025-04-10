using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManagerL2 : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip finalWaveCompleteSound;

    private int waveNumber = 1;
    private int totalKills = 0;
    private int enemiesRemaining = 0;
    private bool isBossWave = false;
    private bool bossSpawned = false;

    private int finalWave = 5;

    private int[] enemiesPerWave = { 4, 8, 12, 15, 2 };
    //
    // Use this one for easy checks
    // private int[] enemiesPerWave = { 1, 2, 3, 4, 5 };

    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private TextMeshProUGUI killsText;

    private void Start()
    {
        StartNewWave();
    }

    private void StartNewWave()
    {
        bossSpawned = false;
        isBossWave = (waveNumber == 5 || waveNumber == finalWave);

        int enemyCount = GetEnemyCountForWave(waveNumber);
        enemiesRemaining = enemyCount + (isBossWave ? 1 : 0);

        waveText.text = isBossWave ? $"Wave {waveNumber} - BOSS FIGHT!" : $"Wave: {waveNumber}";
        enemiesText.text = $"Enemies Left: {enemiesRemaining}";
        killsText.text = $"Total Kills: {totalKills}";

        enemySpawner.StartWave(enemyCount, waveNumber);

        if (isBossWave && !bossSpawned)
        {
            enemySpawner.SpawnBoss(waveNumber);
            bossSpawned = true;
        }
    }

    private int GetEnemyCountForWave(int wave)
    {
        if (wave - 1 < enemiesPerWave.Length)
        {
            return enemiesPerWave[wave - 1];
        }
        return 50;
    }

    public void EnemyDefeated()
    {
        if (enemiesRemaining > 0) 
        {
            totalKills++; 
            enemiesRemaining--; 

            enemiesText.text = $"Enemies Left: {enemiesRemaining}";
            killsText.text = $"Total Kills: {totalKills}";
        }

        if (enemiesRemaining == 0)
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        if (enemiesRemaining > 0) return;

        if (waveNumber >= finalWave)
        {
            if (audioSource != null && finalWaveCompleteSound != null)
            {
                audioSource.PlayOneShot(finalWaveCompleteSound);
            }

            return;
        }

        waveNumber++;
        isBossWave = (waveNumber == finalWave);
        int enemyCount = GetEnemyCountForWave(waveNumber);
        enemiesRemaining = enemyCount + (isBossWave ? 1 : 0);

        StartCoroutine(WaitForNextWave());
    }

    private IEnumerator WaitForNextWave()
    {
        yield return new WaitForSeconds(5f);
        StartNewWave();
    }
}
