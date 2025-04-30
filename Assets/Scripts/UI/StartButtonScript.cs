using UnityEngine;
using System.Collections;

public class StartButtonScript : MonoBehaviour
{
    private AudioSource audio; // Moved inside the class

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void OnMouseClick()
    {
        audio.Play();
        StartCoroutine(HandleButtonClick());
    }

    private IEnumerator HandleButtonClick()
    {
        GameInfo.GameMode = -1;
        return null;
    }
}