using UnityEngine;

public class Proliferation
{
    public static void activate(){
        GameInfo.SpawnRate *= 3f; //300% enemy spawn rate
        PlayerInfo.MaxHealth *= 0.6f; //max health capped at 60%
    }
}
