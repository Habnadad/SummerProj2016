using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{
    public int ItemID;
    bool mouseOver = false;
    GameObject highlightCircle;
    Hover hoverScript;

	// Use this for initialization
	void Start ()
    {
        highlightCircle = GameObject.FindGameObjectWithTag("Highlight");
        hoverScript = highlightCircle.GetComponent<Hover>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnMouseEnter()
    {
        mouseOver = true;
        hoverScript.OnHover(this.gameObject);   
    }
    void OnMouseExit()
    {
        mouseOver = false;
        hoverScript.OffHover();
    }

    void OnGUI()
    {
        if(mouseOver)
        {
            GUI.Box(new Rect(300, 10, 270, 50), "Item: " + name);
        }
    }

}
