using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelDialogue : MonoBehaviour
{
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private int[] Times;
    [SerializeField] private int level;
    private int index = 0;

    private Dialogue dialogue;
    private bool isDialogueRunning = false; // Flag to track if the coroutine is running
    private bool active = false;
    private static LevelDialogue instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }
    }

    void Start()
    {
        dialogue = dialogueCanvas.GetComponent<Dialogue>();
    }

    void Update()
    {
        if (GameInfo.GameMode == level && dialogue != null)
        {

            if (Times.Length > index && Timer.GetTime() > Times[index] && !isDialogueRunning)
            {
                StartCoroutine(ShowNextLineWrapper());
                index += 1;
                
            }

        }
    }

    private IEnumerator ShowNextLineWrapper()
    {
        isDialogueRunning = true; 
        yield return StartCoroutine(dialogue.ShowNextLine());
        isDialogueRunning = false;
        

    }

    public static void NextLine(){
        instance.StartCoroutine(instance.ShowNextLineWrapper());
    }
}