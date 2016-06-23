using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; //needed to make our lists

public class Inventory : MonoBehaviour
{
    GameObject inventoryScreen;
    GameObject slotsPanel;
    ItemDatabase database;

    //public so we can wire them up in Unity's inspector
    public GameObject slot;
    public GameObject item;

    public List<Item> itemList = new List<Item>();
    public List<GameObject> slotList = new List<GameObject>();

    int numberOfSlots;
    private bool isActive;
    private Component[] inventoryCanvas;

    void Start()
    {
        //create accessor for the item database
        database = GetComponent<ItemDatabase>();

        numberOfSlots = 35;
        isActive = true;

        //assign variables to elements in hierarchy
        inventoryScreen = GameObject.Find("Inventory Screen");
        slotsPanel = inventoryScreen.transform.FindChild("Slots Panel").gameObject; //grab gameobject listed as Slots Panel from the hierarchy

        //add blank inventory slots and items based on the number of available slots. 
        for (int i = 0; i < numberOfSlots; i++)
        {
            itemList.Add(new Item());
            slotList.Add(Instantiate(slot));
            slotList[i].GetComponent<InventorySlot>().id = i;
            slotList[i].transform.SetParent(slotsPanel.transform); //make the new slot a child of the slotsPanel object
        }

        AddItem(0);
        //AddItem(0);
        AddItem(1);
    }

    void Update()
    {
        if (Input.GetKeyUp("i"))
        {
            ToggleInventory();
        }

        if (Input.GetMouseButtonUp(1))
        {
            AddItem(0);
        }
    }

    //add a new item to an empty slot.
    public void AddItem(int id)
    {
        Item itemToAdd = database.GetItemWithID(id);

        //if the item is stackable, and exists in the inventory, set the text accordingly
        if (itemToAdd.Stackable == true && ItemInventoryCheck(itemToAdd) == true)
        {
            //loop through the list of items
            for (int i = 0; i < itemList.Count; i++)
            {
                //if the ID's match, add one to the item amount. 
                if (itemList[i].ID == id)
                {
                    ItemData data = slotList[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;

                    //set the text component to equal the item amount
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }

        else
        {
            //iterate through list of items
            for (int i = 0; i < itemList.Count; i++)
            {
                //if the ID is null, add the item
                if (itemList[i].ID == -1)
                {
                    itemList[i] = itemToAdd;
                    GameObject itemObject = Instantiate(item);
                    itemObject.GetComponent<ItemData>().item = itemToAdd;       //setup item data so it can be accessed from itemData script
                    itemObject.GetComponent<ItemData>().slotNumber = i;         //set this slot number to equal the item's ID
                    itemObject.transform.SetParent(slotList[i].transform);      //make the item a child of the first empty slot
                    itemObject.transform.position = Vector2.zero;               //set the position to the center anchor of the slot that it was assigned to
                    itemObject.GetComponent<Image>().sprite = itemToAdd.Sprite; //set the image component of the item prefab to the sprite listed
                    itemObject.name = itemToAdd.Name;
                    break;
                }
            }
        }
    }

    //check if the item is in the inventory
    bool ItemInventoryCheck(Item item)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].ID == item.ID)
            {
                return true;
            }
        }
        return false;
    }

    void ToggleInventory()
    {
        if (isActive == true)
        {
            //inventoryScreen.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);

            inventoryScreen.GetComponent<CanvasGroup>().alpha = 0;
            inventoryScreen.GetComponent<CanvasGroup>().interactable = false;
            inventoryScreen.GetComponent<CanvasGroup>().blocksRaycasts = false;
            isActive = false;
        }

        else
        {
            inventoryScreen.GetComponent<CanvasGroup>().alpha = 1;
            inventoryScreen.GetComponent<CanvasGroup>().interactable = true;
            inventoryScreen.GetComponent<CanvasGroup>().blocksRaycasts = true;
            isActive = true;
        }
    }

}
