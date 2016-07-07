using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    public GameObject Target;
    public float Dampening = 1;

    Vector3 Offset;

    // Use this for initialization
    void Start()
    {
        Offset = Target.transform.position - transform.position;
        Offset.y = -9;
        Offset.z = 8;
    }

    // Update is called once per frame
    void LateUpdate()
    { 

        transform.position = Target.transform.position - Offset;

    }
}
