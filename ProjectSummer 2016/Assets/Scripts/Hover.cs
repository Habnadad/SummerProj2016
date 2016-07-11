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

        if(hovering)
        {
            tempPos = whatImOn.transform.position;
            tempPos.y = 0;
            transform.position = tempPos;
        }
	
	}

    public void OnHover(GameObject something)
    {
        gameObject.SetActive(true);
        hovering = true;
        whatImOn = something;
    }

    public void OffHover()
    {
        gameObject.SetActive(false);
        hovering = false;
    }


}
