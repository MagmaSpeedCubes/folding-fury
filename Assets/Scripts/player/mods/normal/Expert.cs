using UnityEngine;

public class Expert
{
    /** 10% angry enemies
* 125% enemy spawn rate
* no natural regeneration
* 3% chance to miss
* score bonus for fast boss kill
*/
    public static void activate(){
        GameInfo.SpawnRate *= 1.25f;
        GameInfo.AngryRate += 0.1f;
        PlayerInfo.MissRate += 0.03f;
        PlayerInfo.RegenRate *= 0f;
        Debug.Log("Expert");
    }
}
