using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class TimePartL3 : MonoBehaviour, IInteractable 
{
    public void Interact()
    {
        
        EndGame();
    }

    void EndGame()
    {
        SceneManager.LoadSceneAsync(4);

    }
}
