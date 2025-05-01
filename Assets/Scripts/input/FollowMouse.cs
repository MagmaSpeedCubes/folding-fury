using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Camera mainCamera;

    private static bool active;


    void Start(){
        mainCamera = Camera.main;
    }

    void Update(){
        if(MouseInWindow() && CameraMoveUp.inLevel){
            if(active){
                FollowMousePositionDelayed(PlayerInfo.MovementSpeed);
            }else if(GameInfo.GameMode > 0 || GameInfo.GameMode == -3){
                FollowMousePosition();
            }
        }else if(!CameraMoveUp.inLevel){
            active = false;
        }

    }

    public static void NewLevel(){
        active = true;
        
    }

    private void FollowMousePositionDelayed(float maxSpeed){
        transform.position = Vector2.MoveTowards(transform.position, GetWorldPositionFromMouse(), maxSpeed * Time.deltaTime);
    }

    private void FollowMousePosition(){
        transform.position = GetWorldPositionFromMouse();
    }

    private Vector2 GetWorldPositionFromMouse(){
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    bool MouseInWindow(){
        Vector2 view = mainCamera.ScreenToViewportPoint( Input.mousePosition );
        bool isOutside = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;
        return !isOutside;
    }

}