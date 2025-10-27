using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public bool playerInRange;

    public string ItemName;

    public string GetItemName()
    {
        return ItemName;
    }

    void Update()
    {
        // selectionmanager-idan on targeti imito gavaketet rom konkretul obieqtze roca miva mausi mashin aigos da ara shemogarenze daklikebit
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerInRange && SelectionManager.Instance.onTarget ) 
        {
            // if the inventory is not full
            if (!InventorySystem.Instance.CheckifFull())
            {
                InventorySystem.Instance.AddToInventory(ItemName);
                Destroy(gameObject);
            }
            else
            {              
                Debug.Log("Inventory is Full");
            }
         
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // we are using "player" because if other object for example rabbit enters pickable object zone, not to get name on screen
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
