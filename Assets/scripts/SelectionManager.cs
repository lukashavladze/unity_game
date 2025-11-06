using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    // sxva failshic rom davitriot aqedan obieqtebi magitom shevqmenit es
    public static SelectionManager Instance { get; set; }


    public bool onTarget;

    //creating game object so when we pick up item it doesnot pick other items also when 2-3 objects are near to each other
    public GameObject selectedObject;

    public GameObject interaction_Info_UI;
    Text interaction_text;

    private void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }
    
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

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject Interactable = selectionTransform.GetComponent<InteractableObject>();

            if (Interactable && Interactable.playerInRange)
            {
                onTarget = true;
                selectedObject = Interactable.gameObject;
                interaction_text.text = Interactable.GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else // if there is a hit, but without an interactable script
            {
                onTarget= false;
                interaction_Info_UI.SetActive(false);
            }

        }

        else // if there is no hit at all
        {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
        }
    }
}