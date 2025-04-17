using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimedImage : MonoBehaviour
{
    public Image imageToShow; 
    public float displayTime = 5f; 

    void Start()
    {
        StartCoroutine(ShowImageTemporarily());
    }

    private IEnumerator ShowImageTemporarily()
    {
        imageToShow.gameObject.SetActive(true); 
        yield return new WaitForSeconds(displayTime); 
        imageToShow.gameObject.SetActive(false); 
    }
}
