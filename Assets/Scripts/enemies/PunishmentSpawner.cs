using System.Collections;
using UnityEngine;

public class PunishmentSpawner : MonoBehaviour
{
    [SerializeField] private float activationTime;
    [SerializeField] private int level;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemiesPerCluster = 5;
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private float clusterCooldown = 5f;
    [SerializeField] private int spawnCycles = 1;
    [SerializeField] private Vector2 spawnDirection = Vector2.right;

    [SerializeField] private Sprite enemySprite;
    [SerializeField] private Sprite angrySprite;
    [SerializeField] private Sprite enragedSprite;
    [SerializeField] private Sprite damagedSprite;

    [SerializeField] private float health = 100f;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float spawnSpeed = 5f;

    [SerializeField] private Transform player; // Reference to the player
    [SerializeField] private float orbitRadius = 5f; // Radius of the orbit
    [SerializeField] private float orbitSpeed = 30f; // Speed of the orbit (degrees per second)

    private bool isSpawning = false; // Tracks if the spawner is currently active
    private bool hasActivated = false; // Tracks if the spawner has already activated for the current condition
    private float orbitAngle = 0f; // Current angle of the spawner in the orbit

    private Coroutine spawnCoroutine; // Tracks the currently running SpawnClusters coroutine

    void Update()
    {
        OrbitPlayer();

        // Check if the spawner should activate
        if (GameInfo.GameMode == level && Timer.GetTime() >= activationTime && !hasActivated)
        {
            hasActivated = true; // Prevent multiple activations for the same condition

            // Stop any existing coroutine to prevent duplicates
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }

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
    private void OrbitPlayer()
    {
        // Increment the orbit angle based on the orbit speed
        orbitAngle += orbitSpeed * Time.deltaTime;
        if (orbitAngle >= 360f)
        {
            orbitAngle -= 360f;
        }

        // Calculate the new position of the spawner
        float radians = orbitAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * orbitRadius;
        transform.position = player.position + offset;
    }

    private IEnumerator SpawnClusters()
    {
        
        isSpawning = true;

        for (int cycle = 0; cycle < spawnCycles; cycle++)
        {
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

    private Coroutine singleClusterCoroutine; // Tracks the currently running SpawnSingleClusterCoroutine

    public void SpawnSingleCluster()
    {
        // Prevent multiple instances of the coroutine
        if (singleClusterCoroutine != null)
        {
            StopCoroutine(singleClusterCoroutine);
        }

        singleClusterCoroutine = StartCoroutine(SpawnSingleClusterCoroutine());
    }

    private IEnumerator SpawnSingleClusterCoroutine()
    {
        isSpawning = true;

        // Spawn a single cluster of enemies
        for (int i = 0; i < enemiesPerCluster; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // Wait before spawning the next enemy
        }

        isSpawning = false; // Mark the spawner as inactive
        singleClusterCoroutine = null; // Clear the reference
    }
    private void SpawnEnemy()
    {
        spawnDirection = (player.position - transform.position).normalized;
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
        enemy.GetComponent<SpriteRenderer>().sprite = enemySprite;
        GameInfo.numEnemies += 1;
        GameInfo.EnemiesSpawned += 1;
    }
}