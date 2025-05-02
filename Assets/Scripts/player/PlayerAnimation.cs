using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private List<Sprite> Fan;
    [SerializeField] private List<Sprite> Shield;
    [SerializeField] private List<Sprite> Airplane;
    [SerializeField] private List<Sprite> Sword;
    [SerializeField] private List<Sprite> Unfolded;
    [SerializeField] private List<Sprite> Box;
    [SerializeField] private List<Sprite> Shuriken;
    [SerializeField] private List<Sprite> Crane;
    [SerializeField] private List<Sprite> FortuneTeller;
    [SerializeField] private int frames = 4; // Number of frames in the animation
    private int tick = 0;

    private void Start()
    {
        // Start the animation coroutine
        StartCoroutine(AnimatePlayer());
    }

    private IEnumerator AnimatePlayer()
    {
        while (true)
        {
            if(FormChangeScript.canFormChange){
                // Get the current animation based on the player's form
                List<Sprite> currentAnimation = GetCurrentAnimation();

                if (currentAnimation != null && currentAnimation.Count > 0 && PlayerInfo.Form != "Unfolded")
                {
                    // Update the sprite to the next frame
                    tick = (tick + 1) % currentAnimation.Count;
                    gameObject.GetComponent<SpriteRenderer>().sprite = currentAnimation[tick];
                }

                

            }
            // Wait for the next frame based on the attack rate
            float delay = 1f / (frames * PlayerInfo.AttackRate);
            yield return new WaitForSeconds(delay);
            

        }
    }

    private List<Sprite> GetCurrentAnimation()
    {
        if (PlayerInfo.Form == "Shuriken")
        {
            return Shuriken;
        }
        else if (PlayerInfo.Form == "Fan")
        {
            return Fan;
        }
        else if (PlayerInfo.Form == "Shield")
        {
            return Shield;
        }
        else if (PlayerInfo.Form == "Airplane")
        {
            return Airplane;
        }
        else if (PlayerInfo.Form == "Sword")
        {
            return Sword;
        }
        else if (PlayerInfo.Form == "Unfolded")
        {
            return Unfolded;
        }
        else if (PlayerInfo.Form == "Box")
        {
            return Box;
        }
        else if (PlayerInfo.Form == "Crane")
        {
            return Crane;
        }
        else if (PlayerInfo.Form == "FortuneTeller")
        {
            return FortuneTeller;
        }
        else
        {
            return null;
        }
    }
}