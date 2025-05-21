using UnityEngine;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] private float scrollSpeed; // Speed of scrolling (percentage of screen height per second)
    [SerializeField] private Canvas canvas;
    [SerializeField] private AudioClip creditsMusic;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioSource audio;

    private float scrollMultiplier;
    private bool scrolling;
    private Vector3 initialPosition;

    private static CreditsScript instance;

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
        // Calculate scroll speed based on screen height
        scrollMultiplier = Screen.height / 100f;

        // Set the initial position to be below the screen
        RectTransform rectTransform = GetComponent<RectTransform>();
        float screenHeight = Screen.height;
        initialPosition = new Vector3(Screen.width/2f, 0, transform.position.z);

        // Apply the initial position
        transform.position = initialPosition;

        canvas.enabled = false;
        scrolling = false;
    }

    void Update()
    {
        if (scrolling)
        {
            // Scroll the text upward
            transform.position += new Vector3(0, scrollSpeed * scrollMultiplier * Time.deltaTime, 0);
        }
    }

    public void ResetPosition(){
                // Calculate scroll speed based on screen height
        scrollMultiplier = Screen.height / 100f;

        // Set the initial position to be below the screen
        RectTransform rectTransform = GetComponent<RectTransform>();
        float screenHeight = Screen.height;
        initialPosition = new Vector3(Screen.width/2f, 0, transform.position.z);

        // Apply the initial position
        transform.position = initialPosition;
    }

    public static void ScrollCredits()
    {
        instance.ResetPosition();
        
        GameInfo.GameMode = -20;
        Debug.Log("Game Mode:" + GameInfo.GameMode);

        // Enable the canvas and reset the position
        instance.canvas.enabled = true;
        instance.transform.position = instance.initialPosition;
        instance.scrolling = true;

        // Play the credits music
        if (!instance.audio.isPlaying)
        {
            instance.audio.loop = true;
            instance.audio.clip = instance.creditsMusic;
            instance.audio.volume = AvatarInfo.MusicVolume;
            instance.audio.Play();
        }
    }

    public static void StopScrolling()
    {
        GameInfo.GameMode = 0;
        Debug.Log("Game Mode:" + GameInfo.GameMode);

        // Disable the canvas and stop scrolling
        instance.canvas.enabled = false;
        instance.scrolling = false;
        instance.transform.position = instance.initialPosition;

        // Stop the audio
        instance.audio.Stop();
    }
}