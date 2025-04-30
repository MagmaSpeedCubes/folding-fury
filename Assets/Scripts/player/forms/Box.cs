using UnityEngine;

public class Box
{
    public static void Instantiate(){
        PlayerInfo.Reset();
        PlayerInfo.Form = "Box";

        PlayerInfo.RegenRate *= 1f;
        PlayerInfo.Size *= 1f;
        PlayerInfo.FoldTime *= 1f;

        PlayerInfo.AttackAngle *= 1f;
        PlayerInfo.AttackDamage *= 0.4f;
        PlayerInfo.AttackRange *= 0.8f;
        PlayerInfo.AttackRate *= 1f;
        PlayerInfo.Knockback *= 1.5f;

        PlayerInfo.MovementSpeed *= 0.6f;
        PlayerInfo.InkResistance *= 3f;
        PlayerInfo.KnifeResistance *= 3f;
        PlayerInfo.Resistance *= 3f;

        Mods.Reactivate();
    }
    public static float getDelay(){
        PlayerInfo.Reset();
        return PlayerInfo.FoldTime * 1f;
    }
}
