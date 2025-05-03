using UnityEngine;

public class CancelUpgrade : MonoBehaviour
{
    [SerializeField] public Canvas upgradePage;

    private static CancelUpgrade instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }


    public void OnMouseClick(){
        UpgradePage.HideUpgradePage();
        Debug.Log("Successfully Upgraded");
    }

}
