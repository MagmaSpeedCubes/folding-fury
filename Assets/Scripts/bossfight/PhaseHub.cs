using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PhaseHub : MonoBehaviour
{
    [SerializeField] private GameObject bossFight;
    [SerializeField] private List<GameObject> phaseSpawners;
    [SerializeField] private float phaseLength;
    [SerializeField] private bool playDialogueOnEnter = false;
    [SerializeField] private bool playDialogueOnExit = false;
    [SerializeField] private Canvas dialogueCanvas;
    private Dialogue dialogue;
    public float tick;

    private BossFight bossFightScript;
    private bool active;

    void Start(){
        if (dialogueCanvas == null)
        {
            Dialogue foundDialogue = FindObjectOfType<Dialogue>();
            if (foundDialogue != null)
            {
                dialogue = foundDialogue;
                
            }
        }


        active = false;
        bossFightScript = bossFight.GetComponent<BossFight>();

    }

    void Update(){
        // Remove destroyed objects from the list
        phaseSpawners.RemoveAll(spawner => spawner == null);

        // Check if all spawners are destroyed
        if (phaseSpawners.Count == 0 && active)
        {
            active = false;
            if(playDialogueOnExit && dialogue != null){
                StartCoroutine(ShowNextLineWrapper());
            }
            StartCoroutine(bossFightScript.StartNextPhase());
        }

    }

    public void ActivatePhase(){
        active = true;
        if(playDialogueOnEnter && dialogue != null){
            StartCoroutine(ShowNextLineWrapper());
        }
        for(int i=0; i<phaseSpawners.Count; i++){
            BossSpawner bossSpawner = phaseSpawners[i].GetComponent<BossSpawner>();
            bossSpawner.active = true;
        }

    }

    private IEnumerator ShowNextLineWrapper()
    {
        yield return StartCoroutine(dialogue.ShowNextLine());
    }

}
