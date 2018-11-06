using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool xModus = true;
    public float jump = 10.0f;
    public bool isGrounded = false;
    public bool isLookingForward = true;
    public Transform nearestTile;
    public Rigidbody rb;


    public Camera camX;
    public Camera camZ;


	// Use this for initialization
	void Start () {
        camZ.enabled = false;
        camX.enabled = true;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        xModus = true;
    }
	
	// Update is called once per frame
	void Update () {
        //
        float direction = Input.GetAxis("Horizontal");
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
        if (Input.GetKeyDown("x"))
        {
            if (xModus)
            {
                ChangeToZ();
            }
            else
            {
                ChangeToX();
            }
        }else if (Input.GetKeyDown("space"))
        {
            rb.AddForce(Vector3.up * jump,ForceMode.Impulse);
            isGrounded = false;
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
        }
    }

    public void ChangeToX()
    {
        if (xModus)
        {
            return;
        }
        transform.Rotate(0,-90,0);
        camZ.enabled = false;
        camX.enabled = true;
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
        transform.Rotate(0, 90, 0);
        camX.enabled = false;
        camZ.enabled = true;
        transform.position = new Vector3(nearestTile.position.x, transform.position.y, transform.position.z);
        rb.constraints =  RigidbodyConstraints.FreezeRotation;
        xModus = false;
    }
}
