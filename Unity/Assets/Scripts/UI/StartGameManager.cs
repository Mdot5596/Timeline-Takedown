using UnityEngine;

public class StartGameManager : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject introCanvas;
    public AudioSource narrationAudio;

    public void OnStartPressed()
    {
        // Hide start screen
        startCanvas.SetActive(false);

        // Show new canvas
        introCanvas.SetActive(true);

        // Play audio
        if (narrationAudio != null)
        {
            narrationAudio.Play();
        }
    }
}
