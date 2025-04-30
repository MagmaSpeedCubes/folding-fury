using UnityEngine;

public class Airplane
{
    public static void Instantiate(){
        PlayerInfo.Reset();
        PlayerInfo.Form = "Airplane";

        PlayerInfo.RegenRate *= 1f;
        PlayerInfo.Size *= 1f;
        PlayerInfo.FoldTime *= 0.5f;

        PlayerInfo.AttackAngle *= 0.2f;
        PlayerInfo.AttackDamage *= 2f;
        PlayerInfo.AttackRange *= 3f;
        PlayerInfo.AttackRate *= 1f;
        PlayerInfo.Knockback *= 0.1f;

        PlayerInfo.MovementSpeed *= 4f;
        PlayerInfo.InkResistance *= 1f;
        PlayerInfo.KnifeResistance *= 1f;
        PlayerInfo.Resistance *= 1f;

        Mods.Reactivate();
    }
    public static float getDelay(){
        PlayerInfo.Reset();
        return PlayerInfo.FoldTime * 0.5f;
    }
}
