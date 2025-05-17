using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePage : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI upgradeTitle;
    [SerializeField] private TextMeshProUGUI upgradeInfo;
    [SerializeField] private TextMeshProUGUI paperclipCost;
    [SerializeField] private TextMeshProUGUI cardCost;
    [SerializeField] private Button confirmUpgrade;

    private static UpgradePage instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas.enabled = false;
    }

    // Update is called once per frame


    public static void SetUpgradePage(string statName, float statLevel, float increase, int pCost, int cCost, int index){
        instance.canvas.enabled = true;

        instance.upgradeTitle.text = "Upgrade " + statName + "?";

        instance.upgradeInfo.text = "" + statLevel + " --> " + (statLevel + increase);

        instance.paperclipCost.text = "" + pCost + " Paperclips";

        instance.cardCost.text = "" + cCost + " Cards";

        ConfirmUpgrade.SetAction(statName, increase, pCost, cCost, index);
    }

    public static void HideUpgradePage(){
        instance.canvas.enabled = false;
    }

}
