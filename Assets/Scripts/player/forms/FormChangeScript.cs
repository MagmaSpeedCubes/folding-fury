using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class FormChangeScript : MonoBehaviour
{

    [SerializeField] private List<Sprite> FanFold;
    [SerializeField] private List<Sprite> ShieldFold;
    [SerializeField] private List<Sprite> AirplaneFold;
    [SerializeField] private List<Sprite> SwordFold;
    [SerializeField] private List<Sprite> BoxFold;
    [SerializeField] private List<Sprite> ShurikenFold;
    [SerializeField] private List<Sprite> CraneFold;
    [SerializeField] private List<Sprite> FortuneTellerFold;
    
    public float delay;
    private bool canFormChange = true;
    private Coroutine animation;

    private List<Sprite> exitSprite;
    void Update()
    {
        //1 = fan
        //2 = shield
        //3 = airplane
        //4 = sword
        //5 = unfolded
        //6 = box
        //7 = shuriken
        //8 = crane
        //9 = fortune teller
        if(canFormChange){
            if (Input.GetKey("1")){
                StartCoroutine(ChangeToFan());
                canFormChange = false;
            }else if (Input.GetKey("2")){
                StartCoroutine(ChangeToShield());
                canFormChange = false;
            }else if (Input.GetKey("3")){
                StartCoroutine(ChangeToAirplane());
                canFormChange = false;
            }else if (Input.GetKey("4")){
                StartCoroutine(ChangeToSword());
                canFormChange = false;
            }else if (Input.GetKey("6")){
                StartCoroutine(ChangeToBox());
                canFormChange = false;
            }else if (Input.GetKey("7")){
                StartCoroutine(ChangeToShuriken());
                canFormChange = false;
            }else if (Input.GetKey("8")){
                StartCoroutine(ChangeToCrane());
                canFormChange = false;
            }else if (Input.GetKey("9")){
                StartCoroutine(ChangeToFortuneTeller());
                canFormChange = false;
            }
        }

    }
    private IEnumerator ChangeToFan(){
        if(animation==null){
            animation = StartCoroutine(Animate(exitSprite, FanFold, PlayerInfo.FoldTime, Fan.getDelay()));
        }
        
        exitSprite = FanFold;
       
        yield return new WaitForSeconds(Fan.getDelay()+PlayerInfo.FoldTime);
        Fan.Instantiate();  
        canFormChange = true;

        
    }


    private IEnumerator ChangeToAirplane(){
        if(animation==null){
            animation = StartCoroutine(Animate(exitSprite, AirplaneFold, PlayerInfo.FoldTime, Airplane.getDelay()));
        }

        exitSprite = AirplaneFold;

        yield return new WaitForSeconds(Airplane.getDelay()+PlayerInfo.FoldTime);
        Airplane.Instantiate();  
        canFormChange = true;
    }


    private IEnumerator ChangeToSword(){
        if(animation==null){
            animation = StartCoroutine(Animate(exitSprite, SwordFold, PlayerInfo.FoldTime, Sword.getDelay()));
        }

        exitSprite = SwordFold;

        yield return new WaitForSeconds(Sword.getDelay()+PlayerInfo.FoldTime);
        Sword.Instantiate(); 
        canFormChange = true; 
    }
    private IEnumerator ChangeToBox(){
        if(animation==null){
            animation = StartCoroutine(Animate(exitSprite, BoxFold, PlayerInfo.FoldTime, Box.getDelay()));
        }

        exitSprite = BoxFold;

        yield return new WaitForSeconds(Box.getDelay()+PlayerInfo.FoldTime);
        Box.Instantiate();  
        canFormChange = true;
    }
    private IEnumerator ChangeToShuriken(){
        if(animation==null){
            animation = StartCoroutine(Animate(exitSprite, ShurikenFold, PlayerInfo.FoldTime, Shuriken.getDelay()));
        }

        exitSprite = ShurikenFold;

        yield return new WaitForSeconds(Shuriken.getDelay()+PlayerInfo.FoldTime);
        Shuriken.Instantiate();  
        canFormChange = true;
    }
    private IEnumerator ChangeToCrane(){
        if(animation==null){
            animation = StartCoroutine(Animate(exitSprite, CraneFold, PlayerInfo.FoldTime, Crane.getDelay()));
        }

        exitSprite = CraneFold;

        yield return new WaitForSeconds(Crane.getDelay()+PlayerInfo.FoldTime);
        Crane.Instantiate();  
        canFormChange = true;
    }
    private IEnumerator ChangeToFortuneTeller(){
        if(animation==null){
            animation = StartCoroutine(Animate(exitSprite, FortuneTellerFold, PlayerInfo.FoldTime, FortuneTeller.getDelay()));
        }

        exitSprite = FortuneTellerFold;

        yield return new WaitForSeconds(FortuneTeller.getDelay()+PlayerInfo.FoldTime);
        FortuneTeller.Instantiate(); 
        canFormChange = true; 
    }
    private IEnumerator ChangeToShield(){
        if(animation==null){
            animation = StartCoroutine(Animate(exitSprite, ShieldFold, PlayerInfo.FoldTime, Shield.getDelay()));
        }

        exitSprite = ShieldFold;
        
        yield return new WaitForSeconds(Shield.getDelay()+PlayerInfo.FoldTime);
        Shield.Instantiate();  
        canFormChange = true;
    }

    private IEnumerator Animate(List<Sprite> first, List<Sprite> second, float firstTime, float secondTime){
        Debug.Log("animating");
        if(first!=null){
            for(int i=first.Count-1; i>=0; i--){
                gameObject.GetComponent<SpriteRenderer>().sprite = first[i];
                float delay = firstTime/first.Count;
                Debug.Log("first");
                yield return new WaitForSeconds(delay);
            }
        }else{
            yield return new WaitForSeconds(firstTime);
        }

        for(int i=0; i<second.Count; i++){
            gameObject.GetComponent<SpriteRenderer>().sprite = second[i];
            float delay = secondTime/second.Count;
            Debug.Log("second");
            yield return new WaitForSeconds(delay);
        }
        endAnimate();
        

    }

    private void endAnimate(){
        StopCoroutine(animation);
        animation = null;
    }

}