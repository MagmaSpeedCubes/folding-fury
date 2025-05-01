using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Half : MonoBehaviour{
    private static bool active;

    public static Half Instance {get; private set;}

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    public static void activate(){
        PlayerInfo.MaxHealth *= 0.5f; //Max health limited to 50%
        if(Instance!=null ){
            Half.active = true; //Regeneration decreases over time
        
        }
    }

    public void Update(){
        if(Half.active){
            if(PlayerInfo.RegenRate > 0){
                PlayerInfo.RegenRate -= 0.001f*Time.deltaTime;
                if(PlayerInfo.RegenRate < 0){
                    PlayerInfo.RegenRate = 0f;
                }
            }

        }
        if(GameInfo.GameMode < 1){
            Half.active = false;
        }
    }

}

