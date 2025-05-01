using UnityEngine;

public class PlayerInfo
{
    public static float Absorption = 0f;
    public static float StartHealth = 100f;
    public static float MaxHealth = 100f;
    public static float CurrentHealth = 100f;
    public static float RegenRate = 1f;
    public static float Size = 1f;
    public static string Form = "Unfolded";
    //1 = fan
    //2 = shield
    //3 = airplane
    //4 = sword
    //5 = unfolded
    //6 = box
    //7 = shuriken
    //8 = crane
    //9 = fortune teller
    public static float FoldTime = 1;
    public static int Modifier = 0;
    //0 = none
    //1 = angry
    //2 = blurred
    //3 = lights
    //4 = volatile
    //5 = half
    //6 = expert
    //7 = event
    //negative of any number = reversed


    //general info

    public static float AttackAngle = 360f;
    public static float AttackDamage = 30f;
    public static float AttackRange = 2.5f;
    public static float AttackRate = 4f;
    public static float Knockback = 1f;
    public static float MissRate = 0f;

    public static float MovementSpeed = 5f;
    public static float FireDamage = 0f;

    public static int PImmortal = 0;
    public static int PManiac = 0;
    public static int PSprinter = 0;

    public static float InkResistance = 0;
    public static float KnifeResistance = 0;
    public static float Resistance = 0;
        /*
    CanFormChange: true,
    FormChangeDamage: 0,
    FormChangeSpeed: 0,
    LastFormChange: 0,
    //Form Change


    */

    public static float getOriginalDelay(){
        return FoldTime;
    }
    public static void Reset(){
        Absorption = 0f;
        StartHealth = 100f;
        MaxHealth = 100f;
        RegenRate = 1f;
        Size = 1f;
        Form = "Unfolded";
        FoldTime = 1;

        AttackAngle = 90f;
        AttackDamage = 10f;
        AttackRange = 1.2f;
        AttackRate = 1f;
        Knockback = 0.4f;
        MissRate = 0f;

        MovementSpeed = 5f;
        FireDamage = 0f;

        PImmortal = 0;
        PManiac = 0;
        PSprinter = 0;

        InkResistance = 0.1f;
        KnifeResistance = 0.1f;
        Resistance = 0.1f;
        GameInfo.ResetEnemy();
        Buffs.Reapply();

    }

    public static void NewLevel(){
        GameInfo.ResetLevel();
        PlayerInfo.Reset();
        Debug.Log("PreActivation");
        Mods.Reactivate();
        CurrentHealth = MaxHealth;
    }
}
