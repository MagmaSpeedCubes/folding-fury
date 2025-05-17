using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Box : MonoBehaviour
{
    private static Box instance;

    [SerializeField] private GameObject decoyPrefab;

    void Awake(){
        if(instance==null){
            instance=this;
        }else{
            Destroy(this);
        }
    }

    public static void Instantiate(){
        PlayerInfo.Reset();
        PlayerInfo.Form = "Box";

        PlayerInfo.RegenRate *= 1f;
        PlayerInfo.MaxHealth *= 2f;
        PlayerInfo.Size *= 1f;
        PlayerInfo.FoldTime *= 1f;

        PlayerInfo.AttackAngle *= 4f;
        PlayerInfo.AttackDamage *= 1.5f;
        PlayerInfo.AttackRange *= 0.8f;
        PlayerInfo.AttackRate *= 0.2f;
        PlayerInfo.Knockback *= 0.5f;

        PlayerInfo.MovementSpeed *= 0.6f;
        PlayerInfo.InkResistance *= 3f;
        PlayerInfo.KnifeResistance *= 3f;
        PlayerInfo.Resistance *= 3f;

        Mods.Reactivate();
    }

    public static float getDelay(){
        return PlayerInfo.getOriginalDelay() * 1f;
    }

    public static void boxAttack(){
        instance.spawnDecoy();
    }

    private void spawnDecoy()
    {
        // Check if an old decoy exists
        if (GameInfo.decoy != null)
        {
            Debug.Log("Destroying old decoy");
            Destroy(GameInfo.decoy);
        }

        // Spawn the new decoy
        GameObject decoy = Instantiate(decoyPrefab, transform.position, Quaternion.identity);
        decoy.SetActive(true);
        GameInfo.decoy = decoy;
        Debug.Log("Spawned new decoy");
    }

    private IEnumerator removeDecoy(GameObject decoy, float delay){
        yield return new WaitForSeconds(delay);
        Destroy(decoy);
        GameInfo.decoy = null;
    }


}
