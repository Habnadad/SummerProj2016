using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {

    public Vector3 startPos;
    Vector3 tempPos;
    bool hovering = false;
    GameObject whatImOn;

	// Use this for initialization
	void Start () {
        whatImOn = null;
    }
	
	// Update is called once per frame
	void Update () {

        if(hovering) // if mouse over gameobject
        {
            tempPos = whatImOn.transform.position; //store pos in vector
            tempPos.y = 0;//y is always 0
            transform.position = tempPos;//make position same as game object
        }
	
	}

    public void OnHover(GameObject something)
    {
        gameObject.SetActive(true);//become visable
        hovering = true;//now hovering over something
        whatImOn = something;//game object stored into variable
    }

    public void OffHover()
    {
        gameObject.SetActive(false);//become invis
        hovering = false;//not hovering
    }


}
