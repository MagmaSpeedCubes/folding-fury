using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollbarScript : MonoBehaviour
{
    [SerializeField] private string statName; // The name of the stat to control
    [SerializeField] private float minValue = 0f; // Minimum value of the stat
    [SerializeField] private float maxValue = 1f; // Maximum value of the stat
    [SerializeField] private Scrollbar scrollbar; // Reference to the Scrollbar component

    void Start()
    {
        if (scrollbar != null)
        {
            // Start a coroutine to wait for data to load before initializing the scrollbar
            StartCoroutine(InitializeScrollbar());
        }
    }

    private IEnumerator InitializeScrollbar()
    {
        // Wait until the data is loaded (replace with your actual loading condition)
        while (!SaveSystem.IsDataLoaded) // Replace with your actual data loading flag
        {
            yield return null; // Wait for the next frame
        }

        // Temporarily remove the listener to prevent triggering OnScrollbarValueChanged
        scrollbar.onValueChanged.RemoveListener(OnScrollbarValueChanged);

        // Initialize the scrollbar value based on the current stat value
        float currentStatValue = GetStatValue(statName);
        scrollbar.value = Mathf.InverseLerp(minValue, maxValue, currentStatValue);

        // Re-add the listener after initialization
        scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
    }

    public void OnScrollbarValueChanged(float value)
    {
        // Map the scrollbar value (0 to 1) to the stat range (minValue to maxValue)
        float statValue = Mathf.Lerp(minValue, maxValue, value);

        // Update the stat value
        SetStatValue(statName, statValue);
    }

    private float GetStatValue(string statName)
    {
        var field = typeof(AvatarInfo).GetField(statName);
        if (field != null)
        {
            Debug.Log("Retrieved value: " + field.GetValue(null));
            return (float)field.GetValue(null); // Assuming AvatarInfo fields are static
        }
        else
        {
            Debug.LogError($"Field '{statName}' not found in AvatarInfo.");
            return 0f; // Default value if the field is not found
        }
    }

    private void SetStatValue(string statName, float statValue)
    {
        var field = typeof(AvatarInfo).GetField(statName);
        if (field != null)
        {
            field.SetValue(null, statValue);
            SaveSystem.SaveData();
            Debug.Log("Successfully updated value: " + statValue);
        }
        else
        {
            Debug.LogError($"Field '{statName}' not found in AvatarInfo.");
        }
    }
}