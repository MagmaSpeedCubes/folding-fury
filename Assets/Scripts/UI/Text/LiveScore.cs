using UnityEngine;
using TMPro;
using System;

public class LiveScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] Canvas canvas;
    
    void Start()
    {
          txt.text = "";
    }
    void Update()
    {
        if (GameInfo.GameMode > 0)
        {
            canvas.enabled = true;
            txt.text = ""+GameInfo.Score;


        }else{
            canvas.enabled = false;
            txt.text = "";
        }
    }



}