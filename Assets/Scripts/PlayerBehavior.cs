using System.Collections;
using UnityEngine;

public enum MovingBehavior
{
    Standart,
    Wheel
}

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 400f;

    public LayerMask whatIsGround;
    private bool canJump = false;
    private bool canMove = true;

    private Rigidbody2D RB2D;

    float horizontalInput = 0f;

    public MovingBehavior MB;

    private void Awake()
    {
        MB = MovingBehavior.Standart;
        

        RB2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            canJump = true;
        }

        if (coll.gameObject.tag == "Wheels")
        {
            GameObject other = coll.gameObject;

            MB = MovingBehavior.Wheel;
            Debug.Log("On a wheel");
            //RB2D.gravityScale = 0;
            //coll.transform.parent = gameObject.transform;

            Debug.Log("OTHER" + other.name);
            //gameObject.transform = coll.transform.parent;

            //other.transform.parent = gameObject.transform;

            Destroy(transform.GetComponent<Rigidbody2D>());
            transform.rotation = Quaternion.Euler(0, 0, 270);
            gameObject.transform.parent = other.transform;

        }
    }



    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }

    //private void OnTriggerEnter2D(Collider2D coll)
    //{
    //    if (coll.tag == "Wheels")
    //    {
    //        MB = MovingBehavior.Wheel;
    //        Debug.Log("On a wheel");
    //        RB2D.bodyType = RigidbodyType2D.Kinematic;
    //    }
    //}

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
                Debug.Log("JUMP");

                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
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
                transform.position = new Vector3(-transform.position.x,
                    transform.position.y, transform.position.z);
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


}
