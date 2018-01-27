using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;

    public bool canMove = true;
    public bool onSegment = false;

    [SerializeField] private float speed = 6f;
    [SerializeField] private int jumpForce = 800;
    [SerializeField] private float snapOffset = 3.6f;

    private Rigidbody2D rigb;
    private float horizInput;
    private bool flagLeft, flagRight, flagJump, canJump, movingLeft, onTeleporter;

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
            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.DownArrow)) {
                anim.SetBool("Moving", false);
                StopChainMovement(false);
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.DownArrow)) {
                anim.SetBool("Moving", true);
                StopChainMovement(true);
            } else if (!PlayerChainSnapExtra.Instance.tempStop && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))) {
                transform.localPosition = new Vector3(-transform.localPosition.x, 0, 0);
                transform.localEulerAngles += new Vector3(0, 180, 0);
            }

        }
    }

    private void StopChainMovement(bool toStop) {
        if (toStop) {
            transform.parent = null;
            PlayerChainSnapExtra.Instance.tempStop = true;
        } else {
            if (PlayerChainSnapExtra.Instance.GetNewParent() == null) Debug.LogError("No archivedChain");
            else {
                transform.parent = PlayerChainSnapExtra.Instance.GetNewParent().transform;
                PlayerChainSnapExtra.Instance.tempStop = false;
                transform.localPosition = new Vector3(snapOffset, 0, 0);
                transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
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
        if (coll.gameObject.tag == "Segment") {
            if (!onSegment && !onTeleporter) {
                rigb.velocity = Vector2.zero;
                rigb.bodyType = RigidbodyType2D.Kinematic;
                canMove = false;
                canJump = false;
                onSegment = true;
                transform.parent = coll.transform;
                transform.localPosition = new Vector3(snapOffset, 0, 0);
                transform.localRotation = Quaternion.Euler(0f, 0f, 180f);
                sprend.flipY = coll.GetComponentInParent<ChainMover>().IsReversed();

                anim.SetTrigger("OnSegment");
                anim.SetBool("Moving", false);
            }
        }
    }

    public void OnTeleportationStart() {
        canMove = false;
        canJump = false;
        onSegment = false;
        onTeleporter = true;
        PlayerChainSnapExtra.Instance.tempStop = false;
        GetComponent<Collider2D>().enabled = false;
        ClearFlags();
        anim.SetTrigger("OffSegment");
        anim.SetBool("Moving", false);
        GetComponent<SpriteRenderer>().flipY = false;
        transform.parent = null;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void OnTeleportationEnd() {
        onTeleporter = false;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
