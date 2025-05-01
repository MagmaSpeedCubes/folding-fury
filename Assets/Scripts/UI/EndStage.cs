using UnityEngine;
using System;
using System.Collections;
using TMPro;


public class EndStage : MonoBehaviour
{
    private static EndStage Instance {get; set;}
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI iscoreText;
    [SerializeField] private TextMeshProUGUI enemyText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI diamondText;
    [SerializeField] private TextMeshProUGUI badgesText;
    [SerializeField] private TextMeshProUGUI fscoreText;

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }




    public static void CompleteStage(bool win){
        
        int oldHighScore = getCurrentScore(GameInfo.GameMode, PlayerInfo.Modifier);
        int level = GameInfo.GameMode;
        int mod = PlayerInfo.Modifier;
        CameraMoveUp.inLevel = false;
        GameInfo.GameMode = -4;
        if(win){
            Instance.mainText.text = "Stage Complete";
        }else{
            Instance.mainText.text = "Game Over";
        }

        
        

        Instance.iscoreText.text = "Score: " + GameInfo.Score;


        int enemyBonus = (int) Math.Pow(GameInfo.EnemiesKilled, 2) / 10;
        GameInfo.Score += enemyBonus;
        Instance.enemyText.text = "Enemies Killed: " + GameInfo.EnemiesKilled + "/" + GameInfo.EnemiesSpawned + " +" + enemyBonus;
        Instance.fscoreText.text = "Score: " + GameInfo.Score;

        int damageBonus = (int) Math.Max(5000 - Math.Pow(GameInfo.DamageTaken, 2), 0);
        GameInfo.Score += damageBonus;
        Instance.damageText.text = "Damage Taken: " + GameInfo.DamageTaken + " +" + damageBonus;
        Instance.fscoreText.text = "Score: " + GameInfo.Score;

        int diamondBonus = (int) Math.Pow(GameInfo.DiamondsCollected, 2) * 50;
        GameInfo.Score += diamondBonus;
        Instance.diamondText.text = "Diamonds Collected: " + GameInfo.DiamondsCollected + " /10 +" + diamondBonus;
        Instance.fscoreText.text = "Score: " + GameInfo.Score;


        
        if(GameInfo.Score > oldHighScore){
            Instance.fscoreText.text = "New Record: ";
            saveRecord(level, mod, (int)GameInfo.Score);
        }else{
            Instance.fscoreText.text = "Final Score: ";
        }

        Instance.fscoreText.text += GameInfo.Score;

        



        



        
        //add delay later

        //summary
        //diamonds collected,more = bonus pts
        //enemies killed,more = bonus pts
        //damage taken,less = bonus pts
        //

        
    }

    public static int getCurrentScore(int level, int modifier){
        
        int[] scoreArray;
        if(modifier < 0){
            scoreArray = AvatarInfo.ReversedHighScores[level];
        }else{
            scoreArray = AvatarInfo.HighScores[level];
        }

        return scoreArray[Math.Abs(modifier)];

    }

    public static void saveRecord(int level, int modifier, int score){

        if(modifier < 0){
            AvatarInfo.ReversedHighScores[level][-modifier] = score;
        }else{
            AvatarInfo.HighScores[level][modifier] = score;
        }
        SaveSystem.SaveData();

        
    }
}
