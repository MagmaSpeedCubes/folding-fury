using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
public class CameraMoveUp : MonoBehaviour
{

    //1-16 = respective levels
    //0 = level selector
    //-2 = credits
    // card upgrades
    // forms
    // achievements
    [SerializeField] private Canvas dialogue;

    



    [SerializeField] private float moveSpeed = 1f; // Speed at which the camera moves upwards
    [SerializeField] private Transform levelSelector;
    [SerializeField] private List<GameObject> levels;
    [SerializeField] private List<TextMeshPro> levelTexts;
    [SerializeField] private List<Canvas> CanvasList;
    public CanvasGroup fadeCanvasGroup;
    [SerializeField] private Transform prologue;
    //0 = level selector
    //-1 = mod selector
    
    private Coroutine fade;

    public static bool inLevel = false;
    private Vector3 targetPosition;

    public static CameraMoveUp Instance { get; private set; } // Singleton instance

    private void Awake()
    {
        // Ensure only one instance of CameraMoveUp exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
        if(inLevel && !GameInfo.BossFight){
            fade = null;
            targetPosition = transform.position + Vector3.up * moveSpeed * Time.deltaTime;
        }else if(GameInfo.GameMode>0 && !GameInfo.BossFight){
            PlayerInfo.NewLevel();
            Timer.Reset();
            ShowCanvas(CanvasList[2]);
            StartCoroutine(FadeCanvas(fadeCanvasGroup, 0.5f, 0.5f, 0f, null));

            Debug.Log("Started Stage "+GameInfo.GameMode+" with modifier "+PlayerInfo.Modifier);
            inLevel = true;
            
            Vector3 offset = new Vector3(0, 0, -10);
            targetPosition = levels[GameInfo.GameMode - 1].GetComponent<Transform>().position + offset;
            string mod = Mods.GetModName();

            
            AvatarInfo.ReversedHighScores [GameInfo.GameMode] [0] += 1;
            
            int numAttempts = AvatarInfo.ReversedHighScores [GameInfo.GameMode] [0];
            TextMeshPro attemptText = levelTexts[GameInfo.GameMode - 1];
            if(attemptText != null){
                attemptText.text = "Attempt " + numAttempts +  "<br>" + mod;
            }else{
                Debug.Log("No object found");
            }


            SaveSystem.SaveData();


            


            
        }else if(GameInfo.BossFight){

        }else if(GameInfo.GameMode == 0){
            HideCanvases();
            ShowCanvas(CanvasList[0]);
        }else if(GameInfo.GameMode == -1){
            ShowCanvas(CanvasList[2]);
            if(fade == null){
                fade = StartCoroutine(FadeCanvas(fadeCanvasGroup, 0.5f, 0.5f, 0f, CanvasList[1]));
            }
            
            
            ShowCanvas(CanvasList[1]);
            
        }else if (GameInfo.GameMode == -3){
            PlayerInfo.NewLevel();
            Timer.Reset();
            ShowCanvas(CanvasList[2]);
            StartCoroutine(FadeCanvas(fadeCanvasGroup, 0.5f, 0.5f, 0f, null));
            Debug.Log("Started Prologue");
            inLevel = true;

            

            Vector3 offset = new Vector3(0, 0, -10);
            targetPosition = prologue.position + offset;
            
            Mods.Reactivate();


        }else if(GameInfo.GameMode == -4){

            HideCanvases();
            ShowCanvas(CanvasList[3]);

        }
        transform.position = targetPosition;
    

        

    }
    private void ShowCanvas(Canvas canvas){
        canvas.enabled = true;
    }
    private void HideCanvases(){
        foreach(Canvas c in CanvasList){
            c.enabled = false;
        }
        ShowCanvas(CanvasList[2]);
    }


    public IEnumerator FadeCanvas(CanvasGroup canvasGroup, float fadeInTime, float fadeOutTime, float holdTime, Canvas secondCanvas)
    {
        // Fade In
        float elapsedTime = 0f;
        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeInTime); // Gradually increase alpha
            yield return null;
        }


        // Hold at full opacity
                    
            
        canvasGroup.alpha = 1f;
        HideCanvases();
        ShowCanvas(CanvasList[2]);
        if(secondCanvas != null){
            ShowCanvas(secondCanvas);
        }

        
        yield return new WaitForSeconds(holdTime);
        

        // Fade Out
        elapsedTime = 0f;
        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1f - (elapsedTime / fadeOutTime)); // Gradually decrease alpha
            yield return null;
        }

        // Ensure fully transparent at the end
        canvasGroup.alpha = 0f;
        if(GameInfo.GameMode == -3 || GameInfo.GameMode > 0){
            FollowMouse.NewLevel();
        }
    }
}