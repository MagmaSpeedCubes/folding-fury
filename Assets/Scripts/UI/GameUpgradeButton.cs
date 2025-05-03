using UnityEngine;
using System;

public class GameUpgradeButton : MonoBehaviour
{
    [SerializeField] private int upgradeNumber; //the index that will be stored in the array
    [SerializeField] private string statName; //the stat that the upgrade effects
    [SerializeField] private int upgradeCost; //the cost in paperclips of the upgrade
    [SerializeField] private float baseStat; //the value the stat starts as
    [SerializeField] private float upgradeIncrease; //the stat boost achieved by the upgrade
    private int currentLevel; //the current level of the upgrade

    void Start()
    {
        
        currentLevel = AvatarInfo.Upgrades[upgradeNumber];
        SetStatValue(statName, baseStat + currentLevel * upgradeIncrease);
    }

    // Update is called once per frame
    void Update()
    {
        currentLevel = AvatarInfo.Upgrades[upgradeNumber];
        SetStatValue(statName, baseStat + currentLevel * upgradeIncrease);
    }


    public void OnClick(){
        UpgradePage.SetUpgradePage(statName, baseStat + currentLevel * upgradeIncrease, upgradeIncrease, paperclipCost(), cardCost(), upgradeNumber);

    }

    private int paperclipCost(){
        return currentLevel * upgradeCost;
    }

    private int cardCost(){
        if(currentLevel%5 == 0){
            return (int) (upgradeCost/250 + 1);
        }
        return 0;
    }



    private float GetStatValue(string statName)
    {
        var field = typeof(GameInfo).GetField(statName);
        if (field != null)
        {
            return (float)field.GetValue(null); // Assuming GameInfo fields are static
        }
        else
        {
            Debug.LogError($"Field '{statName}' not found in GameInfo.");
            return 0f; // Default value if the field is not found
        }
    }


    private void SetStatValue(string statName, float statValue)
    {
        var field = typeof(GameInfo).GetField(statName);
        if (field != null)
        {
            field.SetValue(null, statValue); // Assuming GameInfo fields are static
            Debug.Log($"Field '{statName}' set to {statValue}.");
        }
        else
        {
            Debug.LogError($"Field '{statName}' not found in GameInfo.");
        }
    }


}
