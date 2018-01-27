using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float speed = 6f;
    [SerializeField] private int jumpForce = 800;
    [SerializeField] private Transform snapPos;

    private Rigidbody2D rigb;
    private float horizInput;
    private bool flagLeft, flagRight, flagJump, canJump;
    private bool canMove = true;
    private bool onSegment = false;

    private Animator Anim;
    
	private void Start () {
        rigb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
	}

    private void Update() {
        if (canMove)
        {
            horizInput = Input.GetAxisRaw("Horizontal");

            if (horizInput > 0)
            {
                flagRight = true;
            } else if (horizInput < 0)
            {
                flagLeft = true;
            } else if (horizInput == 0)
            {
                flagLeft = false;
                flagRight = false;
            }
            if (canJump && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))) {
                flagJump = true;
                Anim.SetTrigger("Jumping");
                print("Jumping");
            }



            if (horizInput != 0 && rigb.velocity.y == 0)
            {
                Anim.SetTrigger("Moving");
                print("Moving");
            }
            else if (horizInput == 0 && rigb.velocity.y == 0 && Anim.GetCurrentAnimatorStateInfo(0).IsName("Man Standing Animation") == false)
            {
                Anim.SetTrigger("Standing");
                print("Standing");
            }
        }
        else if (onSegment)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y, 0);
                transform.localEulerAngles += new Vector3(0, 180, 0);
            }

        }
    }

    private void FixedUpdate() {
        if (!onSegment) {
            if (flagRight) rigb.velocity = new Vector2(speed, rigb.velocity.y);
            else if (flagLeft) rigb.velocity = new Vector2(-speed, rigb.velocity.y);
            else rigb.velocity = new Vector2(0, rigb.velocity.y);

            if (flagJump) {
                rigb.velocity = Vector2.zero;
                rigb.AddForce(new Vector2(0f, jumpForce));
            }
        } else {

        }

        if(Input.GetKey(KeyCode.B))
        {
            Anim.SetTrigger("BackwardsStart");
        } else
        {
            if(Anim.GetCurrentAnimatorStateInfo(0).IsName("Man Backwards Animation"))
            {
                Anim.SetTrigger("BackwardsStop");
            }
        }

        /*if (Input.GetKeyDown(KeyCode.B))
        {
            Anim.SetTrigger("BackwardsStart");
            print("BackwardsStart");
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            Anim.SetTrigger("BackwardsStop");
            print("BackwardsStop");
        }*/
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Ground") {
            canJump = true;
            canMove = true;
            Anim.SetTrigger("HitPlatform");
            print("HitPlatform");
        }
    }

    void OnCollisionExit2D(Collision2D coll) {
        if (coll.gameObject.tag == "Ground") {
            canJump = false;
            flagJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Segment" && !onSegment) {
            canMove = false;
            onSegment = true;
            transform.parent = coll.transform;
            rigb.velocity = Vector2.zero;
            rigb.bodyType = RigidbodyType2D.Kinematic;

            Anim.SetTrigger("OnSegment");
            print("OnSegment");
        }
    }


}
