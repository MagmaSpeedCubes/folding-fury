using UnityEngine;
using System.Collections;

public class Prologue : MonoBehaviour
{
    private AudioSource audio; 

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
        yield return new WaitForSeconds(0.5f);
        GameInfo.GameMode = -3;
    }
}