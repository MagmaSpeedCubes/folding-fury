using UnityEngine;
using System.Collections.Generic;

public class BossFight : MonoBehaviour
{
    public List <int> bossTimes = new List<int> {100, 100, 100, 100, 100, 100, 100, 100};
    //change for balance purposes later
    public static int bossBonus = 0;
    public static int fullBonus = 0;

    void update(){
        
        if(bossBonus > 0){
            float subtract = fullBonus/bossTimes[GameInfo.GameMode]*Time.deltaTime;
            bossBonus -= (int) subtract;
        }

        //subtracts the boss bonus every frame until it reaches 0
        //boss bonus starts at the full amount and decreases linearly according to boss times
    }
    
    public static void StartBossFight(){
        GameInfo.BossFight = true;
        if(PlayerInfo.Modifier == -6){
            bossBonus = 100000;
            fullBonus = 100000;
            PlayerInfo.Resistance = -999999;
            PlayerInfo.InkResistance = -999999;
            PlayerInfo.KnifeResistance = -999999;
            //if playing with the trained professional modifier, make all damage fatal
        }else if(PlayerInfo.Modifier == 6){
            bossBonus = 20000;
            fullBonus = 20000;
        }

    }
}