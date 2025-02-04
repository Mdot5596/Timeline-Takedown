using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private int startEnemies = 5;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyMultiplier = 1.2f;

    private int waveNumber = 0;
    private int enemiesRemaining = 0;
    private float countdownTimer;

    // UI References
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        StartCoroutine(StartWaveRoutine());
    }

    private IEnumerator StartWaveRoutine()
    {
        while (true)
        {
            // Countdown before wave starts
            countdownTimer = timeBetweenWaves;
            while (countdownTimer > 0)
            {
                countdownText.text = $"Next Wave: {countdownTimer:F1}s";
                yield return new WaitForSeconds(0.1f);
                countdownTimer -= 0.1f;
            }

            waveNumber++;
            int enemyCount = Mathf.RoundToInt(startEnemies * Mathf.Pow(difficultyMultiplier, waveNumber - 1));
            enemiesRemaining = enemyCount;

            // Update UI
            waveText.text = $"Wave: {waveNumber}";
            enemiesText.text = $"Enemies Left: {enemiesRemaining}";
            countdownText.text = "Wave Incoming!";

            enemySpawner.StartWave(enemyCount, waveNumber);
        }
    }

    public void EnemyDefeated()
    {
        enemiesRemaining--;
        enemiesText.text = $"Enemies Left: {enemiesRemaining}";

        if (enemiesRemaining <= 0)
        {
            StartCoroutine(StartWaveRoutine()); // Start the next wave
        }
    }
}
