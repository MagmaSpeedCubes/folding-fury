using UnityEngine;

public class ConfirmUpgrade : MonoBehaviour
{
    [SerializeField] public Canvas upgradePage;
    public int paperclipCost;
    public int cardCost;
    public string statName;
    public float statBoost;
    public int index;

    private static ConfirmUpgrade instance;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseClick(){
        if(AvatarInfo.Paperclips >= paperclipCost && AvatarInfo.Cards >= cardCost){
            AvatarInfo.Paperclips -= paperclipCost;
            AvatarInfo.Cards -= cardCost;
            SetStatValue(statName, GetStatValue(statName)+statBoost);
            upgradePage.enabled = false;
            AvatarInfo.Upgrades[index] += 1;
            UpgradePage.HideUpgradePage();
            SaveSystem.SaveData();
            Debug.Log("Successfully Upgraded");


        }else{
            Debug.Log("Failed to upgrade");
        }
    }

    public static void SetAction(string statName, float statBoost, int paperclipCost, int cardCost, int index){
        instance.statName = statName;
        instance.statBoost = statBoost;
        instance.cardCost = cardCost;
        instance.paperclipCost = paperclipCost;
        instance.index = index;

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
