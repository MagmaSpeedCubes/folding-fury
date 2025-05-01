using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lights : MonoBehaviour{
    [SerializeField] private CanvasGroup cg;
    private Coroutine ct;
    public static bool active;

    public static Lights Instance {get; private set;}

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    public static void activate(){
        if(Instance!=null && Instance.ct==null){
            Lights.active = true;
            Instance.ct = Instance.StartCoroutine(Instance.Flicker(Instance.cg, 0.5f, 1f, 0.5f, 3f));
        
        }
    }
    public IEnumerator Flicker(CanvasGroup canvasGroup, float fadeInTime, float waitTime, float fadeOutTime, float holdTime)
    {
        while(Lights.active){
            // Fade In
            float elapsedTime = 0f;
            while (elapsedTime < fadeInTime)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeInTime); // Gradually increase alpha
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);
            // Fade Out
            elapsedTime = 0f;
            while (elapsedTime < fadeOutTime)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeOutTime)); // Gradually decrease alpha
                yield return null;
            }
            if(GameInfo.GameMode < 1 && GameInfo.GameMode != -3){
                Lights.active = false;
            }
            yield return new WaitForSeconds(holdTime);
        }

    }
}
