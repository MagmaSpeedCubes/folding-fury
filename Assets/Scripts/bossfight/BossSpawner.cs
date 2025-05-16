using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemiesPerCluster = 5;
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private float clusterCooldown = 5f;
    [SerializeField] private Vector2 spawnDirection = Vector2.right;
    [SerializeField] private bool targetPlayer;


    [SerializeField] private Sprite enemySprite;
    [SerializeField] private Sprite angrySprite;
    [SerializeField] private Sprite enragedSprite;
    [SerializeField] private Sprite damagedSprite;

    [SerializeField] private float health = 100f;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float spawnSpeed = 5f;

    private Transform player = PlayerInfo.playerTransform;

    [SerializeField] private float enemyLoot = 0f;
    
    public bool active = false;


    private bool isSpawning = false; // Tracks if the spawner is currently active
    private float orbitAngle = 0f; // Current angle of the spawner in the orbit

    private Coroutine spawnCoroutine; // Tracks the currently running SpawnClusters coroutine

    void Update()
    {
        
        TargetPlayer();
        if (active && !isSpawning)
        {
            spawnCoroutine = StartCoroutine(SpawnClusters());
        }


    }

    private void TargetPlayer()
    {
        if (player == null || !targetPlayer) return; // Ensure the player reference is valid

        // Calculate the direction to the player
        GameObject decoy = GameInfo.decoy;
        if(decoy != null){
            Vector2 directionToPlayer = (decoy.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            //target the decoy if present
        }else{
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            //target the player if no decoy present
        }

    }

    private IEnumerator SpawnClusters()
    {
        
        isSpawning = true;

        while (active)
        {
            TargetPlayer();
            // Spawn a cluster of enemies
            for (int i = 0; i < enemiesPerCluster * GameInfo.SpawnRate; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next enemy
            }

            // Wait for the cooldown before the next cluster
            yield return new WaitForSeconds(clusterCooldown);
        }

        isSpawning = false; // Mark the spawner as inactive
    }

    private void SpawnEnemy()
    {
        if (targetPlayer)
        {
            GameObject decoy = GameInfo.decoy;
            if (decoy != null)
            {
                spawnDirection = (decoy.transform.position - transform.position).normalized;
            }
            else
            {
                spawnDirection = (player.position - transform.position).normalized;
            }
        }

        // Calculate the spawn position with an offset in the spawning direction
        float spawnOffset = 2f; // Adjust this value as needed to avoid collisions
        Vector3 spawnPosition = transform.position + (Vector3)spawnDirection * spawnOffset;

        // Instantiate the enemy at the calculated position
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
        EnemyMovement em = enemy.GetComponent<EnemyMovement>();
        ProjectileDamage pd = enemy.GetComponent<ProjectileDamage>();

        if (eh != null && em != null)
        {
            eh.damagedSprite = damagedSprite;
            eh.maxHealth = health * GameInfo.EnemyHealth;
            pd.damage = damage * GameInfo.EnemyDamage;

            int enemystate = Random.Range(1, 101);
            if (enemystate <= GameInfo.EnragedRate * 100)
            {
                eh.mainSprite = enragedSprite;
                eh.maxHealth *= 2f;
                em.SetMovement(spawnDirection, spawnSpeed * GameInfo.EnemySpeed * 2f);
                Debug.Log("Enraged Enemy");
                enemy.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0f, 0f, 1 - GameInfo.EnemyTransparency);
            }
            else if (enemystate <= GameInfo.AngryRate * 100)
            {
                eh.mainSprite = angrySprite;
                eh.maxHealth *= 1.5f;
                Debug.Log("Angry Enemy");
                em.SetMovement(spawnDirection, spawnSpeed * GameInfo.EnemySpeed * 1.5f);
                enemy.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f, 1 - GameInfo.EnemyTransparency);
            }
            else
            {
                eh.mainSprite = enemySprite;
                em.SetMovement(spawnDirection, spawnSpeed * GameInfo.EnemySpeed);
                Debug.Log("Normal Enemy");
                enemy.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1 - GameInfo.EnemyTransparency);
            }
        }

        enemy.GetComponent<LootBag>().localLootMultiplier = enemyLoot;

        enemy.GetComponent<SpriteRenderer>().sprite = enemySprite;
        GameInfo.numEnemies += 1;
        GameInfo.EnemiesSpawned += 1;
    }

}