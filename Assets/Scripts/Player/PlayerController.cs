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
    
	private void Start () {
        rigb = GetComponent<Rigidbody2D>();
	}

    private void Update() {
        if (canMove) {
            horizInput = Input.GetAxisRaw("Horizontal");

            if (horizInput > 0) {
                flagRight = true;
            } else if (horizInput < 0) {
                flagLeft = true;
            } else if (horizInput == 0) {
                flagLeft = false;
                flagRight = false;
            }
            if (canJump && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))) {
                flagJump = true;
            }
        } else if (onSegment) {
            if (Input.GetKeyDown(KeyCode.R)) {
                transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y, 0);
                transform.localEulerAngles += new Vector3(0,180,0);
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
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Ground") {
            canJump = true;
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
            Debug.Log("HIT");
            rigb.bodyType = RigidbodyType2D.Kinematic;
        }
    }


}
