using UnityEngine;


public class Collect : MonoBehaviour
{

    [SerializeField] private Canvas dialogue;



    [SerializeField] private Sprite bossFightKey;
    [SerializeField] private Sprite paperclip; 
    [SerializeField] private Sprite goldPaperclip; 
    [SerializeField] private Sprite Card1;
    [SerializeField] private Sprite Card2;
    [SerializeField] private Sprite Card3;
    [SerializeField] private Sprite Card4;
    [SerializeField] private Sprite Card5;
    [SerializeField] private Sprite Book;  
    [SerializeField] private Sprite Diamond;  

    //all possible loot

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRenderer spriteRenderer = collision.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            bool collected = false;

            
            if(spriteRenderer.sprite == bossFightKey){
                BossFight.StartBossFight();
                Destroy(collision.gameObject);
                collected = true;
                
            }else if(spriteRenderer.sprite == paperclip){
                AvatarInfo.Paperclips += 1;
                GameInfo.Score += 5;
                Destroy(collision.gameObject);
                collected = true;

            }else if(spriteRenderer.sprite == goldPaperclip){
                AvatarInfo.Paperclips += 5;
                GameInfo.Score += 25;   
                Destroy(collision.gameObject);
                collected = true;

            }else if(spriteRenderer.sprite == Book){
                AvatarInfo.Books += 1;
                GameInfo.Score += 1000;      
                Destroy(collision.gameObject);
                collected = true;

            }else if(spriteRenderer.sprite == Card1){
                AvatarInfo.Cards += 1;
                GameInfo.Score += 100;     
                Destroy(collision.gameObject);
                collected = true;

            }else if(spriteRenderer.sprite == Card2){
                AvatarInfo.Cards += 2;
                GameInfo.Score += 200;    
                Destroy(collision.gameObject);
                collected = true;

            }else if(spriteRenderer.sprite == Card3){
                AvatarInfo.Cards += 3;
                GameInfo.Score += 400;    
                Destroy(collision.gameObject);
                collected = true;

            }else if(spriteRenderer.sprite == Card4){
                AvatarInfo.Cards += 4;
                GameInfo.Score += 800;     
                Destroy(collision.gameObject);
                collected = true;

            }else if(spriteRenderer.sprite == Card5){
                AvatarInfo.Cards += 5;
                GameInfo.Score += 100;    
                Destroy(collision.gameObject);
                collected = true;
            }else if(spriteRenderer.sprite == Diamond){
                //special item
                GameInfo.DiamondsCollected += 1;
                GameInfo.Score += 1000 * GameInfo.DiamondsCollected;    
                Destroy(collision.gameObject);
                collected = true;
            }
            if(collected){
                PlayerSound.PlayCollect();
            }
        }
    }
}