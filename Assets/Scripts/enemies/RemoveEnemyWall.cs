using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RemoveEnemyWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        EnemyHealth eh = collision.GetComponent<EnemyHealth>();
        if(eh != null){
            eh.Die(false);
            Debug.Log("Enemy Touched Wall");
            
        }
        DecoyHealth dh = collision.GetComponent<DecoyHealth>();
        if(dh != null){
            dh.Die();
            Debug.Log("Removed decoy off screen");
            
        }
    }
}
