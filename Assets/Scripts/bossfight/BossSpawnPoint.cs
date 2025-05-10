using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossSpawnPoint : MonoBehaviour
{

    public GameObject bossPrefab;
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
            SpawnBoss(transform.position);
        }else if(GameInfo.GameMode != level){
            spawned = false;
        }
    }

    public void SpawnBoss(Vector3 spawnPosition){
        GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }
}

