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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        canvas.enabled = false;
    }


    public void OpenSettings(){
        canvas.enabled = true;
    }

    public void CloseSettings(){
        canvas.enabled = false;
    }


}