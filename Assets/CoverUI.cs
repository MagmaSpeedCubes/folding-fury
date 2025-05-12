using UnityEngine;

public class CoverUI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        if(GameInfo.GameMode == -3 || GameInfo.GameMode > 0){
            canvas.enabled = false;
        }else if(GameInfo.GameMode != -4){
            canvas.enabled = true;
        }
    }
}
