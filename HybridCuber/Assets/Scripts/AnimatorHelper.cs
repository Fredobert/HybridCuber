using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHelper : MonoBehaviour {
    Animator animator;
    public GameObject HybridObjects;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        EventManager.OnPerspectiveChange += PerspectiveChanged;
        EventManager.OnDimensionChange += DimensionChanged;
	}

    public void PerspectiveChanged(bool state)
    {
        animator.SetTrigger("PerspectiveChange");
        //animator.ResetTrigger("PerspectiveChange");
    }

    public void DimensionChanged(bool state)
    {
        animator.SetTrigger("DimensionChange");
        animator.SetBool("Dimension3D", state);
        //animator.ResetTrigger("PerspectiveChange");
    }
	
    public bool IsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }


    //Animaition Events
    public void SwitchDimensionState()
    {
        EventManager.SquishDimension(EventManager.Dimension);
    }


    private void OnDestroy()
    {
        EventManager.OnPerspectiveChange -= PerspectiveChanged;
        EventManager.OnDimensionChange -= DimensionChanged;
    }

}
