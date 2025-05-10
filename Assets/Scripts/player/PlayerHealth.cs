using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerHealth : MonoBehaviour
{

    public static PlayerHealth instance;
    private AudioSource audio;
    public AudioClip damage;
    public AudioClip lowHealth;
    public AudioClip death;

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

    private void Start(){
        audio = GetComponent<AudioSource>();
        PlayerInfo.CurrentHealth = PlayerInfo.StartHealth;
    }
    public void Damage(float damageAmount, string attackType){
        float damageMultiplier = 1-PlayerInfo.Resistance;
        damageAmount *= damageMultiplier;
        if(PlayerInfo.Absorption >= damageAmount){
            PlayerInfo.Absorption -= damageAmount;
            return;
        }else if(PlayerInfo.Absorption > 0){
            damageAmount -= PlayerInfo.Absorption;
            PlayerInfo.Absorption = 0;
        }
        PlayerInfo.CurrentHealth -= damageAmount;
        GameInfo.DamageTaken += damageAmount;

        if (PlayerInfo.CurrentHealth <= 0 && GameInfo.GameMode != -3){
            Die();
            
        }
        PlayDamageSound();
    }
    public void Die(){
        PlayDeathSound();
        EndStage.CompleteStage(false);

    }
    private void Update(){
        if (PlayerInfo.CurrentHealth < PlayerInfo.MaxHealth){
            PlayerInfo.CurrentHealth += PlayerInfo.RegenRate * Time.deltaTime;
        }
        if (PlayerInfo.CurrentHealth > PlayerInfo.MaxHealth){
            PlayerInfo.CurrentHealth = PlayerInfo.MaxHealth;
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
