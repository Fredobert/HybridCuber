using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour {

    public Transform target;
    public Camera cam;
    public Vector3 offsetHorizontal;
    public Vector3 offsetVertical;
    public bool horizontal = true;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position + ((horizontal)? offsetHorizontal : offsetVertical);
	}



}
