using UnityEngine;

public class Sword
{
    public static void Instantiate(){
        PlayerInfo.Reset();
        PlayerInfo.Form = "Sword";

        PlayerInfo.RegenRate *= 1f;
        PlayerInfo.Size *= 1f;
        PlayerInfo.FoldTime *= 2f;

        PlayerInfo.AttackAngle *= 1.5f;
        PlayerInfo.AttackDamage *= 6f;
        PlayerInfo.AttackRange *= 3f;
        PlayerInfo.AttackRate *= 1.5f;
        PlayerInfo.Knockback *= 0.1f;

        PlayerInfo.MovementSpeed *= 0.8f;
        PlayerInfo.InkResistance *= 1f;
        PlayerInfo.KnifeResistance *= 5f;
        PlayerInfo.Resistance *= 1f;

        Mods.Reactivate();
    }

    public static float getDelay(){
return PlayerInfo.getOriginalDelay() * 2f;
    }
}
