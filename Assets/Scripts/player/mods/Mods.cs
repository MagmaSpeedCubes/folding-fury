using UnityEngine;

public class Mods
{
    public static void Reactivate()
    {
        if(PlayerInfo.Modifier == 1){
            Angry.activate();
            Debug.Log("Angry");
        }else if(PlayerInfo.Modifier == 2){
            Blurred.activate();
            Debug.Log("Blurred");
        }else if(PlayerInfo.Modifier == 3){
            Lights.activate();
            Debug.Log("Lights");
        }else if(PlayerInfo.Modifier == 4){
            Volatile.activate();
            Debug.Log("Volatile");
        }else if(PlayerInfo.Modifier == 5){
            Half.activate();
            Debug.Log("Half");
        }else if(PlayerInfo.Modifier == 6){
            Expert.activate();
            Debug.Log("Expert");
        }

        else if(PlayerInfo.Modifier == -1){
            FumingAshes.activate();
            Debug.Log("Fuming Ashes");
        }else if(PlayerInfo.Modifier == -2){
            Delusion.activate();
            Debug.Log("Delusion");
        }else if(PlayerInfo.Modifier == -3){
            Blackout.activate();
            Debug.Log("Blackout");
        }else if(PlayerInfo.Modifier == -4){
            Proliferation.activate();
            Debug.Log("Proliferation");
        }else if(PlayerInfo.Modifier == -5){
            BleedingHeart.activate();
            Debug.Log("Bleeding Heart");
        }else if(PlayerInfo.Modifier == -6){
            TrainedProfessional.activate();
            Debug.Log("Trained Professional");
        }


        // fix later

    }
}
