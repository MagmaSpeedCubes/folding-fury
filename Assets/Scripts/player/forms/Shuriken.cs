using UnityEngine;

public class Shuriken
{
    public static void Instantiate(){
        PlayerInfo.Reset();
        PlayerInfo.Form = "Shuriken";

        PlayerInfo.RegenRate *= 1f;
        PlayerInfo.Size *= 1f;
        PlayerInfo.FoldTime *= 2f;

        PlayerInfo.AttackAngle *= 4f;
        PlayerInfo.AttackDamage *= 2f;
        PlayerInfo.AttackRange *= 2f;
        PlayerInfo.AttackRate *= 4f;
        PlayerInfo.Knockback *= 2f;

        PlayerInfo.MovementSpeed *= 1.5f;
        PlayerInfo.InkResistance *= 1f;
        PlayerInfo.KnifeResistance *= 5f;
        PlayerInfo.Resistance *= 1f;

        Mods.Reactivate();
    }

    public static float getDelay(){
return PlayerInfo.getOriginalDelay() * 2f;
    }
}
