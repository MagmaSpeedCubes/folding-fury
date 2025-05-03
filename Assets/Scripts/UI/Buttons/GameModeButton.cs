using UnityEngine;
using System.Collections;

public class GameModeButton : MonoBehaviour
{
    [SerializeField] private int gameMode;
    private AudioSource audio; // Moved inside the class

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void OnMouseClick()
    {
        Debug.Log("GameMode set to " + gameMode);
        audio.Play();
        StartCoroutine(HandleButtonClick());
    }

    private IEnumerator HandleButtonClick()
    {
        GameInfo.GameMode = gameMode;
        return null;
    }
}