using UnityEngine;
using System.Collections.Generic;
public class PhaseHub : MonoBehaviour
{
    [SerializeField] private GameObject bossFight;
    [SerializeField] private List<GameObject> phaseSpawners;
    [SerializeField] private float phaseLength;

    public float tick;

    private BossFight bossFightScript;
    private bool active;

    void Start(){
        active = false;
        bossFightScript = bossFight.GetComponent<BossFight>();
    }

    void Update(){
        // Remove destroyed objects from the list
        phaseSpawners.RemoveAll(spawner => spawner == null);

        Debug.Log("Spawners left: " + phaseSpawners.Count);

        // Check if all spawners are destroyed
        if (phaseSpawners.Count == 0 && active)
        {
            active = false;
            StartCoroutine(bossFightScript.StartNextPhase());
        }

    }

    public void ActivatePhase(){
        active = true;

        for(int i=0; i<phaseSpawners.Count; i++){
            BossSpawner bossSpawner = phaseSpawners[i].GetComponent<BossSpawner>();
            bossSpawner.active = true;
        }

    }

}
