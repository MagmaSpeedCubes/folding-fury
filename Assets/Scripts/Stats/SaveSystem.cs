using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    public static bool IsDataLoaded = false;
    public static void SaveData()
    {
        SaveInfo data = new SaveInfo
        {
            Paperclips = AvatarInfo.Paperclips,
            Books = AvatarInfo.Books,
            Cards = AvatarInfo.Cards,
            SFXVolume = AvatarInfo.SFXVolume,
            MusicVolume = AvatarInfo.MusicVolume,
            Upgrades = AvatarInfo.Upgrades,
            Achievements = AvatarInfo.Achievements,
            HighScores = SaveInfo.generateAStringFromAnArray(AvatarInfo.HighScores),
            ReversedHighScores = SaveInfo.generateAStringFromAnArray(AvatarInfo.ReversedHighScores)
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
        Debug.Log("Data Saved");
    }

    public static bool LoadData()
    {
        if (PlayerPrefs.HasKey("SaveData"))
        {
            string json = PlayerPrefs.GetString("SaveData");
            SaveInfo data = JsonUtility.FromJson<SaveInfo>(json);

            AvatarInfo.Paperclips = data.Paperclips;
            AvatarInfo.Books = data.Books;
            AvatarInfo.Cards = data.Cards;
            AvatarInfo.SFXVolume = data.SFXVolume;
            Debug.Log("Stored music volume:"+data.MusicVolume);
            AvatarInfo.MusicVolume = data.MusicVolume;
            AvatarInfo.Upgrades = data.Upgrades;
            AvatarInfo.Achievements = data.Achievements;
            AvatarInfo.HighScores = SaveInfo.loadAnArrayFromString(data.HighScores);
            AvatarInfo.ReversedHighScores = SaveInfo.loadAnArrayFromString(data.ReversedHighScores);
            Debug.Log("Save successfully loaded");
            IsDataLoaded = true;
            return true;
        }
        else
        {
            Debug.Log("No save data found.");
            return false;

        }
    }
}
