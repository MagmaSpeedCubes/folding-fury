using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DecoyHealth : MonoBehaviour
{

    public static DecoyHealth instance;
    private AudioSource audio;
    public AudioClip damage;
    public AudioClip lowHealth;
    public AudioClip death;

    private float health;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start(){
        audio = GetComponent<AudioSource>();
        health = PlayerInfo.StartHealth;
    }
    public void Damage(float damageAmount, string attackType){
        health -= damageAmount;

        if (health <= 0 && GameInfo.GameMode != -3){
            Die();
            
        }
        PlayDamageSound();
    }
    private void Die(){
        GameInfo.decoy = null;
        PlayDeathSound();
        Destroy(gameObject);

    }
    private void Update(){
        if (health < PlayerInfo.MaxHealth){
            health += PlayerInfo.RegenRate * Time.deltaTime;
        }
        if (health > PlayerInfo.MaxHealth){
            health = PlayerInfo.MaxHealth;
        }

    }

    private void PlayDeathSound()
    {
        if (death != null)
        {
            // Create a temporary GameObject to play the sound
            GameObject soundObject = new GameObject("DeathSound");
            AudioSource tempAudio = soundObject.AddComponent<AudioSource>();

            // Configure the AudioSource
            tempAudio.clip = death;
            tempAudio.volume = AvatarInfo.SFXVolume;
            tempAudio.Play();

            // Destroy the temporary GameObject after the sound finishes
            Destroy(soundObject, death.length);
        }
    }

    private void PlayDamageSound()
    {
        if (death != null)
        {
            // Create a temporary GameObject to play the sound
            GameObject soundObject = new GameObject("DamageSound");
            AudioSource tempAudio = soundObject.AddComponent<AudioSource>();

            // Configure the AudioSource
            tempAudio.clip = damage;
            tempAudio.volume = AvatarInfo.SFXVolume;
            tempAudio.Play();

            // Destroy the temporary GameObject after the sound finishes
            Destroy(soundObject, damage.length);
        }
    }

}
