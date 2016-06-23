using UnityEngine;
using System.Collections;
using LitJson;	//allows us to create json data using a c# object
using System.Collections.Generic;
using System.IO; //gives us access to File call, allowing us to read from the assets folder

public class ItemDatabase : MonoBehaviour
{
    private List<Item> itemdatabase = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        //create an object from json, read all text of the ITEM that we want, which will be placed in the StreamingAssets folder (Items in StreamingAssets folder are not created if non-existent)
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        CreateItemDatabase();

        //Debug.Log(GetItemWithID(0).Description); //get the item based on its ID, and pull the description.
    }

    public Item GetItemWithID(int id)
    {
        //iterate through database
        for (int i = 0; i < itemdatabase.Count; i++)
        {
            if (itemdatabase[i].ID == id) //if the ID matches
            {
                return itemdatabase[i]; //create a new object with the info given from the ID
            }
        }

        return null;
    }

    //Store item values
    void CreateItemDatabase()
    {
        //for each item listed in itemData
        for (int i = 0; i < itemData.Count; i++)
        {
            itemdatabase.Add(new Item(
                (int)itemData[i]["id"],
                itemData[i]["name"].ToString(),
                (int)itemData[i]["stats"]["str"],
                (int)itemData[i]["stats"]["agi"],
                (int)itemData[i]["stats"]["con"],
                itemData[i]["description"].ToString(),
                (bool)itemData[i]["stackable"],
                (int)itemData[i]["cost"],
                itemData[i]["slug"].ToString()
            ));
        }
    }
}

//item properties
public class Item
{
    //private fields with public properties. Properties use a capital letter
    public int ID { get; set; }
    public string Name { get; set; }
    public int Str { get; set; }
    public int Agi { get; set; }
    public int Con { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Cost { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

    //assign item values
    public Item(int id, string name, int str, int agi, int con, string description, bool stackable, int cost, string slug)
    {
        this.ID = id;
        this.Name = name;
        this.Str = str;
        this.Agi = agi;
        this.Con = con;
        this.Description = description;
        this.Stackable = stackable;
        this.Cost = cost;
        this.Slug = Slug;
        this.Sprite = Resources.Load<Sprite>("ItemsTest/" + slug);
    }

    //null item
    public Item()
    {
        this.ID = -1;
    }

}
