using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    private float multiplier = GameInfo.LootMultiplier;
    public float localLootMultiplier;
    public List<Loot> lootList = new List<Loot>();

    List<Loot> GetDroppedItems()
    {
        List<Loot> possibleItems = new List<Loot>();

        foreach (Loot item in lootList)
        {
            float adjustedDropChance = item.dropChance * multiplier * localLootMultiplier * PlayerInfo.PlayerLuck; // Adjust drop chance by multiplier

            // Add guaranteed drops for every 100 in adjustedDropChance
            while (adjustedDropChance >= 100)
            {
                possibleItems.Add(item);
                adjustedDropChance -= 100;
            }
            int full = 100;
            while(adjustedDropChance%1 >0){
                adjustedDropChance = adjustedDropChance * 10;
                full = full * 10;
            }
            // For the remainder, randomly decide if the item should drop
            if (Random.Range(1, full+1) <= adjustedDropChance)
            {
                possibleItems.Add(item);
            }
        }

        return possibleItems;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        List<Loot> droppedItems = GetDroppedItems();
        if (droppedItems != null && droppedItems.Count > 0)
        {
            foreach (Loot item in droppedItems)
            {
                GameObject loot = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
                loot.GetComponent<SpriteRenderer>().sprite = item.lootSprite;

                float dropForce = Random.Range(100f, 300f);
                Vector3 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                Rigidbody2D rb = loot.GetComponent<Rigidbody2D>();
                rb.AddForce(dropDirection * dropForce, ForceMode2D.Impulse);

                // Add drag to slow down the loot over time
                rb.linearDamping = 2f; // Adjust this value as needed
            }
        }
        
        GameInfo.Score += 100 * multiplier * localLootMultiplier * PlayerInfo.PlayerLuck;
    }

}
