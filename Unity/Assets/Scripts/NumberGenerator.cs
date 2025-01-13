using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class NumberGenerator : MonoBehaviour, IInteractable 
{
    public void Interact()
    {
        // Log a random number (keeping your original functionality)
        Debug.Log(Random.Range(0, 100));
        
        // End the game (load a game over scene or quit)
        EndGame();
    }

    void EndGame()
    {
        // Option 1: Load a "Game Over" scene
        // Ensure you have added the scene to the Build Settings
        SceneManager.LoadSceneAsync(3);

        // Option 2: Quit the application (works only in a built game)
      //  Application.Quit();

        // Log to confirm in the editor
        Debug.Log("Game Over! The application is quitting...");
    }
}
