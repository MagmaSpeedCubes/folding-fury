using UnityEngine;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshPro healthText; // Reference to the TextMeshPro component

    private void Update()
    {
        // Update the text box with the rounded value of PlayerInfo.CurrentHealth
        healthText.text = "" + PlayerInfo.CurrentHealth.ToString("F2")+"%";
    }
}