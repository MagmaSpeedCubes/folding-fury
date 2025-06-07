using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveOnUnfocus : MonoBehaviour
{

    void OnApplicationQuit() {
        PlayerPrefs.Save();
    }

    void OnApplicationFocus(bool hasFocus) {
        if (!hasFocus) PlayerPrefs.Save();
    }
}
