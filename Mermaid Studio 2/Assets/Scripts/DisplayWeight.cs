using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayWeight : MonoBehaviour
{
    [SerializeField] InventoryObject pcInventory;

    TextMeshProUGUI weigthText;

    Sprite sprite;
    
    void Start()
    {
        if (GetComponent<Image>().sprite != null)
        {
            sprite = GetComponent<Image>().sprite;
        }
        weigthText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdateDisplay();
    }
    void UpdateDisplay()
    {
        weigthText.text = pcInventory.TotalWeigth.ToString();
    }
}
