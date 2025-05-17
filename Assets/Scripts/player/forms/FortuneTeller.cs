using UnityEngine;

public class FortuneTeller
{
    public static void Instantiate(){
        PlayerInfo.Reset();
        PlayerInfo.Form = "FortuneTeller";

        PlayerInfo.RegenRate *= 1f;
        PlayerInfo.Size *= 1f;
        PlayerInfo.FoldTime *= 3f;

        PlayerInfo.AttackAngle *= 0f;
        PlayerInfo.AttackDamage *= 1f;
        PlayerInfo.AttackRange *= 0f;
        PlayerInfo.AttackRate *= 0.2f;
        PlayerInfo.Knockback *= 0f;

        PlayerInfo.MovementSpeed *= 1f;
        PlayerInfo.InkResistance *= 0f;
        PlayerInfo.KnifeResistance *= 0f;
        PlayerInfo.Resistance *= 0f;

        Mods.Reactivate();
    }

    public static float getDelay(){
        return PlayerInfo.getOriginalDelay() * 3f;
    }

    public static void fortuneTellerAttack(){
        PlayerInfo.PlayerLuck += (PlayerInfo.AttackDamage / 100);
        Debug.Log("Player Luck:" + PlayerInfo.PlayerLuck);
    }
}
