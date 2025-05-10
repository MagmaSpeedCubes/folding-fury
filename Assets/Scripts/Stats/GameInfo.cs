using UnityEngine;

public class GameInfo
{
    public static float MaxHealth = 100f;
    public static float RegenRate = 1f;
    public static float Absorption = 0f;

    public static float MovementSpeed = 5f;
    public static float FoldingSpeed = 1f;
    public static float LootMultiplier = 1f;

    public static float AttackRange = 1.2f;
    public static float AttackDamage = 10f;
    public static float AttackRate = 1f;


    public static float Score = 0f;


    public static int numEnemies = 0;
    public static float SpawnRate = 1f;
    public static float AngryRate = 0f;
    public static float EnragedRate = 0f;

    public static float EnemyTransparency = 0f;
    public static float EnemyHealth = 1f;
    public static float EnemyRegen = 0f;
    public static float EnemySize = 1f;
    public static float EnemySpeed = 1f;

    public static float EnemyDamage = 1f;

   
    //stationary enemies will get an attack rate boost
    public static bool BossFight = false;
    public static float BossFightStart = 0f;
    
    public static int GameMode = 0;
    public static int SelectedLevel = 1;
    public static int SelectedModifier = 0;

    public static int NumLevels = 8;
    public static int NumModifiers = 6;

    public static GameObject decoy;
    //1-16 = respective levels
    //0 = level selector
    //-1 = mod selector
    //-2 = credits
    //-3 = prologue
    //-4 = game over/stage complete
    // card upgrades
    // forms
    // achievements
    // 


    public static int EnemiesKilled = 0;
    public static int EnemiesSpawned = 0;

    public static int DiamondsCollected = 0;

    public static float DamageTaken = 0f;

    public static void Reset(){
        Score = 0f;
        LootMultiplier = 1f;


        numEnemies = 0;
        SpawnRate = 1f;
        AngryRate = 0f;
        EnragedRate = 0f;

        EnemyTransparency = 0f;
        EnemyHealth = 1f;
        EnemyRegen = 0f;
        EnemySize = 1f;
        EnemySpeed = 1f;

        EnemyDamage = 1f;

        BossFight = false;
        BossFightStart = 0f;
        
        GameMode = 0;
        SelectedLevel = 0;
        SelectedModifier = 0;

        NumLevels = 8;

        EnemiesKilled = 0;
        EnemiesSpawned = 0;

        DiamondsCollected = 0;

        DamageTaken = 0f;

        decoy = null;
    }

    public static void ResetLevel(){
        Score = 0f;
        LootMultiplier = 1f;


        numEnemies = 0;
        SpawnRate = 1f;
        AngryRate = 0f;
        EnragedRate = 0f;

        EnemyTransparency = 0f;
        EnemyHealth = 1f;
        EnemyRegen = 0f;
        EnemySize = 1f;
        EnemySpeed = 1f;

        EnemyDamage = 1f;

    

        BossFight = false;
        BossFightStart = 0f;

        EnemiesKilled = 0;
        EnemiesSpawned = 0;

        DiamondsCollected = 0;

        DamageTaken = 0f;
        decoy = null;
        
    }

    public static void ResetEnemy(){
        SpawnRate = 1f;
        AngryRate = 0f;
        EnragedRate = 0f;

        EnemyTransparency = 0f;
        EnemyHealth = 1f;
        EnemyRegen = 0f;
        EnemySize = 1f;
        EnemySpeed = 1f;

        EnemyDamage = 1f;
    }

}
