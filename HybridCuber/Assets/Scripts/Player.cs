using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float jump = 10.0f;
    public bool isGrounded = false;
    public bool isLookingForward = true;
    public Transform nearestTile;
    public Rigidbody rb;
    public CameraHandler ch;
    [SerializeField] private Transform groundPoint;         // point at the botton of the character
    [SerializeField] private LayerMask whatIsGround;        // determine what is "ground"
    private float groundRadius = 0.25f;                      // area around groundPoint, to check collision with objects
    private bool doubleJump = false;

    void Start () {
        ch.SwitchCamera(true);
        EventManager.OnPerspectiveChange += ChangePerspective;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
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
            EventManager.DimensionChange(!EventManager.Dimension);
        }
        else if (Input.GetKeyDown("c")) //change perspective
        {
            EventManager.PerspectiveChange(!EventManager.Perspective);
        }
        else if (Input.GetKeyDown("p"))
        {
            //For Debug
            EventManager.SquishDimension(0.1f);
        }
        else if (Input.GetKeyDown("space")) //jump
        {
            if (IsOnGround() || doubleJump)
            {
                if (!IsOnGround())
                    doubleJump = false;
                else
                    doubleJump = true;
                rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            }
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Time.deltaTime * (-5.0f);                                                    //-5 as a test value, alternativ: Physics.gravity.y (equals -9.81)
        }
    }

    private bool IsOnGround()
    {
            Collider[] colliders = Physics.OverlapSphere(groundPoint.position, groundRadius, whatIsGround);          //effizienter: colliders.length > 1 && colliders[i].gameObject != gameObject
            
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)                                                           //check if colliders game Object is different from the player
                {
                    return true;
                }
            }
        return false;
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
                
                Vector3 pos3d = ho.pos3d;
                pos3d.y = transform.position.y;
                GetComponent<HybridObject>().UpdatePos(pos3d);

            }
        }
    }

    public void ChangePerspective(bool perspective)
    {
        transform.Rotate(0, (!perspective)? 90: -90 , 0);
        transform.position = new Vector3(nearestTile.position.x, transform.position.y, transform.position.z);
    }
}
