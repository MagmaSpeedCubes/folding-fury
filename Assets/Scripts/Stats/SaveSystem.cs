using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static bool IsDataLoaded = false;

    public static bool DataLoaded => IsDataLoaded; // Public getter for IsDataLoaded

    public static void SaveData()
{
    if (IsDataLoaded) // Only save if the data has been loaded
    {
        SaveInfo data = new SaveInfo(
            AvatarInfo.Paperclips,
            AvatarInfo.Books,
            AvatarInfo.Cards,
            AvatarInfo.SFXVolume,
            AvatarInfo.MusicVolume,
            AvatarInfo.Upgrades,
            AvatarInfo.Achievements,
            SaveInfo.generateAStringFromAnArray(AvatarInfo.HighScores),
            SaveInfo.generateAStringFromAnArray(AvatarInfo.ReversedHighScores)
        );

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
        Debug.Log("Data Saved");
    }
    else
    {
        Debug.LogWarning("Attempted to save data before it was loaded.");
    }
}

    public static bool LoadData()
    {
        if (PlayerPrefs.HasKey("SaveData"))
        {
            string json = PlayerPrefs.GetString("SaveData");
            SaveInfo data = JsonUtility.FromJson<SaveInfo>(json);

            AvatarInfo.Paperclips = data.Paperclips;
            Debug.Log("Saved Paperclips:"+data.Paperclips);
            AvatarInfo.Books = data.Books;
            AvatarInfo.Cards = data.Cards;
            AvatarInfo.SFXVolume = data.SFXVolume;
            Debug.Log("Stored music volume:" + data.MusicVolume);
            AvatarInfo.MusicVolume = data.MusicVolume;
            AvatarInfo.Upgrades = data.Upgrades;
            AvatarInfo.Achievements = data.Achievements;
            AvatarInfo.HighScores = SaveInfo.loadAnArrayFromString(data.HighScores);
            AvatarInfo.ReversedHighScores = SaveInfo.loadAnArrayFromString(data.ReversedHighScores);
            Debug.Log("Save successfully loaded");
            IsDataLoaded = true; // Mark data as loaded
            return true;
        }
        else
        {
            Debug.Log("No save data found.");
            return false;
        }
    }
}