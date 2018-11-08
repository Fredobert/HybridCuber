using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HybridObject : MonoBehaviour {

    public Vector3 pos3d;


	// Use this for initialization
	void Start () {
        EventManager.OnDimensionChange += DimensionChange;
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


    public void DimensionChange(bool mode3d)
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


   //Todo Do it with Shader
    private float curSquish = 1;
    public void Squish(float squish)
    {
        curSquish -= squish;
        if (curSquish <0)
        {
            curSquish = 0;
        }
        if (EventManager.Perspective)
        {
            transform.localScale = new Vector3(1, 1, 1 * curSquish);
            transform.position = new Vector3(pos3d.x, pos3d.y, pos3d.z * curSquish);
        }
        else
        {
            transform.localScale = new Vector3(1 * curSquish, 1, 1 );
            transform.position = new Vector3(pos3d.x * curSquish, pos3d.y, pos3d.z );
        }
    }

    public void ResetScale()
    {
        curSquish = 1;
        transform.localScale = new Vector3(1, 1, 1 );
    }



}
