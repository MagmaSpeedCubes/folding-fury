using UnityEngine;
[System.Serializable]

public class AvatarInfo : MonoBehaviour
{
    public static int Paperclips;
    public static int Books;
    public static int Cards;
    

    public static float SFXVolume;
    public static float MusicVolume;

    public static int[][] HighScores;
    public static int[][] ReversedHighScores; 

    public static int[] Upgrades;

    public static bool [] Achievements;


    public AvatarInfo(){
        Paperclips = 0;
        Books = 0;
        Cards = 0;

        SFXVolume = 1f;
        MusicVolume = 1f;


        HighScores = new int[GameInfo.NumLevels+1][];
        ReversedHighScores = new int[GameInfo.NumLevels+1][];

        Upgrades = new int[9] {0, 0, 0, 0, 0, 0, 0, 0, 0};

        Achievements = new bool[12] {false, false, false, false, false, false, false, false, false, false, false, false};
        

        for(int i=1; i<=GameInfo.NumLevels; i++){
            HighScores[i] = new int[] {-1, -1, -1, -1, -1, -1, -1};
            ReversedHighScores[i] = new int[] {0, -1, -1, -1, -1, -1, -1}; //number at index 0 will store the number of attempts
        }
    }










    public void Start(){
        bool loaded = SaveSystem.LoadData();
        if(!loaded){
            SaveSystem.SaveData();
            Debug.Log("No data found, created new save");
        }
        // if(info!=null){
        //     Paperclips = info.Paperclips;
        //     Books = info.Books;
        //     Cards = info.Cards;

        //     SFXVolume = info.SFXVolume;
        //     MusicVolume = info.MusicVolume;
        //     Debug.Log("Retrieved data file");
        // }else{
        //     SaveSystem.SaveData();
        //     Debug.Log("Created new data file");
        // }
    }
    // void Start()
    // {
    //     SaveSystem.SaveData(); // Save data
    //     SaveInfo loadedData = SaveSystem.LoadData(); // Load data

    //     if (loadedData != null)
    //     {
    //         Debug.Log("Data loaded successfully!");
    //     }
    // }


    // the data file needs to be replaced every time format is changed

}
