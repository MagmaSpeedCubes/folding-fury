using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public static float time = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    private void Update()
    {
        time += Time.deltaTime;
        textComponent.text = ""+time;
    }

    public static void Reset(){
        time = 0;
    }

    public static float GetTime(){
        return time;
    }
}
