using UnityEngine;
using System.Collections.Generic;


public class Achievements : MonoBehaviour
{


    [SerializeField] private List<GameObject> AchievementButtons;
    
    private static Achievements instance;

    private void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    private void Start(){
        OnActivate();
    }

    public static void OnActivate(){
        for(int i=0; i<instance.AchievementButtons.Count; i++){
            AchievementButton ab = instance.AchievementButtons[i].GetComponent<AchievementButton>();
            bool earned;
            if(GetAchievement(i)){
                Debug.Log("Loaded Achievement "+i);
                earned = true;
                
            }else{
                earned = false;
            }

            ab.Show(true);

        }
    }

    public static bool GetAchievement(int index){
        return AvatarInfo.Achievements[index];
    }

    public static void UnlockAchievement(int index){
        Debug.Log("Unlocked Achievement "+index);
        AvatarInfo.Achievements[index] = true;
        SaveSystem.SaveData();
    }
}
