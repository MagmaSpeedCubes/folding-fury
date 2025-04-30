using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(AudioSource))]

public class Dialogue : MonoBehaviour
{
    public AudioSource audio;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public AudioClip[] sounds;
    public float textSpeed = 30f;
    public float fadeTime = 1f;
    public float holdTime = 5f;
    public float volume = 5f;

    [SerializeField] private CanvasGroup canvasGroup;

    private int index;

    void Start(){

        audio = GetComponent<AudioSource>();

        textComponent.text = string.Empty;
        
        
    }

    void StartDialogue(){
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine(){
        audio.volume = AvatarInfo.SFXVolume*volume;
        audio.clip = sounds[index];
        audio.Play();
        if(index!=null){
            foreach(char c in lines[index]){
                textComponent.text += c;
                yield return new WaitForSeconds(1/textSpeed);
            }
        }

    }

    public IEnumerator ShowNextLine()
    {
        // Clear the text at the start
        textComponent.text = "";
        float elapsedTime = 0f;

        // Fade in the canvas
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeTime);
            yield return null;
        }

        // Play the audio for the line
        audio.volume = AvatarInfo.SFXVolume * volume;
        audio.clip = sounds[index];
        audio.Play();

        // Reveal the text over time
        elapsedTime = 0f;
        int lastCharIndex = 0; // Track the last character index revealed
        while (elapsedTime < lines[index].Length / textSpeed)
        {
            int charIndex = Mathf.FloorToInt(elapsedTime * textSpeed); // Calculate the current character index
            if (charIndex > lastCharIndex) // Only update if a new character should be revealed
            {
                textComponent.text = lines[index].Substring(0, charIndex);
                lastCharIndex = charIndex; // Update the last character index
            }
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the full line is displayed at the end
        textComponent.text = lines[index];

        // Hold the text for a while
        yield return new WaitForSeconds(holdTime);

        // Fade out the canvas
        elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeTime));
            yield return null;
        }

        // Clear the text after fading out
        textComponent.text = "";
        NextLine();
    }


    public void SetIndexFromLevel(){
        if(GameInfo.GameMode == -3){
            index = 0;
        }
        StartDialogue();
    }

    public void NextLine(){
        index += 1;
    }

    public void SetIndex(int i){
        index = i;
    }

    
}
