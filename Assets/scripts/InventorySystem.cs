using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    // this list will contain slots not items
    public List<GameObject> slotLitst = new List<GameObject>();

    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;

    public bool isOpen;

    //public bool isFull;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;

        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        //searching all the children of inventoryscreenui. because we dont want all elements to be child in inventoryscreenui, we are going to tag slots in unity.
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotLitst.Add(child.gameObject);
            }
        }
    }



    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B) && !isOpen)
        {

            Debug.Log("B is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.B) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }


    public void AddToInventory(string itemName)
    {
      
        whatSlotToEquip = FindNextEmptySlot();

        itemToAdd = (GameObject)Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        itemList.Add(itemName);
        
    }

    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotLitst)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }        
        }
        return new GameObject();
    }

    public bool CheckifFull()
    {
        int counter = 0;

        foreach(GameObject slot in slotLitst)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }       
        }
        if (counter == 21)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}