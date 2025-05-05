using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour
{

    [SerializeField] private Sprite locked;
    [SerializeField] private Sprite unlocked;
    [SerializeField] private string name;
    [SerializeField] private string description;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public void OnClick(){
        nameText.text = name;
        descriptionText.text = description;
    }

    public void Show(bool isUnlocked){
        if(isUnlocked){
            gameObject.GetComponent<Image>().sprite = unlocked;
        }else{
            gameObject.GetComponent<Image>().sprite = locked;
        }
    }
}
