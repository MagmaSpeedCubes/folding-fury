using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FumingAshes : MonoBehaviour
{
    [SerializeField] private GameObject ps;

    private static bool active;

    public static FumingAshes Instance {get; private set;}

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    public static void activate(){
        GameInfo.AngryRate = 1f;
        GameInfo.EnragedRate = 0.1f;
        active = true;

    }

    public void Update(){
        if(active && GameInfo.numEnemies < 3){
            Instance.spawnEnemies();
        }
    }

    public void spawnEnemies(){
        
        PunishmentSpawner pss = ps.GetComponent<PunishmentSpawner>();
        if(pss!=null){
            pss.SpawnSingleCluster();
        }
    }
}
