using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{

    public GameObject craftingScreenUI;
    public GameObject toolsScreenUi;

    public List<string> InventoryItemList = new List<string>();
    //Category buttons
    Button toolsBTN;

    //craft buttons 
    Button craftAxeBTN;
    // requirment TEXT (for crafting item requirments)
    Text AxeReq1, AxeReq2;

    public bool isOpen;

    // ALL blueprint
    public blueprint AxeBLP = new blueprint("Axe", 2, "Stone", 3, "Stick", 3);



    public static CraftingSystem instance {get; set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created\

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        isOpen = false;

        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { openToolsCategory();});

        // AXE
        AxeReq1 = toolsScreenUi.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = toolsScreenUi.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();

        craftAxeBTN = toolsScreenUi.transform.Find("Axe").transform.Find("CraftButton").GetComponent <Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });
    }

    void CraftAnyItem(blueprint blueprintToCraft)
    {
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);

        if (blueprintToCraft.numOfRequirments == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
        }
        else if (blueprintToCraft.numOfRequirments == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }

        StartCoroutine(calculate());


        RefreshNeededItems();

    }

    public IEnumerator calculate()
    {
        yield return new WaitForSeconds(1f);

        InventorySystem.Instance.ReCalculateList();
    }

    void openToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUi.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // to update inventory after pickup items or removing in every frame
        RefreshNeededItems();

        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {

            
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUi.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            
            isOpen = false;
        }
    }

    private void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;

        InventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in InventoryItemList)
        {
            switch (itemName)
            {
                case "Stone":
                    stone_count += 1;
                    break;

                case "Stick":
                    stick_count += 1;
                    break;
            }
        }

        // AXE

        AxeReq1.text = "3 Stone[" + stone_count + "]";
        AxeReq2.text = "3 Stick[" + stick_count + "]";

        if (stone_count >= 3 && stick_count >= 3)
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);    
        }


    }
}
