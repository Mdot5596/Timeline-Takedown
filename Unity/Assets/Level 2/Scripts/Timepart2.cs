using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class TimePart2 : MonoBehaviour, IInteractable 
{
    public void Interact()
    {
        
        EndGame();
    }

    void EndGame()
    {
        SceneManager.LoadSceneAsync(4);
        //Need to add a level select maybe or a cutscene or a loading screen

    }
}
