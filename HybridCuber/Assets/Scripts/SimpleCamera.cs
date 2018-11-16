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
        EventManager.OnPerspectiveChange += RotateCamera;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.position + ((horizontal)? offsetHorizontal : offsetVertical);
	}

    public void RotateCamera(bool horizontal)
    {
        this.horizontal = horizontal;
        /*if (horizontal)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }*/
    }

    private void OnDestroy()
    {
        EventManager.OnPerspectiveChange -= RotateCamera;
    }

}
