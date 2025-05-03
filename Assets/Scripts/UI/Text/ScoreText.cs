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
            m_Object.text = "" + EndStage.getCurrentScore(GameInfo.SelectedLevel, 0);

        }
    }



}