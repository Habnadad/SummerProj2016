using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler //fire these interfaces during beginning, during, and end drag handlers. We need to call a method from this interface before it will work
{
    public Item item;
    public int amount;
    public int slotNumber;

    private Inventory inventoryObject;
    private Vector2 offset;

    void Start()
    {
        inventoryObject = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    //at the beginning of the click-to-drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            //create an offset so that the mouse won't drag from the center of the item's icon
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);

            //set the parent to the slot panel so that all items go up front instead of the order that the slots are made
            this.transform.SetParent(this.transform.parent.parent);

            //make the item move according to the mouse
            this.transform.position = eventData.position - offset;

            //after the drag begin is detected, block the raycast
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            //make the item move according to the mouse and subtract it to offset so that it doesn't snap to the center of item
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inventoryObject.slotList[slotNumber].transform);             //set the parent to the slotnumber object and transform based on the current slot we're at
        this.transform.position = inventoryObject.slotList[slotNumber].transform.position;    //set the position of item to the assigned slot
        GetComponent<CanvasGroup>().blocksRaycasts = true;                                    //unblock raycast
    }
}
