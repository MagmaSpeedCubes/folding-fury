using UnityEngine;
using System.Collections.Generic;
public class BossFight : MonoBehaviour
{
    
    [SerializeField] private int level;
    [SerializeField] private float activationTime;
    [SerializeField] private List<GameObject> hubs;
    [SerializeField] private int phases;

    private int index;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = 0;
        phases = hubs.Count;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameInfo.BossFight && GameInfo.GameMode == level && Timer.GetTime() > activationTime){
            ActivateBossFight();
            Debug.Log("Started Boss Fight");
        }
    }

    public void StartNextPhase(){
        Debug.Log("Started Next Phase");
        if(index == hubs.Count){
            EndBossFight();
            Debug.Log("Ended Boss Fight");
        }else{
        GameObject hub = hubs[index];
        PhaseHub hubScript = hub.GetComponent<PhaseHub>();
        hubScript.ActivatePhase();
        index += 1;
        }


        
    }

    private void ActivateBossFight(){
        GameInfo.BossFight = true;
        GameInfo.BossFightStart = Timer.GetTime();
        StartNextPhase();
    }

    private void EndBossFight(){
        GameInfo.BossFight = false;
        EndStage.CompleteStage(true);
    }

    
}
