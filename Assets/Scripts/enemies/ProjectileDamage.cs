using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ProjectileDamage : MonoBehaviour
{

    


    public float damage;
    public string attackType;
    public float hits;
    public float speed;
    public float attackCooldown;
    public GameObject player;

    private bool onCooldown = false;


    private void Update(){
        if(GameInfo.GameMode < 1 && GameInfo.GameMode != -3){
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Get the PlayerHealth component from the collided object
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null && !onCooldown)
        {
            // Call the Damage method on the PlayerHealth component
            playerHealth.Damage(damage, attackType);
            hits -= 1;
            if (hits <= 0)
            {
                EnemyHealth eh = GetComponent<EnemyHealth>();
                if(eh!=null){
                    eh.Die(true);
                }
            }
            onCooldown = true;
            StartCoroutine(resetCooldown());
        }
    }

    private IEnumerator resetCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        onCooldown = false;
    }
}