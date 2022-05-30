using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOnOff : MonoBehaviour
{
    private GameObject inventory;
    private void Start(){
        inventory = FindObjectOfType<Inventory>().gameObject;
        inventory = inventory.transform.GetChild(0).gameObject;
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            inventory.SetActive(!inventory.activeSelf);
        }
    }
}
