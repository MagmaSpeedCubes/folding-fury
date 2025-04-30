using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
    public class DialogueTrigger : MonoBehaviour
    {

        [SerializeField] private Canvas dialogue;
        [SerializeField] private bool isFirstTrigger;

        void OnTriggerEnter2d(Collider other)
        {
            Debug.Log("t1");
            Dialogue d = dialogue.GetComponent<Dialogue>();
            if(d!=null){
                if(isFirstTrigger){
                    d.SetIndexFromLevel();
                }
                Debug.Log("t2");
                d.NextLine();
            }
        }

    }