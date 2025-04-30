using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiamondSpawner : MonoBehaviour
{

    public GameObject droppedItemPrefab;
    [SerializeField] private Sprite diamondSprite;
    [SerializeField] private int level;

    private bool spawned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameInfo.GameMode == level && !spawned){
            spawned = true;
            SpawnDiamond(transform.position);
        }else if(GameInfo.GameMode != level){
            spawned = false;
        }
    }

    public void SpawnDiamond(Vector3 spawnPosition){
        GameObject diamond = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
        diamond.GetComponent<SpriteRenderer>().sprite = diamondSprite;
    }
}
