using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ModBadges : MonoBehaviour
{

    [SerializeField] private Sprite locked;
    [SerializeField] private Sprite unlocked;
    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite reversed;
    [SerializeField] private int modNumber;


    // Update is called once per frame
    void Update()
    {


        int score = EndStage.getCurrentScore(GameInfo.SelectedLevel, 0);
        int modScore = EndStage.getCurrentScore(GameInfo.SelectedLevel, modNumber);
        int reverseModScore = EndStage.getCurrentScore(GameInfo.SelectedLevel, -modNumber);
        if(score <= 0){
            gameObject.GetComponent<Image>().sprite = locked;
            //level was either failed or not attempted
        }
        else if(modScore <= 0){
            gameObject.GetComponent<Image>().sprite = unlocked;
            //level was beat on base
        }
        else if(reverseModScore <= 0){
            gameObject.GetComponent<Image>().sprite = normal;
            //level was beat on base mod
        }else{
            gameObject.GetComponent<Image>().sprite = reversed;
            //level was beat on reverse mod
        }
    }
}
