using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using TMPro;
[RequireComponent(typeof(AudioSource))]
public class ModButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    
    private static ModButton instance;
    private AudioSource audio;
    private float xpos;
    private float ypos;
    private Coroutine waveTextCoroutine; 
    private Coroutine waveTextCoroutine2;
    private Coroutine spinCoroutine; 
    private Coroutine oscillateCoroutine; 
    
    

    public AudioClip locked;
    public AudioClip select;
    public AudioClip deselect;
    public AudioClip reverse;
    public AudioClip unlock;

    

    public static bool modSelected = false;

    Quaternion originalRotation;

    [SerializeField] private int mod;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unselected;
    [SerializeField] private Sprite selected;
    [SerializeField] private Sprite reversed;
    [SerializeField] private Button button;

    [SerializeField] private GameObject CentralMod;
    [SerializeField] private TextMeshProUGUI ModTextObject;
    [SerializeField] private TextMeshProUGUI ReverseModTextObject;
    [SerializeField] private TextMeshProUGUI ModSubtextObject;


    [SerializeField] private string modText;
    [SerializeField] private string modSubtext;
    [SerializeField] private string reverseModText;
    [SerializeField] private string reverseModSubtext;

    private int state = 0; // 0 = locked, 1 = unselected, 2 = selected, 3 = reversed

    private bool isHolding = false;
    private bool wasHeld = false; // Tracks if the button was held long enough to trigger OnMouseHold
    private float holdTime = 0f; // Tracks how long the mouse is held down
    private const float holdThreshold = 1f; // Time in seconds to trigger OnMouseHold
    

    private void Start()
    {
        originalRotation = transform.rotation;
        instance = this;
        xpos = transform.position.x;
        ypos = transform.position.y;
        audio = GetComponent<AudioSource>();




        if (AvatarInfo.HighScores[GameInfo.SelectedLevel][0] > 0)
        {
            button.GetComponent<Image>().sprite = unselected;
            state = 1;
        }
        else
        {
            button.GetComponent<Image>().sprite = lockedSprite;
            state = 0;
        }
    }

    private void Update()
    {
        if (AvatarInfo.HighScores[GameInfo.SelectedLevel][0] > 0)
        {
            if (state == 0)
            {
                state = 1;
                instance.audio.loop = false;
                audio.volume = AvatarInfo.SFXVolume;
                audio.clip = unlock;
                audio.Play();
            }
        }else{
            button.GetComponent<Image>().sprite = lockedSprite;
            state = 0;
        }


        // Handle hold logic
        if (isHolding)
        {

            holdTime += Time.deltaTime; // Increment hold time
            if (holdTime >= holdThreshold)
            {
                OnMouseHold();
                wasHeld = true; // Mark as held
                isHolding = false; // Prevent multiple activations
            }
        }else{
            if(state!=3){
                transform.rotation = originalRotation;
            }
        }
    }


    public void OnMouseClick()
    {
        if (wasHeld)
        {
            wasHeld = false;
            return;
        }

        if (state == 0)
        {
            button.GetComponent<Image>().sprite = lockedSprite;
            instance.audio.loop = false;
            audio.volume = AvatarInfo.SFXVolume;
            audio.clip = locked;
            audio.Play();
        }
        else if (state == 1)
        {
            if(AvatarInfo.HighScores[GameInfo.SelectedLevel][0] > 0){
                selectMod();
            }
            
        }
        else if (state == 2 || state == 3)
        {
            deselectMod();
        }
    }


    public void OnMouseHold()
    {
        if (state == 1)
        {
            if(AvatarInfo.HighScores[GameInfo.SelectedLevel][mod] >= 0){
                selectReverseMod();
            }
        }
    }



    private IEnumerator moveTo(float x, float y, float tme){
        Vector3 startPosition = transform.position; // Starting position
        Vector3 targetPosition = new Vector3(x, y, 0); // Target position
        float elapsedTime = 0f; // Time elapsed since the start of the movement

        while (elapsedTime < tme)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Interpolate position based on elapsed time
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / tme);

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final position is exactly the target position
        transform.position = targetPosition;
    }

    

    private IEnumerator SpinButton(float maxSpeed, float acceleration)
    {
        float currentSpeed = 0f; // Start with no rotation

        while (isHolding && state == 1) // Spin while the button is held and in the unselected state
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed); // Increase speed up to maxSpeed
            transform.Rotate(0, 0, currentSpeed * Time.deltaTime); // Rotate the button
            yield return null; // Wait for the next frame
        }

        // Reset to the original orientation when the spinning stops
        transform.rotation = originalRotation;
    }

    

    private IEnumerator OscillateRotation(float centerAngle, float range, float speed)
    {
        while (state == 3) // Continue oscillating while the reverse mod is selected
        {
            // Calculate the target angle using Mathf.PingPong
            float angle = centerAngle + Mathf.Sin(Time.time * speed) * range;

            // Apply the rotation
            transform.rotation = Quaternion.Euler(0, 0, angle);

            yield return null; // Wait for the next frame
        }

        // Reset to the original orientation when the oscillation stops
        transform.rotation = originalRotation;
    }



    private IEnumerator WaveTextEffect(TextMeshProUGUI text, float waveDuration, float interval)
    {
        while (state == 3) // Continue while the reverse mod is selected
        {
            yield return StartCoroutine(waveText(text, waveDuration)); // Perform the wave effect
            yield return new WaitForSeconds(interval - waveDuration); // Wait for the remaining interval time
        }
    }

    private IEnumerator waveText(TextMeshProUGUI text, float tme)
    {
        string originalText = text.text;
        for (int i = 0; i < originalText.Length; i++)
        {
            if(state != 1){
                text.text = originalText.Substring(0, i + 1) + "  " + originalText.Substring(i + 1);   
            }else{
                break;
            }
            yield return new WaitForSeconds(tme / originalText.Length);
        }
    }


    private IEnumerator revealText(TextMeshProUGUI text, float tme){
        string originalText = text.text;
        for(int i=0; i<originalText.Length; i++){
            if(state != 1){
                text.text = originalText.Substring(0, i+1);
            }else{
                break;
            }
            yield return new WaitForSeconds(tme/originalText.Length);
        }
    }
        
    public void selectReverseMod()
    {
        if(!modSelected){
            button.GetComponent<Image>().sprite = reversed;
            state = 3;
            instance.audio.loop = false;
            audio.volume = AvatarInfo.SFXVolume;
            audio.clip = reverse;
            audio.Play();
            float x = CentralMod.transform.position.x;
            float y = CentralMod.transform.position.y;
            StartCoroutine(moveTo(x, y, 0.15f));
            ModTextObject.text = reverseModText;
            ReverseModTextObject.text = reverseModText;
            ModSubtextObject.text = reverseModSubtext;
            StartCoroutine(revealText(ModTextObject, 0.5f));
            StartCoroutine(revealText(ReverseModTextObject, 0.5f));
            StartCoroutine(revealText(ModSubtextObject, 2f));
            ModSubtextObject.color = new Color(1, 0, 0, 1);
            GameInfo.SelectedModifier = -mod;

            // Start the wave effect coroutine if it's not already running
            if (waveTextCoroutine == null)
            {
                waveTextCoroutine = StartCoroutine(WaveTextEffect(ModTextObject, 1.5f, 1.5f));
                waveTextCoroutine2 = StartCoroutine(WaveTextEffect(ReverseModTextObject, 1.5f, 1.5f));
            }

            if (oscillateCoroutine == null)
            {
                oscillateCoroutine = StartCoroutine(OscillateRotation(180f, 10f, 2f)); 
            }
            modSelected = true;
        }

    }



    public void selectMod(){
        if(!modSelected){
            button.GetComponent<Image>().sprite = selected;
            state = 2;
            instance.audio.loop = false;
            audio.volume = AvatarInfo.SFXVolume;
            audio.clip = select;
            audio.Play();
            GameInfo.SelectedModifier = mod;
            ModTextObject.text = modText;
            ModSubtextObject.text = modSubtext;
            ModSubtextObject.color = new Color(1, 1, 1, 1);
            modSelected = true;
        }        
    }

    public void deselectMod()
    {
        
        button.GetComponent<Image>().sprite = unselected;
        instance.audio.loop = false;
        audio.volume = AvatarInfo.SFXVolume;
        audio.clip = deselect;
        audio.Play();


        if (state == 3)
        {
            StartCoroutine(moveTo(xpos, ypos, 0.05f));
            if (waveTextCoroutine != null)
            {
                StopCoroutine(waveTextCoroutine);
                StopCoroutine(waveTextCoroutine2);
                waveTextCoroutine = null;
                waveTextCoroutine2 = null;
            }

            if (oscillateCoroutine != null) {
            StopCoroutine(oscillateCoroutine);
            oscillateCoroutine = null;
             }
                        
        }
        state = 1;
        clearText();

        GameInfo.SelectedModifier = 0;
        
        modSelected = false;


    }



    public void clearText(){
        ModTextObject.text = "";
        ReverseModTextObject.text = "";
        ModSubtextObject.text = "";
    }



    // Detect when the mouse button is pressed down
// Detect when the mouse button is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        wasHeld = false; // Reset the hold flag
        holdTime = 0f; // Reset hold time

        // Start spinning the button
        if (spinCoroutine == null)
        {
            spinCoroutine = StartCoroutine(SpinButton(2880f, 1440f)); // Max speed: 360 degrees/sec, Acceleration: 180 degrees/sec²
        }
    }

    // Detect when the mouse button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        holdTime = 0f; // Reset hold time

        // Stop spinning the button
        if (spinCoroutine != null)
        {
            StopCoroutine(spinCoroutine);
            spinCoroutine = null;
            transform.rotation = originalRotation;
        }
    }
}