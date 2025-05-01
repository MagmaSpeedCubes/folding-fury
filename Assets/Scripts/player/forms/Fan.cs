using UnityEngine;

public class Fan
{
    public static void Instantiate(){
        PlayerInfo.Reset();
        PlayerInfo.Form = "Fan";

        PlayerInfo.RegenRate *= 1f;
        PlayerInfo.Size *= 1f;
        PlayerInfo.FoldTime *= 0.2f;

        PlayerInfo.AttackAngle *= 2f;
        PlayerInfo.AttackDamage *= 0.01f;
        PlayerInfo.AttackRange *= 2f;
        PlayerInfo.AttackRate *= 4f;
        PlayerInfo.Knockback *= 3f;

        PlayerInfo.MovementSpeed *= 1f;
        PlayerInfo.InkResistance *= 4f;
        PlayerInfo.KnifeResistance *= 0.5f;
        PlayerInfo.Resistance *= 0.5f;

        Mods.Reactivate();
    }
    public static float getDelay(){
        return PlayerInfo.getOriginalDelay() * 0.2f;
    }
}
