using System.Collections;
using UnityEngine;

public enum MovingBehavior
{
    Standart,
    Wheel,
    Dead
}

public enum SideBehavior
{
    StartSide,
    OtherSide
}

public class PlayerBehavior : MonoBehaviour
{
    public Transform StartSide;
    public Transform OtherSide;

    public float speed = 6f;
    public float jumpForce = 400f;

    public LayerMask whatIsGround;
    private bool canJump = false;
    private bool canMove = true;

    private Rigidbody2D RB2D;

    float horizontalInput = 0f;

    public MovingBehavior MB;

    public SideBehavior SB;

    private void Awake()
    {
        MB = MovingBehavior.Standart;        
        RB2D = GetComponent<Rigidbody2D>();

        SB = SideBehavior.StartSide;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            canJump = true;
        }

        if (coll.gameObject.tag == "Wheels")
        {
            if (MB == MovingBehavior.Wheel)
                return;

            GameObject other = coll.gameObject;

            MB = MovingBehavior.Wheel;
            //Debug.Log("On a wheel");
            //RB2D.gravityScale = 0;
            //coll.transform.parent = gameObject.transform;

            //Debug.Log("OTHER" + other.name);
            //gameObject.transform = coll.transform.parent;

            //other.transform.parent = gameObject.transform;

            Destroy(transform.GetComponent<Rigidbody2D>());

            //transform.GetComponent<BoxCollider2D>().enabled = false;
            transform.GetComponent<BoxCollider2D>().isTrigger = true;
            //RB2D.bodyType = RigidbodyType2D.Static;
            //transform.rotation = Quaternion.Euler(0, 0, 270);
            gameObject.transform.parent = other.transform;

            var objectRotation = transform.parent.eulerAngles.z;

            transform.rotation = Quaternion.Euler(0, 0, objectRotation -90);

            //Vector3 dir = transform.parent.position - transform.position;
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (coll.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Wheels")
        {
            //Debug.Log("On a wheel");
            //RB2D.gravityScale = 0;
            //coll.transform.parent = gameObject.transform;

            //Debug.Log("OTHER" + other.name);
            //gameObject.transform = coll.transform.parent;

            //other.transform.parent = gameObject.transform;

            //RB2D.bodyType = RigidbodyType2D.Static;
            //transform.rotation = Quaternion.Euler(0, 0, 270);

            //transform.rotation = Quaternion.Euler(0, 0, -110);

            Vector3 dir = transform.parent.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.GetComponent<SpriteRenderer>().flipY = true;
        }
    }



    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        //if (coll.tag == "Wheels")
        //{
        //    MB = MovingBehavior.Wheel;
        //    Debug.Log("On a wheel");
        //    RB2D.bodyType = RigidbodyType2D.Kinematic;
        //}

        if (coll.tag == "Teleport")
        {
            transform.parent = null;
            AddRigidBody();

            MB = MovingBehavior.Standart;
        }
    }

    //private void OnTriggerExit2D(Collider2D coll)
    //{
    //    if (coll.tag == "Wheels")
    //    {
    //        MB = MovingBehavior.Standart;
    //        RB2D.bodyType = RigidbodyType2D.Dynamic;
    //    }
    //}


    private void Update()
    {
        if (MB == MovingBehavior.Standart)
        {
            if (canJump)
            {
                //Debug.Log("JUMP");

                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    //RB2D.velocity = new Vector2(speed, RB2D.velocity.y);
                    RB2D.velocity = Vector2.zero;
                    RB2D.AddForce(new Vector2(0f, jumpForce));
                }
            }
        }

        if (MB == MovingBehavior.Wheel)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (SB == SideBehavior.StartSide)
                {
                    transform.position = OtherSide.transform.position;
                }
                else if (SB == SideBehavior.OtherSide)
                {
                    transform.position = StartSide.transform.position;
                }
            }
            var objectRotation = transform.parent.eulerAngles.z;

            transform.rotation = Quaternion.Euler(0, 0, objectRotation - 90);

        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (SB == SideBehavior.StartSide)
            {
                SB = SideBehavior.OtherSide;

                transform.position = OtherSide.transform.position;
                transform.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (SB == SideBehavior.OtherSide)
            {
                SB = SideBehavior.StartSide;

                transform.position = StartSide.transform.position;
                transform.GetComponent<SpriteRenderer>().flipX = false;

            }
        }
    }

    private void FixedUpdate()
    {
        if (MB == MovingBehavior.Standart)
        {
            if (canMove)
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");

                if (horizontalInput > 0)
                {
                    RB2D.velocity = new Vector2(speed, RB2D.velocity.y);
                }
                else if (horizontalInput < 0)
                {
                    RB2D.velocity = new Vector2(-speed, RB2D.velocity.y);
                }
                else if (horizontalInput == 0)
                {
                    RB2D.velocity = new Vector2(0, RB2D.velocity.y);
                }
            }
        }
        else if (MB == MovingBehavior.Wheel)
        {
        }
        
    }

    [ContextMenu("KILL")]
    public void DisableInput()
    {
        MB = MovingBehavior.Dead;
    }

    public void EnableInput()
    {
        MB = MovingBehavior.Standart;
    }

    public void AddRigidBody()
    {      
        Rigidbody2D gameObjectsRigidBody = gameObject.AddComponent<Rigidbody2D>(); // Add the rigidbody.
        gameObjectsRigidBody.mass = 3; // Set the GO's mass to 5 via the Rigidbody.
    }
}
