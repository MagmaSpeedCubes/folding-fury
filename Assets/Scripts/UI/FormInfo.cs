using UnityEngine;
using TMPro;

public class FormInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI infoText;

    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private string info;

    public void OnClick(){
        nameText.text = name;
        descriptionText.text = description;
        infoText.text = info;
    }
}
