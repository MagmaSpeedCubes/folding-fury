using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    public static void SaveData()
    {
        SaveInfo data = new SaveInfo
        {
            Paperclips = AvatarInfo.Paperclips,
            Books = AvatarInfo.Books,
            Cards = AvatarInfo.Cards,
            SFXVolume = AvatarInfo.SFXVolume,
            MusicVolume = AvatarInfo.MusicVolume,
            HighScores = SaveInfo.generateAStringFromAnArray(AvatarInfo.HighScores),
            ReversedHighScores = SaveInfo.generateAStringFromAnArray(AvatarInfo.ReversedHighScores)
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
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
            AvatarInfo.MusicVolume = data.MusicVolume;
            AvatarInfo.HighScores = SaveInfo.loadAnArrayFromString(data.HighScores);
            AvatarInfo.ReversedHighScores = SaveInfo.loadAnArrayFromString(data.ReversedHighScores);
            Debug.Log("Save successfully loaded");
            return true;
        }
        else
        {
            Debug.Log("No save data found.");
            return false;

        }
    }
}
