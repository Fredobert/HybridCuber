using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float jump = 4.5f;
    public bool isGrounded = false;
    public bool isLookingForward = true;
    public Transform nearestTile;
    public Rigidbody rb;
    public CameraHandler ch;
    public AnimatorHelper a;
    public float movementSpeed = 7.0f;
    [SerializeField] private Transform groundPoint;         // point at the botton of the character
    [SerializeField] private LayerMask whatIsGround;        // determine what is "ground"
    private float groundRadius = 0.25f;                     // area around groundPoint, to check collision with objects
    private bool doubleJump = false;  

    void Start () {
        ch.SwitchCamera(true);
        EventManager.OnPerspectiveChange += ChangePerspective;
    }


    void Update() {
        //Move
        float direction = Input.GetAxis("Horizontal");
        if (EventManager.Perspective)
            rb.MovePosition(new Vector3(transform.position.x + direction * movementSpeed * Time.deltaTime, transform.position.y, transform.position.z));
        else
        {
            rb.MovePosition(new Vector3(transform.position.x, transform.position.y, transform.position.z + direction * movementSpeed * Time.deltaTime * (-1.0f)));
        }
        if (Input.GetKeyDown("x")) //Switch between 3d and 2d
        {
            if (!a.IsPlaying())
            {
                EventManager.DimensionChange(!EventManager.Dimension);
            }
        }
        else if (Input.GetKeyDown("c")) //change perspective
        {
            if (!a.IsPlaying())
            {
                EventManager.PerspectiveChange(!EventManager.Perspective);
            }
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
        playerDeath();
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
    private void playerDeath ()     // if the Player is below a fixed Y value, he respawns
    {
        if (transform.position.y < -5)
        {
            rb.MovePosition(new Vector3(0, 5.0f, 0));
            rb.velocity.Set(0, 0, 0);
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
                
                Vector3 pos3d = ho.pos3d;
                pos3d.y = transform.position.y;
                GetComponent<HybridObject>().UpdatePos(pos3d);

            }
        }
    }

    public void ChangePerspective(bool perspective)
    {

        rb.constraints = ((perspective) ?RigidbodyConstraints.FreezePositionZ : RigidbodyConstraints.FreezePositionX) | RigidbodyConstraints.FreezeRotation;
        transform.Rotate(0, (!perspective)? 90: -90 , 0);
        transform.position = new Vector3(nearestTile.position.x, transform.position.y, transform.position.z);
    }
}
