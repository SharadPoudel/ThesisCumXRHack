using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
     // Maximum number of items the basket can hold
    public int capacity = 5;
    // Current number of collected items
    private int itemCount = 0;



    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the tag "Collectible"
        if (other.gameObject.CompareTag("Collectible"))
        {
            // Check if the basket is full
            if (itemCount < capacity)
            {
                // Call the method to collect the item
                CollectItem(other.gameObject);
            }
            else
            {
                Debug.Log("Basket is full. Cannot collect more items.");
            }
        }
    }

        private void CollectItem(GameObject item)
    {
        // Increment the item count
        itemCount++;

        // You can add logic here to update score or inventory
        Debug.Log("Item collected: " + item.name);

        // Register the collected item with the GameManager
        GameManager.Instance.RegisterCollectedItem(item);

        // Destroy the collected item
        Destroy(item);
    }

}
