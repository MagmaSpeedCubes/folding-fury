using UnityEngine;
using System.Collections;

public class StartButton2Script : MonoBehaviour
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
        GameInfo.GameMode = GameInfo.SelectedLevel;
        PlayerInfo.Modifier = GameInfo.SelectedModifier;
        return null;
    }
}