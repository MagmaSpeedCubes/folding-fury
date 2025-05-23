using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioSource audio;

    private bool isPlayingMenuMusic = false; // Track if the menu music is already playing

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameInfo.GameMode <= 0 && GameInfo.GameMode != -3 && GameInfo.GameMode != -20)
        {
            if (!isPlayingMenuMusic)
            {
                audio.loop = true;
                audio.clip = menuMusic;
                audio.volume = AvatarInfo.MusicVolume;

                if (menuMusic != null && AvatarInfo.MusicVolume > 0)
                {
                    audio.Play();
                    isPlayingMenuMusic = true; // Mark that the menu music is playing
                    Debug.Log("Playing Menu Music");
                }
                else
                {
                    Debug.LogError("Menu music clip is null or volume is zero!");
                }
            }
        }
        else
        {
            if (isPlayingMenuMusic)
            {
                audio.Stop();
                isPlayingMenuMusic = false; // Mark that the menu music is no longer playing
                Debug.Log("Stopped Menu Music");
            }
        }
    }
}