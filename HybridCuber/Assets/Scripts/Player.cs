using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool xModus = true;
    public bool Modus3d = true;
    public float jump = 10.0f;
    public bool isGrounded = false;
    public bool isLookingForward = true;
    public Transform nearestTile;
    public Rigidbody rb;

    public CameraHandler ch;


	void Start () {
        EventManager.OnModeChange += ModeChange;
        ch.SwitchCamera(true);
        ch.RotateCamera(true);
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        xModus = true;
        Modus3d = true;
    }
	

	void Update () {

        //Move
        float direction = Input.GetAxis("Horizontal");
        //Change direction if moving in the other direction
        if (direction  > 0 && !isLookingForward )
        {
            isLookingForward = true;
            transform.Rotate(0f, 180.0f, 0f);
        }else if (direction < 0 && isLookingForward )
        {
            isLookingForward = false;
            transform.Rotate(0f, 180.0f, 0f);
        }
        var z = Mathf.Abs(direction) * Time.deltaTime * 3.0f;
        transform.Translate(0, 0, z);


        if (Input.GetKeyDown("x")) //Switch between 3d and 2d
        {
            if (!Modus3d)  
            {
                ch.SwitchCamera(true);
                EventManager.OnModeChangeAction(false, !xModus);
                Modus3d = true;
            }
            else
            {
                ch.SwitchCamera(false);
                EventManager.OnModeChangeAction(true, !xModus);
                Modus3d = false;
            }
        }
        else if (Input.GetKeyDown("c")) //change perspective
        {  
            if (!xModus)
            {
                ChangeToX();
            }
            else
            {
                ChangeToZ();
            }
        }
        else if (Input.GetKeyDown("space")) //jump
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            isGrounded = false;
        }

    }

    public Vector3 pos3d;
    public void ModeChange(bool mode2d, bool xAxis)
    {
        if (mode2d)
        {
            if (xAxis)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }
        else
        {
            transform.position = new Vector3(pos3d.x, transform.position.y, pos3d.z);
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (transform.position.y < collision.gameObject.transform.position.y)
        {
            return;
        }
        if (nearestTile == null)
        {
            nearestTile = collision.gameObject.transform;
            return;
        }

        float oldD = Vector3.Distance(transform.position, nearestTile.position);
        float newD = Vector3.Distance(transform.position, collision.gameObject.transform.position);
        if (oldD > newD)
        {
            nearestTile = collision.gameObject.transform;
            HybridObject ho = collision.gameObject.GetComponent<HybridObject>();
            if (ho != null)
            {
                pos3d = ho.pos3d;
            }
        }
    }

    public void ChangeToX()
    {
        if (xModus)
        {
            return;
        }
        ch.RotateCamera(true);
        transform.Rotate(0,-90,0);
        transform.position = new Vector3(nearestTile.position.x, transform.position.y, transform.position.z);
        rb.constraints =  RigidbodyConstraints.FreezeRotation;
        xModus = true;
    }
    public void ChangeToZ()
    {
        if (!xModus)
        {
            return;
        }
        ch.RotateCamera(false);
        transform.Rotate(0, 90, 0);
        transform.position = new Vector3(nearestTile.position.x, transform.position.y, transform.position.z);
        rb.constraints =  RigidbodyConstraints.FreezeRotation;
        xModus = false;
    }
}
