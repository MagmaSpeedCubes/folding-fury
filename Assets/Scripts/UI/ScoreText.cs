using UnityEngine;
using TMPro;
using System;

public class ScoreText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_Object;
    
    void Start()
    {
          m_Object.text = "";
    }
    void Update()
    {
        if (GameInfo.SelectedLevel > 0)
        {
            m_Object.text = "" + getCurrentScore(GameInfo.SelectedLevel, 0);

        }
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
}