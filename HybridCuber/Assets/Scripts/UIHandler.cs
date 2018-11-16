using UnityEngine.UI;
using UnityEngine;

public class UIHandler : MonoBehaviour {

    public Text dimensionText;


    // Use this for initialization
    void Start () {
        EventManager.OnDimensionChange += DimensionChange;
	}

	void DimensionChange(bool mode3d)
    {
        if (EventManager.Dimension)
        {
            dimensionText.text = "X = Switch to 2D";
        }
        else
        {
            dimensionText.text = "X = Switch to 3D";
        }
    }


    private void OnDestroy()
    {
        EventManager.OnDimensionChange -= DimensionChange;
    }
}

