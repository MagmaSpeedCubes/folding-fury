using UnityEngine;

public class GameInfo
{
    public static float Score = 0f;
    public static float ScoreMultiplier = 1f;
    public static float LootMultiplier = 1f;


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
    public static int SelectedLevel = 0;
    public static int SelectedModifier = 0;

    public static int NumLevels = 8;
    //1-16 = respective levels
    //0 = level selector
    //-1 = mod selector
    //-2 = credits
    //-3 = prologue
    // card upgrades
    // forms
    // achievements
    // 


    public static int EnemiesKilled = 0;
    public static int EnemiesSpawned = 0;

    public static int DiamondsCollected = 0;

    public static float DamageTaken = 0f;

}
