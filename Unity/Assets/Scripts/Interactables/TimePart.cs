using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class TimePart : MonoBehaviour, IInteractable 
{
    public void Interact()
    {
        
        EndGame();
    }

    void EndGame()
    {
        SceneManager.LoadSceneAsync(2);
        //Need to add a level select maybe or a cutscene or a loading screen

    }
}
