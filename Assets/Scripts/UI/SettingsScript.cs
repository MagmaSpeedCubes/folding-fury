using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{

    [SerializeField] private Canvas canvas;


    private static SettingsScript instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.canvas.enabled = false;

        }
        else
        {
            Destroy(gameObject);
        }
    }



    public static void OpenSettings()
    {

        instance.canvas.enabled = true;
  


    }

    public static void CloseSettings()
    {
        instance.canvas.enabled = false;
    }
}