using UnityEngine;

public class Crane
{
    public static void Instantiate(){
        PlayerInfo.Reset();
        PlayerInfo.Form = "Crane";

        PlayerInfo.RegenRate *= 3f;
        PlayerInfo.Size *= 1f;
        PlayerInfo.FoldTime *= 2f;

        PlayerInfo.AttackAngle *= 1.5f;
        PlayerInfo.AttackDamage *= 0.6f;
        PlayerInfo.AttackRange *= 4f;
        PlayerInfo.AttackRate *= 1f;
        PlayerInfo.Knockback *= 0.5f;

        PlayerInfo.MovementSpeed *= 1.5f;
        PlayerInfo.InkResistance *= 1.5f;
        PlayerInfo.KnifeResistance *= 4f;
        PlayerInfo.Resistance *= 1.5f;

        Mods.Reactivate();
    }
    public static float getDelay(){
        PlayerInfo.Reset();
return PlayerInfo.getOriginalDelay() * 2f;
    }
}
