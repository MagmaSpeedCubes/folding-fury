using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerSound : MonoBehaviour
{
    private static PlayerSound instance;

    private AudioSource audio;
    public AudioClip inGame;
    public AudioClip bossFight;
    public AudioClip menu;
    public AudioClip prologue;
    public AudioClip collect;
    public AudioClip death;
    
    public string currentClip;
    public bool fading = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameInfo.GameMode > 0 && !GameInfo.BossFight)
        {
            if (!audio.isPlaying || currentClip != "InGame")
            {
                playLoopedTrack(inGame, AvatarInfo.MusicVolume);
                Debug.Log("Playing InGame");
                currentClip = "InGame";
            }
        }
        else if (GameInfo.BossFight)
        {
            if (!audio.isPlaying)
            {
                playLoopedTrack(bossFight, AvatarInfo.MusicVolume);
            }else if(currentClip != "BossFight"){
                StartCoroutine(fadeTrack(bossFight, 3f, AvatarInfo.MusicVolume));
                Debug.Log("Playing BossFight");
                currentClip = "BossFight";
            }
        }else if(GameInfo.GameMode == -3){

            if (!audio.isPlaying||currentClip != "Prologue")
            {
                playLoopedTrack(prologue, AvatarInfo.MusicVolume);
                currentClip = "Prologue";
            }
        }
    }
/*


*/
    public static void PlayCollect()
    {
        if (instance != null)
        {
            instance.audio.loop = false; // Ensure sound effects do not loop
            instance.audio.PlayOneShot(instance.collect, AvatarInfo.SFXVolume * 0.15f);
        }
    }

    private void playLoopedTrack(AudioClip clip, float volume){
        instance.audio.loop = true;
        audio.volume = volume;
        audio.clip = clip;
        audio.Play();

    }

    private IEnumerator fadeTrack(AudioClip newClip, float fadeTime, float newVolume)
    {
        if (!fading)
        {
            fading = true;
            // Debounce purposes
            float startVolume = newVolume;

            for (float t = 0; t < fadeTime; t += Time.deltaTime)
            {
                audio.volume = Mathf.Lerp(startVolume, 0, t / fadeTime);
                yield return null;
            }
            audio.Stop();
            audio.clip = newClip;
            audio.loop = true; // Ensure music loops
            audio.Play();
            for (float t = 0; t < fadeTime; t += Time.deltaTime)
            {
                audio.volume = Mathf.Lerp(0, startVolume, t / fadeTime);
                yield return null;
            }
            fading = false;
        }
    }
}