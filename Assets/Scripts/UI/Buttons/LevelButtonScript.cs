using UnityEngine;
using TMPro;
public class LevelButtonScript : MonoBehaviour
{
    [SerializeField] private int level; // Level number assigned in the Inspector
    [SerializeField] private Sprite unselected;
    [SerializeField] private Sprite selected;
    [SerializeField] private TextMeshProUGUI buttonText;

    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip select;
    [SerializeField] private AudioClip error;

    private void Start(){
        audio = GetComponent<AudioSource>();
    }

    private void Update(){
        if(isUnlocked()){
            buttonText.text = ""+level+"";
            
        }else{
            buttonText.text = "X";
            
        }
    }


    public void OnMouseClick()
    {
        if(isUnlocked()){
            GameInfo.SelectedLevel = level;
            PlayAudio(select, AvatarInfo.SFXVolume);
        }else{
            PlayAudio(error, AvatarInfo.SFXVolume);
        }
        
    }


    private bool isUnlocked(){
        return true;
        //for debugging purposes
        if(level==1){return true;}
        return AvatarInfo.HighScores[level-1][0] > 0;
    }

    private void PlayAudio(AudioClip clip, float volume){
        this.audio.loop = false;
        audio.volume = volume;
        audio.clip = clip;
        audio.Play();
    }
}