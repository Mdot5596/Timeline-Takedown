using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management

public class EndGameOnPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the player interacts with the object
        if (other.CompareTag("Player"))
        {
            // End the game (you can replace this with a "Game Over" scene)
            Debug.Log("Game Over! Player picked up the object.");
            EndGame();
        }
    }

    void EndGame()
    {
        // Here, you could:
        // 1. Load a "Game Over" scene:
        // SceneManager.LoadScene("GameOver");

        // OR

        // 2. Quit the game (works in a built game, not in the editor):
        Application.Quit();

        // For now, let's log it:
        Debug.Log("Quitting the game...");
    }
}
