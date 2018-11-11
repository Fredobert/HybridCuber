using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    private static bool perspective = true;
    /// <summary>
    /// True if xAxis, false if zAxis
    /// </summary>
    public static bool Perspective { get { return perspective; } }

    public delegate void PerspectiveChangeAction(bool xAxis);
    public static event PerspectiveChangeAction OnPerspectiveChange;

    public static void PerspectiveChange(bool xAxis)
    {
        perspective = xAxis;
        if (OnPerspectiveChange != null)
        {
            OnPerspectiveChange(xAxis);
        }
    }


    private static bool dimension = true;
    /// <summary>
    /// True if 3D, false if 2D
    /// </summary>
    public static bool Dimension { get { return dimension; } }

    public delegate void DimensionChangeAction(bool xAxis);
    public static event DimensionChangeAction OnDimensionChange;
    public static void DimensionChange(bool dim3d)
    {
        dimension = dim3d;
        if (OnDimensionChange != null)
        {
            OnDimensionChange(dim3d);
        }
    }


    public delegate void SquishDimensionAction(bool mode3d);
    public static event SquishDimensionAction OnSquishDimension;
    public static void SquishDimension(bool mode3d)
    {
        if (OnSquishDimension != null)
        {
            OnSquishDimension(mode3d);
        }
    }

}
