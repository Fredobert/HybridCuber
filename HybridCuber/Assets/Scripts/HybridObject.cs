using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridObject : MonoBehaviour {

    public Vector3 pos3d;


	// Use this for initialization
	void Start () {
        EventManager.OnModeChange += ModeChange;
        CalculatePos();

    }
	
    public void CalculatePos()
    {
        pos3d = transform.position;
    }


    public void ModeChange(bool mode2d, bool xAxis)
    {
        if (mode2d)
        {
            if (xAxis)
            {
                transform.position = new Vector3(0, pos3d.y, pos3d.z);
               
            }
            else
            {
                transform.position = new Vector3(pos3d.x, pos3d.y, 0);
            }
        }
        else
        {
            transform.position = pos3d;
        }
    }


}
