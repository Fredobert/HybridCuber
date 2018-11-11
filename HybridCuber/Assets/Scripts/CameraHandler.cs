using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour {

    public SimpleCamera cam2d;
    public SimpleCamera cam3d;

    public SimpleCamera acvtive;

    void Start()
    {
        EventManager.OnDimensionChange += SwitchCamera;
    }


    public void SwitchCamera(bool camera3d)
    {
        if (camera3d)
        {
            //cam2d.cam.enabled = false;
            //cam3d.cam.enabled = true;
            acvtive = cam3d;
        }
        else
        {
            //cam2d.cam.enabled = true;
            //cam3d.cam.enabled = false;
            acvtive = cam2d;
        }
    }
}
