using UnityEngine;
[CreateAssetMenu]
public class Projectile : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private string attackType;
    [SerializeField] private float hits;
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Sprite projectileSprite;

    void Start(){
        ProjectileDamage projectileDamage = projectile.GetComponent<ProjectileDamage>();
        if( projectileDamage != null){
            projectileDamage.damage = this.damage;
            projectileDamage.attackType = this.attackType;
            projectileDamage.hits = this.hits;
            projectileDamage.speed = this.speed;
            projectileDamage.attackCooldown = this.attackCooldown;
            projectileDamage.player = this.player;

        }
        projectile.GetComponent<SpriteRenderer>().sprite = projectileSprite;
    }


    
}
