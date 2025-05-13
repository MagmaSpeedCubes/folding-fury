using System.Collections;
using UnityEngine;
using System;

public class TapeHazard : MonoBehaviour
{    
    [SerializeField] private GameObject player;

    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite triggerdSprite;


    [SerializeField] private float radius = 2f;

    [SerializeField] private int level;

    private bool active;
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
            FormChangeScript.canFormChange = false;
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
