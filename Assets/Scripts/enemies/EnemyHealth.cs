using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(AudioSource))]

public class EnemyHealth : MonoBehaviour, IDamageable
{
    
    private AudioSource audio;
    public AudioClip damage;
    public AudioClip death;

    public float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    public Sprite mainSprite;
    public Sprite damagedSprite;
    private bool isDead = false; // Flag to track if the enemy is already dead


    private void Start()
    {
        audio = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth < maxHealth && !isDead)
        {
            currentHealth += GameInfo.EnemyRegen * Time.deltaTime;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private IEnumerator ResetSprite(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!isDead) // Only reset the sprite if the enemy is not dead
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = mainSprite;
        }
    }

    public void Damage(float damageAmount)
    {
        if (isDead) return; // Prevent further damage if the enemy is already dead

        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {

            Die(true);

        }
        else
        {
            audio.volume = AvatarInfo.SFXVolume;
            audio.clip = damage;
            audio.Play();

            gameObject.GetComponent<SpriteRenderer>().sprite = damagedSprite;
            StartCoroutine(ResetSprite(0.1f));
        }
    }

    public void Die(bool killedByPlayer)
    {
        if (isDead) return; // Prevent multiple calls to Die()
        isDead = true; // Mark the enemy as dead

        if (killedByPlayer)
        {
            GameInfo.EnemiesKilled +=1;
            GetComponent<LootBag>().InstantiateLoot(transform.position);
            PlayDeathSound();
        }

        GameInfo.numEnemies -= 1;

        // Play the death sound on a temporary GameObject
        

        // Destroy the enemy immediately
        Destroy(gameObject);
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

}