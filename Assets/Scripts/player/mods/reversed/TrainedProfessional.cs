using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainedProfessional : MonoBehaviour
{
    private static bool active;

    public static TrainedProfessional Instance {get; private set;}

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    public static void activate(){
        Expert.activate();
        GameInfo.SpawnRate *= 2f;
        GameInfo.EnragedRate = 0.03f;
        GameInfo.EnemyRegen = 1f;
        PlayerInfo.MaxHealth *= 0.4f;

        active = true;
    }

    public void Update(){
        if(GameInfo.GameMode < 1 && GameInfo.GameMode != -3){
            active = false;
        }
        if(active){
            if(PlayerInfo.MovementSpeed > 0.4){
                PlayerInfo.MovementSpeed -= 0.001f*Time.deltaTime;
                if(PlayerInfo.MovementSpeed < 0.4){
                    PlayerInfo.MovementSpeed = 0.4f;
                }
            }
            if(GameInfo.EnemyDamage < 5){
                GameInfo.EnemyDamage += 0.004f*Time.deltaTime;
                if(GameInfo.EnemyDamage > 5){
                    GameInfo.EnemyDamage = 5f;
                }
            }
        }


    }


}
