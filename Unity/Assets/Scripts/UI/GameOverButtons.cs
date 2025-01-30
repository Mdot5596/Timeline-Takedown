using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    void Start()
    {
       //for some reason this was making it drag around??/ 
      //  Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false; 
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0); 
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit(); 

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }
}
