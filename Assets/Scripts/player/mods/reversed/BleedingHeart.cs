using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BleedingHeart : MonoBehaviour
{
    public static bool active;
    public static BleedingHeart Instance {get; private set;}

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    public static void activate(){
        PlayerInfo.MaxHealth = 0.01f; // A single enemy hit instantly kills you
        GameInfo.SpawnRate *= 0.5f; // Half as many enemies spawn
        if(Instance!=null){
            active = true; //enemy speed increases over time
        }
    }

    public void Update(){
        if(active){
            if(GameInfo.EnemySpeed < 3){
                GameInfo.EnemySpeed += 0.005f*Time.deltaTime;
                if(GameInfo.EnemySpeed > 3){
                    GameInfo.EnemySpeed = 3f;
                }
            }
        }
    }

}
