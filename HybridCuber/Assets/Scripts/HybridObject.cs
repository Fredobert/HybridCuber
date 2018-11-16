using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridObject : MonoBehaviour {

    public Vector3 pos3d;

	void Start () {
        EventManager.OnSquishDimension += Squish;
        UpdatePos();
    }
	
    public void UpdatePos(Vector3 pos)
    {
        pos3d = pos;
    }

    public void UpdatePos()
    {
        pos3d = transform.position;
    }


    public void Squish(bool mode3d)
    {
      
        if (mode3d)
        {
            transform.position = pos3d;
        }
        else
        {
            if (!EventManager.Perspective)
            {
                transform.position = new Vector3(0, pos3d.y, pos3d.z);
            
            }
            else
            {
                transform.position = new Vector3(pos3d.x, pos3d.y, 0);
            }
        }
    }

    private void OnDestroy()
    {
        EventManager.OnSquishDimension -= Squish;
    }
}
