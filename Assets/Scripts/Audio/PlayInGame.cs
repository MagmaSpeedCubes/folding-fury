using UnityEngine;

public class PlayInGame : MonoBehaviour
{
    private AudioSource audio;
    void Start(){
        audio = GetComponent<AudioSource>();
    }
    void Update(){
        if(GameInfo.GameMode>0 && !GameInfo.BossFight){
            if(!audio.isPlaying){
                audio.Play();
            }
        }
    }
}
