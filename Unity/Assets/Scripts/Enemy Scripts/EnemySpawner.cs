using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Enemy prefabs will go here:
    [SerializeField] private GameObject skeletonPrefab; 

    //How often the enemys spawn in 
    [SerializeField] private float spawnInterval = 3.5f; 
    //Example ->  [SerializeField] private float spawnInterval = 3.5f; 

    //Round Number
   // [SerializeField] private float roundNumber = 1; 


    // Array of predefined spawn points
    [SerializeField] private Transform[] spawnPoints; 

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

//rnd logic
 //   private void roundNumber()
  //  {

 //   }
//
    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(skeletonPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
