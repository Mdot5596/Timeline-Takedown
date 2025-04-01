using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public string nextLevel; // Set this in the Inspector for each level

    private void OnTriggerEnter(Collider other) // Use OnTriggerEnter if it's a trigger-based pickup
    {
        if (other.CompareTag("Player")) // Ensure it's the player picking up the object
        {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        // Load the loading screen first
        SceneManager.LoadScene("LoadingScreen");

        // Wait a short time for effect (optional)
        yield return new WaitForSeconds(2f);

        // Load the actual level asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextLevel);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
