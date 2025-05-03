using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
[System.Serializable]
public class SaveInfo
{
    public int Paperclips;
    public int Books;
    public int Cards;
    

    public float SFXVolume;
    public float MusicVolume;

    public string HighScores;
    public string ReversedHighScores;

    public int[] Upgrades;

    public bool [] Achievements;



    public SaveInfo(){
        Paperclips = AvatarInfo.Paperclips;
        Books = AvatarInfo.Books;
        Cards = AvatarInfo.Cards;

        SFXVolume = AvatarInfo.SFXVolume;
        MusicVolume = AvatarInfo.MusicVolume;

        Upgrades = AvatarInfo.Upgrades;

        Achievements = AvatarInfo.Achievements;

        HighScores = SaveInfo.generateAStringFromAnArray(AvatarInfo.HighScores);
        ReversedHighScores = SaveInfo.generateAStringFromAnArray(AvatarInfo.ReversedHighScores);




    }

    public static void CopyStream(Stream input, Stream output)
    // Helper funtion to copy from one stream to another
    {
        // Magic number is 2^16
        byte[] buffer = new byte[32768];
        int read;
        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            output.Write(buffer, 0, read);
        }
    }


    public static string Compress(string s)
    {
        var bytes = Encoding.UTF8.GetBytes(s);
        using (var msi = new MemoryStream(bytes))
        using (var mso = new MemoryStream())
        {
            using (var gs = new GZipStream(mso, CompressionMode.Compress))
            {

                CopyStream(msi, gs);
            }
            return Convert.ToBase64String(mso.ToArray());
        }
    }

    public static string Decompress(string s)
    {
        var bytes = Convert.FromBase64String(s);
        using (var msi = new MemoryStream(bytes))
        using (var mso = new MemoryStream())
        {
            using (var gs = new GZipStream(msi, CompressionMode.Decompress))
            {
                CopyStream(gs, mso);
            }
            return Encoding.UTF8.GetString(mso.ToArray());
        }
    }



    public static string generateAStringFromAnArray(int[][] arrayWeWantToSave)
    {
        string s = "";


        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, arrayWeWantToSave);
        
        s = Convert.ToBase64String(ms.ToArray());
        s = Compress(s);
        Debug.Log("Successfully stored high scores");
        return s;
    }

    public static int[][] loadAnArrayFromString(string s)
    {
        s = Decompress(s);

        BinaryFormatter bf = new BinaryFormatter();
        Byte[] by = Convert.FromBase64String(s);
        MemoryStream sr = new MemoryStream(by);
        Debug.Log("Successfully loaded high scores");

        return (int[][])bf.Deserialize(sr);

    }
    
}



    


