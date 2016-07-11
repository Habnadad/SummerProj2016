using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
    public int ItemID;
    bool mouseOver = false;

	// Use this for initialization
	void Start ()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnMouseEnter()
    {
        mouseOver = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    void OnMouseExit()
    {
        mouseOver = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void OnGUI()
    {
        if(mouseOver)
        {
            GUI.Box(new Rect(300, 10, 270, 50), "Item: " + name);
        }
    }

}
