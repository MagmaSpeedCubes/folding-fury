using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelDialogue : MonoBehaviour
{
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private int level;
    [SerializeField] private float[] dialogueTimes;
    [SerializeField] private int startingIndex;
    private int index = 0;

    private Dialogue dialogue;
    private bool isDialogueRunning = false;
    private bool active = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogue = dialogueCanvas.GetComponent<Dialogue>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!active && GameInfo.GameMode == level){
            active = true;
            dialogue.SetIndexFromLevel();
        }
        if(active && dialogueTimes.Length > index && Timer.GetTime()>= dialogueTimes[index]){
            StartCoroutine(ShowNextLineWrapper());
            index += 1;
        }
    }

    private IEnumerator ShowNextLineWrapper()
    {
        isDialogueRunning = true; 
        yield return StartCoroutine(dialogue.ShowNextLine());
        isDialogueRunning = false;
        

    }
}
