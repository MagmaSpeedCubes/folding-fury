using UnityEngine;

public class Shield
{
    public static void Instantiate(){
        PlayerInfo.Reset();
        PlayerInfo.Form = "Shield";

        PlayerInfo.RegenRate *= 1.2f;
        PlayerInfo.Size *= 1f;
        PlayerInfo.FoldTime *= 0.5f;

        PlayerInfo.AttackAngle *= 1f;
        PlayerInfo.AttackDamage *= 0.2f;
        PlayerInfo.AttackRange *= 1f;
        PlayerInfo.AttackRate *= 1f;
        PlayerInfo.Knockback *= 0.4f;

        PlayerInfo.MovementSpeed *= 0.6f;
        PlayerInfo.InkResistance *= 0f;
        PlayerInfo.KnifeResistance *= 9f;
        PlayerInfo.Resistance *= 9f;

        Mods.Reactivate();
    }

    public static float getDelay(){
        PlayerInfo.Reset();
        return PlayerInfo.FoldTime * 0.5f;
    }
}
