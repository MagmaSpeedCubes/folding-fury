using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blackout : MonoBehaviour
{
    [SerializeField] private GameObject cover;
    public static Blackout Instance {get; private set;}

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    public static void activate(){
        Lights.activate();//in addition to base effects
        Instance.addCover();
        GameInfo.EnemyTransparency = 0.9f; //enemies are 90% transparent
        GameInfo.SpawnRate *= 0.8f; //80% as many enemies spawn
        PlayerInfo.RegenRate *= 0.5f; //50% regen rate
    }

    public static void deactivate(){
        Instance.removeCover();
    }

    public void addCover(){
        cover.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    public void removeCover(){
        cover.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }
}
