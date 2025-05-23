using System.Collections;
using UnityEngine;
using System;

public class Hazard : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite triggerdSprite;


    [SerializeField] private float dps = 50f;
    [SerializeField] private float sps = 0.8f;
    [SerializeField] private float radius = 2f;

    [SerializeField] private int level;

    private bool active;

    void Start(){
        active = false;
    }

    void Update(){
        if(GameInfo.GameMode == level){
            active = true;
        }else{
            active = false;
        }

        if (GameInfo.GameMode == -4 || !gameActive()){
            return; 
        }

        if(inRange()){
            player.GetComponent<PlayerHealth>().Damage(dps*Time.deltaTime, "Hazard");
            PlayerInfo.MovementSpeed *= (float) Math.Pow(sps, Time.deltaTime);
            Debug.Log("Damaging");
        }
    }

    bool inRange()
    {
        Vector2 hazardPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);

        return Vector2.Distance(hazardPosition, playerPosition) <= radius;
    }
    
    bool alive(){
        return PlayerInfo.CurrentHealth > 0;
    }

    bool gameActive(){
        return CameraMoveUp.inLevel;
    }


   
}