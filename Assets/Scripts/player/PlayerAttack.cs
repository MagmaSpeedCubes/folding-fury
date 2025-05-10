using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackTransform;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private Material attackRangeMaterial; // Material for the attack range visualization

    private bool isAttacking = false;

    private void Update()
    {
        if (!isAttacking) // Only attack if not on cooldown
        {
            StartCoroutine(AttackWithCooldown());
        }
    }

    private IEnumerator AttackWithCooldown()
    {
        isAttacking = true; // Set attacking state


        
        if (Random.Range(1, 101) > PlayerInfo.MissRate * 100)
        {
            // Create a new attack range visualization

            
            Attack(); // Perform the attack
            if(PlayerInfo.Modifier != -2){
                GameObject attackRangeObject = CreateAttackRangeMesh();
                StartCoroutine(FadeOutAttackRange(attackRangeObject));
            }
            
        }
        else
        {
            Debug.Log("miss");
        }

        // Start fading out the attack range
        
        

        yield return new WaitForSeconds(1 / PlayerInfo.AttackRate); // Wait for cooldown
        isAttacking = false; // Reset attacking state
    }

    private GameObject CreateAttackRangeMesh()
    {
        int segments = 30; // Number of segments for the arc
        float angleStep = PlayerInfo.AttackAngle / segments;

        Vector3[] vertices = new Vector3[segments + 2];
        int[] triangles = new int[segments * 3];

        // The first vertex is the center of the arc
        vertices[0] = Vector3.zero;

        // Generate vertices for the arc
        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = -PlayerInfo.AttackAngle / 2 + i * angleStep;
            Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * attackTransform.up;
            vertices[i + 1] = direction * PlayerInfo.AttackRange;
        }

        // Generate triangles for the mesh
        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        // Create a new GameObject for the attack range
        GameObject attackRangeObject = new GameObject("AttackRange");
        attackRangeObject.transform.SetParent(transform);
        attackRangeObject.transform.localPosition = Vector3.zero;

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = attackRangeObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = attackRangeObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(attackRangeMaterial); // Create a new instance of the material

        // Create and assign the mesh
        Mesh attackRangeMesh = new Mesh();
        attackRangeMesh.vertices = vertices;
        attackRangeMesh.triangles = triangles;
        attackRangeMesh.RecalculateNormals();
        meshFilter.mesh = attackRangeMesh;

        return attackRangeObject;
    }

    private IEnumerator FadeOutAttackRange(GameObject attackRangeObject)
    {
        MeshRenderer meshRenderer = attackRangeObject.GetComponent<MeshRenderer>();
        Material material = meshRenderer.material;

        Color color = material.color;
        float fadeDuration = 1 / PlayerInfo.AttackRate; // Duration of the fade-out
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Gradually reduce alpha
            material.color = color;
            yield return null;
        }

        // Destroy the attack range object after fading out
        Destroy(attackRangeObject);
    }

    private void Attack()
    {
        // Get all colliders within the attack range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackTransform.position, PlayerInfo.AttackRange, attackableLayer);

        foreach (Collider2D collider in colliders)
        {
            // Calculate the direction to the target
            Vector2 directionToTarget = (collider.transform.position - attackTransform.position).normalized;

            // Calculate the angle between the player's forward direction and the target
            float angleToTarget = Vector2.Angle(attackTransform.up, directionToTarget);

            // Check if the target is within the attack angle
            if (angleToTarget <= PlayerInfo.AttackAngle / 2)
            {
                // Apply damage if the target implements IDamageable
                IDamageable idamageable = collider.GetComponent<IDamageable>();
                if (idamageable != null)
                {
                    idamageable.Damage(PlayerInfo.AttackDamage); // Pass damageAmount
                }

                // Apply knockback if the target has a Rigidbody2D
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    float knockbackForce = PlayerInfo.Knockback; // Get knockback force from PlayerInfo
                    rb.AddForce(directionToTarget * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }

        if(PlayerInfo.Form == "Box"){
            Box.boxAttack();
        }else if(PlayerInfo.Form == "FortuneTeller"){
            FortuneTeller.fortuneTellerAttack();
        }
    }
}