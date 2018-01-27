using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;

    public bool canMove = true;
    public bool onSegment = false;

    [SerializeField] private float speed = 6f;
    [SerializeField] private int jumpForce = 800;
    [SerializeField] private Transform snapPos;

    private Rigidbody2D rigb;
    private float horizInput;
    private bool flagLeft, flagRight, flagJump, canJump, movingLeft;

    private Animator anim;
    private SpriteRenderer sprend;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start () {
        rigb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprend = GetComponent<SpriteRenderer>();
        sprend.flipY = false;
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
                anim.SetTrigger("Jumping");
            }
        } else if (onSegment) {
            if (Input.GetKeyDown(KeyCode.R)) {
                transform.localPosition = new Vector3(-transform.localPosition.x, 0, 0);
                transform.localEulerAngles += new Vector3(0, 180, 0);
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                anim.SetBool("Moving", true);

            }
            if (Input.GetKeyUp(KeyCode.E)) {
                anim.SetBool("Moving", false);

            }

        }
    }

    public void ClearFlags() {
        flagRight = false;
        flagLeft = false;
        flagJump = false;
    }

    private void FixedUpdate() {
        if (!onSegment) {
            if (flagRight) {
                rigb.velocity = new Vector2(speed, rigb.velocity.y);
                movingLeft = false;
                anim.SetBool("Moving", true);
            } else if (flagLeft) {
                rigb.velocity = new Vector2(-speed, rigb.velocity.y);
                movingLeft = true;
                anim.SetBool("Moving", true);
            } else {
                rigb.velocity = new Vector2(0, rigb.velocity.y);
                movingLeft = false;
                anim.SetBool("Moving", false);
            }

            if (movingLeft != sprend.flipX) {
                sprend.flipX = movingLeft; 
            }

            if (flagJump) {
                rigb.velocity = Vector2.zero;
                rigb.AddForce(new Vector2(0f, jumpForce));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Ground") {
            canJump = true;
            canMove = true;
            anim.SetTrigger("OffSegment");
        }
    }

    private void OnCollisionExit2D(Collision2D coll) {
        if (coll.gameObject.tag == "Ground") {
            canJump = false;
            flagJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Segment" && !onSegment) {
            rigb.velocity = Vector2.zero;
            rigb.bodyType = RigidbodyType2D.Kinematic;
            canMove = false;
            canJump = false;
            onSegment = true;
            transform.parent = coll.transform;
            transform.localPosition = new Vector3(2.8f, 0, 0);
            transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
            sprend.flipY = coll.GetComponentInParent<ChainMover>().IsReversed();

            anim.SetTrigger("OnSegment");
        }
    }

    public void OnTeleportationStart() {
        anim.SetTrigger("OffSegment");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().flipY = false;
        canMove = false;
        transform.parent = null;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        onSegment = false;
        ClearFlags();
    }
}
