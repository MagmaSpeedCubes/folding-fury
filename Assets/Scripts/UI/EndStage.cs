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

        GameInfo.BossFight = false;

        Debug.Log("Level: "+GameInfo.GameMode);
        int level = GameInfo.GameMode;
        
        GameInfo.GameMode = -4;
        CameraMoveUp.inLevel = false;
        int oldHighScore = getCurrentScore(level, PlayerInfo.Modifier);
        
        int mod = PlayerInfo.Modifier;

        if(win){
            unlockAchievements(level, mod, (int)GameInfo.DamageTaken);
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

        int multiplier = 1;
        for(int i=0; i<GameInfo.NumModifiers; i++){
            multiplier += getModBadges(i+1);
        }

        GameInfo.Score *= multiplier;
        Instance.badgesText.text = "Badge Multiplier x" + multiplier;
        Instance.fscoreText.text = "Score: " + GameInfo.Score;


        
        if(GameInfo.Score > oldHighScore && win){
            Instance.fscoreText.text = "New Record: ";
            saveRecord(level, mod, (int)GameInfo.Score);
            //high scores are only saved on victory

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

    public static int getCurrentScore(int level, int modifier) {
        int[] scoreArray;
        if (modifier < 0) {
            if (AvatarInfo.ReversedHighScores == null || AvatarInfo.ReversedHighScores.Length <= level || AvatarInfo.ReversedHighScores[level] == null) {
                Debug.LogError($"ReversedHighScores array is not initialized for level {level}");
                return 0; // Default score
            }
            scoreArray = AvatarInfo.ReversedHighScores[level];
        } else {
            if (AvatarInfo.HighScores == null || AvatarInfo.HighScores.Length <= level || AvatarInfo.HighScores[level] == null) {
                Debug.LogError($"HighScores array is not initialized for level {level}");
                return 0; // Default score
            }
            scoreArray = AvatarInfo.HighScores[level];
        }

        if (scoreArray.Length <= Math.Abs(modifier)) {
            Debug.LogError($"Modifier {modifier} is out of bounds for level {level}");
            return 0; // Default score
        }

        if (scoreArray.Length <= Math.Abs(modifier)) {
            Debug.LogError($"Modifier {modifier} is out of bounds for level {level}");
            return 0; // Default score
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

    public static int getModBadges(int modNumber){
        int score = EndStage.getCurrentScore(GameInfo.SelectedLevel, 0);
        int modScore = EndStage.getCurrentScore(GameInfo.SelectedLevel, modNumber);
        int reverseModScore = EndStage.getCurrentScore(GameInfo.SelectedLevel, -modNumber);
        if(score <= 0){
            return 0;
            //level was either failed or not attempted
        }
        else if(modScore <= 0){
            return 0;
            //level was beat on base
        }
        else if(reverseModScore <= 0){
            return 1;
            //level was beat on base mod
        }else{
            return 2;
            //level was beat on reverse mod
        }
    }

    public static void unlockAchievements(int stage, int modifier, int damage){
        if(stage == 1 && !Achievements.GetAchievement(2)){
            Achievements.UnlockAchievement(2);
        }

        if(modifier == 1 && !Achievements.GetAchievement(5)){
            Achievements.UnlockAchievement(5);
        }

        if(modifier == 2 && !Achievements.GetAchievement(6)){
            Achievements.UnlockAchievement(6);
        }

        if(modifier == -1 && !Achievements.GetAchievement(7)){
            Achievements.UnlockAchievement(7);
        }

        if(modifier == -5 && !Achievements.GetAchievement(8)){
            Achievements.UnlockAchievement(8);
        }

        if(modifier == -3 && !Achievements.GetAchievement(9) && damage == 0){
            Achievements.UnlockAchievement(8);
        }

        if(stage == 8 && modifier == -6 && !Achievements.GetAchievement(11)){
            Achievements.UnlockAchievement(11);
        }
    }
}
