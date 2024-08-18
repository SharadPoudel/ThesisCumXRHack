using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   // Singleton instance
    public static GameManager Instance { get; private set; }

    // List to store collected items
    private List<GameObject> collectedItems = new List<GameObject>();

    private void Awake()
    {
        // Ensure that there is only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to register a collected item
    public void RegisterCollectedItem(GameObject item)
    {
        collectedItems.Add(item);
        Debug.Log("Registered collected item: " + item.name);
    }

    // Method to get the list of collected items
    public List<GameObject> GetCollectedItems()
    {
        return collectedItems;
    }

}
