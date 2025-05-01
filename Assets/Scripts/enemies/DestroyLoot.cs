using UnityEngine;

public class DestroyLoot : MonoBehaviour
{
    void Update(){
        if(!CameraMoveUp.inLevel){
            Destroy(gameObject);
        }
    }
}
