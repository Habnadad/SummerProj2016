using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public int id;
    private Inventory invObject;

    void Start()
    {
        //allows access to the object from inventory
        invObject = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //When the player drops the item, get the itemdata
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

        //if there is no item in this slot
        if (invObject.itemList[id].ID == -1)
        {

            //clear out the item slot before setting assigning it an id
            invObject.itemList[droppedItem.slotNumber] = new Item();

            //set item's id to the same as slot number
            invObject.itemList[id] = droppedItem.item;

            //set the slot id 
            droppedItem.slotNumber = id;
        }

        //else if there is an item in the slot, swap the two items, their parents/positions/ID's
        else
        {
            //create an object for the item in this slot (there is only one child object in this case)
            Transform item = this.transform.GetChild(0);

            item.GetComponent<ItemData>().slotNumber = droppedItem.slotNumber;                          //item's slot number is now that of the old item's slot number
            item.transform.SetParent(invObject.slotList[droppedItem.slotNumber].transform);             //swap parents of the current item and the old item that you're replacing
            item.transform.position = invObject.slotList[droppedItem.slotNumber].transform.position;    //set positions

            droppedItem.slotNumber = id;
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;

            //swap 
            invObject.itemList[droppedItem.slotNumber] = item.GetComponent<ItemData>().item;            //the old item's slot number is now the current item's slot number
            invObject.itemList[id] = droppedItem.item;
        }
    }
}
