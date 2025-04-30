using UnityEngine;

public class Delusion
{
    public static void activate(){
        PlayerInfo.MissRate = 0.3f; //30% miss rate
        PlayerInfo.AttackRange *= 0.8f; //Range reduced by 20%
        GameInfo.EnemyTransparency = 0.6f; //Enemies are 60% invisible
        PlayerInfo.CurrentHealth = PlayerInfo.MaxHealth * 0.2f; //Start with 20% health;
    }
}
