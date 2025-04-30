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

    public void Start(){
        Instance.canvasGroup.alpha = 0f;
    }



    public static void CompleteStage(bool win){
        
        CameraMoveUp.inLevel = false;
        Instance.canvasGroup.alpha = 1f;
        if(win){
            Instance.mainText.text = "Stage Complete";
        }else{
            Instance.mainText.text = "Game Over";
        }

        
        GameInfo.GameMode = -999;

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

        

        Instance.fscoreText.text = "Final Score: " + GameInfo.Score;

        



        



        
        //add delay later

        //summary
        //diamonds collected,more = bonus pts
        //enemies killed,more = bonus pts
        //damage taken,less = bonus pts
        //

        
    }

    public static void getCurrentScore(int level, int modifier){
        int[] scoreArray;


    }
}
