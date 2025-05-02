using UnityEngine;

public class Mods
{
    public static string Reactivate()
    {

        Debug.Log("Activation");
        if(PlayerInfo.Modifier == 1){
            Angry.activate();
            return "Angry Paper";
        }else if(PlayerInfo.Modifier == 2){
            Blurred.activate();
            return "Blurred Senses";
        }else if(PlayerInfo.Modifier == 3){
            Lights.activate();
            return "Flickering Lights";
        }else if(PlayerInfo.Modifier == 4){
            Volatile.activate();
            return "Volatile Spawners";
        }else if(PlayerInfo.Modifier == 5){
            Half.activate();
            return "Glass Half Full";
        }else if(PlayerInfo.Modifier == 6){
            Expert.activate();
            return "Expert Mode";
        }

        else if(PlayerInfo.Modifier == -1){
            FumingAshes.activate();
            return "Fuming Ashes";
        }else if(PlayerInfo.Modifier == -2){
            Delusion.activate();
            return "Delusion";
        }else if(PlayerInfo.Modifier == -3){
            Blackout.activate();
            return "Blackout";
        }else if(PlayerInfo.Modifier == -4){
            Proliferation.activate();
            return "Proliferation";
        }else if(PlayerInfo.Modifier == -5){
            BleedingHeart.activate();
            return "Bleeding Heart";
        }else if(PlayerInfo.Modifier == -6){
            TrainedProfessional.activate();
            return "Trained Professional";
        }

        return "";


        // fix later

    }
    public static string GetModName()
    {

        if(PlayerInfo.Modifier == 1){
            return "Angry Paper";
        }else if(PlayerInfo.Modifier == 2){
            return "Blurred Senses";
        }else if(PlayerInfo.Modifier == 3){
            return "Flickering Lights";
        }else if(PlayerInfo.Modifier == 4){
            return "Volatile Spawners";
        }else if(PlayerInfo.Modifier == 5){
            return "Glass Half Full";
        }else if(PlayerInfo.Modifier == 6){
            return "Expert Mode";
        }else if(PlayerInfo.Modifier == -1){
            return "Fuming Ashes";
        }else if(PlayerInfo.Modifier == -2){
            return "Delusion";
        }else if(PlayerInfo.Modifier == -3){
            return "Blackout";
        }else if(PlayerInfo.Modifier == -4){
            return "Proliferation";
        }else if(PlayerInfo.Modifier == -5){
            return "Bleeding Heart";
        }else if(PlayerInfo.Modifier == -6){
            return "Trained Professional";
        }

        return "";


        // fix later

    }

    public static void Deactivate(){
        Lights.deactivate();
        Blackout.deactivate();
    }
}
