
using UnityEngine;


public class Player : MonoBehaviour {

    public Game game;
    public CharacterController cc;

    public float jumpPower = 4;
    public float movementSpeed = 4.0f;
    public int maxJumps = 2;

    //public for debug reasons
    public Transform nearestTile;  
    public int currentJumps = 0;


    bool isLookingForward = true;
    bool jump = true;
    float movement;
    Vector3 gravity = new Vector3();
    Vector3 move = new Vector3();

    void Start () {
        EventManager.OnPerspectiveChange += ChangePerspective;
    }

    private void Update()
    {
        CheckOutOfWorld();

        SetInput();
        DoMovement();
    }


    void SetInput()
    {
        movement = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("x")) //Switch between 3d and 2d
        {
            game.SwitchDimension();
        }
        else if (Input.GetKeyDown("c")) //change perspective
        {
            game.SwitchPerspective();
        }
        else if (Input.GetKeyDown("space")) //jump
        {
            jump = true;
        }
    }

    void DoMovement()
    {
        //Rotate Player if walking in opposite direction
        if (movement > 0 && !isLookingForward)
        {
            transform.Rotate(0, 180, 0);
            isLookingForward = true;
        }
        else if (movement < 0 && isLookingForward)
        {
            transform.Rotate(0, 180, 0);
            isLookingForward = false;
        }
        move = transform.forward * Mathf.Abs(movement) * movementSpeed;

        if (!cc.isGrounded)
        {
            //if not on Ground move towards Ground
            gravity += Physics.gravity * Time.deltaTime;
        }
        else
        {
            gravity = Vector3.zero;
            currentJumps = 0;
        }
        if (jump)
        {
            if (cc.isGrounded || currentJumps < maxJumps)
            {
                currentJumps++;
                gravity = Vector3.zero;
                gravity.y = jumpPower;
            }
            jump = false;
        }

        cc.Move((move + gravity) * Time.deltaTime);
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Transform collision = hit.collider.gameObject.transform;
        // if colliding with wall skip
        if (transform.position.y < collision.position.y)
        {
            return;
        }
        nearestTile = collision;
        HybridObject ho = collision.GetComponent<HybridObject>();
        Vector3 pos3d;
        if (ho != null)
        {
            pos3d = ho.pos3d;
        }
        else
        {
            pos3d = collision.transform.position;
        }
        pos3d.y = transform.position.y;
        GetComponent<HybridObject>().UpdatePos(pos3d);
    }

    private void CheckOutOfWorld ()     // if the Player is below a fixed Y value, he respawns
    {
        if (transform.position.y < game.outOfLevel)
        {
            Kill();
        }
    }


    public void Kill()
    {
        gravity = Vector3.zero;
        transform.position = game.respawnPos;
    }

    public void ChangePerspective(bool perspective)
    {
        //Rotate Player if perspective changed
        transform.Rotate(0, (!perspective)? 90: -90 , 0);
        transform.position = new Vector3(nearestTile.position.x, transform.position.y, transform.position.z);
    }

    private void OnDestroy()
    {
        EventManager.OnPerspectiveChange -= ChangePerspective;
    }
}
