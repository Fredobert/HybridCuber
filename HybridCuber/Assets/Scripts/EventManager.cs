using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void ModeChange(bool mode2D, bool xAxis);
    public static event ModeChange OnModeChange;

    public static void OnModeChangeAction(bool mode2d, bool xAxis)
    {
        if (OnModeChange != null)
        {
            OnModeChange(mode2d, xAxis);
        }
    }

}
