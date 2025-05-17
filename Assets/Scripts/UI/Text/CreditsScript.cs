using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private Canvas canvas;
    [SerializeField] private AudioClip creditsMusic;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioSource audio;
    

    private bool scrolling;
    private Vector3 initialPosition;

    
    

    private static CreditsScript instance;

    void Awake(){
        if(instance==null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    void Start(){

        scrollSpeed *= Screen.height / 100;
        canvas.enabled = false;
        scrolling = false;
        initialPosition = transform.position;
    }   


    void Update(){
        if(scrolling){
            transform.position += new Vector3(0, scrollSpeed*Time.deltaTime, 0);
        }
    }

    public static void ScrollCredits(){
        GameInfo.GameMode = -20;
        Debug.Log("Game Mode:" + GameInfo.GameMode);
        instance.canvas.enabled = true;
        instance.transform.position = instance.initialPosition;
        instance.scrolling = true;

        if(!instance.audio.isPlaying){
            instance.audio.loop = true;
            instance.audio.clip = instance.creditsMusic;
            instance.audio.volume = AvatarInfo.MusicVolume;
            instance.audio.Play();
        }

    }

    public static void StopScrolling(){
        GameInfo.GameMode = 0;
        Debug.Log("Game Mode:" + GameInfo.GameMode);
        instance.canvas.enabled = false;
        instance.scrolling = false;
        instance.transform.position = instance.initialPosition;

        instance.audio.Stop();


    }




}
