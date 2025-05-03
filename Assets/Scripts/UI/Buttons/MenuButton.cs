using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour
{
    private AudioSource audio; // Moved inside the class

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void OnMouseClick()
    {
        Debug.Log("Pressed");
        audio.Play();
        StartCoroutine(HandleButtonClick());
    }

    private IEnumerator HandleButtonClick()
    {
        GameInfo.GameMode = 0;
        return null;
    }
}