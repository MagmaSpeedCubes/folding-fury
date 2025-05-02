using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrologueDialogue : MonoBehaviour
{
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private int[] Times;
    private int index = 0;

    private Dialogue dialogue;
    private bool isDialogueRunning = false; // Flag to track if the coroutine is running

    void Start()
    {
        dialogue = dialogueCanvas.GetComponent<Dialogue>();
    }

    void Update()
    {
        if (GameInfo.GameMode == -3 && dialogue != null)
        {
            if (index == 0)
            {
                dialogue.SetIndexFromLevel();
                Airplane.Instantiate();  
            }

            if (Times.Length > index && Timer.GetTime() > Times[index] && !isDialogueRunning)
            {
                if(index == 3){
                    Debug.Log("Activate Blackout");
                    PlayerInfo.Modifier = -3;
                    Mods.Reactivate();
                }   
                index += 1;
                StartCoroutine(ShowNextLineWrapper());
                
            }
            if((Timer.GetTime()>145 && !GameInfo.BossFight)){
                Lights.active = false;
                Blackout.deactivate();
                GameInfo.BossFight = true;
            }

            if(PlayerInfo.CurrentHealth < 0){
                GameInfo.GameMode = 0;
                GameInfo.BossFight = false;
                CameraMoveUp.inLevel = false;
            }
        }
    }

    private IEnumerator ShowNextLineWrapper()
    {
        isDialogueRunning = true; // Set the flag to true
        yield return StartCoroutine(dialogue.ShowNextLine());
        isDialogueRunning = false; // Reset the flag after the coroutine finishes

    }
}