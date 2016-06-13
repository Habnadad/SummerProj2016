using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour 
{
	NavMeshAgent navAgent;

	// Use this for initialization
	void Start () 
	{
		//
		navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//raycast hit variable
		RaycastHit hit;

		//create a ray that checks mouse position
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		//if left mouse button is clicked
		if(Input.GetMouseButtonDown(0))
		{
			//cast a ray out, if it is under 100.0f, move the player
			if (Physics.Raycast(ray, out hit, 100.0f)) 
			{
				//Debug.Log (hit.point);
				navAgent.SetDestination(hit.point);
			}
		}
	}
}
