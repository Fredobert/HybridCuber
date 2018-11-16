using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public AnimatorHelper animatorHelper;
    public float outOfLevel = -5;
    public Vector3 respawnPos;


    public bool SwitchDimension()
    {
        if (!animatorHelper.IsPlaying())
        {
            EventManager.DimensionChange(!EventManager.Dimension);
            return true;
        }
        return false;
    }

    public bool SwitchPerspective()
    {
        if (!animatorHelper.IsPlaying())
        {
            EventManager.PerspectiveChange(!EventManager.Perspective);
            return true;
        }
        return false;
    }

	

}
