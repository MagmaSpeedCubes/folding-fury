using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float enemyLoot = 1f;

    [SerializeField] private float activationTime;
    [SerializeField] private int level;

    [SerializeField] private GameObject enemyPrefab; 
    [SerializeField] private int enemiesPerCluster = 5; 
    [SerializeField] private float spawnInterval = 0.5f; 
    [SerializeField] private float clusterCooldown = 5f; 
    [SerializeField] private int spawnCycles = 3; 
    [SerializeField] private Vector2 spawnDirection = Vector2.right;
    
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private Sprite angrySprite;
    [SerializeField] private Sprite enragedSprite;
    [SerializeField] private Sprite damagedSprite;

    [SerializeField] private float health = 100f;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float spawnSpeed = 5f; 

    

    private bool isSpawning = false; // Tracks if the spawner is currently active
    private bool hasActivated = false; // Tracks if the spawner has already activated for the current condition

    private Coroutine spawnCoroutine; // Tracks the currently running SpawnClusters coroutine

    void Update()
    {
        // Check if the spawner should activate
        if (GameInfo.GameMode == level && Timer.GetTime() >= activationTime && !hasActivated)
        {
            hasActivated = true; // Prevent multiple activations for the same condition
            spawnCoroutine = StartCoroutine(SpawnClusters());
        }

        // Stop spawning and reset the activation flag if the GameMode changes
        if (GameInfo.GameMode != level)
        {
            hasActivated = false;

            // Stop the currently running coroutine if it exists
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null; // Clear the reference
            }

            isSpawning = false; // Ensure the spawner is marked as inactive
        }
    }


    private IEnumerator SpawnClusters()
    {
        isSpawning = true;

        for (int cycle = 0; cycle < spawnCycles; cycle++)
        {
            // Spawn a cluster of enemies
            for (int i = 0; i < enemiesPerCluster*GameInfo.SpawnRate; i++)
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
        // Instantiate the enemy at the spawner's position
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
        EnemyMovement em = enemy.GetComponent<EnemyMovement>();
        ProjectileDamage pd = enemy.GetComponent<ProjectileDamage>();
        if (eh != null && em != null)
        {
            eh.damagedSprite = damagedSprite;
            eh.maxHealth = health * GameInfo.EnemyHealth;
            pd.damage = damage * GameInfo.EnemyDamage;

            int enemystate = Random.Range(1, 101);
            if (enemystate <= GameInfo.EnragedRate*100)
            {
                eh.mainSprite = enragedSprite;
                eh.maxHealth *= 2f;
                em.SetMovement(spawnDirection, spawnSpeed * GameInfo.EnemySpeed * 2f);
                Debug.Log("Enraged Enemy");
                enemy.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0f, 0f, 1 - GameInfo.EnemyTransparency);
            }
            else if (enemystate <= GameInfo.AngryRate*100)
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

            enemy.GetComponent<LootBag>().localLootMultiplier = enemyLoot;
            
        }
        enemy.GetComponent<SpriteRenderer>().sprite = enemySprite;
        GameInfo.numEnemies += 1;
        GameInfo.EnemiesSpawned += 1;
    }
}